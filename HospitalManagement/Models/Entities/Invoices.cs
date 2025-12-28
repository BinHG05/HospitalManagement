using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Invoices
    {
        [Key]
        public int InvoiceID { get; set; }
        public int? PaymentID { get; set; }
        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? TaxAmount { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DiscountAmount { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal FinalAmount { get; set; }
        [StringLength(20)]
        public string InvoiceStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(PaymentID))]
        [InverseProperty(nameof(Payments.Invoices))]
        public virtual Payments Payment { get; set; }
    }
}
