using HospitalManagement.Models.Entities;
using HospitalManagement.Models.DTOs;
using System;
using System.Collections.Generic;

namespace HospitalManagement.Services.Interfaces
{
    public interface IServiceRequestService
    {
        // Tìm bác sĩ đang trực ca cho dịch vụ cụ thể
        DoctorSchedules FindAvailableDoctorForService(int serviceId, DateTime date);
        
        // Tạo yêu cầu dịch vụ và auto-assign bác sĩ
        ServiceRequests CreateServiceRequest(
            int? interactionId, // ExaminationID or AppointmentID
            int serviceId, 
            int requestingDoctorId, 
            string doctorNotes,
            bool isExamination = true);
        
        // Lấy danh sách yêu cầu chờ thực hiện cho bác sĩ dịch vụ
        IEnumerable<ServiceRequestInfo> GetPendingRequestsForDoctor(int doctorId);
        
        // Lấy danh sách yêu cầu đã chỉ định cho exam (để BÁC SĨ ĐIỀU TRỊ xem)
        IEnumerable<ServiceRequestInfo> GetRequestsByExamination(int examinationId);

        // Lấy danh sách dịch vụ đang hoạt động
        IEnumerable<MedicalServices> GetActiveServices();
        
        // Cập nhật trạng thái (requested → in_progress → completed)
        bool UpdateStatus(int requestId, string newStatus);

        // Lấy thông tin yêu cầu cụ thể
        ServiceRequestInfo GetRequestById(int requestId);
    }
}
