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
                _view.ShowError("Tên đăng nhập phải có ít nhất 4 ký tự!");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                _view.ShowError("Vui lòng nhập mật khẩu!");
                return;
            }

            if (password.Length < 6)
            {
                _view.ShowError("Mật khẩu phải có ít nhất 6 ký tự!");
                return;
            }

            if (password != confirmPassword)
            {
                _view.ShowError("Mật khẩu xác nhận không khớp!");
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                _view.ShowError("Vui lòng nhập email!");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                _view.ShowError("Email không hợp lệ!");
                return;
            }

            if (string.IsNullOrEmpty(phone))
            {
                _view.ShowError("Vui lòng nhập số điện thoại!");
                return;
            }

            if (phone.Length < 10)
            {
                _view.ShowError("Số điện thoại không hợp lệ!");
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
