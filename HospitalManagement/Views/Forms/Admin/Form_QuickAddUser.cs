using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Forms.Admin
{
    public partial class Form_QuickAddUser : Form
    {
        public Users CreatedUser { get; private set; }

        public Form_QuickAddUser()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new HospitalDbContext())
                {
                    if (context.Users.Any(u => u.Username == txtUsername.Text.Trim()))
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    CreatedUser = new Users
                    {
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Text, // Should be hashed in production
                        FullName = txtFullName.Text.Trim(),
                        Role = "Doctor",
                        Status = "active", // FIXED: Users entity uses Status string instead of IsActive bool
                        CreatedAt = DateTime.Now
                    };

                    context.Users.Add(CreatedUser);
                    context.SaveChanges();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
