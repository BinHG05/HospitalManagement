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

        public void NavigateToPrescription(int examinationId)
        {
            _onPrescribe?.Invoke(examinationId);
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

        private void btnAssignService_Click(object sender, EventArgs e)
        {
            using (var dialog = new HospitalManagement.Views.Forms.Doctor.ServiceAssignmentDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var serviceName = dialog.SelectedService;
                    var doctorId = dialog.SelectedDoctorId;
                    var doctorName = dialog.SelectedDoctorName;

                    // 1. Call logic to assign service in DB
                    var startSuccess = _presenter.AssignService(serviceName, doctorId);

                    if (startSuccess)
                    {
                        // 2. Export file logic
                        try
                        {
                            string fileName = $"PhieuChiDinh_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HospitalManagement", "ServiceRequests");
                            
                            if (!System.IO.Directory.Exists(folderPath))
                            {
                                System.IO.Directory.CreateDirectory(folderPath);
                            }

                            string filePath = System.IO.Path.Combine(folderPath, fileName);
                            
                            string content = $@"
========================================
       PHIẾU CHỈ ĐỊNH DỊCH VỤ
========================================
Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}
Bệnh nhân: {lblPatientName.Text}

Dịch vụ yêu cầu: {serviceName}
Bác sĩ thực hiện: {doctorName}

Ghi chú chẩn đoán sơ bộ:
{txtDiagnosis.Text}

----------------------------------------
Bác sĩ chỉ định:
(Đã ký)
========================================
";
                            System.IO.File.WriteAllText(filePath, content);
                            MessageBox.Show($"Đã xuất phiếu chỉ định thành công!\nĐường dẫn: {filePath}", "Xuất file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi xuất file: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
