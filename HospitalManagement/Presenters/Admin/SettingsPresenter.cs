using System;
using System.Linq;
using HospitalManagement.Config;
using HospitalManagement.Models.EF;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Presenters.Admin
{
    public class SettingsPresenter
    {
        private readonly ISettingsView _view;
        private readonly int _currentUserId;

        public SettingsPresenter(ISettingsView view, int currentUserId)
        {
            _view = view;
            _currentUserId = currentUserId;
        }

        public void LoadSettings()
        {
            _view.HospitalName = AppConfig.AppName;
            _view.Address = AppConfig.AppAddress;
            _view.Hotline = AppConfig.AppPhone;
        }

        public void SaveGeneralInfo()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_view.HospitalName))
                {
                    _view.ShowError("Tên bệnh viện không được để trống.");
                    return;
                }

                // Update Static Config (Runtime only)
                AppConfig.AppName = _view.HospitalName;
                AppConfig.AppAddress = _view.Address;
                AppConfig.AppPhone = _view.Hotline;

                _view.ShowMessage("Đã lưu thông tin thành công!");
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi lưu cài đặt: " + ex.Message);
            }
        }

        public void ChangePassword()
        {
            try
            {
                string currentPass = _view.CurrentPassword;
                string newPass = _view.NewPassword;
                string confirmPass = _view.ConfirmPassword;

                if (string.IsNullOrEmpty(currentPass) || string.IsNullOrEmpty(newPass))
                {
                    _view.ShowError("Vui lòng nhập đầy đủ thông tin mật khẩu.");
                    return;
                }

                if (newPass != confirmPass)
                {
                    _view.ShowError("Mật khẩu xác nhận không khớp.");
                    return;
                }

                using (var context = new HospitalDbContext())
                {
                    var user = context.Users.Find(_currentUserId);
                    if (user == null)
                    {
                        _view.ShowError("Không tìm thấy người dùng.");
                        return;
                    }

                    // Check old password (plain text for simplicity as per existing logic, or hash if implemented)
                    if (user.Password != currentPass)
                    {
                        _view.ShowError("Mật khẩu hiện tại không đúng.");
                        return;
                    }

                    user.Password = newPass;
                    context.SaveChanges();

                    _view.ShowMessage("Đổi mật khẩu thành công!");
                    _view.ClearPasswordFields();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi đổi mật khẩu: " + ex.Message);
            }
        }
    }
}
