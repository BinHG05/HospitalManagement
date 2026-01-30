using HospitalManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.EF
{
    public partial class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
        {
        }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<DoctorSchedules> DoctorSchedules { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<Examinations> Examinations { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<MedicalHistory> MedicalHistory { get; set; }
        public virtual DbSet<MedicalRecords> MedicalRecords { get; set; }
        public virtual DbSet<MedicalServices> MedicalServices { get; set; }
        public virtual DbSet<Medicines> Medicines { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Prescriptions> Prescriptions { get; set; }
        public virtual DbSet<ServiceRequests> ServiceRequests { get; set; }
        public virtual DbSet<ServiceResults> ServiceResults { get; set; }
        public virtual DbSet<Shifts> Shifts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)   
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conn = ConfigurationManager
                           .ConnectionStrings["HospitalDb"]
                           .ConnectionString;

                optionsBuilder.UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.HasKey(e => e.AppointmentID)
                    .HasName("PK__Appointm__8ECDFCA2E7EFB8C8");

                entity.HasIndex(e => e.AppointmentDate)
                    .HasName("IX_Appointments_Date");

                entity.HasIndex(e => e.DoctorID);

                entity.HasIndex(e => e.PatientID);

                entity.HasIndex(e => e.Status);

                entity.HasIndex(e => new { e.ScheduleID, e.AppointmentNumber })
                    .HasName("UX_Appointments_Slot")
                    .IsUnique()
                    .HasFilter("([Status] IN ('pending', 'confirmed'))");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK__Appointme__Depar__5AEE82B9");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorID)
                    .HasConstraintName("FK__Appointme__Docto__59FA5E80");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientID)
                    .HasConstraintName("FK__Appointme__Patie__59063A47");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ScheduleID)
                    .HasConstraintName("FK__Appointme__Sched__5BE2A6F2");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ShiftID)
                    .HasConstraintName("FK__Appointme__Shift__5CD6CB2B");
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DepartmentID)
                    .HasName("PK__Departme__B2079BCD65B259CC");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.HasOne(d => d.HeadDoctor)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.HeadDoctorID)
                    .HasConstraintName("FK_Departments_HeadDoctor");
            });

            modelBuilder.Entity<DoctorSchedules>(entity =>
            {
                entity.HasKey(e => e.ScheduleID)
                    .HasName("PK__DoctorSc__9C8A5B69723BB438");

                entity.HasIndex(e => new { e.DoctorID, e.ScheduleDate, e.ShiftID })
                    .HasName("UQ__DoctorSc__34F9B765A206EFD3")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DoctorSchedules)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK__DoctorSch__Depar__534D60F1");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorSchedules)
                    .HasForeignKey(d => d.DoctorID)
                    .HasConstraintName("FK__DoctorSch__Docto__52593CB8");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.DoctorSchedules)
                    .HasForeignKey(d => d.ShiftID)
                    .HasConstraintName("FK__DoctorSch__Shift__5441852A");

                // New Approval Workflow Columns
                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Approved')");

                entity.HasOne(d => d.ApprovedByUser)
                    .WithMany()
                    .HasForeignKey(d => d.ApprovedByUserID)
                    .HasConstraintName("FK_DoctorSchedules_ApprovedByUser");
            });

            modelBuilder.Entity<Doctors>(entity =>
            {
                entity.HasKey(e => e.DoctorID)
                    .HasName("PK__Doctors__2DC00EDFD6C512E9");

                entity.HasIndex(e => e.LicenseNumber)
                    .HasName("UQ__Doctors__E889016609C6C468")
                    .IsUnique();

                entity.Property(e => e.ConsultationFee).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.LicenseNumber).IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK_Doctors_Department");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK__Doctors__UserID__44FF419A");

                // New Quota Columns
                entity.Property(e => e.MinShiftsPerMonth).HasDefaultValueSql("((15))");
                entity.Property(e => e.MaxShiftsPerMonth).HasDefaultValueSql("((25))");
            });

            modelBuilder.Entity<Examinations>(entity =>
            {
                entity.HasKey(e => e.ExaminationID)
                    .HasName("PK__Examinat__C4A924C0649EBA7C");

                entity.HasIndex(e => e.AppointmentID);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExaminationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('in_progress')");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.AppointmentID)
                    .HasConstraintName("FK__Examinati__Appoi__6383C8BA");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.DoctorID)
                    .HasConstraintName("FK__Examinati__Docto__6477ECF3");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.PatientID)
                    .HasConstraintName("FK__Examinati__Patie__656C112C");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(e => e.InvoiceID)
                    .HasName("PK__Invoices__D796AAD546ADCE89");

                entity.HasIndex(e => e.InvoiceNumber)
                    .HasName("UQ__Invoices__D776E981F9E0C3DC")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DiscountAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvoiceDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvoiceNumber).IsUnicode(false);

                entity.Property(e => e.InvoiceStatus)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('unpaid')");

                entity.Property(e => e.TaxAmount).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.PaymentID)
                    .HasConstraintName("FK__Invoices__Paymen__0B91BA14");
            });

            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryID)
                    .HasName("PK__MedicalH__4D7B4ADD49C01952");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MedicalHistory)
                    .HasForeignKey(d => d.DoctorID)
                    .HasConstraintName("FK__MedicalHi__Docto__208CD6FA");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.MedicalHistory)
                    .HasForeignKey(d => d.PatientID)
                    .HasConstraintName("FK__MedicalHi__Patie__1EA48E88");

                entity.HasOne(d => d.Record)
                    .WithMany(p => p.MedicalHistory)
                    .HasForeignKey(d => d.RecordID)
                    .HasConstraintName("FK__MedicalHi__Recor__1F98B2C1");
            });

            modelBuilder.Entity<MedicalRecords>(entity =>
            {
                entity.HasKey(e => e.RecordID)
                    .HasName("PK__MedicalR__FBDF78C9E7633A51");

                entity.HasIndex(e => e.PatientID);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecordDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Examination)
                    .WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.ExaminationID)
                    .HasConstraintName("FK__MedicalRe__Exami__151B244E");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.MedicalRecords)
                    .HasForeignKey(d => d.PatientID)
                    .HasConstraintName("FK__MedicalRe__Patie__14270015");
            });

            modelBuilder.Entity<MedicalServices>(entity =>
            {
                entity.HasKey(e => e.ServiceID)
                    .HasName("PK__MedicalS__C51BB0EA610F0FBE");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ServiceType).IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MedicalServices)
                    .HasForeignKey(d => d.DepartmentID)
                    .HasConstraintName("FK__MedicalSe__Depar__72C60C4A");
            });

            modelBuilder.Entity<Medicines>(entity =>
            {
                entity.HasKey(e => e.MedicineID)
                    .HasName("PK__Medicine__4F2128F01C3DCFD1");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.StockQuantity).HasDefaultValueSql("((0))");

                entity.Property(e => e.Unit).IsUnicode(false);
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasKey(e => e.PatientID)
                    .HasName("PK__Patients__970EC34644497A60");

                entity.Property(e => e.BloodType).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmergencyPhone).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.InsuranceNumber).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK__Patients__UserID__3F466844");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => e.PaymentID)
                    .HasName("PK__Payments__9B556A588C9C99C1");

                entity.HasIndex(e => e.PatientID);

                entity.HasIndex(e => e.PaymentStatus)
                    .HasName("IX_Payments_Status");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentMethod).IsUnicode(false);

                entity.Property(e => e.PaymentStatus)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.PaymentType).IsUnicode(false);

                entity.Property(e => e.RefundedAmount).HasDefaultValueSql("((0))");

                entity.Property(e => e.TransactionID).IsUnicode(false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AppointmentID)
                    .HasConstraintName("FK__Payments__Appoin__01142BA1");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PatientID)
                    .HasConstraintName("FK__Payments__Patien__02084FDA");
            });

            modelBuilder.Entity<Prescriptions>(entity =>
            {
                entity.HasKey(e => e.PrescriptionID)
                    .HasName("PK__Prescrip__40130812DE46FE7E");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.MedicineID)
                    .HasConstraintName("FK__Prescript__Medic__1AD3FDA4");

                entity.HasOne(d => d.Record)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.RecordID)
                    .HasConstraintName("FK__Prescript__Recor__19DFD96B");
            });

            modelBuilder.Entity<ServiceRequests>(entity =>
            {
                entity.HasKey(e => e.RequestID)
                    .HasName("PK__ServiceR__33A8519AB8AFEDE4");

                entity.HasIndex(e => new { e.ExaminationID, e.RequestNumber })
                    .HasName("UX_ServiceRequest_Order")
                    .IsUnique()
                    .HasFilter("([Status]<>'cancelled')");

                entity.Property(e => e.RequestedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('requested')");

                entity.HasOne(d => d.Examination)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.ExaminationID)
                    .HasConstraintName("FK__ServiceRe__Exami__6C190EBB");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.ServiceID)
                    .HasConstraintName("FK_ServiceRequests_Service");
            });

            modelBuilder.Entity<ServiceResults>(entity =>
            {
                entity.HasKey(e => e.ResultID)
                    .HasName("PK__ServiceR__976902281D1F00DE");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ResultFile).IsUnicode(false);

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.ServiceResults)
                    .HasForeignKey(d => d.RequestID)
                    .HasConstraintName("FK__ServiceRe__Reque__778AC167");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceResults)
                    .HasForeignKey(d => d.ServiceID)
                    .HasConstraintName("FK__ServiceRe__Servi__787EE5A0");
            });

            modelBuilder.Entity<Shifts>(entity =>
            {
                entity.HasKey(e => e.ShiftID)
                    .HasName("PK__Shifts__C0A838E1C5518101");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MaxSlots).HasDefaultValueSql("((100))");

                // New Quota Columns
                entity.Property(e => e.MinDoctorsPerShift).HasDefaultValueSql("((2))");
                entity.Property(e => e.MaxDoctorsPerShift).HasDefaultValueSql("((5))");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserID)
                    .HasName("PK__Users__1788CCACE897AC2B");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Users__A9D10534488B1786")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("UQ__Users__5C7E359E90C42F36")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Users__536C85E40BB08567")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.Role).IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('active')");

                entity.Property(e => e.Username).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
