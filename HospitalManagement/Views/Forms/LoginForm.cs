using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters;
using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces;
using HospitalManagement.Views.Forms;
using System;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms
{
    public partial class LoginForm : Form, ILoginView
    {
        private readonly LoginPresenter _presenter;
        
        public Users LoggedInUser { get; private set; }

        // ILoginView implementation
        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;

        public LoginForm()
        {
            InitializeComponent();
            
            // Create service and presenter
            IAuthService authService = new AuthService();
            _presenter = new LoginPresenter(this, authService);

            // Thiết lập phong cách Luxury nổi bật
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(251, 191, 36); // Vàng Gold nổi bật
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184); // Bạc Slate hiện đại
            lblLogo.ForeColor = System.Drawing.Color.FromArgb(251, 191, 36); 

            // Thiết lập nội dung cho cả hai chức năng
            lblNoAccount.Text = "Chưa có tài khoản?";
            lnkRegister.Text = "Đăng ký ngay";
            lblForgotPassword.Text = "Bạn quên mật khẩu?";
            lnkForgotPassword.Text = "Lấy lại mật khẩu";
        }

        public void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }

        public void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            lblError.Visible = false;
        }

        public void SetLoginResult(Users user)
        {
            LoggedInUser = user;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void OpenRegisterForm()
        {
            var registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        public void OpenForgotPasswordForm()
        {
            var forgotForm = new ForgotPasswordForm();
            forgotForm.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            _presenter.Login();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _presenter.OpenRegister();
        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _presenter.ForgotPassword();
        }
    }
}
