using HospitalManagement.Infrastructure.Helpers;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces;

namespace HospitalManagement.Presenters
{
    public class RegisterPresenter
    {
        private readonly IRegisterView _view;
        private readonly IAuthService _authService;

        public RegisterPresenter(IRegisterView view, IAuthService authService)
        {
            _view = view;
            _authService = authService;
        }

        public void Register()
        {
            var username = _view.Username?.Trim();
            var password = _view.Password;
            var confirmPassword = _view.ConfirmPassword;
            var email = _view.Email?.Trim();
            var phone = _view.Phone?.Trim();
            var fullName = _view.FullName?.Trim();

            // Validation
            if (string.IsNullOrEmpty(username))
            {
                _view.ShowError("Vui lòng nhập tên đăng nhập!");
                return;
            }

            if (username.Length < 4)
            {
                _view.ShowError("Tên đăng nhập phải có nhất 4 ký tự!");
                return;
            }

            if (!ValidationHelper.IsValidPassword(password))
            {
                _view.ShowError("Mật khẩu phải có ít nhất 6 ký tự!");
                return;
            }

            if (password != confirmPassword)
            {
                _view.ShowError("Mật khẩu xác nhận không khớp!");
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                _view.ShowError("Email không đúng định dạng!");
                return;
            }

            if (!ValidationHelper.IsValidPhone(phone))
            {
                _view.ShowError("Số điện thoại phải bao gồm 10 chữ số!");
                return;
            }

            if (string.IsNullOrEmpty(fullName))
            {
                _view.ShowError("Vui lòng nhập họ và tên!");
                return;
            }

            // Register - default role is patient
            var result = _authService.Register(username, password, email, phone, fullName, "patient");

            if (result.Success)
            {
                _view.ShowSuccess(result.Message);
                _view.CloseForm();
            }
            else
            {
                _view.ShowError(result.Message);
            }
        }
    }
}
