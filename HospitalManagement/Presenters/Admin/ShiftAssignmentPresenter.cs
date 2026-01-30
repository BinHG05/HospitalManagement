using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.Entities;
using HospitalManagement.Views.Interfaces.Admin;
using HospitalManagement.Models.EF;

namespace HospitalManagement.Presenters.Admin
{
    public class ShiftAssignmentPresenter
    {
        private readonly IShiftAssignmentView _view;

        public ShiftAssignmentPresenter(IShiftAssignmentView view)
        {
            _view = view;
        }

        public void LoadInitialData()
        {
            try
            {
                _view.ShowLoading(true);
                using (var context = new HospitalDbContext())
                {
                    var doctors = context.Users
                        .Where(u => u.Role == "Doctor" || u.Role == "doctor")
                        .OrderBy(u => u.FullName)
                        .ToList();
                    _view.SetDoctorList(doctors);

                    var shifts = context.Shifts.OrderBy(s => s.StartTime).ToList();
                    _view.SetShiftList(shifts);
                }
                LoadSchedule();
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void LoadSchedule()
        {
            try
            {
                var date = _view.SelectedDate.Date;
                using (var context = new HospitalDbContext())
                {
                    var schedules = context.DoctorSchedules
                        .Include(ds => ds.Doctor)
                        .Include(ds => ds.Doctor.User)
                        .Include(ds => ds.Shift)
                        .Include(ds => ds.Department)
                        .Where(ds => ds.ScheduleDate == date)
                        .OrderBy(ds => ds.Shift.StartTime)
                        .ToList();

                    _view.SetScheduleList(schedules);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải lịch trực: " + ex.Message);
            }
        }

        public void AssignShift()
        {
            try
            {
                var userId = _view.SelectedDoctorId;
                var shiftId = _view.SelectedShiftId;
                var date = _view.SelectedDate.Date;

                if (!userId.HasValue || !shiftId.HasValue)
                {
                    _view.ShowError("Vui lòng chọn bác sĩ và ca trực.");
                    return;
                }

                if (date < DateTime.Today)
                {
                    _view.ShowError("Không thể phân ca cho ngày trong quá khứ.");
                    return;
                }

                using (var context = new HospitalDbContext())
                {
                    var doctor = context.Doctors.FirstOrDefault(d => d.UserID == userId.Value);
                    if (doctor == null)
                    {
                        _view.ShowError("Tài khoản này chưa có hồ sơ Bác sĩ (Doctor Profile). Vui lòng tạo hồ sơ trước.");
                        return;
                    }

                    var exists = context.DoctorSchedules.Any(ds =>
                        ds.DoctorID == doctor.DoctorID &&
                        ds.ShiftID == shiftId &&
                        ds.ScheduleDate == date);

                    if (exists)
                    {
                        _view.ShowError("Bác sĩ đã được phân ca này trong ngày chọn.");
                        return;
                    }

                    var shift = context.Shifts.Find(shiftId.Value);

                    var schedule = new DoctorSchedules
                    {
                        DoctorID = doctor.DoctorID,
                        DepartmentID = doctor.DepartmentID,
                        ShiftID = shiftId.Value,
                        ScheduleDate = date,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        AvailableSlots = shift?.MaxSlots ?? 20
                    };

                    context.DoctorSchedules.Add(schedule);
                    context.SaveChanges();
                    _view.ShowMessage("Phân ca thành công!");
                    LoadSchedule();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi phân ca: " + ex.Message);
            }
        }

        public void DeleteAssignment(int scheduleId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var schedule = context.DoctorSchedules.Find(scheduleId);
                    if (schedule != null)
                    {
                        var hasApps = context.Appointments.Any(a => a.ScheduleID == scheduleId);
                        if (hasApps)
                        {
                            _view.ShowError("Không thể xóa lịch này vì đã có bệnh nhân đặt hẹn.");
                            return;
                        }

                        context.DoctorSchedules.Remove(schedule);
                        context.SaveChanges();
                        _view.ShowMessage("Đã hủy phân ca.");
                        LoadSchedule();
                    }
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi xóa: " + ex.Message);
            }
        }
    }
}
