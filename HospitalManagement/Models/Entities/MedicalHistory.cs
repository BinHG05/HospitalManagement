using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class MedicalHistory
    {
        [Key]
        public int HistoryID { get; set; }
        public int? PatientID { get; set; }
        public int? RecordID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VisitDate { get; set; }
        public int? DoctorID { get; set; }
        [StringLength(1000)]
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NextAppointmentDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(DoctorID))]
        [InverseProperty(nameof(Doctors.MedicalHistory))]
        public virtual Doctors Doctor { get; set; }
        [ForeignKey(nameof(PatientID))]
        [InverseProperty(nameof(Patients.MedicalHistory))]
        public virtual Patients Patient { get; set; }
        [ForeignKey(nameof(RecordID))]
        [InverseProperty(nameof(MedicalRecords.MedicalHistory))]
        public virtual MedicalRecords Record { get; set; }
    }
}
