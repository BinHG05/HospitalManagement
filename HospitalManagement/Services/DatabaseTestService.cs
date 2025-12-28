using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Services
{
    public class DatabaseTestService
    {
        private StringBuilder _log = new StringBuilder();

        public string GetLog() => _log.ToString();

        private void Log(string message)
        {
            _log.AppendLine($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        public async Task<bool> RunFullTest()
        {
            _log.Clear();
            Log("Bắt đầu bài test toàn diện...");

            using (var db = new HospitalDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Kiểm tra/Tạo User (Login simulation)
                        Log("Đang kiểm tra User...");
                        var user = db.Users.FirstOrDefault(u => u.Username == "test_user");
                        if (user == null)
                        {
                            user = new Users
                            {
                                Username = "test_user",
                                Password = "password123",
                                Email = "test@example.com",
                                Phone = "0123456789",
                                FullName = "Test User Account",
                                Role = "patient",
                                Status = "active"
                            };
                            db.Users.Add(user);
                            db.SaveChanges();
                            Log("Đã tạo User 'test_user'.");
                        }
                        else
                        {
                            Log($"Đã tìm thấy User: {user.FullName}");
                        }

                        // 2. Kiểm tra/Tạo Patient
                        Log("Đang kiểm tra Patient...");
                        var patient = db.Patients.FirstOrDefault(p => p.UserID == user.UserID);
                        if (patient == null)
                        {
                            patient = new Patients
                            {
                                UserID = user.UserID,
                                Gender = "Male",
                                DateOfBirth = new DateTime(1990, 1, 1),
                                Address = "123 Test St",
                                InsuranceNumber = "INS123456"
                            };
                            db.Patients.Add(patient);
                            db.SaveChanges();
                            Log("Đã tạo hồ sơ bệnh nhân.");
                        }
                        else
                        {
                            Log($"Đã tìm thấy bệnh nhân ID: {patient.PatientID}");
                        }

                        // 3. Chuẩn bị dữ liệu khám (Dept, Doctor, Shift)
                        Log("Đang chuẩn bị dữ liệu phòng ban, bác sĩ...");
                        var dept = db.Departments.FirstOrDefault() ?? new Departments { DepartmentName = "Nội tổng quát", Location = "Tầng 1" };
                        if (dept.DepartmentID == 0) { db.Departments.Add(dept); db.SaveChanges(); }

                        var shift = db.Shifts.FirstOrDefault() ?? new Shifts { ShiftName = "Ca sáng", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(12, 0, 0) };
                        if (shift.ShiftID == 0) { db.Shifts.Add(shift); db.SaveChanges(); }

                        var doctor = db.Doctors.FirstOrDefault() ?? new Doctors 
                        { 
                            UserID = user.UserID, // Reusing test user as doctor for simplicity in test
                            Specialization = "Tổng quát",
                            LicenseNumber = "DOC-TEST-001",
                            ConsultationFee = 100000,
                            DepartmentID = dept.DepartmentID
                        };
                        if (doctor.DoctorID == 0) { db.Doctors.Add(doctor); db.SaveChanges(); }

                        // 4. Đặt lịch (Appointment) - Lấy số thứ tự tiếp theo để tránh trùng (Unique Index)
                        Log("Đang thực hiện Đặt lịch...");
                        int nextNumber = 1;
                        var lastAppointment = db.Appointments
                            .Where(a => a.AppointmentDate == DateTime.Today && a.ScheduleID == null)
                            .OrderByDescending(a => a.AppointmentNumber)
                            .FirstOrDefault();
                        if (lastAppointment != null)
                        {
                            nextNumber = lastAppointment.AppointmentNumber + 1;
                        }

                        var appointment = new Appointments
                        {
                            PatientID = patient.PatientID,
                            DoctorID = doctor.DoctorID,
                            DepartmentID = dept.DepartmentID,
                            AppointmentDate = DateTime.Today,
                            ShiftID = shift.ShiftID,
                            AppointmentNumber = nextNumber,
                            Symptoms = "Đau đầu, chóng mặt (Test)",
                            Status = "confirmed"
                        };
                        db.Appointments.Add(appointment);
                        db.SaveChanges();
                        Log($"Đã tạo lịch hẹn thành công. Số thứ tự: {nextNumber}, ID: {appointment.AppointmentID}");

                        // 5. Khám bệnh (Examination)
                        Log("Đang thực hiện Khám bệnh...");
                        var exam = new Examinations
                        {
                            AppointmentID = appointment.AppointmentID,
                            DoctorID = doctor.DoctorID,
                            PatientID = patient.PatientID,
                            ExaminationDate = DateTime.Now,
                            Symptoms = appointment.Symptoms,
                            PreliminaryDiagnosis = "Suy nhược cơ thể (Test)",
                            Status = "completed"
                        };
                        db.Examinations.Add(exam);
                        db.SaveChanges();
                        Log($"Đã lưu kết quả khám. ID: {exam.ExaminationID}");

                        // 6. Dịch vụ (Medical Service Request)
                        Log("Đang thực hiện đăng ký Dịch vụ...");
                        var service = db.MedicalServices.FirstOrDefault() ?? new MedicalServices 
                        { 
                            ServiceName = "Xét nghiệm máu",
                            Price = 200000,
                            ServiceType = "Lab"
                        };
                        if (service.ServiceID == 0) { db.MedicalServices.Add(service); db.SaveChanges(); }

                        var serviceReq = new ServiceRequests
                        {
                            ExaminationID = exam.ExaminationID,
                            ServiceID = service.ServiceID,
                            RequestNumber = 1,
                            Status = "completed",
                            RequestedAt = DateTime.Now
                        };
                        db.ServiceRequests.Add(serviceReq);
                        db.SaveChanges();
                        Log($"Đã yêu cầu dịch vụ: {service.ServiceName}");

                        // 7. Thanh toán (Payment & Invoice)
                        Log("Đang thực hiện Thanh toán...");
                        var totalAmount = (doctor.ConsultationFee ?? 0) + service.Price;
                        var payment = new Payments
                        {
                            AppointmentID = appointment.AppointmentID,
                            PatientID = patient.PatientID,
                            Amount = totalAmount,
                            PaymentType = "appointment",
                            PaymentMethod = "Cash",
                            PaymentStatus = "completed",
                            PaymentDate = DateTime.Now
                        };
                        db.Payments.Add(payment);
                        db.SaveChanges();

                        var invoice = new Invoices
                        {
                            PaymentID = payment.PaymentID,
                            InvoiceNumber = "INV-" + DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                            InvoiceDate = DateTime.Now,
                            TotalAmount = totalAmount,
                            FinalAmount = totalAmount,
                            InvoiceStatus = "paid"
                        };
                        db.Invoices.Add(invoice);
                        db.SaveChanges();
                        Log($"Đã tạo hóa đơn {invoice.InvoiceNumber} thành công.");

                        // Commit transaction
                        transaction.Commit();
                        Log(">>> TOÀN BỘ QUY TRÌNH TEST THÀNH CÔNG! <<<");
                        Log($"- User: {user.Username}");
                        Log($"- Bệnh nhân: {patient.PatientID}");
                        Log($"- Số thứ tự khám: {nextNumber}");
                        Log($"- Hóa đơn: {invoice.InvoiceNumber}");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        var message = ex.Message;
                        if (ex.InnerException != null)
                        {
                            message += $"\nInner Exception: {ex.InnerException.Message}";
                            if (ex.InnerException.InnerException != null)
                            {
                                message += $"\nRoot Cause: {ex.InnerException.InnerException.Message}";
                            }
                        }
                        Log($"!!! LỖI TRONG QUÁ TRÌNH TEST: {message}");
                        Log(ex.StackTrace);
                        return false;
                    }
                }
            }
        }
    }
}
