using HospitalManagement.Presenters.Patient;
using HospitalManagement.Views.Interfaces.Patient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Patient
{
    public partial class UC_HealthRecord : UserControl, IHealthRecordView
    {
        private HealthRecordPresenter _presenter;
        private int _patientId;
        private System.Windows.Forms.Button btnUpdateInfo;

        public UC_HealthRecord()
        {
            InitializeComponent();
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            dgvHistory.CellClick += DgvHistory_CellClick;
            InitializeUpdateButton();
        }

        private void InitializeUpdateButton()
        {
            btnUpdateInfo = new System.Windows.Forms.Button
            {
                Text = "⚙️ Cập nhật thông tin",
                Size = new System.Drawing.Size(180, 40),
                Location = new System.Drawing.Point(750, 20),
                BackColor = System.Drawing.Color.FromArgb(241, 245, 249),
                ForeColor = System.Drawing.Color.FromArgb(59, 130, 246),
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold)
            };
            btnUpdateInfo.FlatAppearance.BorderSize = 0;
            btnUpdateInfo.Click += BtnUpdateInfo_Click;
            
            // Add to the profile panel header area
            panelPersonalHeader.Controls.Add(btnUpdateInfo);
            btnUpdateInfo.BringToFront();
        }

        private void BtnUpdateInfo_Click(object sender, EventArgs e)
        {
            using (var form = new HospitalManagement.Views.Forms.Form_EditPatient(_patientId))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _presenter.LoadProfile();
                }
            }
        }

        public void Initialize(int patientId)
        {
            _patientId = patientId;
            _presenter = new HealthRecordPresenter(this, patientId);
            _presenter.LoadProfile();
        }

        #region IHealthRecordView Implementation

        public void LoadPatientInfo(PatientProfileInfo profile)
        {
            lblFullNameValue.Text = profile.FullName ?? "-";
            lblEmailValue.Text = profile.Email ?? "-";
            lblPhoneValue.Text = profile.Phone ?? "-";
            lblDobValue.Text = profile.DateOfBirth?.ToString("dd/MM/yyyy") ?? "-";
            lblGenderValue.Text = profile.GenderDisplay ?? "-";
            lblAgeValue.Text = profile.Age.HasValue ? $"{profile.Age} Tuổi" : "-";
            lblAddressValue.Text = profile.Address ?? "-";
            lblBloodTypeValue.Text = profile.BloodType ?? "-";
            lblInsuranceValue.Text = profile.InsuranceNumber ?? "-";
            lblPatientIdValue.Text = $"ID: BN{profile.PatientId:D4}";
            lblJoinDateValue.Text = $"Thành viên từ: {profile.CreatedAt?.ToString("dd/MM/yyyy") ?? "N/A"}";
            lblEmergencyValue.Text = !string.IsNullOrEmpty(profile.EmergencyContact) 
                ? $"{profile.EmergencyContact} ({profile.EmergencyPhone})" 
                : "-";
        }

        public void LoadMedicalHistory(IEnumerable<MedicalHistoryDisplayInfo> history)
        {
            dgvHistory.Rows.Clear();

            foreach (var item in history)
            {
                var rowIndex = dgvHistory.Rows.Add();
                var row = dgvHistory.Rows[rowIndex];

                row.Cells["colVisitDate"].Value = item.VisitDate.ToString("dd/MM/yyyy");
                row.Cells["colDoctor"].Value = item.DoctorName;
                row.Cells["colDepartment"].Value = item.DepartmentName;
                row.Cells["colDiagnosis"].Value = item.Diagnosis ?? "-";
                row.Cells["colTreatment"].Value = item.Treatment ?? "-";
                row.Tag = item.RecordId;
            }

            if (dgvHistory.Rows.Count == 0)
            {
                dgvHistory.Rows.Add();
                dgvHistory.Rows[0].Cells["colVisitDate"].Value = "Chưa có lịch sử khám bệnh";
            }
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

        #endregion

        #region Event Handlers

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabHistory && _presenter != null)
            {
                _presenter.LoadMedicalHistory();
            }
        }

        private void DgvHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHistory.Columns[e.ColumnIndex].Name == "colTreatment")
            {
                var val = dgvHistory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (!string.IsNullOrEmpty(val) && val != "-")
                {
                    MessageBox.Show(val, "Chi tiết phác đồ & Hướng điều trị", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion
    }
}
