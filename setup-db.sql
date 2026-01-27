USE master;
GO

-- Xóa database nếu đã tồn tại để khởi tạo mới
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'HospitalManagement')
BEGIN
    ALTER DATABASE HospitalManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HospitalManagement;
END
GO

CREATE DATABASE HospitalManagement;
GO

USE HospitalManagement;
GO

-- 1. Bảng Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Phone VARCHAR(15) UNIQUE NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('patient', 'doctor', 'admin')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    Status VARCHAR(20) DEFAULT 'active'
);

-- 2. Bảng Departments
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Location NVARCHAR(200),
    Phone VARCHAR(15),
    HeadDoctorID INT, -- Sẽ thêm FK sau khi bảng Doctors được tạo
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 3. Bảng Patients
CREATE TABLE Patients (
    PatientID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    DateOfBirth DATE,
    Gender NVARCHAR(10) CHECK (Gender IN (N'Nam', N'Nữ', N'Khác')),
    Address NVARCHAR(255),
    InsuranceNumber VARCHAR(50),
    EmergencyContact NVARCHAR(100),
    EmergencyPhone VARCHAR(15),
    BloodType VARCHAR(5),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 4. Bảng Doctors
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Specialization NVARCHAR(100) NOT NULL,
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    LicenseNumber VARCHAR(50) UNIQUE NOT NULL,
    YearsOfExperience INT,
    Qualifications NVARCHAR(255),
    ConsultationFee DECIMAL(10,2) DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Thêm Foreign Key từ Departments tới Doctors (Tránh vòng lặp khi tạo bảng)
ALTER TABLE Departments 
ADD CONSTRAINT FK_Departments_HeadDoctor 
FOREIGN KEY (HeadDoctorID) REFERENCES Doctors(DoctorID);

-- 5. Bảng Shifts
CREATE TABLE Shifts (
    ShiftID INT PRIMARY KEY IDENTITY(1,1),
    ShiftName NVARCHAR(50) NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    MaxSlots INT DEFAULT 100,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 6. Bảng DoctorSchedules
CREATE TABLE DoctorSchedules (
    ScheduleID INT PRIMARY KEY IDENTITY(1,1),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    ShiftID INT FOREIGN KEY REFERENCES Shifts(ShiftID),
    ScheduleDate DATE NOT NULL,
    AvailableSlots INT,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UNIQUE(DoctorID, ScheduleDate, ShiftID)
);

-- 7. Bảng Appointments
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    ScheduleID INT FOREIGN KEY REFERENCES DoctorSchedules(ScheduleID),
    AppointmentDate DATE NOT NULL,
    ShiftID INT FOREIGN KEY REFERENCES Shifts(ShiftID),
    AppointmentNumber INT NOT NULL,
    Symptoms NVARCHAR(500),
    Status VARCHAR(20) DEFAULT 'pending' CHECK (Status IN ('pending', 'confirmed', 'cancelled', 'completed', 'no-show')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- 8. Bảng Examinations
CREATE TABLE Examinations (
    ExaminationID INT PRIMARY KEY IDENTITY(1,1),
    AppointmentID INT FOREIGN KEY REFERENCES Appointments(AppointmentID),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    ExaminationDate DATETIME DEFAULT GETDATE(),
    Symptoms NVARCHAR(500),
    PreliminaryDiagnosis NVARCHAR(1000),
    Notes NVARCHAR(MAX),
    Status VARCHAR(20) DEFAULT 'in_progress' CHECK (Status IN ('in_progress', 'waiting_results', 'completed')),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 9. Bảng MedicalServices
CREATE TABLE MedicalServices (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(100) NOT NULL,
    ServiceType VARCHAR(50),
    Description NVARCHAR(500),
    Price DECIMAL(10,2) NOT NULL,
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    EstimatedTime INT,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 10. Bảng ServiceRequests
CREATE TABLE ServiceRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    ExaminationID INT FOREIGN KEY REFERENCES Examinations(ExaminationID),
    ServiceID INT FOREIGN KEY REFERENCES MedicalServices(ServiceID),
    RequestNumber INT NOT NULL,
    Status VARCHAR(20) DEFAULT 'requested' CHECK (Status IN ('requested', 'paid', 'in_progress', 'completed', 'cancelled')),
    RequestedAt DATETIME DEFAULT GETDATE(),
    CompletedAt DATETIME
);

-- 11. Bảng ServiceResults
CREATE TABLE ServiceResults (
    ResultID INT PRIMARY KEY IDENTITY(1,1),
    RequestID INT FOREIGN KEY REFERENCES ServiceRequests(RequestID),
    ServiceID INT FOREIGN KEY REFERENCES MedicalServices(ServiceID),
    ResultDetails NVARCHAR(MAX),
    ResultFile VARCHAR(255),
    PerformedBy INT,
    PerformedAt DATETIME,
    VerifiedBy INT,
    VerifiedAt DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 12. Bảng Medicines
CREATE TABLE Medicines (
    MedicineID INT PRIMARY KEY IDENTITY(1,1),
    MedicineName NVARCHAR(100) NOT NULL,
    GenericName NVARCHAR(100),
    Unit NVARCHAR(20), -- Sửa từ VARCHAR sang NVARCHAR để hỗ trợ tiếng Việt (Viên, Hộp...)
    PricePerUnit DECIMAL(10,2),
    StockQuantity INT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 13. Bảng Payments
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    AppointmentID INT FOREIGN KEY REFERENCES Appointments(AppointmentID),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    PaymentType VARCHAR(20) CHECK (PaymentType IN ('appointment', 'service', 'medicine')),
    ReferenceID INT,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentMethod VARCHAR(20) CHECK (PaymentMethod IN ('cash', 'bank_transfer', 'credit_card', 'ewallet')),
    PaymentStatus VARCHAR(20) DEFAULT 'pending' CHECK (PaymentStatus IN ('pending', 'completed', 'failed', 'refunded')),
    TransactionID VARCHAR(100),
    PaymentDate DATETIME,
    RefundedAmount DECIMAL(10,2) DEFAULT 0,
    RefundedAt DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 14. Bảng Invoices
CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    PaymentID INT FOREIGN KEY REFERENCES Payments(PaymentID),
    InvoiceNumber VARCHAR(50) UNIQUE NOT NULL,
    InvoiceDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    TaxAmount DECIMAL(10,2) DEFAULT 0,
    DiscountAmount DECIMAL(10,2) DEFAULT 0,
    FinalAmount DECIMAL(10,2) NOT NULL,
    InvoiceStatus VARCHAR(20) DEFAULT 'unpaid' CHECK (InvoiceStatus IN ('unpaid', 'paid', 'cancelled')),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 15. Bảng MedicalRecords
CREATE TABLE MedicalRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    ExaminationID INT FOREIGN KEY REFERENCES Examinations(ExaminationID),
    Diagnosis NVARCHAR(1000) NOT NULL,
    TreatmentPlan NVARCHAR(MAX),
    Recommendations NVARCHAR(MAX),
    RecordDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 16. Bảng Prescriptions
CREATE TABLE Prescriptions (
    PrescriptionID INT PRIMARY KEY IDENTITY(1,1),
    RecordID INT FOREIGN KEY REFERENCES MedicalRecords(RecordID),
    MedicineID INT FOREIGN KEY REFERENCES Medicines(MedicineID),
    Dosage NVARCHAR(100) NOT NULL,
    Frequency NVARCHAR(100) NOT NULL,
    Duration INT,
    Quantity INT NOT NULL,
    Instructions NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 17. Bảng MedicalHistory
CREATE TABLE MedicalHistory (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    RecordID INT FOREIGN KEY REFERENCES MedicalRecords(RecordID),
    VisitDate DATETIME NOT NULL,
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    Diagnosis NVARCHAR(1000),
    Treatment NVARCHAR(MAX),
    NextAppointmentDate DATE,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- =============================================
-- DỮ LIỆU MẪU (10 DÒNG MỖI BẢNG)
-- =============================================

-- 1. Users (15 user: 2 admin, 5 doctor, 8 patient)
INSERT INTO Users (Username, Password, Email, Phone, FullName, Role) VALUES
('admin1', '123', 'admin1@hosp.com', '0123456780', N'Nguyễn Quản Trị 1', 'admin'),
('admin2', '123', 'admin2@hosp.com', '0123456781', N'Trần Quản Trị 2', 'admin'),
('doctor1', '123', 'dr1@hosp.com', '0901234001', N'BS. Nguyễn Văn An', 'doctor'),
('doctor2', '123', 'dr2@hosp.com', '0901234002', N'BS. Trần Thị Bình', 'doctor'),
('doctor3', '123', 'dr3@hosp.com', '0901234003', N'BS. Lê Văn Cường', 'doctor'),
('doctor4', '123', 'dr4@hosp.com', '0901234004', N'BS. Phạm Minh Đức', 'doctor'),
('doctor5', '123', 'dr5@hosp.com', '0901234005', N'BS. Hoàng Tuyết Mai', 'doctor'),
('patient1', '123', 'p1@gmail.com', '0801234001', N'Lê Minh Anh', 'patient'),
('patient2', '123', 'p2@gmail.com', '0801234002', N'Nguyễn Hồng Hạnh', 'patient'),
('patient3', '123', 'p3@gmail.com', '0801234003', N'Vũ Gia Bảo', 'patient'),
('patient4', '123', 'p4@gmail.com', '0801234004', N'Đặng Thu Thảo', 'patient'),
('patient5', '123', 'p5@gmail.com', '0801234005', N'Bùi Quốc Toản', 'patient'),
('patient6', '123', 'p6@gmail.com', '0801234006', N'Trương Mỹ Linh', 'patient'),
('patient7', '123', 'p7@gmail.com', '0801234007', N'Phan Văn Khải', 'patient'),
('patient8', '123', 'p8@gmail.com', '0801234008', N'Lý Hải Yến', 'patient');

-- 2. Departments (10 khoa)
INSERT INTO Departments (DepartmentName, Description, Location, Phone) VALUES
(N'Khoa Nội Tổng Quát', N'Điều trị nội khoa tổng hợp', N'Tầng 1 - Khu A', '028111222'),
(N'Khoa Ngoại', N'Phẫu thuật ngoại khoa', N'Tầng 2 - Khu B', '028111333'),
(N'Khoa Nhi', N'Khám nhi đồng', N'Tầng 1 - Khu C', '028111444'),
(N'Khoa Sản', N'Chăm sóc sản phụ', N'Tầng 3 - Khu A', '028111555'),
(N'Khoa Tim Mạch', N'Chẩn đoán và điều trị tim mạch', N'Tầng 2 - Khu A', '028111666'),
(N'Khoa Mắt', N'Khám và phẫu thuật mắt', N'Tầng 4 - Khu B', '028111777'),
(N'Khoa Tai Mũi Họng', N'Khám TMH', N'Tầng 4 - Khu C', '028111888'),
(N'Khoa Răng Hàm Mặt', N'Nha khoa kỹ thuật cao', N'Tầng 5 - Khu B', '028111999'),
(N'Khoa Da Liễu', N'Điều trị các bệnh ngoài da', N'Tầng 3 - Khu C', '028222111'),
(N'Khoa Xét Nghiệm', N'Thực hiện các xét nghiệm y tế', N'Tầng hầm - Khu D', '028222333');

-- 3. Patients (Tương ứng với UserID 8-15)
INSERT INTO Patients (UserID, DateOfBirth, Gender, Address, InsuranceNumber, BloodType) VALUES
(8, '1995-10-10', N'Nam', N'123 CMT8, Q3, TP.HCM', 'HI001', 'O+'),
(9, '1990-05-20', N'Nữ', N'456 Lý Tự Trọng, Q1, TP.HCM', 'HI002', 'A+'),
(10, '2000-01-15', N'Nam', N'789 Nguyễn Huệ, Q1, TP.HCM', 'HI003', 'B+'),
(11, '1988-12-30', N'Nữ', N'321 Lê Lợi, Q.Gò Vấp, TP.HCM', 'HI004', 'AB+'),
(12, '1975-03-25', N'Nam', N'159 Điện Biên Phủ, Q.Bình Thạnh', 'HI005', 'O-'),
(13, '1992-07-08', N'Nữ', N'753 Trần Hưng Đạo, Q5, TP.HCM', 'HI006', 'A-'),
(14, '1982-11-12', N'Nam', N'456 Kinh Dương Vương, Q6, TP.HCM', 'HI007', 'B-'),
(15, '1998-09-05', N'Nữ', N'123 Nguyễn Văn Linh, Q7, TP.HCM', 'HI008', 'O+'),
(10, '2005-02-14', N'Nam', N'101 Cộng Hòa, Q.Tân Bình', 'HI009', 'A+'),
(11, '1970-06-21', N'Nữ', N'202 Võ Văn Ngân, TP.Thủ Đức', 'HI010', 'AB-');

-- 4. Doctors (Tương ứng với UserID 3-7)
INSERT INTO Doctors (UserID, Specialization, DepartmentID, LicenseNumber, YearsOfExperience, ConsultationFee) VALUES
(3, N'Nội tổng quát', 1, 'LIC001', 15, 200000),
(4, N'Ngoại lồng ngực', 2, 'LIC002', 10, 300000),
(5, N'Nhi khoa', 3, 'LIC003', 8, 150000),
(6, N'Sản phụ khoa', 4, 'LIC004', 12, 250000),
(7, N'Tim mạch viện nhân dân', 5, 'LIC005', 20, 500000),
(3, N'Nội tiêu hóa', 1, 'LIC006', 15, 200000),
(4, N'Chấn thương chỉnh hình', 2, 'LIC007', 9, 300000),
(5, N'Nhi sơ sinh', 3, 'LIC008', 7, 150000),
(6, N'Hiếm muộn', 4, 'LIC009', 11, 400000),
(7, N'Can thiệp tim mạch', 5, 'LIC010', 18, 600000);

-- Cập nhật Trưởng khoa
UPDATE Departments SET HeadDoctorID = 1 WHERE DepartmentID = 1;
UPDATE Departments SET HeadDoctorID = 2 WHERE DepartmentID = 2;
UPDATE Departments SET HeadDoctorID = 3 WHERE DepartmentID = 3;
UPDATE Departments SET HeadDoctorID = 4 WHERE DepartmentID = 4;
UPDATE Departments SET HeadDoctorID = 5 WHERE DepartmentID = 5;

-- 5. Shifts
INSERT INTO Shifts (ShiftName, StartTime, EndTime, MaxSlots) VALUES
(N'Sáng 1', '07:30:00', '09:30:00', 20),
(N'Sáng 2', '09:30:00', '11:30:00', 20),
(N'Chiều 1', '13:30:00', '15:30:00', 20),
(N'Chiều 2', '15:30:00', '17:30:00', 20),
(N'Tối', '18:00:00', '21:00:00', 30),
(N'Sáng cuối tuần', '08:00:00', '11:00:00', 40),
(N'Trực đêm', '21:00:00', '07:00:00', 10),
(N'Sáng sớm', '06:00:00', '07:30:00', 10),
(N'Nghỉ trưa', '11:30:00', '13:30:00', 0),
(N'Hành chính', '08:00:00', '17:00:00', 50);

-- 6. DoctorSchedules
SET IDENTITY_INSERT DoctorSchedules ON;
INSERT INTO DoctorSchedules (ScheduleID, DoctorID, DepartmentID, ShiftID, ScheduleDate, AvailableSlots) VALUES
(1, 1, 1, 1, '2026-02-01', 19),
(2, 2, 2, 3, '2026-02-01', 20),
(3, 3, 3, 1, '2026-02-01', 15),
(4, 4, 4, 3, '2026-02-01', 10),
(5, 5, 5, 5, '2026-02-01', 30),
(6, 6, 1, 2, '2026-02-02', 20),
(7, 7, 2, 4, '2026-02-02', 20),
(8, 8, 3, 2, '2026-02-02', 20),
(9, 9, 4, 4, '2026-02-02', 20),
(10, 10, 5, 10, '2026-02-02', 50),
(11, 1, 1, 1, '2026-01-27', 20),
(12, 1, 1, 2, '2026-01-27', 20),
(13, 2, 2, 3, '2026-01-27', 20),
(14, 9, 9, 1, '2026-01-27', 20),
(15, 9, 9, 2, '2026-01-27', 20),
(16, 9, 9, 3, '2026-01-28', 20),
(17, 9, 9, 4, '2026-01-28', 20);
SET IDENTITY_INSERT DoctorSchedules OFF;

-- 7. Appointments
INSERT INTO Appointments (PatientID, DoctorID, DepartmentID, ScheduleID, AppointmentDate, ShiftID, AppointmentNumber, Symptoms, Status) VALUES
(1, 1, 1, 1, '2026-02-01', 1, 1, N'Đau bụng âm ỉ', 'confirmed'),
(2, 3, 3, 3, '2026-02-01', 1, 1, N'Bé sốt cao', 'pending'),
(3, 1, 1, 1, '2026-02-01', 1, 2, N'Kiểm tra sức khỏe định kỳ', 'confirmed'),
(4, 2, 2, 2, '2026-02-01', 3, 1, N'Đau khớp gối', 'cancelled'),
(5, 5, 5, 5, '2026-02-01', 5, 1, N'Đau thắt ngực', 'completed'),
(6, 6, 1, 6, '2026-02-02', 2, 1, N'Ho khan kéo dài', 'pending'),
(7, 7, 2, 7, '2026-02-02', 4, 1, N'Nhức đầu chóng mặt', 'confirmed'),
(8, 8, 3, 8, '2026-02-02', 2, 1, N'Trẻ biếng ăn', 'pending'),
(9, 9, 4, 9, '2026-02-02', 4, 1, N'Khám thai định kỳ', 'confirmed'),
(10, 10, 5, 10, '2026-02-02', 10, 1, N'Nhịp tim không đều', 'completed');

-- 8. Examinations
INSERT INTO Examinations (AppointmentID, DoctorID, PatientID, Symptoms, PreliminaryDiagnosis, Status) VALUES
(1, 1, 1, N'Đau bụng âm ỉ', N'Viêm dạ dày', 'in_progress'),
(3, 1, 3, N'Không symptoms', N'Cần xét nghiệm máu tổng quát', 'waiting_results'),
(5, 5, 5, N'Đau thắt ngực', N'Thiếu máu cơ tim', 'completed'),
(7, 7, 7, N'Nhức đầu', N'Rối loạn tiền đình', 'in_progress'),
(10, 10, 10, N'Tim đập nhanh', N'Hở van tim 2 lá', 'completed'),
(1, 1, 1, N'Đau bụng tăng', N'Theo dõi viêm ruột thừa', 'in_progress'),
(3, 1, 3, N'Mệt mỏi', N'Suy nhược cơ thể', 'waiting_results'),
(5, 5, 5, N'Khó thở', N'Hen phế quản', 'completed'),
(7, 7, 7, N'Hoa mắt', N'Thiếu máu não', 'in_progress'),
(10, 10, 10, N'Hồi hộp', N'Loạn nhịp tim', 'completed');

-- 9. MedicalServices
INSERT INTO MedicalServices (ServiceName, ServiceType, Description, Price, DepartmentID, EstimatedTime) VALUES
(N'Xét nghiệm máu', 'xet_nghiem', N'Xét nghiệm công thức máu toàn phần', 150000, 10, 30),
(N'Siêu âm bụng', 'sieu_am', N'Siêu âm ổ bụng tổng quát', 250000, 10, 20),
(N'Chụp X-quang phổi', 'chup_xquang', N'Chụp X-quang kỹ thuật cao', 200000, 10, 15),
(N'Nội soi dạ dày', 'khac', N'Nội soi không đau', 1500000, 1, 60),
(N'Xét nghiệm nước tiểu', 'xet_nghiem', N'Kiểm tra định kỳ', 100000, 10, 20),
(N'Điện tâm đồ (ECG)', 'khac', N'Đo hoạt động điện của tim', 120000, 5, 15),
(N'Chụp CT Scanner', 'chup_xquang', N'Chụp cắt lớp vi tính', 2500000, 10, 45),
(N'MRI Não', 'chup_xquang', N'Chụp cộng hưởng từ', 3500000, 10, 60),
(N'Xét nghiệm tiểu đường', 'xet_nghiem', N'Đo đường huyết nhanh', 80000, 10, 10),
(N'Siêu âm tim', 'sieu_am', N'Khảo sát chức năng tim', 400000, 5, 30);

-- 10. ServiceRequests
INSERT INTO ServiceRequests (ExaminationID, ServiceID, RequestNumber, Status) VALUES
(1, 4, 1, 'in_progress'),
(2, 1, 1, 'requested'),
(3, 6, 1, 'completed'),
(4, 8, 1, 'requested'),
(5, 10, 1, 'completed'),
(6, 2, 1, 'requested'),
(7, 7, 1, 'paid'),
(8, 3, 1, 'completed'),
(9, 5, 1, 'paid'),
(10, 6, 2, 'completed');

-- 11. ServiceResults
INSERT INTO ServiceResults (RequestID, ServiceID, ResultDetails, PerformedBy, VerifiedAt) VALUES
(3, 6, N'Nhịp tim xoang đều, tần số 75ck/phút', 5, GETDATE()),
(5, 10, N'Hở van 2 lá nhẹ 1/4', 5, GETDATE()),
(8, 3, N'Hình ảnh phổi bình thường', 10, GETDATE()),
(10, 6, N'Rung nhĩ nhẹ', 5, GETDATE()),
(3, 6, N'Lần test lại: Ổn định', 5, GETDATE()),
(5, 10, N'Chức năng tâm thu thất trái bảo tồn', 5, GETDATE()),
(8, 3, N'Không thấy tổn thương khu trú', 10, GETDATE()),
(10, 6, N'Ngoại tâm thu thất thưa', 5, GETDATE()),
(3, 6, N'Kết quả bình thường', 5, GETDATE()),
(10, 6, N'Nhịp nhanh xoang', 5, GETDATE());

-- 12. Medicines
INSERT INTO Medicines (MedicineName, GenericName, Unit, PricePerUnit, StockQuantity) VALUES
(N'Paracetamol 500mg', 'Paracetamol', N'Viên', 1500, 1000),
(N'Amoxicillin 250mg', 'Amoxicillin', N'Hộp', 45000, 200),
(N'Panadol Extra', 'Paracetamol+Caffeine', N'Viên', 2500, 500),
(N'Gaviscon 10ml', 'Sodium alginate', N'Gói', 12000, 300),
(N'Berberin', 'Berberine', N'Lọ', 20000, 150),
(N'Vitamin C 1g', 'Acid Ascorbic', N'Viên sủi', 5000, 400),
(N'Nurofen 200mg', 'Ibuprofen', N'Viên', 6000, 300),
(N'Decolgen', 'Paracetamol++', N'Vỉ', 15000, 250),
(N'Efferalgan 500', 'Paracetamol', N'Viên', 2000, 600),
(N'V-Rohto', 'Eye drops', N'Chai', 55000, 100);

-- 13. Payments
INSERT INTO Payments (AppointmentID, PatientID, PaymentType, Amount, PaymentMethod, PaymentStatus) VALUES
(1, 1, 'appointment', 200000, 'cash', 'pending'),
(5, 5, 'appointment', 500000, 'bank_transfer', 'completed'),
(10, 10, 'appointment', 600000, 'credit_card', 'completed'),
(7, 7, 'service', 2500000, 'bank_transfer', 'completed'),
(9, 9, 'service', 100000, 'cash', 'completed'),
(1, 1, 'medicine', 150000, 'cash', 'completed'),
(5, 5, 'medicine', 300000, 'ewallet', 'completed'),
(10, 10, 'medicine', 120000, 'cash', 'completed'),
(3, 3, 'appointment', 200000, 'cash', 'completed'),
(2, 2, 'appointment', 150000, 'cash', 'pending');

-- 14. Invoices
INSERT INTO Invoices (PaymentID, InvoiceNumber, TotalAmount, TaxAmount, FinalAmount, InvoiceStatus) VALUES
(1, 'INV2026001', 200000, 0, 200000, 'unpaid'),
(2, 'INV2026002', 500000, 0, 500000, 'paid'),
(3, 'INV2026003', 600000, 30000, 630000, 'paid'),
(4, 'INV2026004', 2500000, 0, 2500000, 'paid'),
(5, 'INV2026005', 100000, 0, 100000, 'paid'),
(6, 'INV2026006', 150000, 0, 150000, 'paid'),
(7, 'INV2026007', 300000, 0, 300000, 'paid'),
(8, 'INV2026008', 120000, 0, 120000, 'paid'),
(9, 'INV2026009', 200000, 10000, 210000, 'paid'),
(10, 'INV2026010', 150000, 0, 150000, 'unpaid');

-- 15. MedicalRecords
INSERT INTO MedicalRecords (PatientID, ExaminationID, Diagnosis, TreatmentPlan) VALUES
(1, 1, N'Viêm dạ dày cấp', N'Dùng thuốc 1 tuần, kiêng cay nóng'),
(3, 2, N'Suy nhược cơ thể nhẹ', N'Bổ sung vitamin và nghỉ ngơi'),
(5, 3, N'Bệnh tim thiếu máu cục bộ', N'Uống thuốc hàng ngày, tái khám sau 1 tháng'),
(7, 4, N'Rối loạn tiền đình', N'Vật lý trị liệu và dùng hoạt huyết'),
(10, 5, N'Hở van tim 2 lá 1/4', N'Theo dõi định kỳ, giảm vận động mạnh'),
(1, 6, N'Trào ngược dạ dày THỰC QUẢN', N'Kết hợp thuốc và thay đổi thói quen ăn uống'),
(3, 7, N'Thiếu máu nhẹ', N'Ăn nhiều thực phẩm giàu sắt'),
(5, 8, N'Viêm phế quan co thắt', N'Sử dụng thuốc xịt giãn phế quản'),
(7, 9, N'Thiếu máu não', N'Dùng thuốc tuần hoàn não'),
(10, 10, N'Rối loạn nhịp tim', N'Điều chỉnh thuốc ổn định nhịp');

-- 16. Prescriptions
INSERT INTO Prescriptions (RecordID, MedicineID, Dosage, Frequency, Duration, Quantity) VALUES
(1, 1, N'500mg', N'2 lần/ngày', 7, 14),
(1, 4, N'1 gói', N'3 lần/ngày', 7, 21),
(5, 9, N'1 viên', N'1 lần/ngày', 30, 30),
(2, 6, N'1 viên', N'1 lần/ngày', 10, 10),
(3, 3, N'500mg', N'2 lần/ngày', 5, 10),
(6, 4, N'1 gói', N'2 lần/ngày', 14, 28),
(8, 7, N'200mg', N'3 lần/ngày', 3, 9),
(10, 1, N'500mg', N'2 lần/ngày', 5, 10),
(4, 5, N'2 viên', N'2 lần/ngày', 5, 20),
(9, 8, N'1 vỉ', N'Tự uống khi đau', 2, 2);

-- 17. MedicalHistory
INSERT INTO MedicalHistory (PatientID, RecordID, VisitDate, DoctorID, Diagnosis, Treatment) VALUES
(1, 1, '2026-02-01', 1, N'Viêm dạ dày cấp', N'Uống thuốc theo đơn'),
(3, 2, '2026-02-01', 1, N'Suy nhược cơ thể', N'Nghỉ ngơi 3 ngày'),
(5, 3, '2026-02-01', 5, N'Bệnh tim thiếu máu', N'Uống thuốc và theo dõi'),
(7, 4, '2026-02-02', 7, N'Rối loạn tiền đình', N'Hoạt huyết dưỡng não'),
(10, 5, '2026-02-02', 10, N'Hở van tim 2 lá', N'Tái khám sau 3 tháng'),
(1, 6, '2026-02-05', 1, N'Khám lại dạ dày', N'Tiếp tục liệu trình'),
(5, 8, '2026-01-20', 5, N'Viêm phế quản cũ', N'Đã điều trị ổn định'),
(10, 10, '2026-01-15', 10, N'Loạn nhịp cũ', N'Duy trì thuốc'),
(1, 1, '2026-02-01', 1, N'Khám đầu tiên', N'Chưa rõ'),
(7, 9, '2026-02-02', 7, N'Thiếu máu não', N'Uống thuốc 10 ngày');

PRINT N'✅ Database HospitalManagement and full sample data initialized successfully!';
GO
