using System;
using System.Windows.Forms;
using HospitalManagement.Presenters.Admin;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Views.UserControls.Admin
{
    public partial class UC_Settings : UserControl, ISettingsView
    {
        private SettingsPresenter _presenter;
        private readonly int _currentUserId;

        public UC_Settings(int currentUserId)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
            _presenter = new SettingsPresenter(this, _currentUserId);

            // Wire up events
            btnSaveInfo.Click += (s, e) => _presenter.SaveGeneralInfo();
            btnChangePass.Click += (s, e) => _presenter.ChangePassword();
            
            // Load initial data
            this.Load += (s, e) => _presenter.LoadSettings();
        }

        // General Info Properties
        public string HospitalName
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }

        public string Address
        {
            get => txtAddress.Text;
            set => txtAddress.Text = value;
        }

        public string Hotline
        {
            get => txtHotline.Text;
            set => txtHotline.Text = value;
        }

        // Password Properties
        public string CurrentPassword => txtCurrentPass.Text;
        public string NewPassword => txtNewPass.Text;
        public string ConfirmPassword => txtConfirmPass.Text;

        // Actions
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ClearPasswordFields()
        {
            txtCurrentPass.Clear();
            txtNewPass.Clear();
            txtConfirmPass.Clear();
        }
    }
}
