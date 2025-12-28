using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class ServiceRequests
    {
        public ServiceRequests()
        {
            ServiceResults = new HashSet<ServiceResults>();
        }

        [Key]
        public int RequestID { get; set; }
        public int? ExaminationID { get; set; }
        public int? ServiceID { get; set; }
        public int RequestNumber { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequestedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedAt { get; set; }

        [ForeignKey(nameof(ExaminationID))]
        [InverseProperty(nameof(Examinations.ServiceRequests))]
        public virtual Examinations Examination { get; set; }
        [ForeignKey(nameof(ServiceID))]
        [InverseProperty(nameof(MedicalServices.ServiceRequests))]
        public virtual MedicalServices Service { get; set; }
        [InverseProperty("Request")]
        public virtual ICollection<ServiceResults> ServiceResults { get; set; }
    }
}
