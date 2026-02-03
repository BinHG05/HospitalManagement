using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Implementations;
using HospitalManagement.Views.Interfaces.Admin;
using System;
using System.Linq;

namespace HospitalManagement.Presenters.Admin
{
    public class UserManagementPresenter
    {
        private readonly IUserManagementView _view;
        private readonly AuthService _authService; // Reusing AuthService for user ops or need a UserService?
        // Ideally we need a UserService. For now, we can validly use EF direct or create a UserService.
        // Given project structure, let's use a simple direct context or service if available.
        // AuthService handles Login/Register. 
        // Let's assume we will use a repo or direct context for management to be quick, 
        // OR extend AuthService. Let's stick to 'AuthService' if it has Register, but we need 'Update' and 'Get'.
        // Code analysis: AuthService usually has Login/Register.
        // I'll create a simple logic here using DB Context directly for MVP or standard pattern.
        // The project uses Services. Let's check if we have a UserService. 
        // If not, I'll implement logic here using the Context directly for now (Pragmatic for Phase 1).
        
        // Wait, "Register" in AuthService creates a user. 
        // Let's implement basic CRUD here.

        public UserManagementPresenter(IUserManagementView view)
        {
            _view = view;
            // _authService = new AuthService(); 
        }

        public void LoadUsers()
        {
            try
            {
                _view.ShowLoading(true);
                using (var context = new HospitalDbContext())
                {
                    var users = context.Users.OrderByDescending(u => u.CreatedAt).ToList();
                    _view.SetUserList(users);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải danh sách người dùng: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void SearchUsers()
        {
            try
            {
                var keyword = _view.SearchKeyword.ToLower();
                using (var context = new HospitalDbContext())
                {
                    var users = context.Users
                        .Where(u => u.Username.Contains(keyword) || (u.FullName != null && u.FullName.Contains(keyword)))
                        .OrderByDescending(u => u.CreatedAt)
                        .ToList();
                    _view.SetUserList(users);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        public void SaveUser()
        {
            try
            {
                var username = _view.Username;
                var password = _view.Password; // Only relevant for new users or password reset
                var displayName = _view.DisplayName;
                var role = _view.SelectedRole;
                var isActive = _view.IsActive;

                if (string.IsNullOrWhiteSpace(username))
                {
                    _view.ShowError("Vui lòng nhập Username.");
                    return;
                }

                using (var context = new HospitalDbContext())
                {
                    if (_view.SelectedUserId.HasValue)
                    {
                        // Update
                        var user = context.Users.Find(_view.SelectedUserId.Value);
                        if (user != null)
                        {
                            user.FullName = displayName;
                            user.Role = role;
                            user.Status = isActive ? "active" : "locked";
                            
                            // Only update password if provided
                            if (!string.IsNullOrWhiteSpace(password))
                            {
                                // Check if target user is Admin (assuming 'admin' is the role code)
                                if (user.Role.ToLower() == "admin")
                                {
                                    string key = _view.GetSecretKeyInput("Bạn đang thay đổi mật khẩu của ADMIN. Vui lòng nhập khóa bảo mật (Master Key):");
                                    // Hardcoded key for demonstration: "admin123@" or "1905"
                                    if (key != "1905") 
                                    {
                                        _view.ShowError("Khóa bảo mật không chính xác! Không thể đổi mật khẩu.");
                                        return;
                                    }
                                }
                                user.Password = password; // Using plain text to match AuthService
                            }
                            
                            // Property UpdatedAt does not exist in this entity
                            // user.UpdatedAt = DateTime.Now;
                            
                            context.SaveChanges();
                            _view.ShowMessage("Cập nhật thành công!");
                        }
                    }
                    else
                    {
                        // Add New
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            _view.ShowError("Vui lòng nhập mật khẩu cho người dùng mới.");
                            return;
                        }

                        if (context.Users.Any(u => u.Username == username))
                        {
                            _view.ShowError("Username đã tồn tại.");
                            return;
                        }

                        var newUser = new Users
                        {
                            Username = username,
                            Password = password, // Plain text
                            FullName = displayName,
                            Role = role,
                            Status = isActive ? "active" : "locked",
                            CreatedAt = DateTime.Now,
                            // Email/Phone defaults
                            Email = username + "@hospital.com", 
                            Phone = "0000000000"
                        };

                        context.Users.Add(newUser);
                        context.SaveChanges();
                        _view.ShowMessage("Thêm người dùng thành công!");
                    }
                }
                
                _view.ClearInputs();
                LoadUsers();
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi lưu người dùng: " + ex.Message);
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var user = context.Users.Find(userId);
                    if (user != null)
                    {
                        // Soft delete usually better, but let's do hard delete or deactivate?
                        // "Delete/Lock" was the requirement. Let's just Deactivate if it has related data.
                        // For now, let's try strict delete, catch constraint error, fallback to deactivate.
                        
                        // Actually, safer to just Deactivate (Lock)
                        user.Status = "locked";
                        // user.UpdatedAt = DateTime.Now; // Removed as property doesn't exist
                        context.SaveChanges();
                        _view.ShowMessage($"Đã khóa tài khoản {user.Username}.");
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi xóa/khóa: " + ex.Message);
            }
        }
    }
}
