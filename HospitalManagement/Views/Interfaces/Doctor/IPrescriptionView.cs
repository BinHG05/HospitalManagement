using System;
using System.Collections.Generic;

namespace HospitalManagement.Views.Interfaces.Doctor
{
    // DTO for medicine in prescription
    public class PrescriptionItemDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Unit { get; set; }
        public string Dosage { get; set; }        // Liều dùng: "1 viên"
        public string Frequency { get; set; }     // Tần suất: "3 lần/ngày"
        public int Duration { get; set; }         // Số ngày uống
        public int Quantity { get; set; }         // Tổng số lượng
        public string Instructions { get; set; }  // Hướng dẫn: "Uống sau ăn"
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice => Quantity * PricePerUnit;
    }

    // DTO for medicine selection
    public class MedicineDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string GenericName { get; set; }
        public string Unit { get; set; }
        public decimal PricePerUnit { get; set; }
        public int StockQuantity { get; set; }

        public override string ToString() => $"{MedicineName} ({Unit}) - {PricePerUnit:N0}đ";
    }

    // DTO for patient info display
    public class PrescriptionPatientDto
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Diagnosis { get; set; }
    }

    public interface IPrescriptionView
    {
        // Patient info
        void LoadPatientInfo(PrescriptionPatientDto patient);
        
        // Medicine list for selection
        void LoadMedicineList(IEnumerable<MedicineDto> medicines);
        
        // Prescription items
        void AddPrescriptionItem(PrescriptionItemDto item);
        void RemovePrescriptionItem(int medicineId);
        void ClearPrescriptionItems();
        IEnumerable<PrescriptionItemDto> GetPrescriptionItems();
        
        // UI state
        void ShowLoading(bool isLoading);
        void ShowError(string message);
        void ShowSuccess(string message);
        
        // Events
        event EventHandler SaveRequested;
        event EventHandler PrintRequested;
        event EventHandler CancelRequested;
        
        // Properties
        int RecordId { get; set; }
        int ExaminationId { get; set; }
    }
}
