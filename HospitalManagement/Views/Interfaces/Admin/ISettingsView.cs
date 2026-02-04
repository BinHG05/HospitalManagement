using System;

namespace HospitalManagement.Views.Interfaces.Admin
{
    public interface ISettingsView
    {
        // General Info
        string HospitalName { get; set; }
        string Address { get; set; }
        string Hotline { get; set; }

        // Change Password
        string CurrentPassword { get; }
        string NewPassword { get; }
        string ConfirmPassword { get; }

        // Actions
        void ShowMessage(string message);
        void ShowError(string message);
        void ClearPasswordFields();
    }
}
