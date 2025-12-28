using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class MedicalRecords
    {
        public MedicalRecords()
        {
            MedicalHistory = new HashSet<MedicalHistory>();
            Prescriptions = new HashSet<Prescriptions>();
        }

        [Key]
        public int RecordID { get; set; }
        public int? PatientID { get; set; }
        public int? ExaminationID { get; set; }
        [Required]
        [StringLength(1000)]
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; }
        public string Recommendations { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RecordDate { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(ExaminationID))]
        [InverseProperty(nameof(Examinations.MedicalRecords))]
        public virtual Examinations Examination { get; set; }
        [ForeignKey(nameof(PatientID))]
        [InverseProperty(nameof(Patients.MedicalRecords))]
        public virtual Patients Patient { get; set; }
        [InverseProperty("Record")]
        public virtual ICollection<MedicalHistory> MedicalHistory { get; set; }
        [InverseProperty("Record")]
        public virtual ICollection<Prescriptions> Prescriptions { get; set; }
    }
}
