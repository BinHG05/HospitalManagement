using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Patient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        public IEnumerable<Payments> GetPatientPayments(int patientId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Payments
                    .Include(p => p.Appointment)
                    .Include(p => p.Appointment.Doctor)
                    .Include(p => p.Appointment.Department)
                    .Where(p => p.PatientID == patientId)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToList();
            }
        }

        public IEnumerable<InvoiceDisplayInfo> GetPatientInvoices(int patientId)
        {
            using (var context = new HospitalDbContext())
            {
                var invoices = context.Invoices
                    .Include(i => i.Payment)
                    .Include(i => i.Payment.Appointment)
                    .Include(i => i.Payment.Appointment.Doctor)
                    .Include(i => i.Payment.Appointment.Doctor.User)
                    .Include(i => i.Payment.Appointment.Department)
                    .Where(i => i.Payment.PatientID == patientId)
                    .OrderByDescending(i => i.InvoiceDate)
                    .ToList();

                return invoices.Select(i => {
                    var info = new InvoiceDisplayInfo
                    {
                        InvoiceId = i.InvoiceID,
                        InvoiceNumber = i.InvoiceNumber,
                        InvoiceDate = i.InvoiceDate,
                        TotalAmount = i.TotalAmount,
                        DiscountAmount = i.DiscountAmount ?? 0,
                        FinalAmount = i.FinalAmount,
                        InvoiceStatus = i.InvoiceStatus,
                        PaymentType = i.Payment?.PaymentType,
                        PaymentMethod = i.Payment?.PaymentMethod,
                        AppointmentDate = i.Payment?.Appointment?.AppointmentDate,
                        DepartmentName = i.Payment?.Appointment?.Department?.DepartmentName,
                        DoctorName = i.Payment?.Appointment?.Doctor?.User?.FullName
                    };

                    if (info.PaymentType == "appointment")
                    {
                        info.Description = "Khám " + (info.DepartmentName ?? "Tổng quát");
                    }
                    else if (info.PaymentType == "medicine")
                    {
                        info.Description = "Mua thuốc theo đơn";
                        
                        // Trace back to get Doctor, Department, Diagnosis and Prescription Items
                        if (i.Payment?.ReferenceID != null)
                        {
                            int recordId = i.Payment.ReferenceID.Value;
                            var detail = context.MedicalRecords
                                .Include(mr => mr.Prescriptions)
                                .ThenInclude(p => p.Medicine)
                                .Where(mr => mr.RecordID == recordId)
                                .Select(mr => new { 
                                    DocName = mr.Examination.Doctor.User.FullName,
                                    DeptName = mr.Examination.Doctor.Department.DepartmentName,
                                    Diagnosis = mr.Diagnosis,
                                    PrescriptionItems = mr.Prescriptions.Select(p => new PrescriptionItemDto {
                                        MedicineName = p.Medicine.MedicineName,
                                        Dosage = p.Dosage,
                                        Quantity = p.Quantity,
                                        Instructions = p.Instructions,
                                        Price = p.Medicine.PricePerUnit ?? 0
                                    }).ToList()
                                })
                                .FirstOrDefault();
                            
                            if (detail != null)
                            {
                                info.DoctorName = detail.DocName;
                                info.DepartmentName = detail.DeptName;
                                info.Diagnosis = detail.Diagnosis;
                                info.Items = detail.PrescriptionItems;
                            }
                        }
                    }
                    else if (info.PaymentType == "service")
                    {
                        info.Description = "Dịch vụ cận lâm sàng";
                    }

                    return info;
                }).ToList();
            }
        }

        public IEnumerable<InvoiceDisplayInfo> GetPendingInvoices(int patientId)
        {
            return GetPatientInvoices(patientId).Where(i => i.InvoiceStatus == "unpaid");
        }

        public bool PayInvoice(int invoiceId, string paymentMethod)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var invoice = context.Invoices
                        .Include(i => i.Payment)
                        .Include(i => i.Payment.Appointment)
                        .FirstOrDefault(i => i.InvoiceID == invoiceId);

                    if (invoice == null) return false;

                    invoice.InvoiceStatus = "paid";
                    
                    if (invoice.Payment != null)
                    {
                        invoice.Payment.PaymentStatus = "completed";
                        invoice.Payment.PaymentMethod = paymentMethod;
                        invoice.Payment.PaymentDate = DateTime.Now;

                        // Tự động xác nhận lịch khám nếu đây là hóa đơn tiền khám
                        if (invoice.Payment.PaymentType == "appointment" && invoice.Payment.Appointment != null)
                        {
                            invoice.Payment.Appointment.Status = "confirmed";
                            invoice.Payment.Appointment.UpdatedAt = DateTime.Now;
                        }
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
