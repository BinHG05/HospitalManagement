USE HospitalManagement_New;
GO

-- Add DefaultRoom to Doctors if missing
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'DefaultRoom')
BEGIN
    ALTER TABLE Doctors ADD DefaultRoom NVARCHAR(50);
    PRINT 'Added DefaultRoom to Doctors table.';
    
    -- Update existing records with dummy data if needed, or leave null
    -- UPDATE Doctors SET DefaultRoom = 'P.101' WHERE DefaultRoom IS NULL;
END
GO

-- Add RoomNumber to DoctorSchedules if missing
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSchedules]') AND name = 'RoomNumber')
BEGIN
    ALTER TABLE DoctorSchedules ADD RoomNumber NVARCHAR(50);
    PRINT 'Added RoomNumber to DoctorSchedules table.';
END
GO

-- Add RoomNumber to Appointments if missing
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Appointments]') AND name = 'RoomNumber')
BEGIN
    ALTER TABLE Appointments ADD RoomNumber NVARCHAR(50);
    PRINT 'Added RoomNumber to Appointments table.';
END
GO
