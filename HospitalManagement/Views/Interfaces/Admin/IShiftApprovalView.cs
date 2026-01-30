using System;
using System.Collections.Generic;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces.Admin
{
    /// <summary>
    /// Interface for Admin's Shift Approval view
    /// Allows admin to approve/reject doctor shift registrations
    /// </summary>
    public interface IShiftApprovalView
    {
        // Selected values
        DateTime SelectedDate { get; }
        int? SelectedMonth { get; }
        int? SelectedYear { get; }

        // Data binding
        void SetPendingRequests(IEnumerable<ShiftRequestInfo> requests);
        void SetShiftQuotaSummary(IEnumerable<ShiftQuotaInfo> quotas);
        void SetDoctorQuotaSummary(IEnumerable<DoctorQuotaInfo> quotas);
        void SetPendingCount(int count);

        // UI feedback
        void ShowLoading(bool isLoading);
        void ShowMessage(string message);
        void ShowError(string message);
        void RefreshData();
    }

    /// <summary>
    /// DTO for pending shift request
    /// </summary>
    public class ShiftRequestInfo
    {
        public int ScheduleID { get; set; }
        public string DoctorName { get; set; }
        public string Department { get; set; }
        public DateTime Date { get; set; }
        public string ShiftName { get; set; }
        public string Time { get; set; }
        public DateTime? RequestedAt { get; set; }
        public string Status { get; set; }
    }

    /// <summary>
    /// DTO for shift quota summary (per shift per day)
    /// </summary>
    public class ShiftQuotaInfo
    {
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public string DepartmentName { get; set; }
        public int CurrentRegistered { get; set; }
        public int MinDoctors { get; set; }
        public int MaxDoctors { get; set; }
        public string StatusIcon => CurrentRegistered < MinDoctors ? "🔴 Thiếu" : 
                                    (CurrentRegistered < MaxDoctors ? "🟡 Đủ" : "🟢 Đầy");
        public bool IsUnderStaffed => CurrentRegistered < MinDoctors;
    }

    /// <summary>
    /// DTO for doctor monthly quota summary
    /// </summary>
    public class DoctorQuotaInfo
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string Department { get; set; }
        public int ApprovedShifts { get; set; }
        public int PendingShifts { get; set; }
        public int TotalShifts => ApprovedShifts + PendingShifts;
        public int MinRequired { get; set; }
        public int MaxAllowed { get; set; }
        public string StatusIcon => TotalShifts < MinRequired ? "🔴" : 
                                    (TotalShifts >= MaxAllowed ? "🟢" : "🟡");
        public string StatusText => TotalShifts < MinRequired ? "Chưa đủ" : 
                                    (TotalShifts >= MaxAllowed ? "Đạt max" : "Đủ");
        public int RemainingRequired => Math.Max(0, MinRequired - TotalShifts);
    }
}
