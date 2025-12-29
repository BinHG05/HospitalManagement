using HospitalManagement.Services.Interfaces;
using System.Collections.Generic;

namespace HospitalManagement.Views.Interfaces.Doctor
{
    public interface IPatientQueueView
    {
        // Load queue
        void LoadQueue(IEnumerable<QueuePatientInfo> queue);
        
        // Selected patient
        int SelectedAppointmentId { get; }
        
        // Actions
        void ShowPatientDetails(PatientExamInfo patient);
        void OpenExamination(int appointmentId);
        
        // UI State
        void ShowLoading(bool isLoading);
        void ShowError(string message);
        void ShowSuccess(string message);
        void RefreshQueue();
    }
}
