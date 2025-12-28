-- =============================================
-- Hospital Management Database - Init Script
-- =============================================
-- Chạy script này để tạo cấu trúc database
-- =============================================

-- Tạo database nếu chưa có
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HospitalManagement')
BEGIN
    CREATE DATABASE HospitalManagement;
END
GO

USE HospitalManagement;
GO

-- =============================================
-- BẢNG USERS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        Password NVARCHAR(255) NOT NULL,
        Email NVARCHAR(100) UNIQUE,
        Phone NVARCHAR(20) UNIQUE,
        FullName NVARCHAR(100),
        Role VARCHAR(20) CHECK (Role IN ('admin', 'doctor', 'patient')) DEFAULT 'patient',
        Status VARCHAR(20) DEFAULT 'active',
        CreatedAt DATETIME DEFAULT GETDATE(),
        UpdatedAt DATETIME DEFAULT GETDATE()
    );
    
    CREATE INDEX IX_Users_Role ON Users(Role);
    CREATE INDEX IX_Users_Status ON Users(Status);
END
GO

-- =============================================
-- BẢNG DEPARTMENTS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Departments')
BEGIN
    CREATE TABLE Departments (
        DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
        DepartmentName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        Location NVARCHAR(100),
        PhoneNumber NVARCHAR(20),
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- BẢNG SHIFTS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Shifts')
BEGIN
    CREATE TABLE Shifts (
        ShiftID INT IDENTITY(1,1) PRIMARY KEY,
        ShiftName NVARCHAR(50) NOT NULL,
        StartTime TIME NOT NULL,
        EndTime TIME NOT NULL,
        Description NVARCHAR(200)
    );
END
GO

-- =============================================
-- BẢNG DOCTORS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Doctors')
BEGIN
    CREATE TABLE Doctors (
        DoctorID INT IDENTITY(1,1) PRIMARY KEY,
        UserID INT FOREIGN KEY REFERENCES Users(UserID),
        DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
        Specialization NVARCHAR(100),
        Qualification NVARCHAR(200),
        ExperienceYears INT,
        ConsultationFee DECIMAL(10,2),
        Bio NVARCHAR(1000),
        IsAvailable BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- BẢNG PATIENTS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Patients')
BEGIN
    CREATE TABLE Patients (
        PatientID INT IDENTITY(1,1) PRIMARY KEY,
        UserID INT FOREIGN KEY REFERENCES Users(UserID),
        DateOfBirth DATE,
        Gender VARCHAR(10),
        Address NVARCHAR(200),
        BloodType VARCHAR(5),
        Allergies NVARCHAR(500),
        EmergencyContact NVARCHAR(100),
        EmergencyPhone NVARCHAR(20),
        InsuranceNumber NVARCHAR(50),
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- BẢNG DOCTOR_SCHEDULES
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DoctorSchedules')
BEGIN
    CREATE TABLE DoctorSchedules (
        ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
        DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
        ShiftID INT FOREIGN KEY REFERENCES Shifts(ShiftID),
        DayOfWeek INT CHECK (DayOfWeek BETWEEN 0 AND 6),
        MaxPatients INT DEFAULT 20,
        IsActive BIT DEFAULT 1
    );
END
GO

-- =============================================
-- BẢNG APPOINTMENTS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Appointments')
BEGIN
    CREATE TABLE Appointments (
        AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
        PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
        DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
        DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
        ScheduleID INT FOREIGN KEY REFERENCES DoctorSchedules(ScheduleID),
        AppointmentDate DATE NOT NULL,
        AppointmentNumber INT,
        Reason NVARCHAR(500),
        Status VARCHAR(20) CHECK (Status IN ('pending', 'confirmed', 'completed', 'cancelled')) DEFAULT 'pending',
        Notes NVARCHAR(1000),
        CreatedAt DATETIME DEFAULT GETDATE(),
        UpdatedAt DATETIME DEFAULT GETDATE()
    );
    
    CREATE INDEX IX_Appointments_Date ON Appointments(AppointmentDate);
    CREATE INDEX IX_Appointments_Status ON Appointments(Status);
END
GO

-- =============================================
-- BẢNG MEDICAL_SERVICES
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MedicalServices')
BEGIN
    CREATE TABLE MedicalServices (
        ServiceID INT IDENTITY(1,1) PRIMARY KEY,
        ServiceName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        Price DECIMAL(10,2) NOT NULL,
        DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- BẢNG EXAMINATIONS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Examinations')
BEGIN
    CREATE TABLE Examinations (
        ExaminationID INT IDENTITY(1,1) PRIMARY KEY,
        AppointmentID INT FOREIGN KEY REFERENCES Appointments(AppointmentID),
        DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
        PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
        Symptoms NVARCHAR(1000),
        Diagnosis NVARCHAR(1000),
        Treatment NVARCHAR(1000),
        Notes NVARCHAR(1000),
        ExaminationDate DATETIME DEFAULT GETDATE(),
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- BẢNG MEDICINES
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Medicines')
BEGIN
    CREATE TABLE Medicines (
        MedicineID INT IDENTITY(1,1) PRIMARY KEY,
        MedicineName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        Unit NVARCHAR(20),
        Price DECIMAL(10,2),
        StockQuantity INT DEFAULT 0,
        IsActive BIT DEFAULT 1
    );
END
GO

-- =============================================
-- BẢNG PRESCRIPTIONS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Prescriptions')
BEGIN
    CREATE TABLE Prescriptions (
        PrescriptionID INT IDENTITY(1,1) PRIMARY KEY,
        ExaminationID INT FOREIGN KEY REFERENCES Examinations(ExaminationID),
        MedicineID INT FOREIGN KEY REFERENCES Medicines(MedicineID),
        Quantity INT,
        Dosage NVARCHAR(100),
        Instructions NVARCHAR(500),
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- BẢNG PAYMENTS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Payments')
BEGIN
    CREATE TABLE Payments (
        PaymentID INT IDENTITY(1,1) PRIMARY KEY,
        AppointmentID INT FOREIGN KEY REFERENCES Appointments(AppointmentID),
        PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
        Amount DECIMAL(10,2) NOT NULL,
        PaymentType VARCHAR(20) CHECK (PaymentType IN ('appointment', 'service', 'medicine')),
        PaymentMethod VARCHAR(20),
        PaymentStatus VARCHAR(20) DEFAULT 'pending',
        PaymentDate DATETIME DEFAULT GETDATE(),
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- BẢNG INVOICES
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Invoices')
BEGIN
    CREATE TABLE Invoices (
        InvoiceID INT IDENTITY(1,1) PRIMARY KEY,
        PaymentID INT FOREIGN KEY REFERENCES Payments(PaymentID),
        InvoiceNumber NVARCHAR(50) UNIQUE,
        InvoiceDate DATETIME DEFAULT GETDATE(),
        TotalAmount DECIMAL(10,2),
        DiscountAmount DECIMAL(10,2) DEFAULT 0,
        FinalAmount DECIMAL(10,2),
        InvoiceStatus VARCHAR(20) DEFAULT 'pending',
        CreatedAt DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- DỮ LIỆU MẪU
-- =============================================

-- Thêm Departments mẫu
IF NOT EXISTS (SELECT * FROM Departments)
BEGIN
    INSERT INTO Departments (DepartmentName, Description, Location) VALUES
    (N'Khoa Nội', N'Khám và điều trị bệnh nội khoa', N'Tầng 1'),
    (N'Khoa Ngoại', N'Phẫu thuật và điều trị bệnh ngoại khoa', N'Tầng 2'),
    (N'Khoa Nhi', N'Khám và điều trị bệnh trẻ em', N'Tầng 1'),
    (N'Khoa Sản', N'Khám và chăm sóc sức khỏe phụ nữ', N'Tầng 3'),
    (N'Khoa Mắt', N'Khám và điều trị bệnh về mắt', N'Tầng 2');
END
GO

-- Thêm Shifts mẫu
IF NOT EXISTS (SELECT * FROM Shifts)
BEGIN
    INSERT INTO Shifts (ShiftName, StartTime, EndTime, Description) VALUES
    (N'Ca sáng', '08:00:00', '12:00:00', N'Ca làm việc buổi sáng'),
    (N'Ca chiều', '13:00:00', '17:00:00', N'Ca làm việc buổi chiều'),
    (N'Ca tối', '18:00:00', '22:00:00', N'Ca làm việc buổi tối');
END
GO

-- Thêm Users mẫu (Admin, Doctor, Patient)
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
    INSERT INTO Users (Username, Password, Email, Phone, FullName, Role, Status) VALUES
    ('admin', 'admin123', 'admin@hospital.com', '0901234567', N'Quản trị viên', 'admin', 'active'),
    ('doctor1', 'doctor123', 'doctor1@hospital.com', '0901234568', N'BS. Nguyễn Văn A', 'doctor', 'active'),
    ('patient1', 'patient123', 'patient1@gmail.com', '0901234569', N'Trần Văn B', 'patient', 'active');
END
GO

-- Thêm Doctor cho user doctor1
IF NOT EXISTS (SELECT * FROM Doctors)
BEGIN
    DECLARE @DoctorUserID INT = (SELECT UserID FROM Users WHERE Username = 'doctor1');
    DECLARE @DeptID INT = (SELECT TOP 1 DepartmentID FROM Departments);
    
    IF @DoctorUserID IS NOT NULL AND @DeptID IS NOT NULL
    BEGIN
        INSERT INTO Doctors (UserID, DepartmentID, Specialization, Qualification, ExperienceYears, ConsultationFee)
        VALUES (@DoctorUserID, @DeptID, N'Nội tổng quát', N'Tiến sĩ Y khoa', 10, 200000);
    END
END
GO

-- Thêm Patient cho user patient1
IF NOT EXISTS (SELECT * FROM Patients)
BEGIN
    DECLARE @PatientUserID INT = (SELECT UserID FROM Users WHERE Username = 'patient1');
    
    IF @PatientUserID IS NOT NULL
    BEGIN
        INSERT INTO Patients (UserID, DateOfBirth, Gender, Address, BloodType)
        VALUES (@PatientUserID, '1990-01-15', 'Male', N'123 Đường ABC, TP.HCM', 'O+');
    END
END
GO

PRINT N'✅ Database HospitalManagement đã được khởi tạo thành công!';
GO
