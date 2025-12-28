using HospitalManagement.Presenters;
using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces;
using System;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms
{
    public partial class RegisterForm : Form, IRegisterView
    {
        private readonly RegisterPresenter _presenter;

        // IRegisterView implementation
        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;
        public string ConfirmPassword => txtConfirmPassword.Text;
        public string Email => txtEmail.Text;
        public string Phone => txtPhone.Text;
        public string FullName => txtFullName.Text;

        public RegisterForm()
        {
            InitializeComponent();

            // Create service and presenter
            IAuthService authService = new AuthService();
            _presenter = new RegisterPresenter(this, authService);
        }

        public void ShowError(string message)
        {
            lblError.Text = message;
            lblError.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            lblError.Visible = true;
        }

        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ClearFields()
        {
            txtUsername.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            lblError.Visible = false;
        }

        public void CloseForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            _presenter.Register();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
