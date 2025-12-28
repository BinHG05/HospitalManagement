using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Patients
    {
        public Patients()
        {
            Appointments = new HashSet<Appointments>();
            Examinations = new HashSet<Examinations>();
            MedicalHistory = new HashSet<MedicalHistory>();
            MedicalRecords = new HashSet<MedicalRecords>();
            Payments = new HashSet<Payments>();
        }

        [Key]
        public int PatientID { get; set; }
        public int? UserID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(50)]
        public string InsuranceNumber { get; set; }
        [StringLength(100)]
        public string EmergencyContact { get; set; }
        [StringLength(15)]
        public string EmergencyPhone { get; set; }
        [StringLength(5)]
        public string BloodType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(UserID))]
        [InverseProperty(nameof(Users.Patients))]
        public virtual Users User { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<Appointments> Appointments { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<Examinations> Examinations { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<MedicalHistory> MedicalHistory { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<MedicalRecords> MedicalRecords { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<Payments> Payments { get; set; }
    }
}
