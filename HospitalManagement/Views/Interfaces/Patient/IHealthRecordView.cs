using HospitalManagement.Models.Entities;
using System;
using System.Collections.Generic;

namespace HospitalManagement.Views.Interfaces.Patient
{
    public interface IHealthRecordView
    {
        // Load patient info
        void LoadPatientInfo(PatientProfileInfo profile);
        
        // Load medical history
        void LoadMedicalHistory(IEnumerable<MedicalHistoryDisplayInfo> history);
        
        // UI State
        void ShowLoading(bool isLoading);
        void ShowError(string message);
    }

    public class PatientProfileInfo
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string BloodType { get; set; }
        public string InsuranceNumber { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string GenderDisplay
        {
            get
            {
                switch (Gender)
                {
                    case "male": return "Nam";
                    case "female": return "Nữ";
                    case "other": return "Khác";
                    default: return Gender;
                }
            }
        }

        public int? Age => DateOfBirth.HasValue 
            ? (int?)((DateTime.Today - DateOfBirth.Value).TotalDays / 365.25) 
            : (int?)null;
    }

    public class MedicalHistoryDisplayInfo
    {
        public int RecordId { get; set; }
        public DateTime VisitDate { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
    }
}
