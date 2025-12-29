using HospitalManagement.Presenters.Doctor;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Doctor;
using System;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_Examination : UserControl, IExaminationView
    {
        private ExaminationPresenter _presenter;
        private Action _onClose;

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

        public void Initialize(int appointmentId, Action onClose = null)
        {
            _onClose = onClose;
            _presenter = new ExaminationPresenter(this, appointmentId);
            _presenter.LoadPatient();
        }

        #region IExaminationView Implementation

        public void LoadPatientInfo(PatientExamInfo patient)
        {
            lblPatientName.Text = patient.PatientName;
            lblPatientDetails.Text =
                $"🎂 Ngày sinh: {patient.DateOfBirth:dd/MM/yyyy}\n\n" +
                $"👤 Giới tính: {(patient.Gender == "male" ? "Nam" : patient.Gender == "female" ? "Nữ" : patient.Gender)}\n\n" +
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

        #endregion

        #region Event Handlers

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

        #endregion
    }
}
