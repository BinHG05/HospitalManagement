using HospitalManagement.Presenters.Doctor;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Doctor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_PatientQueue : UserControl, IPatientQueueView
    {
        private PatientQueuePresenter _presenter;
        private int _selectedAppointmentId;
        private Action<int> _onStartExamination;

        public int SelectedAppointmentId => _selectedAppointmentId;

        public UC_PatientQueue()
        {
            InitializeComponent();
            lblDate.Text = $"Hôm nay: {DateTime.Today:dd/MM/yyyy}";
        }

        public void Initialize(int doctorId, Action<int> onStartExamination = null)
        {
            _onStartExamination = onStartExamination;
            _presenter = new PatientQueuePresenter(this, doctorId);
            _presenter.LoadQueue();
        }

        #region IPatientQueueView Implementation

        public void LoadQueue(IEnumerable<QueuePatientInfo> queue)
        {
            dgvQueue.Rows.Clear();

            foreach (var patient in queue)
            {
                var rowIndex = dgvQueue.Rows.Add();
                var row = dgvQueue.Rows[rowIndex];

                row.Cells["colNumber"].Value = patient.QueueNumber;
                row.Cells["colPatientName"].Value = patient.PatientName;
                row.Cells["colAge"].Value = patient.Age?.ToString() ?? "-";
                row.Cells["colGender"].Value = patient.Gender == "male" ? "Nam" : 
                    patient.Gender == "female" ? "Nữ" : patient.Gender;
                row.Cells["colSymptoms"].Value = patient.Symptoms ?? "-";
                row.Cells["colStatus"].Value = patient.StatusDisplay;
                row.Tag = patient.AppointmentId;

                if (patient.Status == "confirmed")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(241, 196, 15);
                }
            }

            if (dgvQueue.Rows.Count == 0)
            {
                dgvQueue.Rows.Add();
                dgvQueue.Rows[0].Cells["colPatientName"].Value = "Không có bệnh nhân trong hàng đợi";
            }
        }

        public void ShowPatientDetails(PatientExamInfo patient)
        {
            _selectedAppointmentId = patient.AppointmentId;

            lblDetailsContent.Text =
                $"👤 Họ tên: {patient.PatientName}\n\n" +
                $"🎂 Ngày sinh: {patient.DateOfBirth:dd/MM/yyyy}\n\n" +
                $"👤 Giới tính: {(patient.Gender == "male" ? "Nam" : patient.Gender == "female" ? "Nữ" : patient.Gender)}\n\n" +
                $"🩸 Nhóm máu: {patient.BloodType ?? "N/A"}\n\n" +
                $"🏠 Địa chỉ: {patient.Address ?? "N/A"}\n\n" +
                $"💳 Số BHYT: {patient.InsuranceNumber ?? "N/A"}\n\n" +
                $"📝 Triệu chứng: {patient.Symptoms ?? "N/A"}\n\n" +
                $"📊 Số lần khám: {patient.TotalVisits}\n" +
                $"📋 Chẩn đoán gần nhất: {patient.LastDiagnosis ?? "Không có"}";

            panelDetails.Visible = true;
            panelDetails.BringToFront();
            panelDetails.Location = new Point(
                (this.Width - panelDetails.Width) / 2,
                (this.Height - panelDetails.Height) / 2
            );
        }

        public void OpenExamination(int appointmentId)
        {
            _onStartExamination?.Invoke(appointmentId);
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

        public void RefreshQueue()
        {
            panelDetails.Visible = false;
            _presenter.LoadQueue();
        }

        #endregion

        #region Event Handlers

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshQueue();
        }

        private void dgvQueue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var appointmentIdObj = dgvQueue.Rows[e.RowIndex].Tag;
            if (appointmentIdObj == null) return;

            var appointmentId = (int)appointmentIdObj;

            if (e.ColumnIndex == dgvQueue.Columns["colActions"].Index)
            {
                _presenter.ViewPatientDetails(appointmentId);
            }
        }

        private void btnCallPatient_Click(object sender, EventArgs e)
        {
            if (_selectedAppointmentId > 0)
            {
                _presenter.CallPatient(_selectedAppointmentId);
            }
        }

        private void btnStartExam_Click(object sender, EventArgs e)
        {
            if (_selectedAppointmentId > 0)
            {
                _presenter.StartExamination(_selectedAppointmentId);
            }
        }

        private void btnCloseDetails_Click(object sender, EventArgs e)
        {
            panelDetails.Visible = false;
        }

        #endregion
    }
}
