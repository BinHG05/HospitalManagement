using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;

namespace HospitalManagement.Views.UserControls.Patient
{
    public partial class UC_HospitalStatusBoard : UserControl
    {
        private readonly DoctorService _doctorService;
        private Timer _refreshTimer;

        public UC_HospitalStatusBoard()
        {
            InitializeComponent();
            _doctorService = new DoctorService();
            SetupDataGridView();
            InitializeTimer();
            LoadData();
        }

        private void SetupDataGridView()
        {
            dgvStatus.AutoGenerateColumns = false;
            dgvStatus.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42); // Navy Dark
            dgvStatus.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStatus.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvStatus.ColumnHeadersHeight = 45;
            dgvStatus.EnableHeadersVisualStyles = false;
            dgvStatus.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvStatus.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 196, 15); // Highlight with Gold
            dgvStatus.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvStatus.RowTemplate.Height = 50;
            dgvStatus.BorderStyle = BorderStyle.None;
            dgvStatus.BackgroundColor = Color.White;
            dgvStatus.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvStatus.RowHeadersVisible = false;
        }

        private void InitializeTimer()
        {
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 10000; // Refresh every 10 seconds
            _refreshTimer.Tick += (s, e) => LoadData();
            _refreshTimer.Start();
        }

        public void LoadData()
        {
            try
            {
                var activeExams = _doctorService.GetAllActiveExaminations();
                
                dgvStatus.Rows.Clear();
                foreach (var exam in activeExams)
                {
                    dgvStatus.Rows.Add(
                        exam.QueueNumber,
                        exam.PatientName,
                        exam.DepartmentName,
                        exam.DoctorName,
                        GetStatusDisplay(exam.Status)
                    );
                }

                lblLastUpdated.Text = $"Cập nhật lúc: {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Status Board Error: {ex.Message}");
            }
        }

        private string GetStatusDisplay(string status)
        {
            switch (status)
            {
                case "examining": return "⚡ Đang khám";
                case "service_pending": return "🧪 Đang làm xét nghiệm";
                case "service_completed": return "✅ Đã có kết quả";
                default: return status;
            }
        }
    }
}
