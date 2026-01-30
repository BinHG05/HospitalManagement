using System;
using System.Linq;
using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Presenters.Admin
{
    public class ServiceManagementPresenter
    {
        private readonly IServiceManagementView _view;

        public ServiceManagementPresenter(IServiceManagementView view)
        {
            _view = view;
        }

        public void LoadServices()
        {
            try
            {
                _view.ShowLoading(true);
                using (var context = new HospitalDbContext())
                {
                    var query = context.MedicalServices.AsQueryable();

                    if (!string.IsNullOrWhiteSpace(_view.SearchKeyword))
                    {
                        var keyword = _view.SearchKeyword.ToLower();
                        query = query.Where(s => s.ServiceName.Contains(keyword));
                    }

                    var list = query.OrderBy(s => s.ServiceName).ToList();
                    _view.SetServiceList(list);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải danh sách dịch vụ: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void SaveService()
        {
            try
            {
                var name = _view.ServiceName;
                var price = _view.Price;
                var desc = _view.Description;

                if (string.IsNullOrWhiteSpace(name))
                {
                    _view.ShowError("Vui lòng nhập tên dịch vụ.");
                    return;
                }

                if (price < 0)
                {
                    _view.ShowError("Giá dịch vụ không hợp lệ.");
                    return;
                }

                using (var context = new HospitalDbContext())
                {
                    if (_view.SelectedServiceId.HasValue)
                    {
                        // Update
                        var service = context.MedicalServices.Find(_view.SelectedServiceId.Value);
                        if (service != null)
                        {
                            service.ServiceName = name;
                            service.Price = price;
                            service.Description = desc;
                            
                            context.SaveChanges();
                            _view.ShowMessage("Cập nhật dịch vụ thành công!");
                        }
                        else
                        {
                           _view.ShowError("Không tìm thấy dịch vụ để cập nhật.");
                        }
                    }
                    else
                    {
                        // Add New
                        if (context.MedicalServices.Any(s => s.ServiceName == name))
                        {
                            _view.ShowError("Tên dịch vụ đã tồn tại.");
                            return;
                        }

                        var newService = new MedicalServices
                        {
                            ServiceName = name,
                            Price = price,
                            Description = desc
                        };

                        context.MedicalServices.Add(newService);
                        context.SaveChanges();
                        _view.ShowMessage("Thêm dịch vụ thành công!");
                    }

                    LoadServices();
                    _view.ClearInputs();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi lưu dịch vụ: " + ex.Message);
            }
        }

        public void DeleteService(int serviceId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Check usage
                    var requestExists = context.ServiceRequests.Any(r => r.ServiceID == serviceId);
                    if (requestExists)
                    {
                        _view.ShowError("Không thể xóa dịch vụ này vì đã có yêu cầu sử dụng trong lịch sử.");
                        return;
                    }

                    var service = context.MedicalServices.Find(serviceId);
                    if (service != null)
                    {
                        context.MedicalServices.Remove(service);
                        context.SaveChanges();
                        _view.ShowMessage("Đã xóa dịch vụ.");
                        LoadServices();
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
