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
    public class DoctorService : IDoctorService
    {
        public Doctors GetDoctorByUserId(int userId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Doctors
                    .Include(d => d.User)
                    .Include(d => d.Department)
                    .FirstOrDefault(d => d.UserID == userId);
            }
        }

        public IEnumerable<QueuePatientInfo> GetTodayQueue(int doctorId)
        {
            using (var context = new HospitalDbContext())
            {
                var today = DateTime.Today;
                // For demonstration, we allow any year if month and day match today
                // or just stay with strict Today for real production
                var appointments = context.Appointments
                    .AsNoTracking()
                    .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                    .Where(a => a.DoctorID == doctorId
                             && a.Status == "confirmed") 
                    .OrderBy(a => a.AppointmentNumber)
                    .ToList();

                return appointments.Select(a => new QueuePatientInfo
                {
                    AppointmentId = a.AppointmentID,
                    QueueNumber = a.AppointmentNumber,
                    PatientName = a.Patient?.User?.FullName ?? "N/A",
                    Gender = a.Patient?.Gender,
                    Age = a.Patient?.DateOfBirth != null
                        ? (int?)((int)((DateTime.Today - a.Patient.DateOfBirth.Value).TotalDays / 365.25))
                        : null,
                    Symptoms = a.Symptoms,
                    Status = a.Status,
                    CreatedAt = a.CreatedAt
                }).ToList();
            }
        }

        public IEnumerable<ActiveExamInfo> GetActiveExaminations(int doctorId)
        {
            using (var context = new HospitalDbContext())
            {
                var today = DateTime.Today;

                // Active = Confirmed (Examining), Service_Pending, Service_Completed
                // Active = Examining, Service_Pending, Service_Completed
                var appointments = context.Appointments
                    .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                    .Include(a => a.Doctor)
                    .ThenInclude(d => d.User)
                    .Include(a => a.Department)
                    .Where(a => a.DoctorID == doctorId
                             && (a.Status == "examining" || a.Status == "service_pending" || a.Status == "service_completed"))
                    .OrderByDescending(a => a.UpdatedAt)
                    .ToList();

                return appointments.Select(a => new ActiveExamInfo
                {
                    AppointmentId = a.AppointmentID,
                    PatientId = (int)a.PatientID,
                    PatientName = a.Patient?.User?.FullName ?? "N/A",
                    DoctorName = a.Doctor?.User?.FullName ?? "N/A",
                    DepartmentName = a.Department?.DepartmentName ?? "N/A",
                    Gender = a.Patient?.Gender,
                    Age = a.Patient?.DateOfBirth != null
                        ? (int?)((int)((DateTime.Today - a.Patient.DateOfBirth.Value).TotalDays / 365.25))
                        : null,
                    Status = a.Status,
                    // ServiceStatus requires joining with Services/ServiceRequests table which we will implement later.
                    // For now leaving blank or using placeholder.
                    ServiceStatus = a.Status == "service_pending" ? "Chờ kết quả" : 
                                    (a.Status == "service_completed" ? "Đã có kết quả" : ""),
                    StartedAt = a.UpdatedAt ?? a.CreatedAt
                }).ToList();
            }
        }

        public IEnumerable<HospitalActiveExamInfo> GetAllActiveExaminations()
        {
            using (var context = new HospitalDbContext())
            {
                var today = DateTime.Today;

                var appointments = context.Appointments
                    .AsNoTracking()
                    .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                    .Include(a => a.Doctor)
                    .ThenInclude(d => d.User)
                    .Include(a => a.Department)
                    .Where(a => (a.Status == "examining" || a.Status == "service_pending" || a.Status == "service_completed"))
                    .OrderByDescending(a => a.UpdatedAt)
                    .ToList();

                return appointments.Select(a => new HospitalActiveExamInfo
                {
                    AppointmentId = a.AppointmentID,
                    QueueNumber = a.AppointmentNumber,
                    PatientName = a.Patient?.User?.FullName ?? "N/A",
                    DoctorName = a.Doctor?.User?.FullName ?? "N/A",
                    DepartmentName = a.Department?.DepartmentName ?? "N/A",
                    Gender = a.Patient?.Gender,
                    Age = a.Patient?.DateOfBirth != null
                        ? (int?)((int)((DateTime.Today - a.Patient.DateOfBirth.Value).TotalDays / 365.25))
                        : null,
                    Status = a.Status,
                    ServiceStatus = a.Status == "service_pending" ? "Chờ kết quả" : 
                                    (a.Status == "service_completed" ? "Đã có kết quả" : ""),
                    StartedAt = a.UpdatedAt ?? a.CreatedAt ?? DateTime.Now
                }).ToList();
            }
        }

        public IEnumerable<MedicalServices> GetAllServices()
        {
            using (var context = new HospitalDbContext())
            {
                return context.MedicalServices
                    .Where(s => s.IsActive == true)
                    .OrderBy(s => s.ServiceName)
                    .ToList();
            }
        }

        public PatientExamInfo GetPatientForExam(int appointmentId)
        {
            using (var context = new HospitalDbContext())
            {
                var appointment = context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Patient.User)
                    .FirstOrDefault(a => a.AppointmentID == appointmentId);

                if (appointment?.Patient == null) return null;

                var patient = appointment.Patient;
                var totalVisits = context.MedicalHistory.Count(h => h.PatientID == patient.PatientID);
                var lastRecord = context.MedicalHistory
                    .Where(h => h.PatientID == patient.PatientID)
                    .OrderByDescending(h => h.VisitDate)
                    .FirstOrDefault();

                return new PatientExamInfo
                {
                    AppointmentId = appointmentId,
                    PatientId = patient.PatientID,
                    PatientName = patient.User?.FullName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    BloodType = patient.BloodType,
                    Address = patient.Address,
                    Symptoms = appointment.Symptoms,
                    InsuranceNumber = patient.InsuranceNumber,
                    TotalVisits = totalVisits,
                    LastDiagnosis = lastRecord?.Diagnosis
                };
            }
        }

        public bool CallPatient(int appointmentId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var appointment = context.Appointments.Find(appointmentId);
                    if (appointment == null) return false;

                    appointment.Status = "examining"; // Set to examining when doctor calls the patient
                    appointment.UpdatedAt = DateTime.Now;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool MarkAsNoShow(int appointmentId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var appointment = context.Appointments.Find(appointmentId);
                    if (appointment == null) return false;

                    // Move back to confirmed (waiting list) but update UpdatedAt 
                    // so if we order by CreatedAt it stays, but if we want to "re-queue"
                    // we can use UpdatedAt. For now, let's just keep confirmed.
                    appointment.Status = "confirmed"; 
                    appointment.UpdatedAt = DateTime.Now; // Update time to show they were recently handled
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool AssignService(int appointmentId, int serviceId)
        {
            return AssignServices(appointmentId, new List<int> { serviceId });
        }

        public bool AssignServices(int appointmentId, List<int> serviceIds)
        {
            var serviceRequestService = new ServiceRequestService();
            using (var context = new HospitalDbContext())
            {
                var appointment = context.Appointments.Find(appointmentId);
                if (appointment == null) throw new Exception("Không tìm thấy thông tin cuộc hẹn.");

                foreach (var serviceId in serviceIds)
                {
                    serviceRequestService.CreateServiceRequest(appointmentId, serviceId, appointment.DoctorID ?? 0, "", false);
                }
                return true;
            }
        }

        public int CompleteExamination(int appointmentId, ExaminationData data)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try 
                        {
                            var appointment = context.Appointments
                                .Include(a => a.Patient)
                                .FirstOrDefault(a => a.AppointmentID == appointmentId);

                            if (appointment == null) throw new Exception("Không tìm thấy lịch hẹn");

                            // 1. Create examination record
                            var examination = new Examinations
                            {
                                AppointmentID = appointmentId,
                                DoctorID = appointment.DoctorID,
                                PatientID = appointment.PatientID,
                                ExaminationDate = DateTime.Now,
                                Symptoms = data.Symptoms,
                                PreliminaryDiagnosis = data.Diagnosis,
                                Notes = data.Notes,
                                Status = "completed",
                                CreatedAt = DateTime.Now
                            };
                            context.Examinations.Add(examination);

                            // 2. Create medical record (Linked via Navigation Property if possible, or Order)
                            // EF Core will fix IDs if we add to context in order, but standard way with IDs needs SaveChanges
                            // Let's use SaveChanges to be safe with IDs 
                            context.SaveChanges(); 

                            var record = new MedicalRecords
                            {
                                PatientID = appointment.PatientID,
                                ExaminationID = examination.ExaminationID,
                                Diagnosis = data.Diagnosis,
                                TreatmentPlan = data.TreatmentPlan,
                                RecordDate = DateTime.Now,
                                CreatedBy = appointment.DoctorID,
                                CreatedAt = DateTime.Now
                            };
                            context.MedicalRecords.Add(record);
                            context.SaveChanges();

                            // 3. Create medical history
                            // Truncate Diagnosis to match nvarchar(1000) of MedicalHistory
                            string historyDiagnosis = data.Diagnosis;
                            if (!string.IsNullOrEmpty(historyDiagnosis) && historyDiagnosis.Length > 1000)
                            {
                                historyDiagnosis = historyDiagnosis.Substring(0, 997) + "...";
                            }

                            var history = new MedicalHistory
                            {
                                PatientID = appointment.PatientID,
                                RecordID = record.RecordID,
                                VisitDate = DateTime.Now,
                                DoctorID = appointment.DoctorID,
                                Diagnosis = historyDiagnosis,
                                Treatment = data.TreatmentPlan,
                                NextAppointmentDate = data.NextAppointmentDate,
                                CreatedAt = DateTime.Now
                            };
                            context.MedicalHistory.Add(history);

                            // 4. Update appointment status
                            appointment.Status = "completed";
                            appointment.UpdatedAt = DateTime.Now;

                            context.SaveChanges();
                            transaction.Commit();

                            return examination.ExaminationID;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                System.Diagnostics.Debug.WriteLine($"CompleteExamination Error: {ex.Message} | Inner: {innerMessage}");
                throw new Exception($"Lỗi lưu dữ liệu: {ex.Message}\n{innerMessage}");
            }
        }

        public IEnumerable<DoctorScheduleInfo> GetDoctorSchedule(int doctorId, DateTime weekStart)
        {
            var weekEnd = weekStart.AddDays(6);

            using (var context = new HospitalDbContext())
            {
                var schedules = context.DoctorSchedules
                    .Include(ds => ds.Shift)
                    .Where(ds => ds.DoctorID == doctorId
                              && ds.ScheduleDate >= weekStart.Date
                              && ds.ScheduleDate <= weekEnd.Date)
                    .ToList();

                var result = new List<DoctorScheduleInfo>();

                foreach (var schedule in schedules)
                {
                    var bookedCount = context.Appointments
                        .Count(a => a.ScheduleID == schedule.ScheduleID
                                 && (a.Status == "pending" || a.Status == "confirmed"));

                    var completedCount = context.Appointments
                        .Count(a => a.ScheduleID == schedule.ScheduleID && a.Status == "completed");

                    result.Add(new DoctorScheduleInfo
                    {
                        ScheduleId = schedule.ScheduleID,
                        ScheduleDate = schedule.ScheduleDate,
                        ShiftName = schedule.Shift?.ShiftName ?? "N/A",
                        StartTime = schedule.Shift?.StartTime ?? TimeSpan.Zero,
                        EndTime = schedule.Shift?.EndTime ?? TimeSpan.Zero,
                        TotalSlots = schedule.AvailableSlots ?? 50,
                        BookedSlots = bookedCount,
                        CompletedSlots = completedCount,
                        IsActive = schedule.IsActive ?? true
                    });
                }

                return result.OrderBy(s => s.ScheduleDate).ThenBy(s => s.StartTime).ToList();
            }
        }

        public bool GetPatientServiceStatus(int appointmentId)
        {
            using (var context = new HospitalDbContext())
            {
                var requests = context.ServiceRequests
                    .Where(sr => sr.AppointmentID == appointmentId)
                    .ToList();

                if (!requests.Any()) return true;

                return requests.All(sr => sr.Status == "completed");
            }
        }

        public IEnumerable<ServiceRequestInfo> GetActiveAssignedServices(int appointmentId)
        {
            using (var context = new HospitalDbContext())
            {
                var requests = context.ServiceRequests
                    .Include(sr => sr.Service)
                    .Include(sr => sr.ServiceResults)
                    .Where(sr => sr.AppointmentID == appointmentId)
                    .OrderByDescending(sr => sr.RequestedAt)
                    .ToList();

                return requests.Select(sr => new ServiceRequestInfo
                {
                    RequestId = sr.RequestID,
                    ServiceName = sr.Service?.ServiceName ?? "N/A",
                    Status = sr.Status,
                    RequestedAt = sr.RequestedAt ?? DateTime.Now,
                    ResultDetails = sr.ServiceResults.OrderByDescending(res => res.CreatedAt).FirstOrDefault()?.ResultDetails
                }).ToList();
            }
        }
    }
}
