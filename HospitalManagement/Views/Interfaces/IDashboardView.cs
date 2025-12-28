using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces
{
    public interface IDashboardView
    {
        Users CurrentUser { get; set; }
        
        void LoadHomeContent();
        void LoadContent(string contentName);
        void UpdateHeaderTitle(string title);
        void ShowLogoutConfirmation();
    }
}
