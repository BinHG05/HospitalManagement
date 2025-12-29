using HospitalManagement.Models.Entities;
using HospitalManagement.Views.Interfaces.Patient;
using System.Collections.Generic;

namespace HospitalManagement.Services.Interfaces
{
    public interface IPaymentService
    {
        // Get all payments for patient
        IEnumerable<Payments> GetPatientPayments(int patientId);
        
        // Get all invoices for patient
        IEnumerable<InvoiceDisplayInfo> GetPatientInvoices(int patientId);
        
        // Get pending invoices
        IEnumerable<InvoiceDisplayInfo> GetPendingInvoices(int patientId);
        
        // Mark invoice as paid
        bool PayInvoice(int invoiceId, string paymentMethod);
    }
}
