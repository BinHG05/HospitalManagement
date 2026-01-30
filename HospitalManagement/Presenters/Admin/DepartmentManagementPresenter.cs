using System;
using System.Linq;
using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Views.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Presenters.Admin
{
    public class DepartmentManagementPresenter
    {
        private readonly IDepartmentManagementView _view;

        public DepartmentManagementPresenter(IDepartmentManagementView view)
        {
            _view = view;
        }

        public void LoadDepartments()
        {
            try
            {
                _view.ShowLoading(true);
                using (var context = new HospitalDbContext())
                {
                    var query = context.Departments.AsQueryable();

                    if (!string.IsNullOrWhiteSpace(_view.SearchKeyword))
                    {
                        var keyword = _view.SearchKeyword.ToLower();
                        query = query.Where(d => d.DepartmentName.Contains(keyword));
                    }

                    var list = query.OrderBy(d => d.DepartmentName).ToList();
                    _view.SetDepartmentList(list);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải danh sách phòng ban: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void SaveDepartment()
        {
            try
            {
                var name = _view.DepartmentName;
                var desc = _view.Description;

                if (string.IsNullOrWhiteSpace(name))
                {
                    _view.ShowError("Vui lòng nhập tên phòng ban.");
                    return;
                }

                using (var context = new HospitalDbContext())
                {
                    if (_view.SelectedDepartmentId.HasValue)
                    {
                        // Update
                        var dept = context.Departments.Find(_view.SelectedDepartmentId.Value);
                        if (dept != null)
                        {
                            dept.DepartmentName = name;
                            dept.Description = desc;
                            // UpdateAt if available? Usually not in simple Entities.
                            
                            context.SaveChanges();
                            _view.ShowMessage("Cập nhật phòng ban thành công!");
                        }
                    }
                    else
                    {
                        // Add New
                        if (context.Departments.Any(d => d.DepartmentName == name))
                        {
                            _view.ShowError("Tên phòng ban đã tồn tại.");
                            return;
                        }

                        var newDept = new Departments
                        {
                            DepartmentName = name,
                            Description = desc
                        };

                        context.Departments.Add(newDept);
                        context.SaveChanges();
                        _view.ShowMessage("Thêm phòng ban thành công!");
                    }

                    LoadDepartments();
                    _view.ClearInputs();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi lưu phòng ban: " + ex.Message);
            }
        }

        public void DeleteDepartment(int deptId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Check usage before delete
                    var hasDoctors = context.Doctors.Any(d => d.DepartmentID == deptId);
                    if (hasDoctors)
                    {
                        _view.ShowError("Không thể xóa phòng ban này vì đang có bác sĩ trực thuộc.");
                        return;
                    }

                    var dept = context.Departments.Find(deptId);
                    if (dept != null)
                    {
                        context.Departments.Remove(dept);
                        context.SaveChanges();
                        _view.ShowMessage("Đã xóa phòng ban.");
                        LoadDepartments();
                        _view.ClearInputs();
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
