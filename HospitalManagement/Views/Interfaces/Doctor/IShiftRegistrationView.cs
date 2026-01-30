using System;
using System.Collections.Generic;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces.Doctor
{
    /// <summary>
    /// Interface for Doctor's Shift Registration view
    /// Allows doctors to register for available shifts
    /// </summary>
    public interface IShiftRegistrationView
    {
        // Selected values
        DateTime SelectedDate { get; }
        int? SelectedShiftId { get; }

        // Data binding
        void SetAvailableShifts(IEnumerable<ShiftSlotInfo> shifts);
        void SetMyRegistrations(IEnumerable<DoctorSchedules> registrations);
        void SetMonthlyQuota(int current, int min, int max);

        // UI feedback
        void ShowLoading(bool isLoading);
        void ShowMessage(string message);
        void ShowError(string message);
        void ClearSelection();
    }

    /// <summary>
    /// DTO for displaying shift with slot information
    /// </summary>
    public class ShiftSlotInfo
    {
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int CurrentRegistered { get; set; }
        public int MaxDoctors { get; set; }
        public int MinDoctors { get; set; }
        public bool CanRegister => CurrentRegistered < MaxDoctors;
        public string SlotDisplay => $"{CurrentRegistered}/{MaxDoctors}";
        public string StatusIcon => CurrentRegistered < MinDoctors ? "🔴" : 
                                    (CurrentRegistered < MaxDoctors ? "🟡" : "🟢");
    }
}
