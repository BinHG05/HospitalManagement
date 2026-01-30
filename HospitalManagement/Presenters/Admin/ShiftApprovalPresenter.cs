using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.Entities;
using HospitalManagement.Models.EF;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Presenters.Admin
{
    public class ShiftApprovalPresenter
    {
        private readonly IShiftApprovalView _view;
        private readonly int _adminUserId;

        public ShiftApprovalPresenter(IShiftApprovalView view, int adminUserId)
        {
            _view = view;
            _adminUserId = adminUserId;
        }

        /// <summary>
        /// Load all pending shift requests
        /// </summary>
        public void LoadPendingRequests()
        {
            try
            {
                _view.ShowLoading(true);

                using (var context = new HospitalDbContext())
                {
                    var pending = context.DoctorSchedules
                        .Include(ds => ds.Doctor)
                        .Include(ds => ds.Doctor.User)
                        .Include(ds => ds.Shift)
                        .Include(ds => ds.Department)
                        .Where(ds => ds.Status == "Pending")
                        .OrderBy(ds => ds.ScheduleDate)
                        .ThenBy(ds => ds.RequestedAt)
                        .Select(ds => new ShiftRequestInfo
                        {
                            ScheduleID = ds.ScheduleID,
                            DoctorName = ds.Doctor.User.FullName ?? "Unknown",
                            Department = ds.Department.DepartmentName ?? "N/A",
                            Date = ds.ScheduleDate,
                            ShiftName = ds.Shift.ShiftName,
                            Time = ds.Shift.StartTime.ToString(@"hh\:mm") + " - " + ds.Shift.EndTime.ToString(@"hh\:mm"),
                            RequestedAt = ds.RequestedAt,
                            Status = ds.Status
                        })
                        .ToList();

                    _view.SetPendingRequests(pending);
                    _view.SetPendingCount(pending.Count);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải danh sách yêu cầu: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        /// <summary>
        /// Load shift quota summary for a specific date
        /// </summary>
        public void LoadShiftQuotaSummary()
        {
            try
            {
                var date = _view.SelectedDate.Date;

                using (var context = new HospitalDbContext())
                {
                    var shifts = context.Shifts.ToList();
                    var departments = context.Departments.ToList();

                    var quotas = new List<ShiftQuotaInfo>();

                    foreach (var dept in departments)
                    {
                        foreach (var shift in shifts)
                        {
                            var registered = context.DoctorSchedules.Count(ds =>
                                ds.DepartmentID == dept.DepartmentID &&
                                ds.ShiftID == shift.ShiftID &&
                                ds.ScheduleDate == date &&
                                (ds.Status == "Approved" || ds.Status == "Pending"));

                            quotas.Add(new ShiftQuotaInfo
                            {
                                ShiftID = shift.ShiftID,
                                ShiftName = shift.ShiftName,
                                DepartmentName = dept.DepartmentName,
                                CurrentRegistered = registered,
                                MinDoctors = shift.MinDoctorsPerShift,
                                MaxDoctors = shift.MaxDoctorsPerShift
                            });
                        }
                    }

                    _view.SetShiftQuotaSummary(quotas);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải thống kê ca: " + ex.Message);
            }
        }

        /// <summary>
        /// Load doctor monthly quota summary
        /// </summary>
        public void LoadDoctorQuotaSummary()
        {
            try
            {
                var month = _view.SelectedMonth ?? DateTime.Now.Month;
                var year = _view.SelectedYear ?? DateTime.Now.Year;

                var startOfMonth = new DateTime(year, month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                using (var context = new HospitalDbContext())
                {
                    var allDoctors = context.Doctors
                        .Include(d => d.User)
                        .Include(d => d.Department)
                        .Where(d => d.IsActive == true)
                        .ToList();

                    // Filter in-memory to ensure User is not null
                    var doctors = allDoctors.Where(d => d.User != null).ToList();

                    var quotas = new List<DoctorQuotaInfo>();

                    foreach (var d in doctors)
                    {
                        try
                        {
                            var approved = context.DoctorSchedules.Count(ds =>
                                ds.DoctorID == d.DoctorID &&
                                ds.ScheduleDate >= startOfMonth &&
                                ds.ScheduleDate <= endOfMonth &&
                                ds.Status == "Approved");

                            var pending = context.DoctorSchedules.Count(ds =>
                                ds.DoctorID == d.DoctorID &&
                                ds.ScheduleDate >= startOfMonth &&
                                ds.ScheduleDate <= endOfMonth &&
                                ds.Status == "Pending");

                            quotas.Add(new DoctorQuotaInfo
                            {
                                DoctorID = d.DoctorID,
                                DoctorName = d.User?.FullName ?? "Unknown",
                                Department = d.Department?.DepartmentName ?? "N/A",
                                ApprovedShifts = approved,
                                PendingShifts = pending,
                                MinRequired = d.MinShiftsPerMonth,
                                MaxAllowed = d.MaxShiftsPerMonth
                            });
                        }
                        catch
                        {
                            // Skip this doctor if any error occurs
                            continue;
                        }
                    }

                    var sortedQuotas = quotas
                        .OrderBy(q => q.StatusIcon ?? "")
                        .ThenBy(q => q.DoctorName ?? "")
                        .ToList();

                    _view.SetDoctorQuotaSummary(sortedQuotas);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải thống kê bác sĩ: " + ex.Message);
            }
        }

        /// <summary>
        /// Approve a shift request
        /// </summary>
        public void ApproveRequest(int scheduleId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var schedule = context.DoctorSchedules
                        .Include(ds => ds.Shift)
                        .Include(ds => ds.Doctor)
                        .FirstOrDefault(ds => ds.ScheduleID == scheduleId);

                    if (schedule == null)
                    {
                        _view.ShowError("Không tìm thấy yêu cầu.");
                        return;
                    }

                    // Check if shift is already full
                    var approvedCount = context.DoctorSchedules.Count(ds =>
                        ds.ShiftID == schedule.ShiftID &&
                        ds.ScheduleDate == schedule.ScheduleDate &&
                        ds.Status == "Approved");

                    if (approvedCount >= schedule.Shift.MaxDoctorsPerShift)
                    {
                        _view.ShowError("Ca này đã đủ người. Không thể duyệt thêm.");
                        return;
                    }

                    // Check doctor's monthly quota
                    var startOfMonth = new DateTime(schedule.ScheduleDate.Year, schedule.ScheduleDate.Month, 1);
                    var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                    var doctorApproved = context.DoctorSchedules.Count(ds =>
                        ds.DoctorID == schedule.DoctorID &&
                        ds.ScheduleDate >= startOfMonth &&
                        ds.ScheduleDate <= endOfMonth &&
                        ds.Status == "Approved");

                    if (doctorApproved >= schedule.Doctor.MaxShiftsPerMonth)
                    {
                        _view.ShowError($"Bác sĩ đã đạt giới hạn {schedule.Doctor.MaxShiftsPerMonth} ca/tháng.");
                        return;
                    }

                    // Approve
                    schedule.Status = "Approved";
                    schedule.ApprovedByUserID = _adminUserId;
                    schedule.ApprovedAt = DateTime.Now;

                    context.SaveChanges();

                    _view.ShowMessage("Đã duyệt yêu cầu thành công!");
                    _view.RefreshData();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi duyệt yêu cầu: " + ex.Message);
            }
        }

        /// <summary>
        /// Reject a shift request
        /// </summary>
        public void RejectRequest(int scheduleId, string reason)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var schedule = context.DoctorSchedules.Find(scheduleId);
                    if (schedule == null)
                    {
                        _view.ShowError("Không tìm thấy yêu cầu.");
                        return;
                    }

                    schedule.Status = "Rejected";
                    schedule.ApprovedByUserID = _adminUserId;
                    schedule.ApprovedAt = DateTime.Now;
                    schedule.RejectionReason = reason;

                    context.SaveChanges();

                    _view.ShowMessage("Đã từ chối yêu cầu.");
                    _view.RefreshData();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi từ chối yêu cầu: " + ex.Message);
            }
        }

        /// <summary>
        /// Approve multiple requests at once
        /// </summary>
        public void ApproveMultiple(IEnumerable<int> scheduleIds)
        {
            int successCount = 0;
            int failCount = 0;

            foreach (var id in scheduleIds)
            {
                try
                {
                    using (var context = new HospitalDbContext())
                    {
                        var schedule = context.DoctorSchedules
                            .Include(ds => ds.Shift)
                            .Include(ds => ds.Doctor)
                            .FirstOrDefault(ds => ds.ScheduleID == id);

                        if (schedule == null) continue;

                        // Check quotas
                        var approvedCount = context.DoctorSchedules.Count(ds =>
                            ds.ShiftID == schedule.ShiftID &&
                            ds.ScheduleDate == schedule.ScheduleDate &&
                            ds.Status == "Approved");

                        if (approvedCount >= schedule.Shift.MaxDoctorsPerShift)
                        {
                            failCount++;
                            continue;
                        }

                        schedule.Status = "Approved";
                        schedule.ApprovedByUserID = _adminUserId;
                        schedule.ApprovedAt = DateTime.Now;

                        context.SaveChanges();
                        successCount++;
                    }
                }
                catch
                {
                    failCount++;
                }
            }

            if (failCount > 0)
                _view.ShowMessage($"Đã duyệt {successCount} yêu cầu. {failCount} yêu cầu không thể duyệt (đã đủ quota).");
            else
                _view.ShowMessage($"Đã duyệt {successCount} yêu cầu thành công!");

            _view.RefreshData();
        }
    }
}
