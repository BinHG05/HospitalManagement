using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class ServiceResults
    {
        [Key]
        public int ResultID { get; set; }
        public int? RequestID { get; set; }
        public int? ServiceID { get; set; }
        public string ResultDetails { get; set; }
        [StringLength(255)]
        public string ResultFile { get; set; }
        public int? PerformedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PerformedAt { get; set; }
        public int? VerifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerifiedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(RequestID))]
        [InverseProperty(nameof(ServiceRequests.ServiceResults))]
        public virtual ServiceRequests Request { get; set; }
        [ForeignKey(nameof(ServiceID))]
        [InverseProperty(nameof(MedicalServices.ServiceResults))]
        public virtual MedicalServices Service { get; set; }

        [ForeignKey(nameof(PerformedBy))]
        [InverseProperty(nameof(Doctors.PerformedServiceResults))]
        public virtual Doctors PerformedByDoctor { get; set; }

        [ForeignKey(nameof(VerifiedBy))]
        [InverseProperty(nameof(Doctors.VerifiedServiceResults))]
        public virtual Doctors VerifiedByDoctor { get; set; }
    }
}
