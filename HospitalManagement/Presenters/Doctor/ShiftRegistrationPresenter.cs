using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.Entities;
using HospitalManagement.Models.EF;
using HospitalManagement.Views.Interfaces.Doctor;

namespace HospitalManagement.Presenters.Doctor
{
    public class ShiftRegistrationPresenter
    {
        private readonly IShiftRegistrationView _view;
        private readonly int _doctorId;
        private readonly int _userId;

        public ShiftRegistrationPresenter(IShiftRegistrationView view, int doctorId, int userId)
        {
            _view = view;
            _doctorId = doctorId;
            _userId = userId;
        }

        /// <summary>
        /// Load available shifts for chosen date
        /// </summary>
        public void LoadAvailableShifts()
        {
            try
            {
                _view.ShowLoading(true);
                var date = _view.SelectedDate.Date;

                using (var context = new HospitalDbContext())
                {
                    var shifts = context.Shifts.ToList();
                    
                    var shiftsWithSlots = shifts.Select(s =>
                    {
                        // Count approved registrations for this shift on this date
                        var registered = context.DoctorSchedules.Count(ds =>
                            ds.ShiftID == s.ShiftID &&
                            ds.ScheduleDate == date &&
                            (ds.Status == "Approved" || ds.Status == "Pending"));

                        return new ShiftSlotInfo
                        {
                            ShiftID = s.ShiftID,
                            ShiftName = s.ShiftName,
                            StartTime = s.StartTime,
                            EndTime = s.EndTime,
                            CurrentRegistered = registered,
                            MinDoctors = s.MinDoctorsPerShift,
                            MaxDoctors = s.MaxDoctorsPerShift
                        };
                    }).ToList();

                    _view.SetAvailableShifts(shiftsWithSlots);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải danh sách ca: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        /// <summary>
        /// Load current doctor's registrations
        /// </summary>
        public void LoadMyRegistrations()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var registrations = context.DoctorSchedules
                        .Include(ds => ds.Shift)
                        .Include(ds => ds.Department)
                        .Where(ds => ds.DoctorID == _doctorId && 
                                     ds.ScheduleDate >= DateTime.Today)
                        .OrderBy(ds => ds.ScheduleDate)
                        .ThenBy(ds => ds.Shift.StartTime)
                        .ToList();

                    _view.SetMyRegistrations(registrations);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải lịch đăng ký: " + ex.Message);
            }
        }

        /// <summary>
        /// Load monthly quota for current doctor
        /// </summary>
        public void LoadMonthlyQuota()
        {
            try
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                using (var context = new HospitalDbContext())
                {
                    // Get doctor's quota settings
                    var doctor = context.Doctors.Find(_doctorId);
                    if (doctor == null) return;

                    // Count approved shifts this month
                    var approvedCount = context.DoctorSchedules.Count(ds =>
                        ds.DoctorID == _doctorId &&
                        ds.ScheduleDate >= startOfMonth &&
                        ds.ScheduleDate <= endOfMonth &&
                        ds.Status == "Approved");

                    // Count pending shifts this month
                    var pendingCount = context.DoctorSchedules.Count(ds =>
                        ds.DoctorID == _doctorId &&
                        ds.ScheduleDate >= startOfMonth &&
                        ds.ScheduleDate <= endOfMonth &&
                        ds.Status == "Pending");

                    _view.SetMonthlyQuota(
                        approvedCount + pendingCount, 
                        doctor.MinShiftsPerMonth, 
                        doctor.MaxShiftsPerMonth);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải định mức: " + ex.Message);
            }
        }

        /// <summary>
        /// Register for a shift
        /// </summary>
        public void RegisterShift()
        {
            try
            {
                var shiftId = _view.SelectedShiftId;
                var date = _view.SelectedDate.Date;

                if (!shiftId.HasValue)
                {
                    _view.ShowError("Vui lòng chọn ca trực.");
                    return;
                }

                if (date < DateTime.Today)
                {
                    _view.ShowError("Không thể đăng ký ca cho ngày trong quá khứ.");
                    return;
                }

                using (var context = new HospitalDbContext())
                {
                    // Check if already registered
                    var exists = context.DoctorSchedules.Any(ds =>
                        ds.DoctorID == _doctorId &&
                        ds.ShiftID == shiftId &&
                        ds.ScheduleDate == date &&
                        ds.Status != "Rejected");

                    if (exists)
                    {
                        _view.ShowError("Bạn đã đăng ký ca này rồi.");
                        return;
                    }

                    // Check shift slot availability
                    var shift = context.Shifts.Find(shiftId.Value);
                    var registered = context.DoctorSchedules.Count(ds =>
                        ds.ShiftID == shiftId &&
                        ds.ScheduleDate == date &&
                        ds.Status != "Rejected");

                    if (registered >= shift.MaxDoctorsPerShift)
                    {
                        _view.ShowError("Ca này đã đủ người, không thể đăng ký thêm.");
                        return;
                    }

                    // Check monthly quota
                    var now = DateTime.Now;
                    var startOfMonth = new DateTime(date.Year, date.Month, 1);
                    var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                    var doctor = context.Doctors.Find(_doctorId);
                    var monthlyCount = context.DoctorSchedules.Count(ds =>
                        ds.DoctorID == _doctorId &&
                        ds.ScheduleDate >= startOfMonth &&
                        ds.ScheduleDate <= endOfMonth &&
                        ds.Status != "Rejected");

                    if (monthlyCount >= doctor.MaxShiftsPerMonth)
                    {
                        _view.ShowError($"Bạn đã đạt giới hạn {doctor.MaxShiftsPerMonth} ca/tháng.");
                        return;
                    }

                    // Create registration
                    var schedule = new DoctorSchedules
                    {
                        DoctorID = _doctorId,
                        DepartmentID = doctor.DepartmentID,
                        ShiftID = shiftId.Value,
                        ScheduleDate = date,
                        AvailableSlots = shift.MaxSlots ?? 20,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        Status = "Pending",
                        RequestedAt = DateTime.Now
                    };

                    context.DoctorSchedules.Add(schedule);
                    context.SaveChanges();

                    _view.ShowMessage("Đăng ký ca thành công! Đang chờ Admin duyệt.");
                    _view.ClearSelection();
                    LoadAvailableShifts();
                    LoadMyRegistrations();
                    LoadMonthlyQuota();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi đăng ký: " + ex.Message);
            }
        }

        /// <summary>
        /// Cancel a pending registration
        /// </summary>
        public void CancelRegistration(int scheduleId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var schedule = context.DoctorSchedules.Find(scheduleId);
                    if (schedule == null) return;

                    if (schedule.DoctorID != _doctorId)
                    {
                        _view.ShowError("Bạn không có quyền hủy đăng ký này.");
                        return;
                    }

                    if (schedule.Status == "Approved")
                    {
                        // Check if appointments exist
                        var hasApps = context.Appointments.Any(a => a.ScheduleID == scheduleId);
                        if (hasApps)
                        {
                            _view.ShowError("Không thể hủy vì đã có bệnh nhân đặt hẹn.");
                            return;
                        }
                    }

                    context.DoctorSchedules.Remove(schedule);
                    context.SaveChanges();

                    _view.ShowMessage("Đã hủy đăng ký.");
                    LoadMyRegistrations();
                    LoadMonthlyQuota();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi hủy đăng ký: " + ex.Message);
            }
        }
    }
}
