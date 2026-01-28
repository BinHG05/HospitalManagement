using HospitalManagement.Models.EF;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_PatientRecords : UserControl
    {
        private int _doctorId;

        public UC_PatientRecords()
        {
            InitializeComponent();
            SetupDataGridViews();
        }

        public void Initialize(int doctorId)
        {
            _doctorId = doctorId;
            LoadAllPatients();
        }

        private void SetupDataGridViews()
        {
            // Style for main patient grid
            dgvPatients.EnableHeadersVisualStyles = false;
            dgvPatients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(59, 130, 246);
            dgvPatients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPatients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvPatients.ColumnHeadersHeight = 40;
            dgvPatients.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPatients.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvPatients.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dgvPatients.RowTemplate.Height = 40;
            dgvPatients.GridColor = Color.FromArgb(226, 232, 240);
            dgvPatients.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            // Style for history grid
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dgvHistory.ColumnHeadersHeight = 35;
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvHistory.RowTemplate.Height = 32;
            dgvHistory.GridColor = Color.FromArgb(226, 232, 240);
        }

        private void LoadAllPatients()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var patients = context.Patients
                        .Select(p => new
                        {
                            p.PatientID,
                            p.User.FullName,
                            p.User.Phone,
                            p.Gender,
                            p.DateOfBirth,
                            p.BloodType
                        })
                        .OrderBy(p => p.FullName)
                        .ToList();

                    dgvPatients.Rows.Clear();
                    foreach (var p in patients)
                    {
                        var gender = p.Gender == "male" ? "Nam" : p.Gender == "female" ? "Nữ" : "Khác";
                        var dob = p.DateOfBirth.HasValue ? p.DateOfBirth.Value.ToString("dd/MM/yyyy") : "N/A";
                        
                        dgvPatients.Rows.Add(
                            p.PatientID,
                            p.FullName,
                            p.Phone,
                            gender,
                            dob,
                            p.BloodType ?? "N/A"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách bệnh nhân: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchPatients(string keyword)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var query = context.Patients.AsQueryable();

                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        keyword = keyword.ToLower();
                        query = query.Where(p =>
                            p.User.FullName.ToLower().Contains(keyword) ||
                            p.User.Phone.Contains(keyword) ||
                            p.PatientID.ToString().Contains(keyword));
                    }

                    var patients = query
                        .Select(p => new
                        {
                            p.PatientID,
                            p.User.FullName,
                            p.User.Phone,
                            p.Gender,
                            p.DateOfBirth,
                            p.BloodType
                        })
                        .OrderBy(p => p.FullName)
                        .ToList();

                    dgvPatients.Rows.Clear();
                    foreach (var p in patients)
                    {
                        var gender = p.Gender == "male" ? "Nam" : p.Gender == "female" ? "Nữ" : "Khác";
                        var dob = p.DateOfBirth.HasValue ? p.DateOfBirth.Value.ToString("dd/MM/yyyy") : "N/A";

                        dgvPatients.Rows.Add(
                            p.PatientID,
                            p.FullName,
                            p.Phone,
                            gender,
                            dob,
                            p.BloodType ?? "N/A"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowPatientDetails(int patientId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var patient = context.Patients
                        .Where(p => p.PatientID == patientId)
                        .Select(p => new
                        {
                            p.PatientID,
                            p.User.FullName,
                            p.User.Phone,
                            p.Gender,
                            p.DateOfBirth,
                            p.Address,
                            p.BloodType,
                            p.InsuranceNumber
                        })
                        .FirstOrDefault();

                    if (patient == null) return;

                    var gender = patient.Gender == "male" ? "Nam" : patient.Gender == "female" ? "Nữ" : "Khác";
                    int? age = null;
                    if (patient.DateOfBirth.HasValue)
                    {
                        age = DateTime.Today.Year - patient.DateOfBirth.Value.Year;
                        if (patient.DateOfBirth.Value.Date > DateTime.Today.AddYears(-age.Value))
                            age--;
                    }

                    lblPatientName.Text = patient.FullName;
                    lblPatientInfo.Text = $"{gender}, {(age.HasValue ? age + " tuổi" : "N/A")}\n" +
                                          $"SĐT: {patient.Phone}\n" +
                                          $"Địa chỉ: {patient.Address ?? "Chưa có"}\n" +
                                          $"BHYT: {patient.InsuranceNumber ?? "Không có"}";

                    // Load medical history
                    var history = context.MedicalRecords
                        .Where(r => r.PatientID == patientId)
                        .OrderByDescending(r => r.RecordDate)
                        .Take(10)
                        .Select(r => new
                        {
                            Date = r.RecordDate,
                            Doctor = r.Examination.Doctor.User.FullName,
                            r.Diagnosis
                        })
                        .ToList();

                    dgvHistory.Rows.Clear();
                    foreach (var h in history)
                    {
                        dgvHistory.Rows.Add(
                            h.Date.HasValue ? h.Date.Value.ToString("dd/MM/yy") : "N/A",
                            h.Doctor ?? "N/A",
                            h.Diagnosis ?? "N/A"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Event Handlers

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPatients(txtSearch.Text.Trim());
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchPatients(txtSearch.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var patientId = (int)dgvPatients.Rows[e.RowIndex].Cells["colPatientId"].Value;
            ShowPatientDetails(patientId);
        }

        #endregion
    }
}
