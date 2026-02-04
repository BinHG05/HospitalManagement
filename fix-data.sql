USE HospitalManagement_New;
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

-- 1. Thêm Users cho bác sĩ (nếu chưa có)
IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'doctor6')
BEGIN
    INSERT INTO Users (Username, Password, Email, Phone, FullName, Role, Status) VALUES
    ('doctor6', '123', 'dr6@hosp.com', '0901234006', N'BS. Ngô Bá Khá', 'doctor', 'active'),
    ('doctor7', '123', 'dr7@hosp.com', '0901234007', N'BS. Lê Tùng Vân', 'doctor', 'active'),
    ('doctor8', '123', 'dr8@hosp.com', '0901234008', N'BS. Phạm Nhật Vượng', 'doctor', 'active'),
    ('doctor9', '123', 'dr9@hosp.com', '0901234009', N'BS. Trương Mỹ Lan', 'doctor', 'active'),
    ('doctor10', '123', 'dr10@hosp.com', '0901234010', N'BS. Đỗ Anh Dũng', 'doctor', 'active');
END

-- 2. Thêm Doctors (nếu chưa có)
IF NOT EXISTS (SELECT 1 FROM Doctors WHERE LicenseNumber = 'LIC011')
BEGIN
    INSERT INTO Doctors (UserID, Specialization, DepartmentID, LicenseNumber, YearsOfExperience, ConsultationFee, IsActive) VALUES
    (16, N'Mắt kĩ thuật cao', 6, 'LIC011', 10, 200000, 1),
    (17, N'Tai Mũi Họng', 7, 'LIC012', 12, 180000, 1),
    (18, N'Răng Hàm Mặt', 8, 'LIC013', 15, 250000, 1),
    (19, N'Da Liễu chuyên sâu', 9, 'LIC014', 20, 300000, 1),
    (20, N'Xét nghiệm', 10, 'LIC015', 8, 150000, 1);
END

-- 3. Thêm Lịch khám cho hôm nay (31/01/2026)
DELETE FROM DoctorSchedules WHERE ScheduleDate = '2026-01-31';

INSERT INTO DoctorSchedules (DoctorID, DepartmentID, ShiftID, ScheduleDate, AvailableSlots, IsActive, Status) VALUES
(1, 1, 1, '2026-01-31', 20, 1, 'Approved'),
(2, 2, 1, '2026-01-31', 20, 1, 'Approved'),
(3, 3, 1, '2026-01-31', 20, 1, 'Approved'),
(4, 4, 1, '2026-01-31', 20, 1, 'Approved'),
(5, 5, 1, '2026-01-31', 20, 1, 'Approved'),
(11, 6, 1, '2026-01-31', 20, 1, 'Approved'),
(12, 7, 1, '2026-01-31', 20, 1, 'Approved'),
(13, 8, 1, '2026-01-31', 20, 1, 'Approved'),
(14, 9, 1, '2026-01-31', 20, 1, 'Approved'), 
(14, 9, 3, '2026-01-31', 20, 1, 'Approved'),
(15, 10, 1, '2026-01-31', 20, 1, 'Approved');

PRINT N'✅ Fix data applied successfully to HospitalManagement_New!';
GO
