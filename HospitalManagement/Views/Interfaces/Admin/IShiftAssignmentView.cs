using System;
using System.Collections.Generic;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Views.Interfaces.Admin
{
    public interface IShiftAssignmentView
    {
        // Inputs
        int? SelectedDoctorId { get; }
        int? SelectedShiftId { get; }
        DateTime SelectedDate { get; }

        // Data Sources
        void SetDoctorList(IEnumerable<Users> doctors); // Loading Users with Role=Doctor for display
        void SetShiftList(IEnumerable<Shifts> shifts);
        void SetScheduleList(IEnumerable<DoctorSchedules> schedules);

        // UI Feedback
        void ShowLoading(bool isLoading);
        void ShowMessage(string message);
        void ShowError(string message);
        void ClearSelection();
    }
}
