using System.Collections.Generic;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces.Admin
{
    public interface IServiceManagementView
    {
        // Inputs
        string ServiceName { get; }
        decimal Price { get; }
        string Description { get; }
        
        // Selection
        int? SelectedServiceId { get; }
        string SearchKeyword { get; }

        // Data Methods
        void SetServiceList(IEnumerable<MedicalServices> services);
        
        // UI State
        void ShowLoading(bool isLoading);
        void ShowMessage(string message);
        void ShowError(string message);
        void ClearInputs();
        void SetEditMode(bool isEdit);
    }
}
