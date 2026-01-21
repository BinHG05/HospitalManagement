using System;
using System.Collections.Generic;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces.Admin
{
    public interface IUserManagementView
    {
        // Data sources
        void SetUserList(IEnumerable<Users> users);
        
        // Inputs
        string Username { get; }
        string Password { get; }
        string DisplayName { get; }
        string SelectedRole { get; }
        bool IsActive { get; }
        string SearchKeyword { get; }

        // Selection
        int? SelectedUserId { get; }

        // UI State
        void ClearInputs();
        void ShowLoading(bool isLoading);
        void ShowMessage(string message);
        void ShowError(string message);
        void SetEditMode(bool isEdit); // To toggle Add/Update button text or enable/disable fields
    }
}
