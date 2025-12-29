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

                return invoices.Select(i => new InvoiceDisplayInfo
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
                        .FirstOrDefault(i => i.InvoiceID == invoiceId);

                    if (invoice == null) return false;

                    invoice.InvoiceStatus = "paid";
                    
                    if (invoice.Payment != null)
                    {
                        invoice.Payment.PaymentStatus = "completed";
                        invoice.Payment.PaymentMethod = paymentMethod;
                        invoice.Payment.PaymentDate = DateTime.Now;
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
