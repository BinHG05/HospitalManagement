USE HospitalManagement_New;
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

-- Reseed to avoid PK issues
DBCC CHECKIDENT ('Users', RESEED); 
DBCC CHECKIDENT ('Doctors', RESEED);
DBCC CHECKIDENT ('DoctorSchedules', RESEED);

-- Function-like logic to ensure doctors exist for all 10 departments
DECLARE @i INT = 1;
WHILE @i <= 10
BEGIN
    DECLARE @deptName NVARCHAR(100) = (SELECT DepartmentName FROM Departments WHERE DepartmentID = @i);
    DECLARE @username VARCHAR(50) = 'doctor_dept_' + CAST(@i AS VARCHAR);
    
    -- Ensure User exists
    IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = @username)
    BEGIN
        INSERT INTO Users (Username, Password, Email, Phone, FullName, Role)
        VALUES (@username, '123', @username + '@hosp.com', '09' + CAST(10000000 + @i AS VARCHAR), N'BS. ' + @deptName, 'doctor');
    END
    
    DECLARE @uID INT = (SELECT UserID FROM Users WHERE Username = @username);
    
    -- Ensure Doctor exists
    IF NOT EXISTS (SELECT 1 FROM Doctors WHERE UserID = @uID)
    BEGIN
        INSERT INTO Doctors (UserID, Specialization, DepartmentID, LicenseNumber, YearsOfExperience, ConsultationFee)
        VALUES (@uID, @deptName, @i, 'LIC_DEPT_' + CAST(@i AS VARCHAR), 10, 200000);
    END
    
    DECLARE @dID INT = (SELECT DoctorID FROM Doctors WHERE UserID = @uID);
    
    -- Ensure Schedule exists for today (31/01/2026)
    IF NOT EXISTS (SELECT 1 FROM DoctorSchedules WHERE DoctorID = @dID AND ScheduleDate = '2026-01-31' AND ShiftID = 1)
    BEGIN
        INSERT INTO DoctorSchedules (DoctorID, DepartmentID, ShiftID, ScheduleDate, AvailableSlots, IsActive, Status)
        VALUES (@dID, @i, 1, '2026-01-31', 20, 1, 'Approved');
    END
    
    IF NOT EXISTS (SELECT 1 FROM DoctorSchedules WHERE DoctorID = @dID AND ScheduleDate = '2026-01-31' AND ShiftID = 3)
    BEGIN
        INSERT INTO DoctorSchedules (DoctorID, DepartmentID, ShiftID, ScheduleDate, AvailableSlots, IsActive, Status)
        VALUES (@dID, @i, 3, '2026-01-31', 20, 1, 'Approved');
    END

    SET @i = @i + 1;
END

PRINT N'✅ Tất cả các khoa đã có bác sĩ và lịch khám cho ngày 31/01/2026!';
GO
