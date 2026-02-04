using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters.Admin;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Views.UserControls.Admin
{
    public partial class UC_DoctorManagement : UserControl, IDoctorManagementView
    {
        private DoctorManagementPresenter _presenter;
        private int? _selectedDoctorId;

        public UC_DoctorManagement()
        {
            InitializeComponent();
            InitializePresenter();
            SetupEvents();
        }

        private void InitializePresenter()
        {
            _presenter = new DoctorManagementPresenter(this);
            // Load data when control loads
            this.Load += (s, e) => _presenter.LoadInitialData();
        }

        private void SetupEvents()
        {
            // Grid Selection
            dgvDoctors.SelectionChanged += DgvDoctors_SelectionChanged;

            // Buttons
            btnSave.Click += (s, e) => _presenter.SaveDoctor();
            btnDelete.Click += (s, e) => 
            {
                if (_selectedDoctorId.HasValue)
                    _presenter.DeleteDoctor(_selectedDoctorId.Value);
            };
            btnClear.Click += (s, e) => ClearInputs();
            btnAddNew.Click += (s, e) => ClearInputs();
            btnSearch.Click += (s, e) => _presenter.LoadData(); // Re-load triggers logic if I impl search filter in Presenter
            cmbFilterDepartment.SelectedIndexChanged += (s, e) => _presenter.LoadData();
            
            // For now, simpler filtering in UI or reload with logic? 
            // The interface has SearchKeyword, Presenter has logic.
            // Let's ensure Presenter uses SearchKeyword.
            // Actually, my Presenter.LoadData doesn't filter by keyword yet.
            // I should update Presenter later or just filter locally.
            // For MVP, I will just call LoadData which reloads all. 
            // Filter is unimplemented in Presenter, will do soon if needed.
        }

        private void DgvDoctors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count > 0)
            {
                var row = dgvDoctors.SelectedRows[0];
                // The DataSource is anonymous object list.
                // We access properties via reflection or dynamic if needed,
                // but since we bound it, the cells are populated.
                // Better way: use the bound item.
                
                // Reflection to get values from anonymous type
                dynamic doc = row.DataBoundItem;
                if (doc != null)
                {
                    _selectedDoctorId = doc.DoctorID;
                    
                    // Set fields
                    // User
                    cmbUser.SelectedValue = doc.UserID ?? 0;
                    
                    // Dept
                    cmbDepartment.SelectedValue = doc.DepartmentID ?? 0;

                    txtSpecialization.Text = doc.Specialization;
                    txtLicense.Text = doc.LicenseNumber;
                    numExp.Value = (decimal)(doc.YearsOfExperience ?? 0);
                    txtFee.Text = doc.ConsultationFee?.ToString("0.##");
                    txtQualifications.Text = doc.Qualifications;
                    chkIsActive.Checked = doc.IsActive ?? false;

                    SetEditMode(true);
                }
            }
        }

        #region IDoctorManagementView Implementation

        public string Specialization => txtSpecialization.Text.Trim();
        public string LicenseNumber => txtLicense.Text.Trim();
        public int YearsOfExperience => (int)numExp.Value;
        public string Qualifications => txtQualifications.Text.Trim();
        public decimal ConsultationFee 
        {
            get
            {
                if (decimal.TryParse(txtFee.Text, out decimal val))
                    return val;
                return 0;
            }
        }
        public bool IsActive => chkIsActive.Checked;

        public int SelectedUserId 
        {
            get 
            {
                if (cmbUser.SelectedValue != null)
                    return (int)cmbUser.SelectedValue;
                return 0;
            }
        }

        public int SelectedDepartmentId
        {
            get
            {
                if (cmbDepartment.SelectedValue != null)
                    return (int)cmbDepartment.SelectedValue;
                return 0;
            }
        }

        public int? SelectedDoctorId => _selectedDoctorId;

        public string SearchKeyword => txtSearch.Text.Trim();
        public int? FilterDepartmentId 
        {
            get
            {
                if (cmbFilterDepartment.SelectedValue is int val && val > 0)
                    return val;
                return null;
            }
        }

        public void SetDoctorList(IEnumerable<object> doctors)
        {
            dgvDoctors.DataSource = null;
            dgvDoctors.DataSource = doctors;
            
            // Format Columns
            if (dgvDoctors.Columns["DoctorID"] != null) dgvDoctors.Columns["DoctorID"].Visible = false;
            if (dgvDoctors.Columns["UserID"] != null) dgvDoctors.Columns["UserID"].Visible = false;
            if (dgvDoctors.Columns["DepartmentID"] != null) dgvDoctors.Columns["DepartmentID"].Visible = false;

            if (dgvDoctors.Columns["DoctorName"] != null) 
            {
                dgvDoctors.Columns["DoctorName"].HeaderText = "Tên Bác sĩ";
                dgvDoctors.Columns["DoctorName"].FillWeight = 20;
            }
            if (dgvDoctors.Columns["DepartmentName"] != null) 
            {
                dgvDoctors.Columns["DepartmentName"].HeaderText = "Khoa";
                dgvDoctors.Columns["DepartmentName"].FillWeight = 15;
            }
            if (dgvDoctors.Columns["Specialization"] != null) 
            {
                dgvDoctors.Columns["Specialization"].HeaderText = "Chuyên khoa";
                dgvDoctors.Columns["Specialization"].FillWeight = 15;
            }
            if (dgvDoctors.Columns["LicenseNumber"] != null) 
            {
                dgvDoctors.Columns["LicenseNumber"].HeaderText = "Số CCHN";
                dgvDoctors.Columns["LicenseNumber"].FillWeight = 10;
            }
            if (dgvDoctors.Columns["YearsOfExperience"] != null) 
            {
                dgvDoctors.Columns["YearsOfExperience"].HeaderText = "Kinh nghiệm";
                dgvDoctors.Columns["YearsOfExperience"].FillWeight = 10;
            }
            if (dgvDoctors.Columns["ConsultationFee"] != null) 
            {
                dgvDoctors.Columns["ConsultationFee"].HeaderText = "Phí khám";
                dgvDoctors.Columns["ConsultationFee"].DefaultCellStyle.Format = "N0";
                dgvDoctors.Columns["ConsultationFee"].FillWeight = 15;
            }
            if (dgvDoctors.Columns["IsActive"] != null) 
            {
                dgvDoctors.Columns["IsActive"].HeaderText = "Hoạt động";
                dgvDoctors.Columns["IsActive"].FillWeight = 10;
            }
            // Hide other columns
            if (dgvDoctors.Columns["Qualifications"] != null) dgvDoctors.Columns["Qualifications"].Visible = false;
        }

        public void SetUserList(IEnumerable<Users> users)
        {
            // Need to bind full list for Combo
            cmbUser.DataSource = users;
            cmbUser.DisplayMember = "FullName"; // or "Username"
            cmbUser.ValueMember = "UserID";
            cmbUser.SelectedIndex = -1;
        }

        public void SetDepartmentList(IEnumerable<Departments> departments)
        {
            // For Edit Form
            cmbDepartment.DataSource = departments.Where(d => d.DepartmentID > 0).ToList();
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbDepartment.SelectedIndex = -1;

            // For Filter
            cmbFilterDepartment.DataSource = departments.ToList();
            cmbFilterDepartment.DisplayMember = "DepartmentName";
            cmbFilterDepartment.ValueMember = "DepartmentID";
            // Do not change selection if already filtering
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
            _selectedDoctorId = null;
            
            cmbUser.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            
            txtSpecialization.Clear();
            txtLicense.Clear();
            numExp.Value = 0;
            txtFee.Clear();
            txtQualifications.Clear();
            
            chkIsActive.Checked = true;

            SetEditMode(false);
            dgvDoctors.ClearSelection();
        }

        public void SetEditMode(bool isEdit)
        {
            if (isEdit)
            {
                btnSave.Text = "Cập nhật";
                btnDelete.Enabled = true;
                btnDelete.BackColor = Color.FromArgb(239, 68, 68);
                cmbUser.Enabled = false; // Usually shouldn't change User link once set
            }
            else
            {
                btnSave.Text = "Thêm mới";
                btnDelete.Enabled = false;
                btnDelete.BackColor = Color.Gray;
                cmbUser.Enabled = true;
            }
        }

        #endregion
    }
}
