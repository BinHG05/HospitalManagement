using HospitalManagement.Models.Entities;
using HospitalManagement.Models.DTOs;
using System.Collections.Generic;

namespace HospitalManagement.Services.Interfaces
{
    public interface IServiceResultService
    {
        // Bác sĩ dịch vụ nhập kết quả
        ServiceResults CreateResult(
            int requestId, 
            string resultDetails, 
            string resultFilePath, 
            int performedByDoctorId);
        
        // Lấy kết quả theo exam - để bác sĩ điều trị xem trước khi kê đơn
        IEnumerable<ServiceResultInfo> GetResultsByExamination(int examinationId);
    }
}
