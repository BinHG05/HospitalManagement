using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace HospitalManagement.Views.Interfaces.Patient
{
    public interface IAppointmentBookingView
    {
        // Properties
        int SelectedDepartmentId { get; }
        DateTime SelectedDate { get; }
        int SelectedScheduleId { get; }
        int SelectedQueueNumber { get; }
        string Reason { get; }

        // Methods để hiển thị dữ liệu
        void LoadDepartments(IEnumerable<Departments> departments);
        void ShowWeeklySchedule(IEnumerable<DepartmentScheduleInfo> schedules);
        void ShowTimeSlots(IEnumerable<TimeSlotInfo> slots);
        void ShowQueueNumbers(IEnumerable<int> bookedNumbers, int suggestedNumber, int maxPatients);
        
        // UI State
        void ShowLoading(bool isLoading);
        void ShowError(string message);
        void ShowSuccess(string message);
        void ClearSelection();
        
        // Navigation
        void GoToTimeSlotSelection();
        void GoToQueueSelection();
        void GoBackToWeeklyView();
    }
}
