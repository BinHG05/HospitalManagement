namespace HospitalManagement.Views.Interfaces
{
    public interface IRegisterView
    {
        string Username { get; }
        string Password { get; }
        string ConfirmPassword { get; }
        string Email { get; }
        string Phone { get; }
        string FullName { get; }
        
        void ShowError(string message);
        void ShowSuccess(string message);
        void ClearFields();
        void CloseForm();
    }
}
