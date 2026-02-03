USE master;
GO

-- 1. Khởi tạo Database HospitalManagement_New (Khớp với App.config)
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'HospitalManagement_New')
BEGIN
    ALTER DATABASE HospitalManagement_New SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HospitalManagement_New;
END
GO

CREATE DATABASE HospitalManagement_New;
GO

USE HospitalManagement_New;
GO

-- 2. TẠO CẤU TRÚC BẢNG ĐẦY ĐỦ (Merge Schema + User Data)

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Phone VARCHAR(15) UNIQUE NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('patient', 'doctor', 'admin', 'pharmacist')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    Status VARCHAR(20) DEFAULT 'active'
);

CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Location NVARCHAR(200),
    Phone VARCHAR(15),
    HeadDoctorID INT, -- FK nối sau
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Patients (
    PatientID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Address NVARCHAR(255),
    InsuranceNumber VARCHAR(50),
    EmergencyContact NVARCHAR(100),
    EmergencyPhone VARCHAR(15),
    BloodType VARCHAR(5),
    CreatedAt DATETIME DEFAULT GETDATE()
);

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
    CreatedAt DATETIME DEFAULT GETDATE(),
    MinShiftsPerMonth INT DEFAULT 15,
    MaxShiftsPerMonth INT DEFAULT 25,
    DefaultRoom NVARCHAR(50)
);

ALTER TABLE Departments ADD CONSTRAINT FK_Departments_HeadDoctor 
FOREIGN KEY (HeadDoctorID) REFERENCES Doctors(DoctorID);

CREATE TABLE Shifts (
    ShiftID INT PRIMARY KEY IDENTITY(1,1),
    ShiftName NVARCHAR(50) NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    MaxSlots INT DEFAULT 20,
    MinDoctorsPerShift INT DEFAULT 2,
    MaxDoctorsPerShift INT DEFAULT 5,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE DoctorSchedules (
    ScheduleID INT PRIMARY KEY IDENTITY(1,1),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    ShiftID INT FOREIGN KEY REFERENCES Shifts(ShiftID),
    ScheduleDate DATE NOT NULL,
    AvailableSlots INT,
    IsActive BIT DEFAULT 1,
    Status VARCHAR(20) DEFAULT 'Approved',
    RequestedAt DATETIME DEFAULT GETDATE(),
    ApprovedByUserID INT FOREIGN KEY REFERENCES Users(UserID),
    ApprovedAt DATETIME,
    RejectionReason NVARCHAR(500),
    RoomNumber NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    ScheduleID INT FOREIGN KEY REFERENCES DoctorSchedules(ScheduleID),
    ShiftID INT FOREIGN KEY REFERENCES Shifts(ShiftID),
    AppointmentDate DATE NOT NULL,
    AppointmentNumber INT,
    Symptoms NVARCHAR(500),
    Status VARCHAR(20) DEFAULT 'pending',
    RoomNumber NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    AppointmentID INT FOREIGN KEY REFERENCES Appointments(AppointmentID),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    PaymentType VARCHAR(20),
    ReferenceID INT,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentMethod VARCHAR(20),
    PaymentStatus VARCHAR(20) DEFAULT 'pending',
    TransactionID VARCHAR(100),
    PaymentDate DATETIME,
    RefundedAmount DECIMAL(18,2) DEFAULT 0,
    RefundedAt DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    PaymentID INT FOREIGN KEY REFERENCES Payments(PaymentID),
    InvoiceNumber VARCHAR(50) UNIQUE NOT NULL,
    InvoiceDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2),
    DiscountAmount DECIMAL(18,2) DEFAULT 0,
    TaxAmount DECIMAL(18,2) DEFAULT 0,
    FinalAmount DECIMAL(18,2),
    InvoiceStatus VARCHAR(20) DEFAULT 'unpaid',
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Examinations (
    ExaminationID INT PRIMARY KEY IDENTITY(1,1),
    AppointmentID INT FOREIGN KEY REFERENCES Appointments(AppointmentID),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    Symptoms NVARCHAR(MAX),
    PreliminaryDiagnosis NVARCHAR(MAX),
    ExaminationDate DATETIME DEFAULT GETDATE(),
    Status VARCHAR(20) DEFAULT 'in_progress',
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE MedicalRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    ExaminationID INT FOREIGN KEY REFERENCES Examinations(ExaminationID),
    RecordDate DATETIME DEFAULT GETDATE(),
    Diagnosis NVARCHAR(MAX),
    TreatmentPlan NVARCHAR(MAX),
    Recommendations NVARCHAR(MAX),
    CreatedBy INT,
    CreatedAt DATETIME DEFAULT GETDATE()
);

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

CREATE TABLE MedicalServices (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(200) NOT NULL,
    ServiceType VARCHAR(50),
    Description NVARCHAR(500),
    Price DECIMAL(18,2),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    EstimatedTime INT,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE ServiceRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    ExaminationID INT FOREIGN KEY REFERENCES Examinations(ExaminationID),
    ServiceID INT FOREIGN KEY REFERENCES MedicalServices(ServiceID),
    RequestNumber INT,
    Status VARCHAR(20) DEFAULT 'requested',
    RequestedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE ServiceResults (
    ResultID INT PRIMARY KEY IDENTITY(1,1),
    RequestID INT FOREIGN KEY REFERENCES ServiceRequests(RequestID),
    ServiceID INT FOREIGN KEY REFERENCES MedicalServices(ServiceID),
    ResultDetails NVARCHAR(MAX),
    ResultFile VARCHAR(255),
    PerformedBy INT,
    VerifiedAt DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Medicines (
    MedicineID INT PRIMARY KEY IDENTITY(1,1),
    MedicineName NVARCHAR(200) NOT NULL,
    GenericName NVARCHAR(200),
    Unit VARCHAR(50),
    PricePerUnit DECIMAL(18,2),
    StockQuantity INT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Prescriptions (
    PrescriptionID INT PRIMARY KEY IDENTITY(1,1),
    RecordID INT FOREIGN KEY REFERENCES MedicalRecords(RecordID),
    MedicineID INT FOREIGN KEY REFERENCES Medicines(MedicineID),
    Dosage NVARCHAR(200),
    Frequency NVARCHAR(200),
    Duration INT,
    Quantity INT,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- 3. NẠP DỮ LIỆU CỦA USER (Tối ưu hóa thứ tự)

-- 3.1. Users
INSERT INTO Users (Username, Password, Email, Phone, FullName, Role, Status) VALUES
('admin1', '123', 'admin1@hosp.com', '0123456780', N'Nguyễn Quản Trị 1', 'admin', 'active'),
('admin2', '123', 'admin2@hosp.com', '0123456781', N'Trần Quản Trị 2', 'admin', 'active'),
('doctor1', '123', 'dr1@hosp.com', '0901234001', N'BS. Nguyễn Văn An', 'doctor', 'active'),
('doctor2', '123', 'dr2@hosp.com', '0901234002', N'BS. Trần Thị Bình', 'doctor', 'active'),
('doctor3', '123', 'dr3@hosp.com', '0901234003', N'BS. Lê Văn Cường', 'doctor', 'active'),
('doctor4', '123', 'dr4@hosp.com', '0901234004', N'BS. Phạm Minh Đức', 'doctor', 'active'),
('doctor5', '123', 'dr5@hosp.com', '0901234005', N'BS. Hoàng Tuyết Mai', 'doctor', 'active'),
('patient1', '123', 'p1@gmail.com', '0801234001', N'Lê Minh Anh', 'patient', 'active'),
('patient2', '123', 'p2@gmail.com', '0801234002', N'Nguyễn Hồng Hạnh', 'patient', 'active'),
('patient3', '123', 'p3@gmail.com', '0801234003', N'Vũ Gia Bảo', 'patient', 'active'),
('patient4', '123', 'p4@gmail.com', '0801234004', N'Đặng Thu Thảo', 'patient', 'active'),
('patient5', '123', 'p5@gmail.com', '0801234005', N'Bùi Quốc Toản', 'patient', 'active'),
('patient6', '123', 'p6@gmail.com', '0801234006', N'Trương Mỹ Linh', 'patient', 'active'),
('patient7', '123', 'p7@gmail.com', '0801234007', N'Phan Văn Khải', 'patient', 'active'),
('patient8', '123', 'p8@gmail.com', '0801234008', N'Lý Hải Yến', 'patient', 'active'),
('doctor6', '123', 'dr6@hosp.com', '0901234006', N'BS. Ngô Bá Khá', 'doctor', 'active'),
('doctor7', '123', 'dr7@hosp.com', '0901234007', N'BS. Lê Tùng Vân', 'doctor', 'active'),
('doctor8', '123', 'dr8@hosp.com', '0901234008', N'BS. Phạm Nhật Vượng', 'doctor', 'active'),
('doctor9', '123', 'dr9@hosp.com', '0901234009', N'BS. Trương Mỹ Lan', 'doctor', 'active'),
('doctor10', '123', 'dr10@hosp.com', '0901234010', N'BS. Đỗ Anh Dũng', 'doctor', 'active'),
('pharmacist1', '123', 'ph1@hosp.com', '0701234001', N'Dược sĩ Lê Thị Hà', 'pharmacist', 'active'),
('newuser', '123', 'new@gmail.com', '0999888777', N'Nguyễn Văn Người Mới', 'patient', 'active');

-- 3.2. Departments
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

-- 3.3. Doctors
INSERT INTO Doctors (UserID, Specialization, DepartmentID, LicenseNumber, YearsOfExperience, ConsultationFee, IsActive, DefaultRoom) VALUES
(3, N'Nội tổng quát', 1, 'LIC001', 15, 200000, 1, N'P.101'),
(4, N'Ngoại lồng ngực', 2, 'LIC002', 10, 300000, 1, N'P.202'),
(5, N'Nhi khoa', 3, 'LIC003', 8, 150000, 1, N'P.105'),
(6, N'Sản phụ khoa', 4, 'LIC004', 12, 250000, 1, N'P.301'),
(7, N'Tim mạch', 5, 'LIC005', 20, 500000, 1, N'P.201'),
(16, N'Mắt kĩ thuật cao', 6, 'LIC011', 10, 200000, 1, N'P.402'),
(17, N'Tai Mũi Họng', 7, 'LIC012', 12, 180000, 1, N'P.405'),
(18, N'Răng Hàm Mặt', 8, 'LIC013', 15, 250000, 1, N'P.502'),
(19, N'Da Liễu chuyên sâu', 9, 'LIC014', 20, 300000, 1, N'P.301'),
(20, N'Xét nghiệm', 10, 'LIC015', 8, 150000, 1, N'P.005');

-- 3.4. Patients
INSERT INTO Patients (UserID, DateOfBirth, Gender, Address, InsuranceNumber, BloodType) VALUES
(8, '1995-10-10', N'Nam', N'123 CMT8, Q3, TP.HCM', 'HI001', 'O+'),
(9, '1990-05-20', N'Nữ', N'456 Lý Tự Trọng, Q1, TP.HCM', 'HI002', 'A+'),
(10, '2000-01-15', N'Nam', N'789 Nguyễn Huệ, Q1, TP.HCM', 'HI003', 'B+'),
(11, '1988-12-30', N'Nữ', N'321 Lê Lợi, Q.Gò Vấp, TP.HCM', 'HI004', 'AB+'),
(12, '1975-03-25', N'Nam', N'159 Điện Biên Phủ, Q.Bình Thạnh', 'HI005', 'O-'),
(13, '1992-07-08', N'Nữ', N'753 Trần Hưng Đạo, Q5, TP.HCM', 'HI006', 'A-'),
(14, '1982-11-12', N'Nam', N'456 Kinh Dương Vương, Q6, TP.HCM', 'HI007', 'B-'),
(15, '1998-09-05', N'Nữ', N'123 Nguyễn Văn Linh, Q7, TP.HCM', 'HI008', 'O+');

-- 3.5. Shifts
INSERT INTO Shifts (ShiftName, StartTime, EndTime, MaxSlots) VALUES
(N'Sáng 1', '07:30:00', '09:30:00', 20),
(N'Sáng 2', '09:30:00', '11:30:00', 20),
(N'Chiều 1', '13:30:00', '15:30:00', 20),
(N'Chiều 2', '15:30:00', '17:30:00', 20);

-- 3.6. DoctorSchedules (Cho ngày hiện tại 03/02/2026)
INSERT INTO DoctorSchedules (DoctorID, DepartmentID, ShiftID, ScheduleDate, AvailableSlots, IsActive, Status, RoomNumber) VALUES
(1, 1, 1, '2026-02-03', 20, 1, 'Approved', N'P.101'),
(1, 1, 2, '2026-02-03', 20, 1, 'Approved', N'P.101'),
(2, 2, 3, '2026-02-03', 20, 1, 'Approved', N'P.202'),
(3, 3, 1, '2026-02-03', 20, 1, 'Approved', N'P.105'),
(4, 4, 3, '2026-02-03', 20, 1, 'Approved', N'P.301'),
(5, 5, 1, '2026-02-03', 20, 1, 'Approved', N'P.201'),
(6, 6, 1, '2026-02-03', 20, 1, 'Approved', N'P.402'),
(7, 7, 1, '2026-02-03', 20, 1, 'Approved', N'P.405'),
(8, 8, 1, '2026-02-03', 20, 1, 'Approved', N'P.502'),
(9, 9, 1, '2026-02-03', 20, 1, 'Approved', N'P.301'),
(10, 10, 1, '2026-02-03', 20, 1, 'Approved', N'P.005');

-- 3.7. MedicalServices
INSERT INTO MedicalServices (ServiceName, ServiceType, Description, Price, DepartmentID, EstimatedTime, IsActive) VALUES
(N'Xét nghiệm máu', 'xet_nghiem', N'Công thức máu', 150000, 10, 30, 1),
(N'Siêu âm bụng', 'sieu_am', N'Siêu âm tổng quát', 250000, 10, 20, 1);

-- 3.8. Medicines
INSERT INTO Medicines (MedicineName, GenericName, Unit, PricePerUnit, StockQuantity, IsActive) VALUES
(N'Paracetamol 500mg', 'Paracetamol', N'Viên', 1500, 1000, 1);

-- Cập nhật Trưởng khoa
UPDATE Departments SET HeadDoctorID = 1 WHERE DepartmentID = 1;

GO
PRINT N'✅ Database HospitalManagement_New đã hoàn tất với dữ liệu của bạn!';