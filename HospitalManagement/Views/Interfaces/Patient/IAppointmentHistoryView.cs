using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace HospitalManagement.Views.Interfaces.Patient
{
    public interface IAppointmentHistoryView
    {
        // Load appointments list
        void LoadAppointments(IEnumerable<AppointmentDisplayInfo> appointments);
        
        // Filter
        string SelectedStatusFilter { get; }
        
        // Selected appointment
        int SelectedAppointmentId { get; }
        
        // Actions
        void ShowAppointmentDetails(AppointmentDisplayInfo appointment);
        void ShowCancelConfirmation(int appointmentId);
        void ShowRescheduleDialog(int appointmentId);
        
        // UI State
        void ShowLoading(bool isLoading);
        void ShowError(string message);
        void ShowSuccess(string message);
        void RefreshList();
    }

    // DTO for displaying appointments
    public class AppointmentDisplayInfo
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentNumber { get; set; }
        public string DepartmentName { get; set; }
        public string DoctorName { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan? ShiftStartTime { get; set; }
        public TimeSpan? ShiftEndTime { get; set; }
        public string Symptoms { get; set; }
        public string Status { get; set; }
        public string RoomNumber { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string TimeRange => ShiftStartTime.HasValue && ShiftEndTime.HasValue 
            ? $"{ShiftStartTime.Value:hh\\:mm} - {ShiftEndTime.Value:hh\\:mm}" 
            : "N/A";

        public string StatusDisplay
        {
            get
            {
                switch (Status)
                {
                    case "pending": return "Chờ xác nhận";
                    case "confirmed": return "Đã xác nhận";
                    case "examining": return "Đang khám";
                    case "completed": return "Hoàn thành";
                    case "cancelled": return "Đã hủy";
                    case "no-show": return "Vắng mặt";
                    default: return Status;
                }
            }
        }

        public bool CanCancel => Status == "pending" || Status == "confirmed";
        public bool CanReschedule => Status == "pending";
    }
}
