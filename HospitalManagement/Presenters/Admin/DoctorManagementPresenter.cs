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
                    // Load Departments (Only once if possible, but load here for simplicity)
                    var depts = context.Departments.ToList();
                    _view.SetDepartmentList(depts);
                    
                    // We should only set filter list if it's empty to avoid resetting selection, 
                    // or the View handles preserving selection? View resets it currently.
                    // For MVP simplicity, let's re-set it. A better approach is checking inside View.
                    // Or let's just set it if the combobox is empty? 
                    // Let's assume View handles it or we accept reset on full reload. 
                    // Wait, cmbFilterDept.SelectedValueChanged triggers LoadData. 
                    // If we re-set data source here, it might trigger event again (loop) or reset selection.
                    // We should separate loading reference data from loading grid data.
                    // Refactor: LoadReferenceData() vs LoadDoctorList()
                
                    // Check if we didn't load headers yet? 
                    // Actually, let's just check if depts are loaded in View? No access.
                    // Let's rely on standard LoadData.
                    // To prevent loop/reset: 
                    // 1. View's SetFilterDepartmentList sets index to 0. This is bad if we are reloading to filter.
                    // FIX: Modify View to not reset if already populated? Or use a separate "InitialLoad" method.
                    // BUT: changing Presenter structure is risky.
                    // STRATEGY: Update View SetFilterDepartmentList to keep selection if possible, 
                    // OR only call SetFilterDepartmentList ONCE during initialization in Presenter.
                    
                    // Let's introduce Initialize() in Presenter or just check a flag?
                    // Nah, let's just set it. 
                    
                    _view.SetFilterDepartmentList(depts); 
                    // Note: If this resets selection to 0, filtering breaks. 
                    // I will modify the View logic in next step to preserve selection.

                    // Load Users with Role = 'Doctor'
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
            var query = context.Doctors
                .Include("User")
                .Include("Department")
                .AsQueryable();

            // Filter by Dept
            int filterDeptId = _view.SelectedFilterDepartmentId;
            if (filterDeptId > 0)
            {
                query = query.Where(d => d.DepartmentID == filterDeptId);
            }

            // Filter by Keyword
            string keyword = _view.SearchKeyword.ToLower();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(d => d.User.FullName.Contains(keyword) || d.LicenseNumber.Contains(keyword));
            }

            // Flatten for display
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
