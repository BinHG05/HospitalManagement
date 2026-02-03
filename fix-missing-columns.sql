USE HospitalManagement_New;
GO

-- Fix missing columns in MedicalRecords table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[MedicalRecords]') AND name = 'Recommendations')
BEGIN
    ALTER TABLE MedicalRecords ADD Recommendations NVARCHAR(MAX);
    PRINT 'Added Recommendations column to MedicalRecords table.';
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[MedicalRecords]') AND name = 'CreatedBy')
BEGIN
    ALTER TABLE MedicalRecords ADD CreatedBy INT;
    PRINT 'Added CreatedBy column to MedicalRecords table.';
END

GO
