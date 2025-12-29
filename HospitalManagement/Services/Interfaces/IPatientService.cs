using HospitalManagement.Models.Entities;
using HospitalManagement.Views.Interfaces.Patient;
using System.Collections.Generic;

namespace HospitalManagement.Services.Interfaces
{
    public interface IPatientService
    {
        // Get patient by UserID
        Patients GetPatientByUserId(int userId);
        
        // Get patient profile info
        PatientProfileInfo GetPatientProfile(int patientId);
        
        // Get medical history
        IEnumerable<MedicalHistoryDisplayInfo> GetMedicalHistory(int patientId);
        
        // Update patient info
        bool UpdatePatientInfo(int patientId, Patients updatedInfo);
    }
}
