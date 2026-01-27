using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                // Lấy tất cả schedule của khoa trong tuần
                var schedules = context.DoctorSchedules
                    .Include(ds => ds.Doctor)
                    .Include(ds => ds.Shift)
                    .Where(ds => ds.DepartmentID == departmentId 
                              && ds.IsActive == true
                              && ds.ScheduleDate >= weekStart.Date 
                              && ds.ScheduleDate <= weekEnd.Date)
                    .ToList();

                // Cho mỗi ngày trong tuần
                for (int i = 0; i < 7; i++)
                {
                    var currentDate = weekStart.AddDays(i);

                    // Lấy schedules cho ngày này
                    var daySchedules = schedules.Where(s => s.ScheduleDate.Date == currentDate.Date).ToList();
                    
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
                    var totalSlots = daySchedules.Sum(s => s.AvailableSlots ?? 20);

                    // Đếm số đã đặt
                    var scheduleIds = daySchedules.Select(s => s.ScheduleID).ToList();
                    var bookedCount = context.Appointments
                        .Count(a => scheduleIds.Contains(a.ScheduleID ?? 0) 
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
            using (var context = new HospitalDbContext())
            {
                var schedules = context.DoctorSchedules
                    .Include(ds => ds.Doctor)
                    .Include(ds => ds.Shift)
                    .Where(ds => ds.DepartmentID == departmentId 
                              && ds.ScheduleDate.Date == date.Date
                              && ds.IsActive == true)
                    .ToList();

                var result = new List<TimeSlotInfo>();

                foreach (var schedule in schedules)
                {
                    var bookedCount = context.Appointments
                        .Count(a => a.ScheduleID == schedule.ScheduleID
                                 && (a.Status == "pending" || a.Status == "confirmed"));

                    result.Add(new TimeSlotInfo
                    {
                        ScheduleId = schedule.ScheduleID,
                        ShiftId = schedule.ShiftID ?? 0,
                        StartTime = schedule.Shift?.StartTime ?? TimeSpan.Zero,
                        EndTime = schedule.Shift?.EndTime ?? TimeSpan.Zero,
                        MaxPatients = schedule.AvailableSlots ?? 20,
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
                             && (a.Status == "pending" || a.Status == "confirmed"))
                    .Select(a => a.AppointmentNumber)
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

        public int BookAppointment(int patientId, int scheduleId, DateTime appointmentDate, 
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

                    if (schedule == null) return -1;

                    // Kiểm tra STT đã được đặt chưa
                    var isBooked = context.Appointments.Any(a => 
                        a.ScheduleID == scheduleId 
                        && a.AppointmentNumber == queueNumber
                        && (a.Status == "pending" || a.Status == "confirmed"));

                    if (isBooked) return -1;

                    // Tạo appointment mới
                    var appointment = new Appointments
                    {
                        PatientID = patientId,
                        DoctorID = schedule.DoctorID,
                        DepartmentID = schedule.DepartmentID,
                        ScheduleID = scheduleId,
                        ShiftID = schedule.ShiftID,
                        AppointmentDate = appointmentDate.Date,
                        AppointmentNumber = queueNumber,
                        Symptoms = reason,
                        Status = "pending",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    context.Appointments.Add(appointment);
                    context.SaveChanges();

                    // Tự động tạo Payment và Invoice cho lượt khám này
                    var doctor = context.Doctors.Find(schedule.DoctorID);
                    var amount = doctor?.ConsultationFee ?? 150000;

                    var payment = new Payments
                    {
                        AppointmentID = appointment.AppointmentID,
                        PatientID = patientId,
                        PaymentType = "appointment",
                        Amount = amount,
                        PaymentStatus = "pending",
                        CreatedAt = DateTime.Now
                    };
                    context.Payments.Add(payment);
                    context.SaveChanges();

                    var invoice = new Invoices
                    {
                        PaymentID = payment.PaymentID,
                        InvoiceNumber = "INV" + DateTime.Now.ToString("yyyyMMdd") + appointment.AppointmentID.ToString().PadLeft(4, '0'),
                        InvoiceDate = DateTime.Now,
                        TotalAmount = amount,
                        FinalAmount = amount,
                        InvoiceStatus = "unpaid",
                        CreatedAt = DateTime.Now
                    };
                    context.Invoices.Add(invoice);
                    context.SaveChanges();

                    return appointment.AppointmentID;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BookAppointment Error: {ex.Message}");
                return -1;
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
                    .Include(a => a.Shift)
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
        public bool ConfirmAppointment(int appointmentId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var appointment = context.Appointments.Find(appointmentId);
                    if (appointment == null) return false;

                    appointment.Status = "confirmed";
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
