using HospitalManagement.Models.EF;
using HospitalManagement.Views.Interfaces.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement.Presenters.Doctor
{
    public class PrescriptionPresenter
    {
        private readonly IPrescriptionView _view;
        private readonly int _examinationId;
        private int _recordId;
        private int _patientId;
        private int _doctorId;

        public PrescriptionPresenter(IPrescriptionView view, int examinationId)
        {
            _view = view;
            _examinationId = examinationId;
            _view.ExaminationId = examinationId;

            _view.SaveRequested += OnSaveRequested;
            _view.PrintRequested += OnPrintRequested;
            _view.CancelRequested += OnCancelRequested;
        }

        public void Initialize()
        {
            LoadPatientInfo();
            LoadMedicines();
            LoadExistingPrescription();
        }

        private void LoadPatientInfo()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var exam = context.Examinations
                        .Where(e => e.ExaminationID == _examinationId)
                        .Select(e => new
                        {
                            e.PatientID,
                            e.DoctorID,
                            PatientName = e.Patient.User.FullName,
                            e.Patient.DateOfBirth,
                            e.Patient.Gender,
                            e.Patient.Address,
                            e.Patient.InsuranceNumber,
                            e.Patient.User.Phone,
                            e.PreliminaryDiagnosis
                        })
                        .FirstOrDefault();

                    if (exam != null)
                    {
                        _patientId = exam.PatientID ?? 0;
                        _doctorId = exam.DoctorID ?? 0;

                        int? age = null;
                        if (exam.DateOfBirth.HasValue)
                        {
                            age = DateTime.Today.Year - exam.DateOfBirth.Value.Year;
                            if (exam.DateOfBirth.Value.Date > DateTime.Today.AddYears(-age.Value))
                                age--;
                        }

                        _view.LoadPatientInfo(new PrescriptionPatientDto
                        {
                            PatientId = _patientId,
                            FullName = exam.PatientName,
                            Age = age,
                            Gender = (exam.Gender == "Nam" || exam.Gender == "male") ? "Nam" : 
                                     (exam.Gender == "Nữ" || exam.Gender == "female") ? "Nữ" : "Khác",
                            Diagnosis = exam.PreliminaryDiagnosis,
                            Address = exam.Address,
                            Phone = exam.Phone,
                            InsuranceNumber = exam.InsuranceNumber
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải thông tin bệnh nhân: {ex.Message}");
            }
        }

        private void LoadMedicines()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var medicines = context.Medicines
                        .Where(m => m.IsActive == true && m.StockQuantity > 0)
                        .Select(m => new MedicineDto
                        {
                            MedicineId = m.MedicineID,
                            MedicineName = m.MedicineName,
                            GenericName = m.GenericName,
                            Unit = m.Unit,
                            PricePerUnit = m.PricePerUnit ?? 0,
                            StockQuantity = m.StockQuantity ?? 0
                        })
                        .OrderBy(m => m.MedicineName)
                        .ToList();

                    _view.LoadMedicineList(medicines);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải danh sách thuốc: {ex.Message}");
            }
        }

        private void LoadExistingPrescription()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Find existing MedicalRecord for this examination
                    var record = context.MedicalRecords
                        .FirstOrDefault(r => r.ExaminationID == _examinationId);

                    if (record != null)
                    {
                        _recordId = record.RecordID;
                        _view.RecordId = _recordId;

                    // Load existing prescriptions
                        var prescriptions = context.Prescriptions
                            .Where(p => p.RecordID == _recordId)
                            .Select(p => new PrescriptionItemDto
                            {
                                MedicineId = p.MedicineID ?? 0,
                                MedicineName = p.Medicine.MedicineName,
                                Unit = p.Medicine.Unit,
                                Dosage = p.Dosage,
                                Frequency = p.Frequency,
                                Duration = p.Duration ?? 0,
                                Quantity = p.Quantity,
                                Instructions = p.Instructions,
                                PricePerUnit = p.Medicine.PricePerUnit ?? 0
                            })
                            .ToList();

                        foreach (var item in prescriptions)
                        {
                            _view.AddPrescriptionItem(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải đơn thuốc: {ex.Message}");
            }
        }

        private void OnSaveRequested(object sender, EventArgs e)
        {
            try
            {
                _view.ShowLoading(true);

                var items = _view.GetPrescriptionItems().ToList();
                if (!items.Any())
                {
                    _view.ShowError("Vui lòng thêm ít nhất một loại thuốc vào đơn.");
                    return;
                }

                using (var context = new HospitalDbContext())
                {
                    // Create or get MedicalRecord
                    var record = context.MedicalRecords
                        .FirstOrDefault(r => r.ExaminationID == _examinationId);

                    if (record == null)
                    {
                        var exam = context.Examinations.Find(_examinationId);
                        record = new Models.Entities.MedicalRecords
                        {
                            PatientID = _patientId,
                            ExaminationID = _examinationId,
                            Diagnosis = exam?.PreliminaryDiagnosis ?? "Chưa có chẩn đoán",
                            RecordDate = DateTime.Now,
                            CreatedBy = _doctorId,
                            CreatedAt = DateTime.Now
                        };
                        context.MedicalRecords.Add(record);
                        context.SaveChanges();
                        _recordId = record.RecordID;
                    }

                    // Remove old prescriptions
                    var oldPrescriptions = context.Prescriptions
                        .Where(p => p.RecordID == _recordId)
                        .ToList();
                    context.Prescriptions.RemoveRange(oldPrescriptions);

                    decimal totalAmount = 0;
                    // Add new prescriptions
                    foreach (var item in items)
                    {
                        var prescription = new Models.Entities.Prescriptions
                        {
                            RecordID = _recordId,
                            MedicineID = item.MedicineId,
                            Dosage = item.Dosage,
                            Frequency = item.Frequency,
                            Duration = item.Duration,
                            Quantity = item.Quantity,
                            Instructions = item.Instructions,
                            CreatedAt = DateTime.Now
                        };
                        context.Prescriptions.Add(prescription);
                        totalAmount += (item.PricePerUnit * item.Quantity);
                    }

                    // --- CẬP NHẬT LỊCH SỬ KHÁM BỆNH ---
                    // Prepare prescription text summary
                    string prescriptionSummary = string.Join("\r\n", items.Select(i => $"- {i.MedicineName}: {i.Quantity} {i.Unit} ({i.Dosage}), {i.Frequency}"));
                    string header = "💊 ĐƠN THUỐC ĐÃ KÊ:";
                    
                    // Update TreatmentPlan in MedicalRecords
                    if (!string.IsNullOrEmpty(record.TreatmentPlan) && record.TreatmentPlan.Contains(header))
                    {
                        // Replace old prescription part if editing
                        int idx = record.TreatmentPlan.IndexOf(header);
                        record.TreatmentPlan = record.TreatmentPlan.Substring(0, idx).TrimEnd() + "\r\n\r\n" + header + "\r\n" + prescriptionSummary;
                    }
                    else
                    {
                        record.TreatmentPlan = (record.TreatmentPlan ?? "").TrimEnd() + "\r\n\r\n" + header + "\r\n" + prescriptionSummary;
                    }

                    // Update Treatment in MedicalHistory
                    var history = context.MedicalHistory.FirstOrDefault(h => h.RecordID == _recordId);
                    if (history != null)
                    {
                        if (!string.IsNullOrEmpty(history.Treatment) && history.Treatment.Contains(header))
                        {
                            int idx = history.Treatment.IndexOf(header);
                            history.Treatment = history.Treatment.Substring(0, idx).TrimEnd() + "\r\n\r\n" + header + "\r\n" + prescriptionSummary;
                        }
                        else
                        {
                            history.Treatment = (history.Treatment ?? "").TrimEnd() + "\r\n\r\n" + header + "\r\n" + prescriptionSummary;
                        }
                    }
                    // ---------------------------------

                    // Tự động tạo hóa đơn tiền thuốc cho bệnh nhân
                    var patient = context.Patients.Find(_patientId);
                    decimal discount = 0;
                    if (patient != null && !string.IsNullOrWhiteSpace(patient.InsuranceNumber))
                    {
                        discount = totalAmount * 0.5m; // Giảm 50% nếu có BHYT
                    }
                    var finalAmount = totalAmount - discount;

                    var existingPayment = context.Payments
                        .FirstOrDefault(p => p.ReferenceID == _recordId && p.PaymentType == "medicine");

                    if (existingPayment == null)
                    {
                        var payment = new Models.Entities.Payments
                        {
                            PatientID = _patientId,
                            ReferenceID = _recordId,
                            PaymentType = "medicine",
                            Amount = finalAmount,
                            PaymentStatus = "pending",
                            CreatedAt = DateTime.Now
                        };
                        context.Payments.Add(payment);
                        context.SaveChanges();

                        var invoice = new Models.Entities.Invoices
                        {
                            PaymentID = payment.PaymentID,
                            InvoiceNumber = "MED" + DateTime.Now.ToString("yyyyMMdd") + _recordId.ToString().PadLeft(4, '0'),
                            InvoiceDate = DateTime.Now,
                            TotalAmount = totalAmount,
                            DiscountAmount = discount,
                            FinalAmount = finalAmount,
                            InvoiceStatus = "unpaid",
                            CreatedAt = DateTime.Now
                        };
                        context.Invoices.Add(invoice);
                    }
                    else
                    {
                        existingPayment.Amount = finalAmount;
                        var invoice = context.Invoices.FirstOrDefault(i => i.PaymentID == existingPayment.PaymentID);
                        if (invoice != null)
                        {
                            invoice.TotalAmount = totalAmount;
                            invoice.DiscountAmount = discount;
                            invoice.FinalAmount = finalAmount;
                        }
                    }

                    context.SaveChanges();
                    _view.ShowSuccess($"Đã lưu đơn thuốc và cập nhật vào lịch sử khám bệnh. Đã tạo hóa đơn thanh toán ({totalAmount:N0} đ)");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi lưu đơn thuốc: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        private void OnPrintRequested(object sender, EventArgs e)
        {
            try
            {
                var items = _view.GetPrescriptionItems().ToList();
                if (!items.Any())
                {
                    _view.ShowError("Vui lòng thêm thuốc trước khi in.");
                    return;
                }

                // First save to ensure data is consistent
                OnSaveRequested(sender, e);

                // Fetch patient info again to ensure it's fresh
                using (var context = new HospitalDbContext())
                {
                    var exam = context.Examinations
                        .Where(ex => ex.ExaminationID == _examinationId)
                        .Select(ex => new PrescriptionPatientDto
                        {
                            FullName = ex.Patient.User.FullName,
                            Diagnosis = ex.PreliminaryDiagnosis,
                            Gender = (ex.Patient.Gender == "Nam" || ex.Patient.Gender == "male") ? "Nam" : "Nữ",
                            Age = ex.Patient.DateOfBirth.HasValue ? DateTime.Today.Year - ex.Patient.DateOfBirth.Value.Year : (int?)null,
                            Address = ex.Patient.Address,
                            Phone = ex.Patient.User.Phone,
                            InsuranceNumber = ex.Patient.InsuranceNumber
                        })
                        .FirstOrDefault();

                    if (exam != null)
                    {
                        using (var printForm = new HospitalManagement.Views.Forms.Doctor.Form_PrescriptionPrint(exam, items))
                        {
                            if (printForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                _view.ShowSuccess("Đơn thuốc đã được xuất thành công. Bệnh nhân cần thanh toán hóa đơn thuốc tại quầy trước khi nhận thuốc.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi in đơn thuốc: {ex.Message}");
            }
        }

        private void OnCancelRequested(object sender, EventArgs e)
        {
            _view.ClearPrescriptionItems();
        }
    }
}
