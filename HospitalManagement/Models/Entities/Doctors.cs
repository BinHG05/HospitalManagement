using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Doctors
    {
        public Doctors()
        {
            Appointments = new HashSet<Appointments>();
            Departments = new HashSet<Departments>();
            DoctorSchedules = new HashSet<DoctorSchedules>();
            Examinations = new HashSet<Examinations>();
            MedicalHistory = new HashSet<MedicalHistory>();
        }

        [Key]
        public int DoctorID { get; set; }
        public int? UserID { get; set; }
        [Required]
        [StringLength(100)]
        public string Specialization { get; set; }
        public int? DepartmentID { get; set; }
        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; }
        public int? YearsOfExperience { get; set; }
        [StringLength(255)]
        public string Qualifications { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ConsultationFee { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        // Monthly Quota: Min/Max shifts per month
        public int MinShiftsPerMonth { get; set; } = 15;
        public int MaxShiftsPerMonth { get; set; } = 25;
        [StringLength(50)]
        public string DefaultRoom { get; set; }

        [ForeignKey(nameof(DepartmentID))]
        [InverseProperty("Doctors")]
        public virtual Departments Department { get; set; }
        [ForeignKey(nameof(UserID))]
        [InverseProperty(nameof(Users.Doctors))]
        public virtual Users User { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<Appointments> Appointments { get; set; }
        [InverseProperty("HeadDoctor")]
        public virtual ICollection<Departments> Departments { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<DoctorSchedules> DoctorSchedules { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<Examinations> Examinations { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<MedicalHistory> MedicalHistory { get; set; }
    }
}
