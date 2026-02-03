using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Pharmacist
{
    public partial class UC_PharmacySales : UserControl
    {
        private List<Payments> _paidPayments;
        private Payments _selectedPayment;

        public UC_PharmacySales()
        {
            InitializeComponent();
            LoadPaidPrescriptions();
        }

        private void LoadPaidPrescriptions()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Fetch medicine payments that are paid (completed) but not yet dispensed
                    var payments = context.Payments
                        .Include(p => p.Patient)
                        .Include(p => p.Patient.User)
                        .Where(p => p.PaymentType == "medicine" && p.PaymentStatus == "completed")
                        .OrderBy(p => p.CreatedAt)
                        .ToList();

                    dgvPrescriptions.Rows.Clear();
                    foreach (var p in payments)
                    {
                        int rowIndex = dgvPrescriptions.Rows.Add(
                            p.Patient?.User?.FullName ?? "N/A",
                            "INV-" + p.PaymentID.ToString("D6"),
                            p.CreatedAt?.ToString("dd/MM/yyyy HH:mm") ?? "-",
                            p.Amount.ToString("N0") + " đ",
                            "Đã thanh toán",
                            "Xem chi tiết"
                        );
                        dgvPrescriptions.Rows[rowIndex].Tag = p;
                    }

                    if (payments.Count == 0)
                    {
                        lblPrescriptionDetails.Text = "Không có đơn thuốc nào đang chờ cấp phát.";
                        btnDispense.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message);
            }
        }

        private void dgvPrescriptions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var payment = dgvPrescriptions.Rows[e.RowIndex].Tag as Payments;
            if (payment == null) return;

            _selectedPayment = payment;
            ShowPrescriptionDetails(payment);
        }

        private void ShowPrescriptionDetails(Payments payment)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // ReferenceID for medicine payments is the RecordID
                    if (!payment.ReferenceID.HasValue) return;

                    int recordId = payment.ReferenceID.Value;
                    var record = context.MedicalRecords
                        .Include(mr => mr.Examination)
                        .Include(mr => mr.Examination.Doctor)
                        .Include(mr => mr.Examination.Doctor.User)
                        .Include(mr => mr.Prescriptions)
                        .ThenInclude(p => p.Medicine)
                        .FirstOrDefault(mr => mr.RecordID == recordId);

                    if (record == null)
                    {
                        lblPrescriptionDetails.Text = "Không tìm thấy thông tin đơn thuốc.";
                        return;
                    }

                    string details = $"📋 CHI TIẾT ĐƠN THUỐC\n";
                    details += $"------------------------------------------\n";
                    details += $"Bệnh nhân: {payment.Patient?.User?.FullName}\n";
                    details += $"Bác sĩ chỉ định: {record.Examination?.Doctor?.User?.FullName}\n";
                    details += $"Chẩn đoán: {record.Diagnosis}\n\n";
                    details += $"DANH SÁCH THUỐC:\n";

                    foreach (var item in record.Prescriptions)
                    {
                        details += $"• {item.Medicine?.MedicineName} - SL: {item.Quantity} {item.Medicine?.Unit}\n";
                        details += $"  HD: {item.Dosage}, {item.Frequency}\n";
                        details += $"  {item.Instructions}\n\n";
                    }

                    lblPrescriptionDetails.Text = details;
                    btnDispense.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblPrescriptionDetails.Text = "Lỗi khi tải chi tiết: " + ex.Message;
            }
        }

        private void btnDispense_Click(object sender, EventArgs e)
        {
            if (_selectedPayment == null) return;

            var result = MessageBox.Show(
                $"Xác nhận đã cấp đủ thuốc cho bệnh nhân {_selectedPayment.Patient?.User?.FullName}?",
                "Xác nhận cấp phát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var context = new HospitalDbContext())
                    {
                        var payment = context.Payments.Find(_selectedPayment.PaymentID);
                        if (payment != null)
                        {
                            payment.PaymentStatus = "dispensed";
                            
                            // Deduct stock quantity
                            if (payment.ReferenceID.HasValue)
                            {
                                int recordId = payment.ReferenceID.Value;
                                var prescriptions = context.Prescriptions
                                    .Include(p => p.Medicine)
                                    .Where(p => p.RecordID == recordId)
                                    .ToList();

                                foreach (var p in prescriptions)
                                {
                                    if (p.Medicine != null)
                                    {
                                        p.Medicine.StockQuantity -= p.Quantity;
                                    }
                                }
                            }

                            context.SaveChanges();
                            MessageBox.Show("Cấp phát thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            lblPrescriptionDetails.Text = "Chọn một đơn thuốc từ danh sách để xem chi tiết...";
                            btnDispense.Enabled = false;
                            _selectedPayment = null;
                            LoadPaidPrescriptions();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPaidPrescriptions();
        }
    }
}
