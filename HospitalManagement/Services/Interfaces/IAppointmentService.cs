using HospitalManagement.Models.Entities;
using System;
using System.Collections.Generic;

namespace HospitalManagement.Services.Interfaces
{
    public interface IAppointmentService
    {
        // Lấy danh sách khoa
        IEnumerable<Departments> GetAllDepartments();

        // Lấy lịch làm việc của khoa theo tuần
        IEnumerable<DepartmentScheduleInfo> GetDepartmentWeeklySchedule(int departmentId, DateTime weekStart);

        // Lấy các khung giờ trong ngày của khoa
        IEnumerable<TimeSlotInfo> GetTimeSlots(int departmentId, DateTime date);

        // Lấy danh sách STT đã đặt trong khung giờ (scheduleId đã chứa thông tin ngày)
        IEnumerable<int> GetBookedQueueNumbers(int scheduleId, DateTime date);

        // Lấy STT nhỏ nhất còn trống
        int GetNextAvailableQueueNumber(int scheduleId, DateTime date, int maxPatients);

        // Đặt lịch khám
        // Đặt lịch khám (Return AppointmentID if success, -1 if fail)
        int BookAppointment(int patientId, int scheduleId, DateTime appointmentDate, 
                            int queueNumber, string reason);

        // Lấy lịch hẹn của bệnh nhân
        IEnumerable<Appointments> GetPatientAppointments(int patientId);

        // Hủy lịch hẹn
        // Xác nhận lịch hẹn (Thanh toán)
        bool ConfirmAppointment(int appointmentId, string paymentMethod);

        bool CancelAppointment(int appointmentId);
    }

    // DTO cho lịch khoa theo ngày
    public class DepartmentScheduleInfo
    {
        public DateTime Date { get; set; }
        public int TotalSlots { get; set; }
        public int BookedSlots { get; set; }
        public int AvailableSlots => TotalSlots - BookedSlots;
        public bool IsFull => AvailableSlots <= 0;
        public string Status => IsFull ? "full" : (AvailableSlots <= 5 ? "almost_full" : "available");
    }

    // DTO cho khung giờ
    public class TimeSlotInfo
    {
        public int ScheduleId { get; set; }
        public int ShiftId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxPatients { get; set; }
        public int BookedCount { get; set; }
        public int AvailableCount => MaxPatients - BookedCount;
        public bool IsFull => AvailableCount <= 0;
        public string TimeRange => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
    }
}
