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
    public class ServiceResultService : IServiceResultService
    {
        public ServiceResults CreateResult(int requestId, string resultDetails, string resultFilePath, int performedByDoctorId)
        {
            using (var context = new HospitalDbContext())
            {
                var request = context.ServiceRequests.Find(requestId);
                if (request == null) return null;

                var result = new ServiceResults
                {
                    RequestID = requestId,
                    ServiceID = request.ServiceID,
                    ResultDetails = resultDetails,
                    ResultFile = resultFilePath,
                    PerformedBy = performedByDoctorId,
                    PerformedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                };

                context.ServiceResults.Add(result);

                // Cập nhật trạng thái yêu cầu
                request.Status = "completed";
                request.CompletedAt = DateTime.Now;

                // Kiểm tra xem tất cả dịch vụ của Appointment này đã xong chưa
                if (request.AppointmentID.HasValue)
                {
                    var allCompleted = !context.ServiceRequests
                        .Any(sr => sr.AppointmentID == request.AppointmentID 
                                && sr.RequestID != requestId 
                                && sr.Status != "completed" 
                                && sr.Status != "cancelled");

                    if (allCompleted)
                    {
                        var appointment = context.Appointments.Find(request.AppointmentID.Value);
                        if (appointment != null && appointment.Status == "service_pending")
                        {
                            appointment.Status = "service_completed";
                        }
                    }
                }

                context.SaveChanges();
                return result;
            }
        }

        public IEnumerable<ServiceResultInfo> GetResultsByExamination(int examinationId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.ServiceResults
                    .Include(r => r.Request)
                        .ThenInclude(req => req.Service)
                    .Include(r => r.PerformedByDoctor)
                        .ThenInclude(d => d.User)
                    .Where(r => r.Request.ExaminationID == examinationId)
                    .Select(r => new ServiceResultInfo
                    {
                        ResultId = r.ResultID,
                        ServiceName = r.Request.Service.ServiceName,
                        ResultDetails = r.ResultDetails,
                        ResultFile = r.ResultFile,
                        PerformedByName = r.PerformedByDoctor.User.FullName,
                        PerformedAt = r.PerformedAt ?? DateTime.Now
                    })
                    .ToList();
            }
        }
    }
}
