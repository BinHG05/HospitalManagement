using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters.Admin;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Views.UserControls.Admin
{
    public partial class UC_ServiceManagement : UserControl, IServiceManagementView
    {
        private ServiceManagementPresenter _presenter;
        private int? _selectedId;

        public UC_ServiceManagement()
        {
            InitializeComponent();
            InitializePresenter();
            SetupEvents();
        }

        private void InitializePresenter()
        {
            _presenter = new ServiceManagementPresenter(this);
            // Load data when control loads
            this.Load += (s, e) => _presenter.LoadServices();
        }

        private void SetupEvents()
        {
            dgvServices.SelectionChanged += DgvServices_SelectionChanged;

            btnSave.Click += (s, e) => _presenter.SaveService();
            btnDelete.Click += (s, e) => 
            {
                if (_selectedId.HasValue)
                    _presenter.DeleteService(_selectedId.Value);
            };
            btnClear.Click += (s, e) => ClearInputs();
            btnAddNew.Click += (s, e) => ClearInputs();
            btnSearch.Click += (s, e) => _presenter.LoadServices();
        }

        private void DgvServices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count > 0)
            {
                var row = dgvServices.SelectedRows[0];
                var service = row.DataBoundItem as MedicalServices;
                if (service != null)
                {
                    _selectedId = service.ServiceID;
                    txtName.Text = service.ServiceName;
                    numPrice.Value = service.Price;
                    txtDescription.Text = service.Description;
                    
                    SetEditMode(true);
                }
            }
        }

        #region IServiceManagementView Implementation

        public string ServiceName => txtName.Text.Trim();
        public decimal Price => numPrice.Value;
        public string Description => txtDescription.Text.Trim();
        public int? SelectedServiceId => _selectedId;
        public string SearchKeyword => txtSearch.Text.Trim();

        public void SetServiceList(IEnumerable<MedicalServices> services)
        {
            dgvServices.DataSource = null;
            dgvServices.DataSource = services;
            
            // Hide unwanted columns
            if (dgvServices.Columns["ServiceID"] != null) dgvServices.Columns["ServiceID"].Visible = false;
            // Hide navigation properties
            if (dgvServices.Columns["Department"] != null) dgvServices.Columns["Department"].Visible = false;
            if (dgvServices.Columns["ServiceRequests"] != null) dgvServices.Columns["ServiceRequests"].Visible = false;
            if (dgvServices.Columns["ServiceResults"] != null) dgvServices.Columns["ServiceResults"].Visible = false;

            if (dgvServices.Columns["ServiceName"] != null) 
            {
                dgvServices.Columns["ServiceName"].HeaderText = "Tên Dịch vụ";
                dgvServices.Columns["ServiceName"].Width = 200;
            }
            if (dgvServices.Columns["ServiceType"] != null) dgvServices.Columns["ServiceType"].HeaderText = "Loại";
            if (dgvServices.Columns["Description"] != null) dgvServices.Columns["Description"].HeaderText = "Mô tả";
            if (dgvServices.Columns["Price"] != null) 
            {
                dgvServices.Columns["Price"].HeaderText = "Giá (VNĐ)";
                dgvServices.Columns["Price"].DefaultCellStyle.Format = "N0";
            }
            if (dgvServices.Columns["EstimatedTime"] != null) dgvServices.Columns["EstimatedTime"].HeaderText = "Thời gian (phút)";
            if (dgvServices.Columns["IsActive"] != null) dgvServices.Columns["IsActive"].Visible = false;
            if (dgvServices.Columns["CreatedAt"] != null) dgvServices.Columns["CreatedAt"].Visible = false;
            if (dgvServices.Columns["DepartmentID"] != null) dgvServices.Columns["DepartmentID"].Visible = false;
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
            numPrice.Value = 0;
            txtDescription.Text = "";
            
            SetEditMode(false);
            dgvServices.ClearSelection();
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
