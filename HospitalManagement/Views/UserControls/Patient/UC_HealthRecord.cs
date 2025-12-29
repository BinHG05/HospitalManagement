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

        public UC_HealthRecord()
        {
            InitializeComponent();
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        public void Initialize(int patientId)
        {
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
            lblAddressValue.Text = profile.Address ?? "-";
            lblBloodTypeValue.Text = profile.BloodType ?? "-";
            lblInsuranceValue.Text = profile.InsuranceNumber ?? "-";
            lblEmergencyValue.Text = !string.IsNullOrEmpty(profile.EmergencyContact) 
                ? $"{profile.EmergencyContact} - {profile.EmergencyPhone}" 
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

        #endregion
    }
}
