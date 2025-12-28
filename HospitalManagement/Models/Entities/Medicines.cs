using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Medicines
    {
        public Medicines()
        {
            Prescriptions = new HashSet<Prescriptions>();
        }

        [Key]
        public int MedicineID { get; set; }
        [Required]
        [StringLength(100)]
        public string MedicineName { get; set; }
        [StringLength(100)]
        public string GenericName { get; set; }
        [StringLength(20)]
        public string Unit { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PricePerUnit { get; set; }
        public int? StockQuantity { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [InverseProperty("Medicine")]
        public virtual ICollection<Prescriptions> Prescriptions { get; set; }
    }
}
