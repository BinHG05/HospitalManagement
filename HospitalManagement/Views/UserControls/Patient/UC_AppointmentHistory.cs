using HospitalManagement.Presenters.Patient;
using HospitalManagement.Views.Interfaces.Patient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Patient
{
    public partial class UC_AppointmentHistory : UserControl, IAppointmentHistoryView
    {
        private AppointmentHistoryPresenter _presenter;
        private List<AppointmentDisplayInfo> _appointments;
        private int _selectedAppointmentId;
        private AppointmentDisplayInfo _selectedAppointment;

        public string SelectedStatusFilter => (cmbStatusFilter.SelectedItem as FilterItem)?.Value ?? "all";
        public int SelectedAppointmentId => _selectedAppointmentId;

        public UC_AppointmentHistory()
        {
            InitializeComponent();
            InitializeFilters();
        }

        public void Initialize(int patientId)
        {
            _presenter = new AppointmentHistoryPresenter(this, patientId);
            _presenter.LoadAppointments();
        }

        private void InitializeFilters()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Tất cả", Value = "all" });
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Chờ xác nhận", Value = "pending" });
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Đã xác nhận", Value = "confirmed" });
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Hoàn thành", Value = "completed" });
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Đã hủy", Value = "cancelled" });
            cmbStatusFilter.SelectedIndex = 0;
        }

        #region IAppointmentHistoryView Implementation

        public void LoadAppointments(IEnumerable<AppointmentDisplayInfo> appointments)
        {
            _appointments = new List<AppointmentDisplayInfo>(appointments);
            dgvAppointments.Rows.Clear();

            foreach (var apt in _appointments)
            {
                var rowIndex = dgvAppointments.Rows.Add();
                var row = dgvAppointments.Rows[rowIndex];

                row.Cells["colDate"].Value = apt.AppointmentDate.ToString("dd/MM/yyyy");
                row.Cells["colTime"].Value = apt.TimeRange;
                row.Cells["colNumber"].Value = apt.AppointmentNumber;
                row.Cells["colDepartment"].Value = apt.DepartmentName;
                row.Cells["colDoctor"].Value = apt.DoctorName;
                row.Cells["colStatus"].Value = apt.StatusDisplay;
                row.Tag = apt.AppointmentId;

                // Color coding for status
                switch (apt.Status)
                {
                    case "pending":
                        row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(241, 196, 15);
                        break;
                    case "confirmed":
                        row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(0, 168, 107);
                        break;
                    case "examining":
                        row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(155, 89, 182); // Purple for examining
                        break;
                    case "completed":
                        row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(0, 102, 204);
                        break;
                    case "cancelled":
                        row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(231, 76, 60);
                        break;
                }
            }
        }

        private string GetStatusDisplay(string status)
        {
            switch (status)
            {
                case "pending": return "Chờ xác nhận";
                case "confirmed": return "Đã xác nhận";
                case "examining": return "Đang khám";
                case "completed": return "Hoàn thành";
                case "cancelled": return "Đã hủy";
                default: return status;
            }
        }

        public void ShowAppointmentDetails(AppointmentDisplayInfo appointment)
        {
            _selectedAppointment = appointment;
            _selectedAppointmentId = appointment.AppointmentId;

            lblDetailsContent.Text = 
                $"📅 Ngày khám: {appointment.AppointmentDate:dd/MM/yyyy}\n\n" +
                $"⏰ Khung giờ: {appointment.TimeRange} ({appointment.ShiftName})\n\n" +
                $"🔢 Số thứ tự: {appointment.AppointmentNumber}\n\n" +
                $"🏥 Khoa: {appointment.DepartmentName}\n\n" +
                $"👨‍⚕️ Bác sĩ: {appointment.DoctorName}\n\n" +
                $"📝 Triệu chứng: {appointment.Symptoms ?? "Không có"}\n\n" +
                $"📊 Trạng thái: {appointment.StatusDisplay}";

            btnCancel.Visible = appointment.CanCancel;
            panelDetails.Visible = true;
            panelDetails.BringToFront();

            // Center the panel
            panelDetails.Location = new Point(
                (this.Width - panelDetails.Width) / 2,
                (this.Height - panelDetails.Height) / 2
            );
        }

        public void ShowCancelConfirmation(int appointmentId)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn hủy lịch hẹn này?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _presenter.CancelAppointment(appointmentId);
            }
        }

        public void ShowRescheduleDialog(int appointmentId)
        {
            // TODO: Implement reschedule dialog
            MessageBox.Show("Chức năng đổi lịch đang được phát triển.", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowLoading(bool isLoading)
        {
            panelLoading.Visible = isLoading;
            panelLoading.BringToFront();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void RefreshList()
        {
            panelDetails.Visible = false;
            _presenter.LoadAppointments(SelectedStatusFilter);
        }

        #endregion

        #region Event Handlers

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                _presenter.LoadAppointments(SelectedStatusFilter);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void dgvAppointments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var appointmentId = (int)dgvAppointments.Rows[e.RowIndex].Tag;

            if (e.ColumnIndex == dgvAppointments.Columns["colActions"].Index)
            {
                _presenter.ViewDetails(appointmentId);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_selectedAppointmentId > 0)
            {
                ShowCancelConfirmation(_selectedAppointmentId);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panelDetails.Visible = false;
        }

        #endregion

        private class FilterItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
