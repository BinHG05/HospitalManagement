using HospitalManagement.Presenters.Doctor;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Doctor;
using HospitalManagement.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_Examination : UserControl, IExaminationView
    {
        private ExaminationPresenter _presenter;
        private Action _onClose;
        private Action<int> _onPrescribe;

        public string Symptoms
        {
            get => txtSymptoms.Text;
            set => txtSymptoms.Text = value;
        }

        public string Diagnosis
        {
            get => txtDiagnosis.Text;
            set => txtDiagnosis.Text = value;
        }

        public string Notes
        {
            get => txtNotes.Text;
            set => txtNotes.Text = value;
        }

        public string TreatmentPlan
        {
            get => txtTreatment.Text;
            set => txtTreatment.Text = value;
        }

        public DateTime? NextAppointmentDate
        {
            get => chkNextAppt.Checked ? dtpNextAppt.Value : (DateTime?)null;
            set
            {
                if (value.HasValue)
                {
                    chkNextAppt.Checked = true;
                    dtpNextAppt.Value = value.Value;
                }
            }
        }

        public UC_Examination()
        {
            InitializeComponent();
        }

        public void Initialize(int appointmentId, Action onClose = null, Action<int> onPrescribe = null)
        {
            _onClose = onClose;
            _onPrescribe = onPrescribe;
            _presenter = new ExaminationPresenter(this, appointmentId);
            _presenter.LoadPatient();
        }

        #region IExaminationView Implementation

        private PatientExamInfo _currentPatient; // [NEW]

        public void LoadPatientInfo(PatientExamInfo patient)
        {
            _currentPatient = patient; // [NEW]
            lblPatientName.Text = patient.PatientName;
            lblPatientDetails.Text =
                $"🎂 Ngày sinh: {patient.DateOfBirth:dd/MM/yyyy}\n\n" +
                $"👤 Giới tính: {((patient.Gender == "Nam" || patient.Gender == "male") ? "Nam" : (patient.Gender == "Nữ" || patient.Gender == "female") ? "Nữ" : patient.Gender)}\n\n" +
                $"🩸 Nhóm máu: {patient.BloodType ?? "N/A"}\n\n" +
                $"💳 Số BHYT: {patient.InsuranceNumber ?? "N/A"}\n\n" +
                $"🏠 Địa chỉ:\n{patient.Address ?? "N/A"}\n\n" +
                $"━━━━━━━━━━━━━━━━━\n\n" +
                $"📊 Số lần khám: {patient.TotalVisits}\n\n" +
                $"📋 Chẩn đoán gần nhất:\n{patient.LastDiagnosis ?? "Không có"}";
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

        public void CloseView()
        {
            _onClose?.Invoke();
        }

        public void NavigateToPrescription(int examinationId)
        {
            _onPrescribe?.Invoke(examinationId);
        }

        public void LoadServiceRequests(IEnumerable<ServiceRequestInfo> services)
        {
            dgvServiceStatus.Visible = services.Any();
            lblServiceSummary.Visible = services.Any();
            btnRefresh.Visible = services.Any();

            dgvServiceStatus.DataSource = services.Select(s => new
            {
                DichVu = s.ServiceName,
                TrangThai = s.Status == "completed" ? "✅ Đã có kết quả" : "⏳ Đang chờ",
                ThoiGian = s.RequestedAt.ToString("HH:mm dd/MM"),
                KetQua = s.ResultDetails ?? "---"
            }).ToList();
        }

        public void SetCompleteButtonEnabled(bool enabled)
        {
            btnSave.Enabled = enabled;
            btnSave.BackColor = enabled ? Color.FromArgb(0, 168, 107) : Color.Gray;
        }

        public void ShowServiceStatus(string status)
        {
            lblServiceSummary.Text = status;
            if (status.Contains("Đang chờ"))
                lblServiceSummary.ForeColor = Color.FromArgb(255, 145, 0);
            else if (status.Contains("Đã có"))
                lblServiceSummary.ForeColor = Color.FromArgb(0, 168, 107);
        }

        #endregion

        #region Event Handlers

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _presenter.RefreshServiceStatus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn quay lại? Dữ liệu chưa lưu sẽ bị mất.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CloseView();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.SaveExamination();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnBack_Click(sender, e);
        }

        private void chkNextAppt_CheckedChanged(object sender, EventArgs e)
        {
            dtpNextAppt.Enabled = chkNextAppt.Checked;
        }

        private void btnAssignService_Click(object sender, EventArgs e)
        {
            using (var dialog = new HospitalManagement.Views.Forms.Doctor.ServiceAssignmentDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var selectedServices = dialog.SelectedServices;
                    if (selectedServices.Count == 0) return;

                    var serviceIds = selectedServices.Select(s => s.Id).ToList();
                    var serviceNames = string.Join(", ", selectedServices.Select(s => s.Name));
                    
                    // 1. Call logic to assign services in DB
                    var success = _presenter.AssignServices(serviceIds, serviceNames);

                    if (success)
                    {
                        // 2. Open Print/Export Form for EACH service
                        foreach (var service in selectedServices)
                        {
                            using (var printForm = new HospitalManagement.Views.Forms.Doctor.Form_ServiceRequestPrint(_currentPatient, service.Name, service.Price, txtDiagnosis.Text))
                            {
                                printForm.ShowDialog();
                            }
                        }
                    }
                }
            }
        }
        
        #endregion
    }
}
