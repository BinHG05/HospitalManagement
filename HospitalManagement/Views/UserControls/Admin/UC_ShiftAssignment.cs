using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Added for Select
using System.Windows.Forms;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters.Admin;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Views.UserControls.Admin
{
    public partial class UC_ShiftAssignment : UserControl, IShiftAssignmentView
    {
        private ShiftAssignmentPresenter _presenter;
        private int? _selectedScheduleId;

        public UC_ShiftAssignment()
        {
            InitializeComponent();
            InitializePresenter();
            SetupEvents();
        }

        private void InitializePresenter()
        {
            _presenter = new ShiftAssignmentPresenter(this);
            // Load initial data
            this.Load += (s, e) => _presenter.LoadInitialData();
        }

        private void SetupEvents()
        {
            // Trigger LoadSchedule when Date changes
            dtpDate.ValueChanged += (s, e) => _presenter.LoadSchedule();
            
            btnAssign.Click += (s, e) => _presenter.AssignShift();
            btnDelete.Click += (s, e) => 
            {
                if (_selectedScheduleId.HasValue)
                    _presenter.DeleteAssignment(_selectedScheduleId.Value);
            };
            
            btnAddNew.Click += (s, e) => ClearSelection(); // "Thêm mới" resets form

            dgvSchedule.SelectionChanged += DgvSchedule_SelectionChanged;
        }

        private void DgvSchedule_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSchedule.SelectedRows.Count > 0)
            {
                var row = dgvSchedule.SelectedRows[0];
                // Note: The DataBoundItem depends on how we set the list.
                // If it's pure Entity, it's DoctorSchedules.
                // We'll handle this in SetScheduleList.
                
                // For now, let's assume we can get ScheduleID from a hidden column if BindingSource or direct object
                if (dgvSchedule.CurrentRow.Cells["ScheduleID"]?.Value != null)
                {
                    _selectedScheduleId = (int)dgvSchedule.CurrentRow.Cells["ScheduleID"].Value;
                    btnDelete.Enabled = true;
                    btnDelete.BackColor = Color.FromArgb(231, 76, 60); // Red
                }
            }
            else
            {
                _selectedScheduleId = null;
                btnDelete.Enabled = false;
                btnDelete.BackColor = Color.Gray;
            }
        }

        #region IShiftAssignmentView Implementation

        public int? SelectedDoctorId => (cmbDoctors.SelectedItem as dynamic)?.Value; // Will use DropdownItem pattern
        public int? SelectedShiftId => (cmbShifts.SelectedItem as dynamic)?.Value;
        public DateTime SelectedDate => dtpDate.Value;

        public void SetDoctorList(IEnumerable<Users> doctors)
        {
            // Create a temporary simplified list for ComboBox
            var list = doctors.Select(u => new { Display = u.FullName, Value = u.UserID }).ToList();
            
            cmbDoctors.DisplayMember = "Display";
            cmbDoctors.ValueMember = "Value";
            cmbDoctors.DataSource = list;
            cmbDoctors.SelectedIndex = -1;
        }

        public void SetShiftList(IEnumerable<Shifts> shifts)
        {
            var list = shifts.Select(s => new {
                Display = $"{s.ShiftName} ({s.StartTime:hh\\:mm} - {s.EndTime:hh\\:mm})",
                Value = s.ShiftID 
            }).ToList();

            cmbShifts.DisplayMember = "Display";
            cmbShifts.ValueMember = "Value";
            cmbShifts.DataSource = list;
            cmbShifts.SelectedIndex = -1;
        }

        public void SetScheduleList(IEnumerable<DoctorSchedules> schedules)
        {
            // Flatten data for DataGridView
            var displayList = schedules.Select(ds => new
            {
                ScheduleID = ds.ScheduleID,
                DoctorName = ds.Doctor?.User?.FullName ?? "Unknown",
                Department = ds.Department?.DepartmentName ?? "N/A",
                ShiftName = ds.Shift?.ShiftName ?? "N/A",
                Time = $"{ds.Shift?.StartTime:hh\\:mm} - {ds.Shift?.EndTime:hh\\:mm}"
            }).ToList();

            dgvSchedule.DataSource = null;
            dgvSchedule.DataSource = displayList;

            // Adjust Columns
            if (dgvSchedule.Columns["ScheduleID"] != null) dgvSchedule.Columns["ScheduleID"].Visible = false;
            
            if (dgvSchedule.Columns["DoctorName"] != null) 
            {
                dgvSchedule.Columns["DoctorName"].HeaderText = "Bác sĩ";
                dgvSchedule.Columns["DoctorName"].Width = 200;
            }
            if (dgvSchedule.Columns["Department"] != null) dgvSchedule.Columns["Department"].HeaderText = "Khoa";
            if (dgvSchedule.Columns["ShiftName"] != null) dgvSchedule.Columns["ShiftName"].HeaderText = "Ca trực";
            if (dgvSchedule.Columns["Time"] != null) dgvSchedule.Columns["Time"].HeaderText = "Thời gian";
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

        public void ClearSelection()
        {
            cmbDoctors.SelectedIndex = -1;
            cmbShifts.SelectedIndex = -1;
            // Ensure Create New mode
            dgvSchedule.ClearSelection();
            _selectedScheduleId = null;
            btnDelete.Enabled = false;
        }

        #endregion
    }
}
