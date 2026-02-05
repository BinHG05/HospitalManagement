using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_ActiveExaminations : UserControl
    {
        private readonly DoctorService _doctorService;
        private int _doctorId;
        private Action<int> _onContinueExam;

        public UC_ActiveExaminations()
        {
            InitializeComponent();
            _doctorService = new DoctorService();
            SetupDataGridView();
        }

        public void Initialize(int doctorId, Action<int> onContinueExam)
        {
            _doctorId = doctorId;
            _onContinueExam = onContinueExam;
            LoadData();
        }

        private void SetupDataGridView()
        {
            dgvExaminations.AutoGenerateColumns = false;
            dgvExaminations.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvExaminations.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            dgvExaminations.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvExaminations.ColumnHeadersHeight = 40;
            dgvExaminations.EnableHeadersVisualStyles = false;
            dgvExaminations.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvExaminations.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240);
            dgvExaminations.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvExaminations.RowTemplate.Height = 45;
        }

        private void LoadData()
        {
            try
            {
                panelLoading.Visible = true;
                panelLoading.BringToFront();

                // Only show active exams for THIS doctor (not all doctors)
                var activeExams = _doctorService.GetActiveExaminations(_doctorId);

                dgvExaminations.Rows.Clear();
                int index = 1;

                foreach (ActiveExamInfo exam in activeExams)
                {
                    int rowIndex = dgvExaminations.Rows.Add(
                        index++,
                        exam.PatientName,
                        exam.Age,
                        (exam.Gender == "Nam" || exam.Gender == "male") ? "Nam" : (exam.Gender == "Nữ" || exam.Gender == "female") ? "Nữ" : exam.Gender,
                        exam.DoctorName,
                        GetStatusDisplay(exam.Status),
                        GetServiceStatusDisplay(exam.ServiceStatus),
                        "Tiếp tục"
                    );
                    
                    // Store appointment ID in tag
                    dgvExaminations.Rows[rowIndex].Tag = exam.AppointmentId;
                    
                    // Color code by status
                    Color rowColor = GetStatusColor(exam.Status);
                    dgvExaminations.Rows[rowIndex].DefaultCellStyle.BackColor = rowColor;
                }

                if (dgvExaminations.Rows.Count == 0)
                {
                    // Maybe show empty state
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                panelLoading.Visible = false;
            }
        }
        
        private string GetStatusDisplay(string status)
        {
            switch (status)
            {
                case "examining": return "Đang khám";
                case "service_pending": return "Chờ dịch vụ";
                case "service_completed": return "Đã có kết quả";
                default: return status;
            }
        }

        private string GetServiceStatusDisplay(string status)
        {
             if (string.IsNullOrEmpty(status)) return "-";
             return status;
        }

        private Color GetStatusColor(string status)
        {
            switch (status)
            {
                case "examining":
                    return Color.FromArgb(254, 249, 195); // Yellow light - đang khám
                case "service_pending":
                    return Color.FromArgb(255, 237, 213); // Orange light - chờ dịch vụ
                case "service_completed":
                    return Color.FromArgb(220, 252, 231); // Green light - đã có kết quả
                default:
                    return Color.White;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DgvExaminations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colAction.Index)
            {
                if (dgvExaminations.Rows[e.RowIndex].Tag is int appointmentId)
                {
                    _onContinueExam?.Invoke(appointmentId);
                }
            }
        }
    }
}
