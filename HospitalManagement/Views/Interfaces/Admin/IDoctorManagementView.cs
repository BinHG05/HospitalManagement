using System;
using System.Collections.Generic;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces.Admin
{
    public interface IDoctorManagementView
    {
        // Doctor Details
        string Specialization { get; }
        string LicenseNumber { get; }
        int YearsOfExperience { get; }
        string Qualifications { get; }
        decimal ConsultationFee { get; }
        bool IsActive { get; }
        
        // Selections
        int SelectedUserId { get; } // ID of the User account linked to this doctor
        int SelectedDepartmentId { get; }
        int? SelectedDoctorId { get; } // For editing existing doctor

        // Search
        string SearchKeyword { get; }

        // Data Methods
        void SetDoctorList(IEnumerable<object> doctors); // Use object or a DTO to flatten data for display
        void SetUserList(IEnumerable<Users> users);
        void SetDepartmentList(IEnumerable<Departments> departments);

        // UI Actions
        void ShowLoading(bool isLoading);
        void ShowMessage(string message);
        void ShowError(string message);
        void ClearInputs();
        void SetEditMode(bool isEdit);
    }
}
