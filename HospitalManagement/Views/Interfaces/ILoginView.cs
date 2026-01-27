using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces
{
    public interface ILoginView
    {
        string Username { get; }
        string Password { get; }
        
        void ShowError(string message);
        void ClearFields();
        void SetLoginResult(Users user);
        void OpenRegisterForm();
        void OpenForgotPasswordForm();
    }
}
