using HospitalManagement.Models.Entities;
using HospitalManagement.Views.Interfaces.Admin;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.EF;

namespace HospitalManagement.Presenters.Admin
{
    public class DoctorManagementPresenter
    {
        private readonly IDoctorManagementView _view;

        public DoctorManagementPresenter(IDoctorManagementView view)
        {
            _view = view;
        }

        public void LoadData()
        {
            try
            {
                _view.ShowLoading(true);
                using (var context = new HospitalDbContext())
                {
                    // Load Departments
                    var depts = context.Departments.ToList();
                    _view.SetDepartmentList(depts);

                    // Load Users with Role = 'Doctor'
                    // Ideally filter out those who already have a Doctor profile if adding new?
                    // For now, just list all available doctor users.
                    var doctorUsers = context.Users
                        .Where(u => u.Role == "Doctor" || u.Role == "doctor")
                        .OrderBy(u => u.FullName)
                        .ToList();
                    _view.SetUserList(doctorUsers);

                    LoadDoctorList(context);
                }
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

        private void LoadDoctorList(HospitalDbContext context)
        {
            // Flatten for display
            var doctors = context.Doctors
                .Include("User")
                .Include("Department")
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
                
                // Validation
                if (userId <= 0) { _view.ShowError("Vui lòng chọn tài khoản liên kết (User)."); return; }
                if (deptId <= 0) { _view.ShowError("Vui lòng chọn phòng ban."); return; }
                if (string.IsNullOrWhiteSpace(specialization)) { _view.ShowError("Vui lòng nhập chuyên khoa."); return; }
                if (string.IsNullOrWhiteSpace(license)) { _view.ShowError("Vui lòng nhập số chứng chỉ hành nghề."); return; }

                using (var context = new HospitalDbContext())
                {
                    if (_view.SelectedDoctorId.HasValue)
                    {
                        // Update
                        var doctor = context.Doctors.Find(_view.SelectedDoctorId.Value);
                        if (doctor != null)
                        {
                            doctor.UserID = userId;
                            doctor.DepartmentID = deptId;
                            doctor.Specialization = specialization;
                            doctor.LicenseNumber = license;
                            doctor.YearsOfExperience = _view.YearsOfExperience;
                            doctor.Qualifications = _view.Qualifications;
                            doctor.ConsultationFee = _view.ConsultationFee;
                            doctor.IsActive = _view.IsActive;

                            context.SaveChanges();
                            _view.ShowMessage("Cập nhật thông tin bác sĩ thành công!");
                        }
                    }
                    else
                    {
                        // Check if user is already a doctor
                        if (context.Doctors.Any(d => d.UserID == userId))
                        {
                            _view.ShowError("Tài khoản này đã được liên kết với một hồ sơ bác sĩ khác.");
                            return;
                        }

                        // Add New
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

                    LoadDoctorList(context);
                    _view.ClearInputs();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi lưu thông tin: " + ex.Message);
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
                        LoadDoctorList(context);
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
