using HospitalManagement.Models.Entities;
using HospitalManagement.Views.Interfaces.Admin;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.EF;
using HospitalManagement.Infrastructure.Helpers;

namespace HospitalManagement.Presenters.Admin
{
    public class DoctorManagementPresenter
    {
        private readonly IDoctorManagementView _view;

        public DoctorManagementPresenter(IDoctorManagementView view)
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
                    // Load Departments for select
                    var depts = context.Departments.OrderBy(d => d.DepartmentName).ToList();
                    
                    // Add "All Departments" option for Filter
                    var filterDepts = depts.ToList();
                    filterDepts.Insert(0, new Departments { DepartmentID = 0, DepartmentName = "-- Tất cả phòng ban --" });
                    _view.SetDepartmentList(filterDepts);

                    // Load Users with Role = 'Doctor'
                    var doctorUsers = context.Users
                        .Where(u => u.Role == "Doctor" || u.Role == "doctor")
                        .OrderBy(u => u.FullName)
                        .ToList();
                    _view.SetUserList(doctorUsers);

                    LoadDoctorList(context, _view.FilterDepartmentId);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải dữ liệu ban đầu: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void LoadData()
        {
            try
            {
                _view.ShowLoading(true);
                using (var context = new HospitalDbContext())
                {
                    LoadDoctorList(context, _view.FilterDepartmentId);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải danh sách bác sĩ: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        private void LoadDoctorList(HospitalDbContext context, int? departmentIdFilter = null)
        {
            // Flatten for display
            var query = context.Doctors
                .Include("User")
                .Include("Department")
                .AsQueryable();

            if (departmentIdFilter.HasValue && departmentIdFilter.Value > 0)
            {
                query = query.Where(d => d.DepartmentID == departmentIdFilter.Value);
            }

            var doctors = query
                .Select(d => new 
                {
                    d.DoctorID,
                    d.UserID,
                    DoctorName = d.User.FullName,
                    d.DepartmentID,
                    DepartmentName = d.Department.DepartmentName,
                    d.Specialization,
                    d.LicenseNumber,
                    d.YearsOfExperience,
                    d.Qualifications,
                    d.ConsultationFee,
                    d.IsActive
                })
                .OrderBy(d => d.DoctorName)
                .ToList();

            _view.SetDoctorList(doctors);
        }

        public void SaveDoctor()
        {
            try
            {
                var userId = _view.SelectedUserId;
                var deptId = _view.SelectedDepartmentId;
                var specialization = _view.Specialization;
                var license = _view.LicenseNumber;
                
                // Validation for Doctor info
                if (deptId <= 0) { _view.ShowError("Vui lòng chọn phòng ban."); return; }
                if (string.IsNullOrWhiteSpace(specialization)) { _view.ShowError("Vui lòng nhập chuyên khoa."); return; }
                if (string.IsNullOrWhiteSpace(license)) { _view.ShowError("Vui lòng nhập số chứng chỉ hành nghề."); return; }

                using (var context = new HospitalDbContext())
                {
                    // Case: Create New Doctor + (Optionally) New User
                    if (!_view.SelectedDoctorId.HasValue)
                    {
                        // 1. Handle User Account
                        if (userId <= 0)
                        {
                            // Try to create new user
                            var newName = _view.NewFullName;
                            var newUsername = _view.NewUsername;
                            var newPassword = _view.NewPassword;
                            var newEmail = _view.NewEmail;
                            var newPhone = _view.NewPhone;

                            if (string.IsNullOrWhiteSpace(newName)) { _view.ShowError("Vui lòng nhập họ và tên bác sĩ."); return; }
                            if (string.IsNullOrWhiteSpace(newUsername)) { _view.ShowError("Vui lòng nhập tên đăng nhập."); return; }
                            
                            // Validation
                            if (!ValidationHelper.IsValidPassword(newPassword))
                            {
                                _view.ShowError("Mật khẩu phải có ít nhất 6 ký tự.");
                                return;
                            }
                            if (!string.IsNullOrWhiteSpace(newEmail) && !ValidationHelper.IsValidEmail(newEmail))
                            {
                                _view.ShowError("Email không đúng định dạng.");
                                return;
                            }
                            if (!string.IsNullOrWhiteSpace(newPhone) && !ValidationHelper.IsValidPhone(newPhone))
                            {
                                _view.ShowError("Số điện thoại phải bao gồm 10 chữ số.");
                                return;
                            }

                            if (context.Users.Any(u => u.Username == newUsername))
                            {
                                _view.ShowError("Tên đăng nhập đã tồn tại.");
                                return;
                            }

                            // Create the user
                            var newUser = new Users
                            {
                                Username = newUsername,
                                Password = newPassword,
                                FullName = newName,
                                Role = "doctor",
                                Email = string.IsNullOrWhiteSpace(newEmail) ? newUsername + "@hospital.com" : newEmail,
                                Phone = string.IsNullOrWhiteSpace(newPhone) ? "0000000000" : newPhone,
                                Status = "active",
                                CreatedAt = DateTime.Now
                            };

                            context.Users.Add(newUser);
                            context.SaveChanges(); // Need ID for Doctor
                            userId = newUser.UserID;
                        }
                        else
                        {
                            // Check if existing user is already a doctor
                            if (context.Doctors.Any(d => d.UserID == userId))
                            {
                                _view.ShowError("Tài khoản này đã được liên kết với một hồ sơ bác sĩ khác.");
                                return;
                            }
                        }

                        // 2. Create Doctor
                        var doctor = new Doctors
                        {
                            UserID = userId,
                            DepartmentID = deptId,
                            Specialization = specialization,
                            LicenseNumber = license,
                            YearsOfExperience = _view.YearsOfExperience,
                            Qualifications = _view.Qualifications,
                            ConsultationFee = _view.ConsultationFee,
                            IsActive = _view.IsActive,
                            CreatedAt = DateTime.Now
                        };

                        context.Doctors.Add(doctor);
                        context.SaveChanges();
                        _view.ShowMessage("Thêm hồ sơ bác sĩ thành công!");
                    }
                    else
                    {
                        // Case: Update Existing Doctor
                        var doctor = context.Doctors.Include(d => d.User).FirstOrDefault(d => d.DoctorID == _view.SelectedDoctorId.Value);
                        if (doctor != null)
                        {
                            doctor.UserID = userId > 0 ? userId : doctor.UserID;
                            doctor.DepartmentID = deptId;
                            doctor.Specialization = specialization;
                            doctor.LicenseNumber = license;
                            doctor.YearsOfExperience = _view.YearsOfExperience;
                            doctor.Qualifications = _view.Qualifications;
                            doctor.ConsultationFee = _view.ConsultationFee;
                            doctor.IsActive = _view.IsActive;

                            // Also update FullName if provided in NewFullName field
                            if (!string.IsNullOrWhiteSpace(_view.NewFullName))
                            {
                                doctor.User.FullName = _view.NewFullName;
                            }

                            context.SaveChanges();
                            _view.ShowMessage("Cập nhật thông tin bác sĩ thành công!");
                        }
                    }

                    LoadDoctorList(context, _view.FilterDepartmentId);
                    LoadInitialData(); // Refresh user list for combo
                    _view.ClearInputs();
                }
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException != null ? "\nChi tiết: " + ex.InnerException.Message : "";
                _view.ShowError("Lỗi lưu thông tin: " + ex.Message + inner);
            }
        }

        public void DeleteDoctor(int doctorId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var doctor = context.Doctors.Find(doctorId);
                    if (doctor != null)
                    {
                        // Soft delete (Deactivate) is safer
                        doctor.IsActive = false;
                        context.SaveChanges();
                        _view.ShowMessage("Đã vô hiệu hóa bác sĩ này.");
                        LoadDoctorList(context, _view.FilterDepartmentId);
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
