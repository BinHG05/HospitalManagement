using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Models.DTOs;
using HospitalManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement.Services.Implementations
{
    public class ServiceRequestService : IServiceRequestService
    {
        public DoctorSchedules FindAvailableDoctorForService(int serviceId, DateTime date)
        {
            using (var context = new HospitalDbContext())
            {
                // 1. Lấy DepartmentID của dịch vụ
                var service = context.MedicalServices.Find(serviceId);
                if (service?.DepartmentID == null) return null;

                var currentTime = DateTime.Now.TimeOfDay;
                var today = date.Date;

                // 2. Tìm bác sĩ đang trực ca tại khoa đó
                // Logic: Phải có lịch trực (Approved), IsActive, khớp ngày, khớp giờ ca trực
                var availableSchedule = context.DoctorSchedules
                    .Include(ds => ds.Doctor)
                        .ThenInclude(d => d.User)
                    .Include(ds => ds.Shift)
                    .Where(ds => 
                        ds.DepartmentID == service.DepartmentID &&
                        ds.Status == "Approved" &&
                        ds.IsActive == true)
                    .ToList() // Mang về client để tránh lỗi phức tạp khi đếm
                    .OrderBy(ds => context.ServiceRequests.Count(sr => sr.AssignedScheduleID == ds.ScheduleID && sr.Status == "requested"))
                    .FirstOrDefault();

                return availableSchedule;
            }
        }

        public ServiceRequests CreateServiceRequest(int? interactionId, int serviceId, int requestingDoctorId, string doctorNotes, bool isExamination = true)
        {
            using (var context = new HospitalDbContext())
            {
                var schedule = FindAvailableDoctorForService(serviceId, DateTime.Now);
                
                var request = new ServiceRequests
                {
                    ServiceID = serviceId,
                    RequestingDoctorID = requestingDoctorId,
                    DoctorNotes = doctorNotes,
                    Status = "requested",
                    RequestedAt = DateTime.Now,
                    AssignedScheduleID = schedule?.ScheduleID
                };

                if (isExamination)
                {
                    request.ExaminationID = interactionId;
                    // Lấy AppointmentID từ Examination
                    var exam = context.Examinations.Find(interactionId);
                    request.AppointmentID = exam?.AppointmentID;
                }
                else
                {
                    request.AppointmentID = interactionId;
                }

                // Tính RequestNumber (STT trong bộ hồ sơ này)
                var lastNumber = context.ServiceRequests
                    .Where(sr => sr.AppointmentID == request.AppointmentID)
                    .Max(sr => (int?)sr.RequestNumber) ?? 0;
                
                request.RequestNumber = lastNumber + 1;

                // Tính ServiceQueueNumber (STT trong hàng đợi của bác sĩ được gán)
                if (schedule != null)
                {
                    var lastQueue = context.ServiceRequests
                        .Where(sr => sr.AssignedScheduleID == schedule.ScheduleID)
                        .Max(sr => (int?)sr.ServiceQueueNumber) ?? 0;
                    request.ServiceQueueNumber = lastQueue + 1;
                }

                context.ServiceRequests.Add(request);

                // Cập nhật trạng thái cuộc hẹn thành service_pending
                if (request.AppointmentID.HasValue)
                {
                    var appointment = context.Appointments.Find(request.AppointmentID.Value);
                    if (appointment != null && appointment.Status != "completed")
                    {
                        appointment.Status = "service_pending";
                    }
                }

                context.SaveChanges();
                return request;
            }
        }

        public IEnumerable<ServiceRequestInfo> GetPendingRequestsForDoctor(int doctorId)
        {
            using (var context = new HospitalDbContext())
            {
                var today = DateTime.Today;
                return context.ServiceRequests
                    .Include(sr => sr.Service)
                    .Include(sr => sr.Examination)
                        .ThenInclude(e => e.Patient)
                            .ThenInclude(p => p.User)
                    .Include(sr => sr.AssignedSchedule)
                    .Where(sr => sr.AssignedSchedule.DoctorID == doctorId 
                              && sr.Status != "completed" 
                              && sr.Status != "cancelled")
                    .OrderBy(sr => sr.ServiceQueueNumber)
                    .Select(sr => new ServiceRequestInfo
                    {
                        RequestId = sr.RequestID,
                        PatientName = sr.Examination.Patient.User.FullName,
                        ServiceName = sr.Service.ServiceName,
                        Status = sr.Status,
                        RequestedAt = sr.RequestedAt ?? DateTime.Now,
                        QueueNumber = sr.ServiceQueueNumber ?? 0,
                        DoctorNotes = sr.DoctorNotes
                    })
                    .ToList();
            }
        }

        public IEnumerable<ServiceRequestInfo> GetRequestsByExamination(int examinationId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.ServiceRequests
                    .Include(sr => sr.Service)
                    .Include(sr => sr.ServiceResults)
                    .Where(sr => sr.ExaminationID == examinationId)
                    .Select(sr => new ServiceRequestInfo
                    {
                        RequestId = sr.RequestID,
                        ServiceName = sr.Service.ServiceName,
                        Status = sr.Status,
                        RequestedAt = sr.RequestedAt ?? DateTime.Now,
                        ResultDetails = sr.ServiceResults.OrderByDescending(res => res.CreatedAt).Select(res => res.ResultDetails).FirstOrDefault()
                    })
                    .ToList();
            }
        }

        public IEnumerable<MedicalServices> GetActiveServices()
        {
            using (var context = new HospitalDbContext())
            {
                return context.MedicalServices.Where(s => s.IsActive == true).ToList();
            }
        }

        public bool UpdateStatus(int requestId, string newStatus)
        {
            using (var context = new HospitalDbContext())
            {
                var request = context.ServiceRequests.Find(requestId);
                if (request == null) return false;

                request.Status = newStatus;
                if (newStatus == "completed")
                {
                    request.CompletedAt = DateTime.Now;
                }

                context.SaveChanges();
                return true;
            }
        }

        public ServiceRequestInfo GetRequestById(int requestId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.ServiceRequests
                    .Include(sr => sr.Service)
                    .Include(sr => sr.Examination)
                        .ThenInclude(e => e.Patient)
                            .ThenInclude(p => p.User)
                    .Where(sr => sr.RequestID == requestId)
                    .Select(sr => new ServiceRequestInfo
                    {
                        RequestId = sr.RequestID,
                        PatientName = sr.Examination.Patient.User.FullName,
                        ServiceName = sr.Service.ServiceName,
                        Status = sr.Status,
                        RequestedAt = sr.RequestedAt ?? DateTime.Now,
                        DoctorNotes = sr.DoctorNotes
                    })
                    .FirstOrDefault();
            }
        }
    }
}
