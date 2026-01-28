using System;
using System.Drawing;
using System.Linq;
using HospitalManagement.Models.EF;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms
{
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try 
            {
                using (var context = new HospitalDbContext())
                {
                    var user = context.Users.FirstOrDefault(u => 
                        u.Username == username && 
                        u.Email == email && 
                        u.Phone == phone);

                    if (user != null)
                    {
                        MessageBox.Show($"Xác minh thành công!\nMật khẩu của bạn là: {user.Password}", 
                            "Cấp lại mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thông tin không chính xác. Vui lòng kiểm tra lại!", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
