using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters.Doctor;
using HospitalManagement.Views.Interfaces.Doctor;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_ShiftRegistration : UserControl, IShiftRegistrationView
    {
        private ShiftRegistrationPresenter _presenter;
        private int? _selectedScheduleId;

        public UC_ShiftRegistration(int doctorId, int userId)
        {
            InitializeComponent();
            _presenter = new ShiftRegistrationPresenter(this, doctorId, userId);
            SetupEvents();
        }

        private void SetupEvents()
        {
            this.Load += (s, e) =>
            {
                _presenter.LoadAvailableShifts();
                _presenter.LoadMyRegistrations();
                _presenter.LoadMonthlyQuota();
            };

            dtpDate.ValueChanged += (s, e) => _presenter.LoadAvailableShifts();
            btnRegister.Click += (s, e) => _presenter.RegisterShift();
            btnCancel.Click += (s, e) =>
            {
                if (_selectedScheduleId.HasValue)
                {
                    var result = MessageBox.Show(
                        "Bạn có chắc muốn hủy đăng ký ca này?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                        _presenter.CancelRegistration(_selectedScheduleId.Value);
                }
            };

            dgvMyRegistrations.SelectionChanged += DgvMyRegistrations_SelectionChanged;
        }

        private void DgvMyRegistrations_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMyRegistrations.SelectedRows.Count > 0)
            {
                var row = dgvMyRegistrations.SelectedRows[0];
                if (row.Cells["ScheduleID"]?.Value != null)
                {
                    _selectedScheduleId = (int)row.Cells["ScheduleID"].Value;
                    btnCancel.Enabled = true;
                }
            }
            else
            {
                _selectedScheduleId = null;
                btnCancel.Enabled = false;
            }
        }

        #region IShiftRegistrationView Implementation

        public DateTime SelectedDate => dtpDate.Value;

        public int? SelectedShiftId
        {
            get
            {
                if (dgvShifts.SelectedRows.Count > 0)
                {
                    var row = dgvShifts.SelectedRows[0];
                    if (row.Cells["ShiftID"]?.Value != null)
                        return (int)row.Cells["ShiftID"].Value;
                }
                return null;
            }
        }

        public void SetAvailableShifts(IEnumerable<ShiftSlotInfo> shifts)
        {
            var displayList = shifts.Select(s => new
            {
                ShiftID = s.ShiftID,
                Status = s.StatusIcon,
                ShiftName = s.ShiftName,
                Time = $"{s.StartTime:hh\\:mm} - {s.EndTime:hh\\:mm}",
                Slots = s.SlotDisplay,
                Available = s.CanRegister ? "✅ Còn chỗ" : "❌ Đầy"
            }).ToList();

            dgvShifts.DataSource = null;
            dgvShifts.DataSource = displayList;

            // Configure columns
            if (dgvShifts.Columns["ShiftID"] != null) 
                dgvShifts.Columns["ShiftID"].Visible = false;
            if (dgvShifts.Columns["Status"] != null)
            {
                dgvShifts.Columns["Status"].HeaderText = "";
                dgvShifts.Columns["Status"].Width = 30;
            }
            if (dgvShifts.Columns["ShiftName"] != null)
                dgvShifts.Columns["ShiftName"].HeaderText = "Ca trực";
            if (dgvShifts.Columns["Time"] != null)
                dgvShifts.Columns["Time"].HeaderText = "Giờ";
            if (dgvShifts.Columns["Slots"] != null)
                dgvShifts.Columns["Slots"].HeaderText = "Đã ĐK";
            if (dgvShifts.Columns["Available"] != null)
                dgvShifts.Columns["Available"].HeaderText = "Trạng thái";
        }

        public void SetMyRegistrations(IEnumerable<DoctorSchedules> registrations)
        {
            var displayList = registrations.Select(ds => new
            {
                ScheduleID = ds.ScheduleID,
                Date = ds.ScheduleDate.ToString("dd/MM/yyyy"),
                DayOfWeek = ds.ScheduleDate.ToString("dddd"),
                ShiftName = ds.Shift?.ShiftName ?? "N/A",
                Time = ds.Shift != null ? $"{ds.Shift.StartTime:hh\\:mm} - {ds.Shift.EndTime:hh\\:mm}" : "",
                Status = GetStatusDisplay(ds.Status),
                StatusRaw = ds.Status
            }).ToList();

            dgvMyRegistrations.DataSource = null;
            dgvMyRegistrations.DataSource = displayList;

            // Configure columns
            if (dgvMyRegistrations.Columns["ScheduleID"] != null)
                dgvMyRegistrations.Columns["ScheduleID"].Visible = false;
            if (dgvMyRegistrations.Columns["StatusRaw"] != null)
                dgvMyRegistrations.Columns["StatusRaw"].Visible = false;
            if (dgvMyRegistrations.Columns["Date"] != null)
                dgvMyRegistrations.Columns["Date"].HeaderText = "Ngày";
            if (dgvMyRegistrations.Columns["DayOfWeek"] != null)
                dgvMyRegistrations.Columns["DayOfWeek"].HeaderText = "Thứ";
            if (dgvMyRegistrations.Columns["ShiftName"] != null)
                dgvMyRegistrations.Columns["ShiftName"].HeaderText = "Ca";
            if (dgvMyRegistrations.Columns["Time"] != null)
                dgvMyRegistrations.Columns["Time"].HeaderText = "Giờ";
            if (dgvMyRegistrations.Columns["Status"] != null)
                dgvMyRegistrations.Columns["Status"].HeaderText = "Trạng thái";
        }

        private string GetStatusDisplay(string status)
        {
            switch (status)
            {
                case "Pending": return "⏳ Chờ duyệt";
                case "Approved": return "✅ Đã duyệt";
                case "Rejected": return "❌ Từ chối";
                default: return status;
            }
        }

        public void SetMonthlyQuota(int current, int min, int max)
        {
            lblQuotaValue.Text = $"{current} / {min} ca (Tối đa: {max})";
            
            // Calculate progress
            int percentage = min > 0 ? (int)((current * 100.0) / min) : 0;
            percentage = Math.Min(percentage, 100);
            progressQuota.Value = percentage;

            // Color based on status
            if (current < min)
            {
                lblQuotaValue.ForeColor = Color.FromArgb(231, 76, 60); // Red
                lblQuotaTitle.Text = "📊 Định mức tháng này: 🔴 Chưa đủ";
            }
            else if (current >= max)
            {
                lblQuotaValue.ForeColor = Color.FromArgb(46, 204, 113); // Green
                lblQuotaTitle.Text = "📊 Định mức tháng này: 🟢 Đạt tối đa";
            }
            else
            {
                lblQuotaValue.ForeColor = Color.FromArgb(241, 196, 15); // Yellow
                lblQuotaTitle.Text = "📊 Định mức tháng này: 🟡 Đã đủ";
            }
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
            dgvShifts.ClearSelection();
            _selectedScheduleId = null;
        }

        #endregion
    }
}
