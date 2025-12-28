using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class MedicalServices
    {
        public MedicalServices()
        {
            ServiceRequests = new HashSet<ServiceRequests>();
            ServiceResults = new HashSet<ServiceResults>();
        }

        [Key]
        public int ServiceID { get; set; }
        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; }
        [StringLength(50)]
        public string ServiceType { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public int? DepartmentID { get; set; }
        public int? EstimatedTime { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(DepartmentID))]
        [InverseProperty(nameof(Departments.MedicalServices))]
        public virtual Departments Department { get; set; }
        [InverseProperty("Service")]
        public virtual ICollection<ServiceRequests> ServiceRequests { get; set; }
        [InverseProperty("Service")]
        public virtual ICollection<ServiceResults> ServiceResults { get; set; }
    }
}
