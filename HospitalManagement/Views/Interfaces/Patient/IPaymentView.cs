using HospitalManagement.Models.Entities;
using System;
using System.Collections.Generic;

namespace HospitalManagement.Views.Interfaces.Patient
{
    public interface IPaymentView
    {
        // Load invoices
        void LoadInvoices(IEnumerable<InvoiceDisplayInfo> invoices);
        
        string SelectedStatusFilter { get; }
        string SelectedTypeFilter { get; }
        
        // UI State
        void ShowLoading(bool isLoading);
        void ShowError(string message);
        void ShowSuccess(string message);
        void ShowInvoiceDetails(InvoiceDisplayInfo invoice);
    }

    public class InvoiceDisplayInfo
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string InvoiceStatus { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMethod { get; set; }
        
        // Related info
        public DateTime? AppointmentDate { get; set; }
        public string DepartmentName { get; set; }
        public string DoctorName { get; set; }
        public string Description { get; set; }
        public string Diagnosis { get; set; }
        public List<PrescriptionItemDto> Items { get; set; } = new List<PrescriptionItemDto>();

        public string StatusDisplay
        {
            get
            {
                switch (InvoiceStatus)
                {
                    case "unpaid": return "Chưa thanh toán";
                    case "paid": return "Đã thanh toán";
                    case "cancelled": return "Đã hủy";
                    default: return InvoiceStatus;
                }
            }
        }

        public string PaymentTypeDisplay
        {
            get
            {
                switch (PaymentType)
                {
                    case "appointment": return "Tiền khám";
                    case "service": return "Dịch vụ";
                    case "medicine": return "Thuốc";
                    default: return PaymentType;
                }
            }
        }

        public bool CanPay => InvoiceStatus == "unpaid";

        public DateTime? PaymentDeadline
        {
            get
            {
                if (PaymentType == "appointment" && AppointmentDate.HasValue && InvoiceStatus == "unpaid")
                {
                    // Hạn là 19h30 ngày trước ngày khám (hoặc ngày hiện tại nếu đăng ký trong ngày?)
                    // Theo logic UC_AppointmentBooking: Ngày trước ngày khám 19:30
                    return AppointmentDate.Value.AddDays(-1).Date.AddHours(19).AddMinutes(30);
                }
                return null;
            }
        }
    }

    public class PrescriptionItemDto
    {
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Price * Quantity;
    }
}
