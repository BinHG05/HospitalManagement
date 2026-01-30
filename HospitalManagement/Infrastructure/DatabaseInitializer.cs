using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Infrastructure
{
    public static class DatabaseInitializer
    {
        public static void Initialize(bool forceRefresh = false)
        {
            using (var context = new HospitalDbContext())
            {
                if (forceRefresh)
                {
                    Console.WriteLine("Force refreshing database...");
                    context.Database.EnsureDeleted();
                }
                
                Console.WriteLine("Checking/Creating database...");
                // EnsureCreated returns true if the database was newly created
                if (context.Database.EnsureCreated())
                {
                    Console.WriteLine("Database created. Seeding initial data...");
                    Seed(context);
                    Console.WriteLine("Seeding complete.");
                }
                else
                {
                    Console.WriteLine("Database already exists.");
                }
            }
        }

        private static void Seed(HospitalDbContext context)
        {
            // 1. Departments
            var depts = new List<Departments>
            {
                new Departments { DepartmentName = "Nội khoa", Description = "Khoa nội tổng quát" },
                new Departments { DepartmentName = "Ngoại khoa", Description = "Khoa chấn thương chỉnh hình" },
                new Departments { DepartmentName = "Nhi khoa", Description = "Khoa nhi" },
                new Departments { DepartmentName = "Sản phụ khoa", Description = "Sản khoa" }
            };
            context.Departments.AddRange(depts);
            context.SaveChanges();

            // 2. Users (Admin + Doctors)
            var adminUser = new Users
            {
                Username = "admin",
                Password = "123", // In real app, hash this
                FullName = "System Administrator",
                Role = "admin",
                Email = "admin@hospital.com",
                Phone = "0123456789",
                Status = "active"
            };

            var doctorUser1 = new Users
            {
                Username = "doctor1",
                Password = "123",
                FullName = "BS. Nguyễn Văn A",
                Role = "doctor",
                Email = "bsa@hospital.com",
                Phone = "0123456781",
                Status = "active"
            };

            var doctorUser2 = new Users
            {
                Username = "doctor2",
                Password = "123",
                FullName = "BS. Trần Thị B",
                Role = "doctor",
                Email = "bsb@hospital.com",
                Phone = "0123456782",
                Status = "active"
            };

            context.Users.AddRange(adminUser, doctorUser1, doctorUser2);
            context.SaveChanges();

            // 3. Doctors
            var doc1 = new Doctors
            {
                UserID = doctorUser1.UserID,
                DepartmentID = depts[0].DepartmentID,
                Specialization = "Nội tổng quát",
                YearsOfExperience = 10,
                Qualifications = "Thạc sĩ",
                LicenseNumber = "LC001",
                ConsultationFee = 200000,
                MinShiftsPerMonth = 15,
                MaxShiftsPerMonth = 25
            };

            var doc2 = new Doctors
            {
                UserID = doctorUser2.UserID,
                DepartmentID = depts[1].DepartmentID,
                Specialization = "Ngoại thần kinh",
                YearsOfExperience = 8,
                Qualifications = "Bác sĩ CKI",
                LicenseNumber = "LC002",
                ConsultationFee = 300000,
                MinShiftsPerMonth = 10,
                MaxShiftsPerMonth = 20
            };

            context.Doctors.AddRange(doc1, doc2);
            context.SaveChanges();

            // 4. Shifts
            var shifts = new List<Shifts>
            {
                new Shifts { ShiftName = "Ca Sáng", StartTime = new TimeSpan(7, 30, 0), EndTime = new TimeSpan(11, 30, 0), MaxSlots = 50, MinDoctorsPerShift = 2, MaxDoctorsPerShift = 5 },
                new Shifts { ShiftName = "Ca Chiều", StartTime = new TimeSpan(13, 30, 0), EndTime = new TimeSpan(17, 30, 0), MaxSlots = 50, MinDoctorsPerShift = 2, MaxDoctorsPerShift = 5 },
                new Shifts { ShiftName = "Ca Tối", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(22, 0, 0), MaxSlots = 30, MinDoctorsPerShift = 1, MaxDoctorsPerShift = 3 }
            };
            context.Shifts.AddRange(shifts);
            context.SaveChanges();
            
            // 5. Some approved schedules for testing
            var today = DateTime.Today;
            context.DoctorSchedules.Add(new DoctorSchedules
            {
                DoctorID = doc1.DoctorID,
                ShiftID = shifts[0].ShiftID,
                DepartmentID = depts[0].DepartmentID,
                ScheduleDate = today,
                Status = "Approved",
                IsActive = true
            });
            context.SaveChanges();
        }
    }
}
