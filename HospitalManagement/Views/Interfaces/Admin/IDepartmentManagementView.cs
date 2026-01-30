using System.Collections.Generic;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces.Admin
{
    public interface IDepartmentManagementView
    {
        // Inputs
        string DepartmentName { get; }
        string Description { get; }
        
        // Selection
        int? SelectedDepartmentId { get; }
        string SearchKeyword { get; }

        // Data Methods
        void SetDepartmentList(IEnumerable<Departments> departments);
        
        // UI State
        void ShowLoading(bool isLoading);
        void ShowMessage(string message);
        void ShowError(string message);
        void ClearInputs();
        void SetEditMode(bool isEdit);
    }
}
