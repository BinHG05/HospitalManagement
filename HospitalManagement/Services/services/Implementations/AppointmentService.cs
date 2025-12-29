using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HospitalManagement.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        public IEnumerable<Departments> GetAllDepartments()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Departments
                    .Where(d => d.IsActive == true)
                    .OrderBy(d => d.DepartmentName)
                    .ToList();
            }
        }

        public IEnumerable<DepartmentScheduleInfo> GetDepartmentWeeklySchedule(int departmentId, DateTime weekStart)
        {
            var weekEnd = weekStart.AddDays(6);
            var result = new List<DepartmentScheduleInfo>();

            using (var context = new HospitalDbContext())
            {
                // Lấy tất cả schedule của khoa
                var schedules = context.DoctorSchedules
                    .Include(ds => ds.Doctor)
                    .Include(ds => ds.Shift)
                    .Where(ds => ds.Doctor.DepartmentID == departmentId && ds.IsActive == true)
                    .ToList();

                // Cho mỗi ngày trong tuần
                for (int i = 0; i < 7; i++)
                {
                    var currentDate = weekStart.AddDays(i);
                    var dayOfWeek = (int)currentDate.DayOfWeek;

                    // Lấy schedules cho ngày này
                    var daySchedules = schedules.Where(s => s.DayOfWeek == dayOfWeek).ToList();
                    
                    if (!daySchedules.Any())
                    {
                        result.Add(new DepartmentScheduleInfo
                        {
                            Date = currentDate,
                            TotalSlots = 0,
                            BookedSlots = 0
                        });
                        continue;
                    }

                    // Tổng số slot
                    var totalSlots = daySchedules.Sum(s => s.MaxPatients ?? 20);

                    // Đếm số đã đặt
                    var scheduleIds = daySchedules.Select(s => s.ScheduleID).ToList();
                    var bookedCount = context.Appointments
                        .Count(a => scheduleIds.Contains(a.ScheduleID ?? 0) 
                                 && DbFunctions.TruncateTime(a.AppointmentDate) == currentDate.Date
                                 && (a.Status == "pending" || a.Status == "confirmed"));

                    result.Add(new DepartmentScheduleInfo
                    {
                        Date = currentDate,
                        TotalSlots = totalSlots,
                        BookedSlots = bookedCount
                    });
                }
            }

            return result;
        }

        public IEnumerable<TimeSlotInfo> GetTimeSlots(int departmentId, DateTime date)
        {
            var dayOfWeek = (int)date.DayOfWeek;

            using (var context = new HospitalDbContext())
            {
                var schedules = context.DoctorSchedules
                    .Include(ds => ds.Doctor)
                    .Include(ds => ds.Shift)
                    .Where(ds => ds.Doctor.DepartmentID == departmentId 
                              && ds.DayOfWeek == dayOfWeek 
                              && ds.IsActive == true)
                    .ToList();

                var result = new List<TimeSlotInfo>();

                foreach (var schedule in schedules)
                {
                    var bookedCount = context.Appointments
                        .Count(a => a.ScheduleID == schedule.ScheduleID
                                 && DbFunctions.TruncateTime(a.AppointmentDate) == date.Date
                                 && (a.Status == "pending" || a.Status == "confirmed"));

                    result.Add(new TimeSlotInfo
                    {
                        ScheduleId = schedule.ScheduleID,
                        ShiftId = schedule.ShiftID ?? 0,
                        StartTime = schedule.Shift?.StartTime ?? TimeSpan.Zero,
                        EndTime = schedule.Shift?.EndTime ?? TimeSpan.Zero,
                        MaxPatients = schedule.MaxPatients ?? 20,
                        BookedCount = bookedCount
                    });
                }

                return result.OrderBy(t => t.StartTime).ToList();
            }
        }

        public IEnumerable<int> GetBookedQueueNumbers(int scheduleId, DateTime date)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Appointments
                    .Where(a => a.ScheduleID == scheduleId
                             && DbFunctions.TruncateTime(a.AppointmentDate) == date.Date
                             && (a.Status == "pending" || a.Status == "confirmed"))
                    .Select(a => a.AppointmentNumber ?? 0)
                    .ToList();
            }
        }

        public int GetNextAvailableQueueNumber(int scheduleId, DateTime date, int maxPatients)
        {
            var bookedNumbers = GetBookedQueueNumbers(scheduleId, date).ToHashSet();

            for (int i = 1; i <= maxPatients; i++)
            {
                if (!bookedNumbers.Contains(i))
                {
                    return i;
                }
            }

            return -1; // Đã đầy
        }

        public bool BookAppointment(int patientId, int scheduleId, DateTime appointmentDate, 
                                   int queueNumber, string reason)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Lấy thông tin schedule
                    var schedule = context.DoctorSchedules
                        .Include(ds => ds.Doctor)
                        .FirstOrDefault(ds => ds.ScheduleID == scheduleId);

                    if (schedule == null) return false;

                    // Kiểm tra STT đã được đặt chưa
                    var isBooked = context.Appointments.Any(a => 
                        a.ScheduleID == scheduleId 
                        && DbFunctions.TruncateTime(a.AppointmentDate) == appointmentDate.Date
                        && a.AppointmentNumber == queueNumber
                        && (a.Status == "pending" || a.Status == "confirmed"));

                    if (isBooked) return false;

                    // Tạo appointment mới
                    var appointment = new Appointments
                    {
                        PatientID = patientId,
                        DoctorID = schedule.DoctorID,
                        DepartmentID = schedule.Doctor?.DepartmentID,
                        ScheduleID = scheduleId,
                        AppointmentDate = appointmentDate.Date,
                        AppointmentNumber = queueNumber,
                        Reason = reason,
                        Status = "pending",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    context.Appointments.Add(appointment);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BookAppointment Error: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Appointments> GetPatientAppointments(int patientId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Appointments
                    .Include(a => a.Doctor)
                    .Include(a => a.Department)
                    .Include(a => a.Schedule)
                    .Include(a => a.Schedule.Shift)
                    .Where(a => a.PatientID == patientId)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ThenBy(a => a.AppointmentNumber)
                    .ToList();
            }
        }

        public bool CancelAppointment(int appointmentId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var appointment = context.Appointments.Find(appointmentId);
                    if (appointment == null) return false;

                    appointment.Status = "cancelled";
                    appointment.UpdatedAt = DateTime.Now;
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
