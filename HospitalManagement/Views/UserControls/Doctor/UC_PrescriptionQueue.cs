using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_PrescriptionQueue : UserControl
    {
        private int _doctorId;
        private Action<int> _onSelectExam;

        public UC_PrescriptionQueue()
        {
            InitializeComponent();
            SetupGrid();
        }

        public void Initialize(int doctorId, Action<int> onSelectExam)
        {
            _doctorId = doctorId;
            _onSelectExam = onSelectExam;
            LoadQueue();
        }

        private void SetupGrid()
        {
            dgvQueue.EnableHeadersVisualStyles = false;
            dgvQueue.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            dgvQueue.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
            dgvQueue.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvQueue.RowTemplate.Height = 45;
        }

        public void LoadQueue()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Show patients who have finished examination (status 'examining' or 'completed')
                    // and might need prescription management.
                    // For simplicity, let's show all examinations from today that this doctor handled.
                    var today = DateTime.Today;
                    var exams = context.Examinations
                        .Where(e => e.DoctorID == _doctorId && e.CreatedAt >= today)
                        .Select(e => new
                        {
                            e.ExaminationID,
                            PatientName = e.Patient.User.FullName,
                            PatientInfo = (e.Patient.Gender == "male" ? "Nam" : "Nữ") + ", " + 
                                         (DateTime.Today.Year - (e.Patient.DateOfBirth.HasValue ? e.Patient.DateOfBirth.Value.Year : DateTime.Today.Year)) + " tuổi",
                            Time = e.CreatedAt,
                            Diagnosis = e.PreliminaryDiagnosis,
                            HasPrescription = context.MedicalRecords.Any(mr => mr.ExaminationID == e.ExaminationID && context.Prescriptions.Any(p => p.RecordID == mr.RecordID))
                        })
                        .OrderByDescending(e => e.Time)
                        .ToList();

                    dgvQueue.Rows.Clear();
                    foreach (var e in exams)
                    {
                        dgvQueue.Rows.Add(
                            e.ExaminationID,
                            e.PatientName,
                            e.PatientInfo,
                            e.Time.Value.ToString("HH:mm"),
                            e.Diagnosis ?? "Chưa có",
                            e.HasPrescription ? "✅ Đã kê đơn" : "⏳ Chờ kê đơn",
                            "Kê đơn"
                        );
                        dgvQueue.Rows[dgvQueue.Rows.Count - 1].Tag = e.ExaminationID;
                        
                        if (!e.HasPrescription)
                        {
                            dgvQueue.Rows[dgvQueue.Rows.Count - 1].Cells[5].Style.ForeColor = Color.OrangeRed;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void dgvQueue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == colAction.Index)
            {
                int examId = (int)dgvQueue.Rows[e.RowIndex].Tag;
                _onSelectExam?.Invoke(examId);
            }
        }

        private void InitializeComponent()
        {
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiagnosis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            this.SuspendLayout();
            
            // dgvQueue
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.BackgroundColor = System.Drawing.Color.White;
            this.dgvQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQueue.ColumnHeadersHeight = 40;
            this.dgvQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId, this.colName, this.colInfo, this.colTime, this.colDiagnosis, this.colStatus, this.colAction});
            this.dgvQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueue.Location = new System.Drawing.Point(20, 70);
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(860, 410);
            this.dgvQueue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQueue_CellClick);

            // Columns setup
            this.colId.HeaderText = "ID"; this.colId.Width = 60;
            this.colName.HeaderText = "Bệnh nhân"; this.colName.Width = 200;
            this.colInfo.HeaderText = "Thông tin"; this.colInfo.Width = 120;
            this.colTime.HeaderText = "TG Khám"; this.colTime.Width = 80;
            this.colDiagnosis.HeaderText = "Chẩn đoán"; this.colDiagnosis.Width = 200;
            this.colStatus.HeaderText = "Trạng thái"; this.colStatus.Width = 120;
            this.colAction.HeaderText = "Thao tác"; this.colAction.Text = "Kê đơn"; this.colAction.UseColumnTextForButtonValue = true;

            // Header Label
            Label lblHeader = new Label {
                Text = "⚡ Danh sách bệnh nhân chờ kê đơn thuốc",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(20, 20),
                AutoSize = true
            };

            this.BackColor = Color.FromArgb(241, 245, 249);
            this.Controls.Add(this.dgvQueue);
            this.Controls.Add(lblHeader);
            this.Padding = new Padding(20);
            this.Size = new Size(900, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            this.ResumeLayout(false);
        }

        private DataGridView dgvQueue;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colInfo;
        private DataGridViewTextBoxColumn colTime;
        private DataGridViewTextBoxColumn colDiagnosis;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewButtonColumn colAction;
    }
}
