create database HospitalManagement;
go
use HospitalManagement;
go


-- Bảng chung cho tất cả người dùng
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

-- Bảng bệnh nhân (mở rộng từ Users)
CREATE TABLE Patients (
    PatientID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    DateOfBirth DATE,
    Gender VARCHAR(10) CHECK (Gender IN ('male', 'female', 'other')),
    Address NVARCHAR(255),
    InsuranceNumber VARCHAR(50),
    EmergencyContact NVARCHAR(100),
    EmergencyPhone VARCHAR(15),
    BloodType VARCHAR(5),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng bác sĩ (mở rộng từ Users)
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Specialization NVARCHAR(100) NOT NULL,
    DepartmentID INT, -- FK đến Departments
    LicenseNumber VARCHAR(50) UNIQUE NOT NULL,
    YearsOfExperience INT,
    Qualifications NVARCHAR(255),
    ConsultationFee DECIMAL(10,2) DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);


-- Bảng khoa/phòng
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Location NVARCHAR(200),
    Phone VARCHAR(15),
    HeadDoctorID INT, -- FK đến Doctors
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng ca làm việc
CREATE TABLE Shifts (
    ShiftID INT PRIMARY KEY IDENTITY(1,1),
    ShiftName NVARCHAR(50) NOT NULL, -- VD: "Sáng", "Chiều"
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    MaxSlots INT DEFAULT 100, -- Số slot tối đa/ca
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng phân công bác sĩ theo ca
CREATE TABLE DoctorSchedules (
    ScheduleID INT PRIMARY KEY IDENTITY(1,1),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    ShiftID INT FOREIGN KEY REFERENCES Shifts(ShiftID),
    ScheduleDate DATE NOT NULL,
    AvailableSlots INT, -- Số slot còn trống
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UNIQUE(DoctorID, ScheduleDate, ShiftID) -- Một bác sĩ không trùng ca
);

-- Bảng lịch hẹn
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    ScheduleID INT FOREIGN KEY REFERENCES DoctorSchedules(ScheduleID),
    AppointmentDate DATE NOT NULL,
    ShiftID INT FOREIGN KEY REFERENCES Shifts(ShiftID),
    AppointmentNumber INT NOT NULL, -- Số thứ tự trong ca
    Symptoms NVARCHAR(500),
    Status VARCHAR(20) DEFAULT 'pending' CHECK (Status IN ('pending', 'confirmed', 'cancelled', 'completed', 'no-show')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);


-- Bảng khám bệnh
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

-- Bảng chỉ định dịch vụ
CREATE TABLE ServiceRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    ExaminationID INT FOREIGN KEY REFERENCES Examinations(ExaminationID),
    ServiceID INT, -- FK đến MedicalServices
    RequestNumber INT NOT NULL, -- Số thứ tự dịch vụ
    Status VARCHAR(20) DEFAULT 'requested' CHECK (Status IN ('requested', 'paid', 'in_progress', 'completed', 'cancelled')),
    RequestedAt DATETIME DEFAULT GETDATE(),
    CompletedAt DATETIME
);


-- Bảng dịch vụ y tế
CREATE TABLE MedicalServices (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(100) NOT NULL,
    ServiceType VARCHAR(50) CHECK (ServiceType IN ('xet_nghiem', 'sieu_am', 'chup_xquang', 'kham', 'khac')),
    Description NVARCHAR(500),
    Price DECIMAL(10,2) NOT NULL,
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    EstimatedTime INT, -- Thời gian ước tính (phút)
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng kết quả dịch vụ
CREATE TABLE ServiceResults (
    ResultID INT PRIMARY KEY IDENTITY(1,1),
    RequestID INT FOREIGN KEY REFERENCES ServiceRequests(RequestID),
    ServiceID INT FOREIGN KEY REFERENCES MedicalServices(ServiceID),
    ResultDetails NVARCHAR(MAX),
    ResultFile VARCHAR(255), -- Đường dẫn file kết quả
    PerformedBy INT, -- Nhân viên thực hiện
    PerformedAt DATETIME,
    VerifiedBy INT, -- Bác sĩ xác nhận
    VerifiedAt DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng danh mục thuốc
CREATE TABLE Medicines (
    MedicineID INT PRIMARY KEY IDENTITY(1,1),
    MedicineName NVARCHAR(100) NOT NULL,
    GenericName NVARCHAR(100),
    Unit VARCHAR(20),
    PricePerUnit DECIMAL(10,2),
    StockQuantity INT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);


-- Bảng thanh toán
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    AppointmentID INT FOREIGN KEY REFERENCES Appointments(AppointmentID),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    PaymentType VARCHAR(20) CHECK (PaymentType IN ('appointment', 'service', 'medicine')),
    ReferenceID INT, -- ID của lịch hẹn/dịch vụ/thuốc
    Amount DECIMAL(10,2) NOT NULL,
    PaymentMethod VARCHAR(20) CHECK (PaymentMethod IN ('cash', 'bank_transfer', 'credit_card', 'ewallet')),
    PaymentStatus VARCHAR(20) DEFAULT 'pending' CHECK (PaymentStatus IN ('pending', 'completed', 'failed', 'refunded')),
    TransactionID VARCHAR(100), -- Mã giao dịch từ cổng thanh toán
    PaymentDate DATETIME,
    RefundedAmount DECIMAL(10,2) DEFAULT 0,
    RefundedAt DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng hóa đơn
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

-- Bảng hồ sơ bệnh án
CREATE TABLE MedicalRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
    ExaminationID INT FOREIGN KEY REFERENCES Examinations(ExaminationID),
    Diagnosis NVARCHAR(1000) NOT NULL,
    TreatmentPlan NVARCHAR(MAX),
    Recommendations NVARCHAR(MAX),
    RecordDate DATETIME DEFAULT GETDATE(),
    CreatedBy INT, -- Bác sĩ tạo hồ sơ
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng đơn thuốc
CREATE TABLE Prescriptions (
    PrescriptionID INT PRIMARY KEY IDENTITY(1,1),
    RecordID INT FOREIGN KEY REFERENCES MedicalRecords(RecordID),
    MedicineID INT FOREIGN KEY REFERENCES Medicines(MedicineID),
    Dosage NVARCHAR(100) NOT NULL,
    Frequency NVARCHAR(100) NOT NULL,
    Duration INT, -- Số ngày
    Quantity INT NOT NULL,
    Instructions NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng lịch sử khám bệnh
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

-- Thêm Foreign Keys chưa được định nghĩa
ALTER TABLE Doctors 
ADD CONSTRAINT FK_Doctors_Department 
FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID);

ALTER TABLE Departments 
ADD CONSTRAINT FK_Departments_HeadDoctor 
FOREIGN KEY (HeadDoctorID) REFERENCES Doctors(DoctorID);

ALTER TABLE ServiceRequests 
ADD CONSTRAINT FK_ServiceRequests_Service 
FOREIGN KEY (ServiceID) REFERENCES MedicalServices(ServiceID);

-- Tạo indexes cho hiệu năng
CREATE INDEX IX_Appointments_PatientID ON Appointments(PatientID);
CREATE INDEX IX_Appointments_DoctorID ON Appointments(DoctorID);
CREATE INDEX IX_Appointments_Status ON Appointments(Status);
CREATE INDEX IX_Appointments_Date ON Appointments(AppointmentDate);

CREATE INDEX IX_Payments_PatientID ON Payments(PatientID);
CREATE INDEX IX_Payments_Status ON Payments(PaymentStatus);

CREATE INDEX IX_Examinations_AppointmentID ON Examinations(AppointmentID);
CREATE INDEX IX_MedicalRecords_PatientID ON MedicalRecords(PatientID);

-- Tạo unique constraint cho số thứ tự trong cùng ca/lịch
CREATE UNIQUE INDEX UX_Appointments_Slot 
ON Appointments(ScheduleID, AppointmentNumber)
WHERE Status IN ('pending', 'confirmed');

CREATE UNIQUE INDEX UX_ServiceRequest_Order
ON ServiceRequests(ExaminationID, RequestNumber)
WHERE Status <> 'cancelled';

-- =============================================
-- DỮ LIỆU MẪU
-- =============================================

-- -- Thêm Departments mẫu
-- INSERT INTO Departments (DepartmentName, Description, Location) VALUES
-- (N'Khoa Nội', N'Khám và điều trị bệnh nội khoa', N'Tầng 1'),
-- (N'Khoa Ngoại', N'Phẫu thuật và điều trị bệnh ngoại khoa', N'Tầng 2'),
-- (N'Khoa Nhi', N'Khám và điều trị bệnh trẻ em', N'Tầng 1'),
-- (N'Khoa Sản', N'Khám và chăm sóc sức khỏe phụ nữ', N'Tầng 3'),
-- (N'Khoa Mắt', N'Khám và điều trị bệnh về mắt', N'Tầng 2');
-- GO

-- -- Thêm Shifts mẫu
-- INSERT INTO Shifts (ShiftName, StartTime, EndTime, MaxSlots) VALUES
-- (N'Ca sáng', '07:00:00', '11:30:00', 50),
-- (N'Ca chiều', '13:00:00', '17:00:00', 50),
-- (N'Ca tối', '18:00:00', '21:00:00', 30);
-- GO

-- -- Thêm Users mẫu (Admin, Doctor, Patient)
-- INSERT INTO Users (Username, Password, Email, Phone, FullName, Role, Status) VALUES
-- ('admin', 'admin123', 'admin@hospital.com', '0901234567', N'Quản trị viên', 'admin', 'active'),
-- ('doctor1', 'doctor123', 'doctor1@hospital.com', '0901234568', N'BS. Nguyễn Văn A', 'doctor', 'active'),
-- ('doctor2', 'doctor123', 'doctor2@hospital.com', '0901234569', N'BS. Trần Thị B', 'doctor', 'active'),
-- ('patient1', 'patient123', 'patient1@gmail.com', '0901234570', N'Lê Văn C', 'patient', 'active'),
-- ('patient2', 'patient123', 'patient2@gmail.com', '0901234571', N'Phạm Thị D', 'patient', 'active');
-- GO

-- -- Thêm Doctor
-- INSERT INTO Doctors (UserID, Specialization, DepartmentID, LicenseNumber, YearsOfExperience, Qualifications, ConsultationFee) VALUES
-- (2, N'Nội tổng quát', 1, 'DOC001', 10, N'Tiến sĩ Y khoa', 200000),
-- (3, N'Nhi khoa', 3, 'DOC002', 8, N'Thạc sĩ Y khoa', 180000);
-- GO

-- -- Thêm Patient
-- INSERT INTO Patients (UserID, DateOfBirth, Gender, Address, BloodType) VALUES
-- (4, '1990-05-15', 'male', N'123 Đường ABC, Quận 1, TP.HCM', 'O+'),
-- (5, '1985-08-20', 'female', N'456 Đường XYZ, Quận 3, TP.HCM', 'A+');
-- GO

-- -- Thêm DoctorSchedules cho tuần tới
-- DECLARE @StartDate DATE = DATEADD(DAY, 1, GETDATE());
-- DECLARE @i INT = 0;

-- WHILE @i < 7
-- BEGIN
--     -- Doctor 1 - Ca sáng
--     INSERT INTO DoctorSchedules (DoctorID, DepartmentID, ShiftID, ScheduleDate, AvailableSlots, IsActive) 
--     VALUES (1, 1, 1, DATEADD(DAY, @i, @StartDate), 50, 1);
    
--     -- Doctor 1 - Ca chiều (chỉ T2, T4, T6)
--     IF DATEPART(WEEKDAY, DATEADD(DAY, @i, @StartDate)) IN (2, 4, 6)
--     BEGIN
--         INSERT INTO DoctorSchedules (DoctorID, DepartmentID, ShiftID, ScheduleDate, AvailableSlots, IsActive) 
--         VALUES (1, 1, 2, DATEADD(DAY, @i, @StartDate), 50, 1);
--     END
    
--     -- Doctor 2 - Ca sáng (chỉ T3, T5, T7)
--     IF DATEPART(WEEKDAY, DATEADD(DAY, @i, @StartDate)) IN (3, 5, 7)
--     BEGIN
--         INSERT INTO DoctorSchedules (DoctorID, DepartmentID, ShiftID, ScheduleDate, AvailableSlots, IsActive) 
--         VALUES (2, 3, 1, DATEADD(DAY, @i, @StartDate), 50, 1);
--     END
    
--     SET @i = @i + 1;
-- END
-- GO

-- -- Thêm một số dịch vụ y tế
-- INSERT INTO MedicalServices (ServiceName, ServiceType, Description, Price, DepartmentID, EstimatedTime, IsActive) VALUES
-- (N'Xét nghiệm máu tổng quát', 'xet_nghiem', N'Xét nghiệm công thức máu', 150000, 1, 30, 1),
-- (N'Siêu âm ổ bụng', 'sieu_am', N'Siêu âm các cơ quan trong ổ bụng', 250000, 1, 20, 1),
-- (N'Chụp X-quang phổi', 'chup_xquang', N'Chụp X-quang vùng ngực', 180000, 1, 15, 1),
-- (N'Điện tâm đồ', 'khac', N'Đo điện tâm đồ ECG', 120000, 1, 15, 1);
-- GO

-- -- Thêm một số thuốc
-- INSERT INTO Medicines (MedicineName, GenericName, Unit, PricePerUnit, StockQuantity, IsActive) VALUES
-- (N'Paracetamol 500mg', 'Paracetamol', N'Viên', 1000, 1000, 1),
-- (N'Amoxicillin 500mg', 'Amoxicillin', N'Viên', 2500, 500, 1),
-- (N'Vitamin C 1000mg', 'Ascorbic Acid', N'Viên', 1500, 800, 1),
-- (N'Omeprazole 20mg', 'Omeprazole', N'Viên', 3000, 400, 1);
-- GO

-- PRINT N'✅ Database HospitalManagement đã được khởi tạo thành công!';
-- GO
