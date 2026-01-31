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
        private Timer _refreshTimer;
        private ContextMenuStrip _paymentMenu;
        private int _currentPaymentId;
        private decimal _currentAmount;

        public UC_PrescriptionQueue()
        {
            InitializeComponent();
            SetupGrid();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 10000; // 10 seconds
            _refreshTimer.Tick += (s, e) => {
                if (this.Visible)
                {
                    LoadQueue();
                }
            };
            _refreshTimer.Start();
        }

        private void InitializePaymentMenu()
        {
            _paymentMenu = new ContextMenuStrip();
            _paymentMenu.Items.Add("💵 Tiền mặt", null, (s, e) => ConfirmPayment(_currentPaymentId, "cash"));
            _paymentMenu.Items.Add("📱 Chuyển khoản (QR)", null, (s, e) => {
                using (var qrForm = new Forms.Patient.Form_QRPayment(_currentAmount.ToString("N0"), "MED_PAY_" + _currentPaymentId)) {
                    if (qrForm.ShowDialog() == DialogResult.OK) ConfirmPayment(_currentPaymentId, "bank_transfer");
                }
            });
            _paymentMenu.Items.Add("💳 Thẻ tín dụng", null, (s, e) => ConfirmPayment(_currentPaymentId, "credit_card"));
            _paymentMenu.Items.Add("👛 Ví điện tử", null, (s, e) => ConfirmPayment(_currentPaymentId, "ewallet"));
        }

        public void Initialize(int doctorId, Action<int> onSelectExam)
        {
            _doctorId = doctorId;
            _onSelectExam = onSelectExam;
            InitializePaymentMenu();
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
                    var exams = (from e in context.Examinations
                                join mr in context.MedicalRecords on e.ExaminationID equals mr.ExaminationID into records
                                from r in records.DefaultIfEmpty()
                                join p in context.Payments on r.RecordID equals p.ReferenceID into paymentGroup
                                from pay in paymentGroup.Where(pg => pg.PaymentType == "medicine").DefaultIfEmpty()
                                where e.DoctorID == _doctorId && e.CreatedAt >= today
                                select new
                                {
                                    e.ExaminationID,
                                    PatientName = e.Patient.User.FullName,
                                    PatientInfo = ((e.Patient.Gender == "Nam" || e.Patient.Gender == "male") ? "Nam" : (e.Patient.Gender == "Nữ" || e.Patient.Gender == "female") ? "Nữ" : "Khác") + ", " + 
                                                 (DateTime.Today.Year - (e.Patient.DateOfBirth.HasValue ? e.Patient.DateOfBirth.Value.Year : DateTime.Today.Year)) + " tuổi",
                                    Time = e.CreatedAt,
                                    Diagnosis = e.PreliminaryDiagnosis,
                                    HasPrescription = r != null,
                                    PaymentStatus = pay != null ? pay.PaymentStatus : null,
                                    PaymentMethod = pay != null ? pay.PaymentMethod : null,
                                    PaymentId = pay != null ? pay.PaymentID : 0,
                                    Amount = pay != null ? pay.Amount : 0
                                })
                                .OrderByDescending(e => e.Time)
                                .ToList();

                    dgvQueue.Rows.Clear();
                    foreach (var e in exams)
                    {
                        string pStatus = "N/A";
                        if (e.HasPrescription) {
                            if (e.PaymentStatus == "completed") {
                                string mText = e.PaymentMethod == "cash" ? "Tiền mặt" :
                                              e.PaymentMethod == "bank_transfer" ? "Chuyển khoản" :
                                              e.PaymentMethod == "credit_card" ? "Thẻ" :
                                              e.PaymentMethod == "ewallet" ? "Ví điện tử" : "Đã TT";
                                pStatus = "✅ " + mText;
                            } else {
                                pStatus = e.PaymentStatus == "pending" ? "💰 Chờ thanh toán" : "⏳ Chưa có HĐ";
                            }
                        }

                        dgvQueue.Rows.Add(
                            e.ExaminationID,
                            e.PatientName,
                            e.PatientInfo,
                            e.Time.Value.ToString("HH:mm"),
                            e.Diagnosis ?? "Chưa có",
                            e.HasPrescription ? "✅ Đã kê đơn" : "⏳ Chờ kê đơn",
                            pStatus,
                            "Kê đơn",
                            (e.PaymentStatus == "pending" ? "⚡ Xác nhận TT" : "")
                        );
                        dgvQueue.Rows[dgvQueue.Rows.Count - 1].Tag = new { ExamId = e.ExaminationID, PaymentId = e.PaymentId, Amount = e.Amount };
                        
                        if (!e.HasPrescription)
                        {
                            dgvQueue.Rows[dgvQueue.Rows.Count - 1].Cells[5].Style.ForeColor = Color.OrangeRed;
                        }
                        if (e.PaymentStatus == "pending") {
                            dgvQueue.Rows[dgvQueue.Rows.Count - 1].Cells[6].Style.ForeColor = Color.Orange;
                            dgvQueue.Rows[dgvQueue.Rows.Count - 1].Cells[6].Style.Font = new Font("Segoe UI Semibold", 9F);
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
            
            dynamic tag = dgvQueue.Rows[e.RowIndex].Tag;
            int examId = tag.ExamId;
            int paymentId = tag.PaymentId;

            if (e.ColumnIndex == colAction.Index)
            {
                _onSelectExam?.Invoke(examId);
            }
            else if (e.ColumnIndex == colConfirm.Index && paymentId > 0)
            {
                _currentPaymentId = paymentId;
                _currentAmount = tag.Amount;
                Rectangle rect = dgvQueue.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                _paymentMenu.Show(dgvQueue, rect.Left, rect.Bottom);
            }
        }

        private void ConfirmPayment(int paymentId, string method)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var payment = context.Payments.Find(paymentId);
                    if (payment != null)
                    {
                        payment.PaymentStatus = "completed";
                        payment.PaymentMethod = method;
                        payment.PaymentDate = DateTime.Now;

                        var invoice = context.Invoices.FirstOrDefault(i => i.PaymentID == paymentId);
                        if (invoice != null)
                        {
                            invoice.InvoiceStatus = "paid";
                        }

                        context.SaveChanges();
                        string methodVN = method == "cash" ? "Tiền mặt" : method == "bank_transfer" ? "Chuyển khoản" : method == "credit_card" ? "Thẻ" : "Ví điện tử";
                        MessageBox.Show($"Đã xác nhận thanh toán {methodVN} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadQueue();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xác nhận: " + ex.Message);
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
            this.colPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colConfirm = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            this.SuspendLayout();
            
            // dgvQueue
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.BackgroundColor = System.Drawing.Color.White;
            this.dgvQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQueue.ColumnHeadersHeight = 40;
            this.dgvQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId, this.colName, this.colInfo, this.colTime, this.colDiagnosis, this.colStatus, this.colPayment, this.colAction, this.colConfirm});
            this.dgvQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueue.Location = new System.Drawing.Point(20, 70);
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(860, 410);
            this.dgvQueue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQueue_CellClick);

            // Columns setup
            this.colId.HeaderText = "ID"; this.colId.Width = 40;
            this.colName.HeaderText = "Bệnh nhân"; this.colName.Width = 140;
            this.colInfo.HeaderText = "Thông tin"; this.colInfo.Width = 90;
            this.colTime.HeaderText = "TG Khám"; this.colTime.Width = 70;
            this.colDiagnosis.HeaderText = "Chẩn đoán"; this.colDiagnosis.Width = 140;
            this.colStatus.HeaderText = "Trạng thái đơn"; this.colStatus.Width = 100;
            this.colPayment.HeaderText = "Thanh toán"; this.colPayment.Width = 110;
            this.colAction.HeaderText = "Thao tác"; this.colAction.Text = "Kê đơn"; this.colAction.UseColumnTextForButtonValue = true; this.colAction.Width = 70;
            this.colConfirm.HeaderText = "Xác nhận TT"; this.colConfirm.Width = 100;

            // Header Label
            Label lblHeader = new Label {
                Text = "⚡ Theo dõi đơn thuốc & Thanh toán",
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
        private DataGridViewTextBoxColumn colPayment;
        private DataGridViewButtonColumn colAction;
        private DataGridViewButtonColumn colConfirm;
    }
}
