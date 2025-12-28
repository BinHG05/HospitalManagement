using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces;

namespace HospitalManagement.Presenters
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly IAuthService _authService;

        public LoginPresenter(ILoginView view, IAuthService authService)
        {
            _view = view;
            _authService = authService;
        }

        public void Login()
        {
            var username = _view.Username?.Trim();
            var password = _view.Password;

            // Validation
            if (string.IsNullOrEmpty(username))
            {
                _view.ShowError("Vui lòng nhập tên đăng nhập!");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                _view.ShowError("Vui lòng nhập mật khẩu!");
                return;
            }

            // Attempt login
            var user = _authService.Login(username, password);

            if (user != null)
            {
                _view.SetLoginResult(user);
            }
            else
            {
                _view.ShowError("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
        }

        public void OpenRegister()
        {
            _view.OpenRegisterForm();
        }
    }
}
