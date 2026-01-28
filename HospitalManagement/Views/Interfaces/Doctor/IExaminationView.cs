using HospitalManagement.Services.Interfaces;
using System;

namespace HospitalManagement.Views.Interfaces.Doctor
{
    public interface IExaminationView
    {
        // Load patient info
        void LoadPatientInfo(PatientExamInfo patient);
        
        // Form fields
        string Symptoms { get; set; }
        string Diagnosis { get; set; }
        string Notes { get; set; }
        string TreatmentPlan { get; set; }
        DateTime? NextAppointmentDate { get; set; }
        
        // UI State
        void ShowLoading(bool isLoading);
        void ShowError(string message);
        void ShowSuccess(string message);
        void CloseView();
        void NavigateToPrescription(int examinationId);
    }
}
