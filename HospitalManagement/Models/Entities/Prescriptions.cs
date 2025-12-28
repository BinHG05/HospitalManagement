using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Prescriptions
    {
        [Key]
        public int PrescriptionID { get; set; }
        public int? RecordID { get; set; }
        public int? MedicineID { get; set; }
        [Required]
        [StringLength(100)]
        public string Dosage { get; set; }
        [Required]
        [StringLength(100)]
        public string Frequency { get; set; }
        public int? Duration { get; set; }
        public int Quantity { get; set; }
        [StringLength(500)]
        public string Instructions { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(MedicineID))]
        [InverseProperty(nameof(Medicines.Prescriptions))]
        public virtual Medicines Medicine { get; set; }
        [ForeignKey(nameof(RecordID))]
        [InverseProperty(nameof(MedicalRecords.Prescriptions))]
        public virtual MedicalRecords Record { get; set; }
    }
}
