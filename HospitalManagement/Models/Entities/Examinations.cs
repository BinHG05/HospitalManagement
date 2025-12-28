using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Examinations
    {
        public Examinations()
        {
            MedicalRecords = new HashSet<MedicalRecords>();
            ServiceRequests = new HashSet<ServiceRequests>();
        }

        [Key]
        public int ExaminationID { get; set; }
        public int? AppointmentID { get; set; }
        public int? DoctorID { get; set; }
        public int? PatientID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExaminationDate { get; set; }
        [StringLength(500)]
        public string Symptoms { get; set; }
        [StringLength(1000)]
        public string PreliminaryDiagnosis { get; set; }
        public string Notes { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(AppointmentID))]
        [InverseProperty(nameof(Appointments.Examinations))]
        public virtual Appointments Appointment { get; set; }
        [ForeignKey(nameof(DoctorID))]
        [InverseProperty(nameof(Doctors.Examinations))]
        public virtual Doctors Doctor { get; set; }
        [ForeignKey(nameof(PatientID))]
        [InverseProperty(nameof(Patients.Examinations))]
        public virtual Patients Patient { get; set; }
        [InverseProperty("Examination")]
        public virtual ICollection<MedicalRecords> MedicalRecords { get; set; }
        [InverseProperty("Examination")]
        public virtual ICollection<ServiceRequests> ServiceRequests { get; set; }
    }
}
