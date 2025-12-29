using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Interfaces;
using System;
using System.Linq;

namespace HospitalManagement.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public Users Login(string username, string password)
        {
            try
            {
                using (var db = new HospitalDbContext())
                {
                    // Debug: Log all users
                    var allUsers = db.Users.ToList();
                    System.Diagnostics.Debug.WriteLine($"Total users in DB: {allUsers.Count}");
                    foreach (var u in allUsers)
                    {
                        System.Diagnostics.Debug.WriteLine($"User: {u.Username}, Pass: {u.Password}, Status: {u.Status}");
                    }

                    var user = db.Users.FirstOrDefault(u =>
                        u.Username == username &&
                        u.Status == "active");

                    if (user == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"User not found: {username}");
                        return null;
                    }

                    // Simple password check (in production, use hashing)
                    if (user.Password == password)
                    {
                        System.Diagnostics.Debug.WriteLine($"Login successful for: {username}");
                        return user;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Password mismatch for: {username}. Expected: {user.Password}, Got: {password}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
                return null;
            }
        }

        public (bool Success, string Message) Register(string username, string password, string email, string phone, string fullName, string role)
        {
            try
            {
                using (var db = new HospitalDbContext())
                {
                    // Check if username exists
                    if (db.Users.Any(u => u.Username == username))
                    {
                        return (false, "Tên đăng nhập đã tồn tại!");
                    }

                    // Check if email exists
                    if (db.Users.Any(u => u.Email == email))
                    {
                        return (false, "Email đã được sử dụng!");
                    }

                    // Check if phone exists
                    if (db.Users.Any(u => u.Phone == phone))
                    {
                        return (false, "Số điện thoại đã được sử dụng!");
                    }

                    // Create new user
                    var newUser = new Users
                    {
                        Username = username,
                        Password = password, // In production, hash this!
                        Email = email,
                        Phone = phone,
                        FullName = fullName,
                        Role = role,
                        Status = "active",
                        CreatedAt = DateTime.Now
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // If registering as patient, create patient record
                    if (role == "patient")
                    {
                        var patient = new Patients
                        {
                            UserID = newUser.UserID,
                            CreatedAt = DateTime.Now
                        };
                        db.Patients.Add(patient);
                        db.SaveChanges();
                    }

                    return (true, "Đăng ký thành công!");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi đăng ký: {ex.Message}");
            }
        }

        public bool UsernameExists(string username)
        {
            using (var db = new HospitalDbContext())
            {
                return db.Users.Any(u => u.Username == username);
            }
        }

        public bool EmailExists(string email)
        {
            using (var db = new HospitalDbContext())
            {
                return db.Users.Any(u => u.Email == email);
            }
        }
    }
}
