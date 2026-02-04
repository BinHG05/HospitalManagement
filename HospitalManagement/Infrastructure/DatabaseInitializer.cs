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
                    Console.WriteLine("Database already exists. Checking for schema updates...");
                }

                // Luôn chạy patch để đảm bảo ngay cả khi DB vừa tạo hoặc đã có đều có đủ cột mới
                ApplyManualPatches(context);
            }
        }

        private static void ApplyManualPatches(HospitalDbContext context)
        {
            try
            {
                // Thêm các cột thiếu cho ServiceResults
                context.Database.ExecuteSqlRaw(@"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceResults]') AND name = 'PerformedAt')
                        ALTER TABLE ServiceResults ADD PerformedAt DATETIME;
                    
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceResults]') AND name = 'VerifiedBy')
                        ALTER TABLE ServiceResults ADD VerifiedBy INT;

                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceResults]') AND name = 'PerformedBy')
                        ALTER TABLE ServiceResults ADD PerformedBy INT;

                    IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ServiceResults_PerformedByDoctor')
                        ALTER TABLE ServiceResults ADD CONSTRAINT FK_ServiceResults_PerformedByDoctor 
                        FOREIGN KEY (PerformedBy) REFERENCES Doctors(DoctorID);

                    IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ServiceResults_VerifiedByDoctor')
                        ALTER TABLE ServiceResults ADD CONSTRAINT FK_ServiceResults_VerifiedByDoctor 
                        FOREIGN KEY (VerifiedBy) REFERENCES Doctors(DoctorID);
                ");

                // Thêm các cột thiếu cho ServiceRequests
                context.Database.ExecuteSqlRaw(@"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceRequests]') AND name = 'AppointmentID')
                        ALTER TABLE ServiceRequests ADD AppointmentID INT;
                    
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceRequests]') AND name = 'CompletedAt')
                        ALTER TABLE ServiceRequests ADD CompletedAt DATETIME;

                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceRequests]') AND name = 'RequestingDoctorID')
                        ALTER TABLE ServiceRequests ADD RequestingDoctorID INT;

                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceRequests]') AND name = 'AssignedScheduleID')
                        ALTER TABLE ServiceRequests ADD AssignedScheduleID INT;

                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceRequests]') AND name = 'ServiceQueueNumber')
                        ALTER TABLE ServiceRequests ADD ServiceQueueNumber INT;

                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[ServiceRequests]') AND name = 'DoctorNotes')
                        ALTER TABLE ServiceRequests ADD DoctorNotes NVARCHAR(500);

                    IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ServiceRequests_AssignedSchedule')
                        ALTER TABLE ServiceRequests ADD CONSTRAINT FK_ServiceRequests_AssignedSchedule 
                        FOREIGN KEY (AssignedScheduleID) REFERENCES DoctorSchedules(ScheduleID);

                    IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_ServiceRequests_RequestingDoctor')
                        ALTER TABLE ServiceRequests ADD CONSTRAINT FK_ServiceRequests_RequestingDoctor 
                        FOREIGN KEY (RequestingDoctorID) REFERENCES Doctors(DoctorID);
                ");

                // Thêm các cột thiếu cho Examinations
                context.Database.ExecuteSqlRaw(@"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[Examinations]') AND name = 'Notes')
                        ALTER TABLE Examinations ADD Notes NVARCHAR(MAX);
                ");

                // Thêm các cột thiếu cho Prescriptions
                context.Database.ExecuteSqlRaw(@"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[Prescriptions]') AND name = 'Instructions')
                        ALTER TABLE Prescriptions ADD Instructions NVARCHAR(500);
                ");

                Console.WriteLine("Schema update check complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not apply schema patches: {ex.Message}");
            }
        }

        private static void Seed(HospitalDbContext context)
        {
            // 1. Departments (6 khoa chính)
            var depts = new List<Departments>
            {
                new Departments { DepartmentName = "Khoa Nội tổng quát", Description = "Khám lâm sàng nội khoa" },
                new Departments { DepartmentName = "Khoa Ngoại", Description = "Phẫu thuật và thủ thuật" },
                new Departments { DepartmentName = "Khoa Nhi", Description = "Chuyên khoa nhi nhi" },
                new Departments { DepartmentName = "Khoa Sản", Description = "Sản phụ khoa" },
                new Departments { DepartmentName = "Khoa Chẩn đoán hình ảnh", Description = "Siêu âm, X-Quang, CT" },
                new Departments { DepartmentName = "Khoa Xét nghiệm", Description = "Xét nghiệm máu, sinh hóa" }
            };
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(depts);
                context.SaveChanges();
            }
            else
            {
                // Nếu đã có DB, load lại để dùng ID
                var existingDepts = context.Departments.ToList();
                depts = existingDepts; 
            }

            // 2. Shifts
            var shifts = new List<Shifts>
            {
                new Shifts { ShiftName = "Ca Sáng", StartTime = new TimeSpan(7, 30, 0), EndTime = new TimeSpan(11, 30, 0), MaxSlots = 50 },
                new Shifts { ShiftName = "Ca Chiều", StartTime = new TimeSpan(13, 30, 0), EndTime = new TimeSpan(17, 30, 0), MaxSlots = 50 },
                new Shifts { ShiftName = "Ca Tối", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(21, 0, 0), MaxSlots = 30 }
            };
            if (!context.Shifts.Any())
            {
                context.Shifts.AddRange(shifts);
                context.SaveChanges();
            }
            else
            {
                shifts = context.Shifts.ToList();
            }

            // 3. ADMINS (2)
            for (int i = 1; i <= 2; i++)
            {
                if (!context.Users.Any(u => u.Username == $"admin{i}"))
                {
                    context.Users.Add(new Users
                    {
                        Username = $"admin{i}",
                        Password = "123",
                        FullName = $"Quản trị viên {i}",
                        Role = "admin",
                        Email = $"admin{i}@hospital.com",
                        Phone = $"010000000{i}",
                        Status = "active",
                        CreatedAt = DateTime.Now
                    });
                }
            }
            context.SaveChanges();

            // 4. DOCTORS (10)
            var doctorUsers = new List<Users>();
            for (int i = 1; i <= 10; i++)
            {
                var username = $"doctor{i}";
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    user = new Users
                    {
                        Username = username,
                        Password = "123",
                        FullName = (i % 2 == 0) ? $"BS. Nguyễn Văn D{i}" : $"BS. Trần Thị D{i}",
                        Role = "doctor",
                        Email = $"doctor{i}@hospital.com",
                        Phone = $"09000000{i:D2}",
                        Status = "active",
                        CreatedAt = DateTime.Now
                    };
                    context.Users.Add(user);
                    doctorUsers.Add(user);
                }
                else
                {
                    doctorUsers.Add(user);
                }
            }
            context.SaveChanges();

            // Create Doctor Profiles if not exist
            doctorUsers = context.Users.Where(u => u.Role == "doctor").ToList(); 
            for (int i = 1; i <= 10; i++)
            {
               var username = $"doctor{i}";
               var u = doctorUsers.FirstOrDefault(x => x.Username == username);
               if (u != null && !context.Doctors.Any(d => d.UserID == u.UserID))
               {
                    var deptIndex = (i - 1) % depts.Count; // Safety
                    context.Doctors.Add(new Doctors
                    {
                        UserID = u.UserID,
                        DepartmentID = depts[deptIndex].DepartmentID,
                        Specialization = depts[deptIndex].DepartmentName.Replace("Khoa ", ""),
                        Qualifications = (i % 3 == 0) ? "Tiến sĩ" : "Thạc sĩ",
                        LicenseNumber = $"LC-DOC-{i:D3}",
                        ConsultationFee = (deptIndex < 4) ? 200000 : 150000, 
                        YearsOfExperience = 5 + i,
                        IsActive = true
                    });
               }
            }
            context.SaveChanges();

            // 5. PHARMACISTS (10)
            for (int i = 1; i <= 10; i++)
            {
                if (!context.Users.Any(u => u.Username == $"pharm{i}"))
                {
                    context.Users.Add(new Users
                    {
                        Username = $"pharm{i}",
                        Password = "123",
                        FullName = $"Dược sĩ {i}",
                        Role = "pharmacist",
                        Email = $"pharm{i}@hospital.com",
                        Phone = $"08000000{i:D2}",
                        Status = "active",
                        CreatedAt = DateTime.Now
                    });
                }
            }
            context.SaveChanges();

            // 6. PATIENTS (10)
            var patientUsers = new List<Users>();
            for (int i = 1; i <= 10; i++)
            {
                var username = $"patient{i}";
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    user = new Users
                    {
                        Username = username,
                        Password = "123",
                        FullName = $"Bệnh nhân {i}",
                        Role = "patient",
                        Email = $"patient{i}@hospital.com",
                        Phone = $"07000000{i:D2}",
                        Status = "active",
                        CreatedAt = DateTime.Now
                    };
                    context.Users.Add(user);
                    patientUsers.Add(user);
                }
                else
                {
                    patientUsers.Add(user);
                }
            }
            context.SaveChanges();

            // Create Patient Profiles
            patientUsers = context.Users.Where(u => u.Role == "patient").Take(10).ToList();
            for (int i = 0; i < patientUsers.Count; i++)
            {
                var uid = patientUsers[i].UserID;
                if (!context.Patients.Any(p => p.UserID == uid))
                {
                    context.Patients.Add(new Patients
                    {
                        UserID = uid,
                        Gender = (i % 2 == 0) ? "Nam" : "Nữ",
                        DateOfBirth = DateTime.Today.AddYears(-20 - i),
                        Address = "Hà Nội, Việt Nam",
                        BloodType = (i % 4 == 0) ? "O" : (i % 4 == 1) ? "A" : "B",
                        InsuranceNumber = $"HY{100000000 + i}",
                        CreatedAt = DateTime.Now
                    });
                }
            }
            context.SaveChanges();

            // 7. MEDICAL SERVICES
            if (!context.MedicalServices.Any())
            {
                var services = new List<MedicalServices>
                {
                    new MedicalServices { ServiceName = "Siêu âm bụng tổng quát", ServiceType = "Siêu âm", Price = 250000, DepartmentID = depts.FirstOrDefault(d => d.DepartmentName.Contains("hình ảnh"))?.DepartmentID ?? depts[0].DepartmentID, IsActive = true },
                    new MedicalServices { ServiceName = "Siêu âm tuyến giáp", ServiceType = "Siêu âm", Price = 200000, DepartmentID = depts.FirstOrDefault(d => d.DepartmentName.Contains("hình ảnh"))?.DepartmentID ?? depts[0].DepartmentID, IsActive = true },
                    new MedicalServices { ServiceName = "X-Quang ngực thẳng", ServiceType = "X-Quang", Price = 150000, DepartmentID = depts.FirstOrDefault(d => d.DepartmentName.Contains("hình ảnh"))?.DepartmentID ?? depts[0].DepartmentID, IsActive = true },
                    new MedicalServices { ServiceName = "Chụp CT Sọ não", ServiceType = "CT", Price = 1200000, DepartmentID = depts.FirstOrDefault(d => d.DepartmentName.Contains("hình ảnh"))?.DepartmentID ?? depts[0].DepartmentID, IsActive = true },
                    new MedicalServices { ServiceName = "Tổng phân tích tế bào máu", ServiceType = "Xét nghiệm", Price = 100000, DepartmentID = depts.FirstOrDefault(d => d.DepartmentName.Contains("Xét nghiệm"))?.DepartmentID ?? depts[0].DepartmentID, IsActive = true },
                    new MedicalServices { ServiceName = "Sinh hóa máu (Glucose, Urea)", ServiceType = "Xét nghiệm", Price = 180000, DepartmentID = depts.FirstOrDefault(d => d.DepartmentName.Contains("Xét nghiệm"))?.DepartmentID ?? depts[0].DepartmentID, IsActive = true },
                    new MedicalServices { ServiceName = "Nội soi dạ dày", ServiceType = "Thủ thuật", Price = 800000, DepartmentID = depts.FirstOrDefault(d => d.DepartmentName.Contains("Ngoại"))?.DepartmentID ?? depts[0].DepartmentID, IsActive = true }
                };
                context.MedicalServices.AddRange(services);
                context.SaveChanges();
            }

            // 8. MEDICINES
            if (!context.Medicines.Any())
            {
                string[] medNames = { "Paracetamol", "Amoxicillin", "Vitamin C", "Aspirin", "Ibuprofen", "Cefalexin", "Metformin", "Amlodipine", "Omeprazole", "Salbutamol" };
                for (int i = 0; i < 20; i++)
                {
                    context.Medicines.Add(new Medicines
                    {
                        MedicineName = $"{medNames[i % 10]} {100 + i * 50}mg",
                        GenericName = medNames[i % 10],
                        Unit = "Viên",
                        PricePerUnit = 1000 + i * 500,
                        StockQuantity = 1000,
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    });
                }
                context.SaveChanges();
            }

            // 9. SCHEDULES (Tạo lịch cho 7 ngày tới cho tất cả bác sĩ)
            var allDoctors = context.Doctors.ToList();
            var today = DateTime.Today;
            foreach (var doc in allDoctors)
            {
                for (int d = 0; d < 7; d++) // 7 ngày tới
                {
                    var date = today.AddDays(d);
                    if (date.DayOfWeek == DayOfWeek.Sunday) continue;

                    if (!context.DoctorSchedules.Any(s => s.DoctorID == doc.DoctorID && s.ScheduleDate == date))
                    {
                        context.DoctorSchedules.Add(new DoctorSchedules
                        {
                            DoctorID = doc.DoctorID,
                            DepartmentID = doc.DepartmentID,
                            ShiftID = shifts[d % 2].ShiftID, // Xen kẽ sáng chiều
                            ScheduleDate = date,
                            Status = "Approved",
                            IsActive = true,
                            AvailableSlots = 30
                        });
                    }
                }
            }
            context.SaveChanges();

            // 10. APPOINTMENTS & DATA FOR TESTING
            // Chung ta can du lieu de test:
            // - Dat lich moi (Confirmed) -> Test kham
            // - Da kham xong cho thanh toan (Pending Payment) -> Test cashier
            // - Da kham xong va co thuoc (Prescription) -> Test duoc si
            // - Dang cho xet nghiem (Pending Service) -> Test ket qua
            
            var allPatients = context.Patients.ToList(); // Should be around 10
            var allSchedules = context.DoctorSchedules.Where(s => s.ScheduleDate >= today).ToList();
            var medServices = context.MedicalServices.ToList();
            var medicines = context.Medicines.ToList();

            if (!context.Appointments.Any(a => a.AppointmentDate >= today))
            {
                // A. FUTURE APPOINTMENTS (Test Booking/Doctor List)
                for (int i = 0; i < 5; i++) // 5 benh nhan dau tien
                {
                    if (i >= allPatients.Count) break;
                    var patient = allPatients[i];
                    var sched = allSchedules[i % allSchedules.Count];
                    
                    context.Appointments.Add(new Appointments
                    {
                        PatientID = patient.PatientID,
                        DoctorID = sched.DoctorID,
                        DepartmentID = sched.DepartmentID,
                        ScheduleID = sched.ScheduleID,
                        ShiftID = sched.ShiftID,
                        AppointmentDate = sched.ScheduleDate,
                        AppointmentNumber = i + 1,
                        Status = "confirmed",
                        Symptoms = "Đau nhẹ, cần kiểm tra",
                        CreatedAt = DateTime.Now
                    });
                }
                context.SaveChanges(); 

                // B. TODAY APPOINTMENTS - COMPLETED & UNPAID (Test Thanh Toan Pending)
                var todayScheds = allSchedules.Where(s => s.ScheduleDate == today).ToList();
                if (todayScheds.Any() && allPatients.Count > 5)
                {
                    for (int i = 5; i < 8; i++) // 3 benh nhan tiep theo
                    {
                        if (i >= allPatients.Count) break;
                        var patient = allPatients[i];
                        var sched = todayScheds[i % todayScheds.Count];

                        var app = new Appointments
                        {
                            PatientID = patient.PatientID,
                            DoctorID = sched.DoctorID,
                            DepartmentID = sched.DepartmentID,
                            ScheduleID = sched.ScheduleID,
                            ShiftID = sched.ShiftID,
                            AppointmentDate = today,
                            AppointmentNumber = 100 + i,
                            Status = "completed",
                            Symptoms = "Sốt cao, đau họng",
                            CreatedAt = DateTime.Now.AddHours(-2)
                        };
                        context.Appointments.Add(app);
                        context.SaveChanges(); // Save to get ID

                        // Tao Payment Pending
                        context.Payments.Add(new Payments
                        {
                             PatientID = patient.PatientID,
                             AppointmentID = app.AppointmentID,
                             Amount = 250000, 
                             PaymentMethod = "Cash",
                             PaymentStatus = "completed", // Changed to completed for Report testing
                             PaymentType = "Examination",
                             PaymentDate = DateTime.Now, // Set valid date
                             CreatedAt = DateTime.Now
                        });

                        // Tao Examination & Record & Prescription
                        var exam = new Examinations
                        {
                            AppointmentID = app.AppointmentID,
                            PatientID = patient.PatientID,
                            DoctorID = sched.DoctorID,
                            PreliminaryDiagnosis = "Viêm họng cấp", // FIXED: Use PreliminaryDiagnosis
                            Notes = "Kê đơn thuốc về uống",
                            Status = "completed",
                            ExaminationDate = today
                        };
                        context.Examinations.Add(exam);
                        context.SaveChanges();

                        var record = new MedicalRecords
                        {
                            PatientID = patient.PatientID,
                            ExaminationID = exam.ExaminationID,
                            Diagnosis = "Viêm họng cấp", // Manual string or copy from PreliminaryDiagnosis
                            TreatmentPlan = "Uống thuốc theo đơn",
                            RecordDate = today,
                            CreatedAt = DateTime.Now
                        };
                        context.MedicalRecords.Add(record);
                        context.SaveChanges();

                        if (medicines.Any())
                        {
                            context.Prescriptions.Add(new Prescriptions
                            {
                                RecordID = record.RecordID,
                                MedicineID = medicines[0].MedicineID,
                                Quantity = 10,
                                Dosage = "2 viên/ngày",
                                Frequency = "Sáng - Tối",
                                Instructions = "Sau ăn no",
                                CreatedAt = DateTime.Now
                            });
                        }
                    }
                }

                // C. SERVICE REQUESTS (Test Dich Vu Can Lam Sang - Pending)
                var specializedDepts = depts.Where(d => d.DepartmentName.Contains("Xét nghiệm") || d.DepartmentName.Contains("Hình ảnh")).ToList();
                if (specializedDepts.Any() && allPatients.Count > 8)
                {
                    for (int i = 8; i < 10; i++) 
                    {
                        if (i >= allPatients.Count) break;
                        var patient = allPatients[i];
                        var doc = allDoctors.FirstOrDefault(); 
                        if (doc == null) continue;

                        var service = medServices.FirstOrDefault(s => s.DepartmentID == specializedDepts[0].DepartmentID);
                        if (service == null) continue;

                        var schedule = todayScheds.FirstOrDefault(s => s.DepartmentID == doc.DepartmentID) ?? todayScheds.First();

                        var app = new Appointments
                        {
                            PatientID = patient.PatientID,
                            DoctorID = schedule.DoctorID,
                            DepartmentID = schedule.DepartmentID,
                            ScheduleID = schedule.ScheduleID, 
                            ShiftID = schedule.ShiftID,
                            AppointmentDate = today,
                            AppointmentNumber = 200 + i,
                            Status = "confirmed",
                            Symptoms = "Đau bụng lâm râm",
                            CreatedAt = DateTime.Now
                        };
                        context.Appointments.Add(app);
                        context.SaveChanges();

                        context.ServiceRequests.Add(new ServiceRequests
                        {
                            AppointmentID = app.AppointmentID,
                            ServiceID = service.ServiceID,
                            RequestingDoctorID = doc.DoctorID,
                            Status = "pending", // Dang cho thuc hien
                            RequestedAt = DateTime.Now,
                            ServiceQueueNumber = i,
                            RequestNumber = i,
                            DoctorNotes = "Kiểm tra gấp vùng bụng"
                        });
                    }
                }
                context.SaveChanges();

                // D. HISTORICAL DATA
                var lastMonth = today.AddMonths(-1);
                var histPatient = allPatients[0];
                var histDoc = allDoctors[0];

                // Create a schedule for history to have valid time
                var histSchedule = new DoctorSchedules
                {
                    DoctorID = histDoc.DoctorID,
                    DepartmentID = histDoc.DepartmentID,
                    ShiftID = shifts[0].ShiftID, // Ca Sang
                    ScheduleDate = lastMonth,
                    Status = "Approved",
                    IsActive = true,
                    AvailableSlots = 30
                };
                context.DoctorSchedules.Add(histSchedule);
                context.SaveChanges();

                var histApp = new Appointments
                {
                    PatientID = histPatient.PatientID,
                    DoctorID = histDoc.DoctorID,
                    DepartmentID = histDoc.DepartmentID,
                    ScheduleID = histSchedule.ScheduleID, // Assigned Schedule
                    ShiftID = histSchedule.ShiftID, // Directly assign ShiftID for View lookup
                    AppointmentDate = lastMonth,
                    AppointmentNumber = 555,
                    Status = "completed",
                    Symptoms = "Tái khám định kỳ",
                    CreatedAt = lastMonth
                };
                context.Appointments.Add(histApp);
                context.SaveChanges();


                // Add Exam for history
                var histExam = new Examinations
                {
                    AppointmentID = histApp.AppointmentID,
                    PatientID = histPatient.PatientID,
                    DoctorID = histDoc.DoctorID,
                    PreliminaryDiagnosis = "Sức khỏe bình thường",
                    Notes = "Duy trì",
                    Status = "completed",
                    ExaminationDate = lastMonth
                };
                context.Examinations.Add(histExam);
                context.SaveChanges();

                // Add Record for history
                context.MedicalRecords.Add(new MedicalRecords
                {
                    PatientID = histPatient.PatientID,
                    ExaminationID = histExam.ExaminationID,
                    Diagnosis = "Sức khỏe bình thường",
                    TreatmentPlan = "Không",
                    RecordDate = lastMonth,
                    CreatedAt = lastMonth
                });

                context.Payments.Add(new Payments
                {
                    PatientID = histPatient.PatientID,
                    AppointmentID = histApp.AppointmentID,
                    Amount = 500000,
                    PaymentMethod = "Bank Transfer",
                    PaymentStatus = "completed",
                    PaymentType = "Examination",
                    PaymentDate = lastMonth,
                    TransactionID = $"VN-{lastMonth.Ticks}"
                });
                context.SaveChanges();
            }
        }
    }
}
