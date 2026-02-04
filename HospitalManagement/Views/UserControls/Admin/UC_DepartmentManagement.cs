using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters.Admin;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Views.UserControls.Admin
{
    public partial class UC_DepartmentManagement : UserControl, IDepartmentManagementView
    {
        private DepartmentManagementPresenter _presenter;
        private int? _selectedId;

        public UC_DepartmentManagement()
        {
            InitializeComponent();
            InitializePresenter();
            SetupEvents();
        }

        private void InitializePresenter()
        {
            _presenter = new DepartmentManagementPresenter(this);
            // Load data when control loads
            this.Load += (s, e) => _presenter.LoadDepartments();
        }

        private void SetupEvents()
        {
            dgvDepartments.SelectionChanged += DgvDepartments_SelectionChanged;

            btnSave.Click += (s, e) => _presenter.SaveDepartment();
            btnDelete.Click += (s, e) => 
            {
                if (_selectedId.HasValue)
                    _presenter.DeleteDepartment(_selectedId.Value);
            };
            btnClear.Click += (s, e) => ClearInputs();
            btnAddNew.Click += (s, e) => ClearInputs();
            btnSearch.Click += (s, e) => _presenter.LoadDepartments();
        }

        private void DgvDepartments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count > 0)
            {
                var row = dgvDepartments.SelectedRows[0];
                var dept = row.DataBoundItem as Departments;
                if (dept != null)
                {
                    _selectedId = dept.DepartmentID;
                    txtName.Text = dept.DepartmentName;
                    txtDescription.Text = dept.Description;
                    
                    SetEditMode(true);
                }
            }
        }

        #region IDepartmentManagementView Implementation

        public string DepartmentName => txtName.Text.Trim();
        public string Description => txtDescription.Text.Trim();
        public int? SelectedDepartmentId => _selectedId;
        public string SearchKeyword => txtSearch.Text.Trim();

        public void SetDepartmentList(IEnumerable<Departments> departments)
        {
            dgvDepartments.DataSource = null;
            dgvDepartments.DataSource = departments;
            
            // Format
            if (dgvDepartments.Columns["DepartmentID"] != null) dgvDepartments.Columns["DepartmentID"].Visible = false;
            // Hide navigation properties
            if (dgvDepartments.Columns["HeadDoctorID"] != null) dgvDepartments.Columns["HeadDoctorID"].Visible = false;
            if (dgvDepartments.Columns["HeadDoctor"] != null) dgvDepartments.Columns["HeadDoctor"].Visible = false;
            if (dgvDepartments.Columns["Appointments"] != null) dgvDepartments.Columns["Appointments"].Visible = false;
            if (dgvDepartments.Columns["DoctorSchedules"] != null) dgvDepartments.Columns["DoctorSchedules"].Visible = false;
            if (dgvDepartments.Columns["Doctors"] != null) dgvDepartments.Columns["Doctors"].Visible = false;
            if (dgvDepartments.Columns["MedicalServices"] != null) dgvDepartments.Columns["MedicalServices"].Visible = false;

            if (dgvDepartments.Columns["DepartmentName"] != null) 
            {
                dgvDepartments.Columns["DepartmentName"].HeaderText = "Tên Phòng ban";
                dgvDepartments.Columns["DepartmentName"].FillWeight = 30;
            }
            if (dgvDepartments.Columns["Description"] != null) 
            {
                dgvDepartments.Columns["Description"].HeaderText = "Mô tả";
                dgvDepartments.Columns["Description"].FillWeight = 40;
            }
            if (dgvDepartments.Columns["Location"] != null) 
            {
                dgvDepartments.Columns["Location"].HeaderText = "Vị trí";
                dgvDepartments.Columns["Location"].FillWeight = 15;
            }
            if (dgvDepartments.Columns["Phone"] != null) 
            {
                dgvDepartments.Columns["Phone"].HeaderText = "Điện thoại";
                dgvDepartments.Columns["Phone"].FillWeight = 15;
            }
            if (dgvDepartments.Columns["CreatedAt"] != null) dgvDepartments.Columns["CreatedAt"].Visible = false;
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

        public void ClearInputs()
        {
            _selectedId = null;
            txtName.Text = "";
            txtDescription.Text = "";
            
            SetEditMode(false);
            dgvDepartments.ClearSelection();
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
    }
}
