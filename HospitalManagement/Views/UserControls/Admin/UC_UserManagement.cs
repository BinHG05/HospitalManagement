using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters.Admin;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Views.UserControls.Admin
{
    public partial class UC_UserManagement : UserControl, IUserManagementView
    {
        private UserManagementPresenter _presenter;
        private int? _selectedUserId;

        public UC_UserManagement()
        {
            InitializeComponent();
            InitializePresenter();
            SetupEvents();
        }

        private void InitializePresenter()
        {
            _presenter = new UserManagementPresenter(this);
            // Initial Load
            this.Load += (s, e) => _presenter.LoadUsers();
        }

        private void SetupEvents()
        {
            btnSearch.Click += (s, e) => _presenter.SearchUsers();
            btnSave.Click += (s, e) => _presenter.SaveUser();
            btnDelete.Click += (s, e) => 
            {
                if (_selectedUserId.HasValue)
                    _presenter.DeleteUser(_selectedUserId.Value);
            };
            btnClear.Click += (s, e) => ClearInputs();
            btnAddNew.Click += (s, e) => ClearInputs();
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;

            // Search on Enter
            txtSearch.KeyDown += (s, e) => 
            {
                if (e.KeyCode == Keys.Enter)
                    _presenter.SearchUsers();
            };
        }

        #region IUserManagementView Implementation

        public string Username => txtUsername.Text.Trim();

        public string Password => txtPassword.Text.Trim();

        public string DisplayName => txtDisplayName.Text.Trim();

        public string SelectedRole => cmbRole.SelectedItem?.ToString();

        public bool IsActive => chkIsActive.Checked;

        public string SearchKeyword => txtSearch.Text.Trim();

        public int? SelectedUserId => _selectedUserId;

        public void SetUserList(IEnumerable<Users> users)
        {
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = users;

            // Hide Sensitive Columns
            if (dgvUsers.Columns["Password"] != null) dgvUsers.Columns["Password"].Visible = false;
            
            // Format Headers (Optional aliases if needed)
            if (dgvUsers.Columns["Username"] != null) dgvUsers.Columns["Username"].HeaderText = "Tài khoản";
            if (dgvUsers.Columns["FullName"] != null) dgvUsers.Columns["FullName"].HeaderText = "Tên hiển thị";
            if (dgvUsers.Columns["Role"] != null) dgvUsers.Columns["Role"].HeaderText = "Vai trò";
            if (dgvUsers.Columns["Status"] != null) dgvUsers.Columns["Status"].HeaderText = "Trạng thái";
        }

        public void ClearInputs()
        {
            _selectedUserId = null;
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtDisplayName.Text = "";
            cmbRole.SelectedIndex = -1;
            chkIsActive.Checked = true;
            
            txtUsername.Enabled = true; // Allow editing username only for new
            SetEditMode(false);
            
            dgvUsers.ClearSelection();
        }

        public void ShowLoading(bool isLoading)
        {
            Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            this.Enabled = !isLoading;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SetEditMode(bool isEdit)
        {
            if (isEdit)
            {
                btnSave.Text = "Cập nhật";
                btnDelete.Enabled = true;
                btnDelete.BackColor = Color.FromArgb(239, 68, 68);
            }
            else
            {
                btnSave.Text = "Thêm mới";
                btnDelete.Enabled = false;
                btnDelete.BackColor = Color.Gray;
            }
        }

        #endregion

        #region Events

        private void DgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var row = dgvUsers.SelectedRows[0];
                var user = row.DataBoundItem as Users;

                if (user != null)
                {
                    _selectedUserId = user.UserID;
                    txtUsername.Text = user.Username;
                    txtUsername.Enabled = false; // Cannot change username
                    txtDisplayName.Text = user.FullName;
                    
                    if (cmbRole.Items.Contains(user.Role))
                        cmbRole.SelectedItem = user.Role;
                    else
                        cmbRole.Text = user.Role;

                    chkIsActive.Checked = user.Status == "active";
                    
                    SetEditMode(true);
                }
            }
        }

        #endregion
    }
}
