using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Payments
    {
        public Payments()
        {
            Invoices = new HashSet<Invoices>();
        }

        [Key]
        public int PaymentID { get; set; }
        public int? AppointmentID { get; set; }
        public int? PatientID { get; set; }
        [StringLength(20)]
        public string PaymentType { get; set; }
        public int? ReferenceID { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        [StringLength(20)]
        public string PaymentMethod { get; set; }
        [StringLength(20)]
        public string PaymentStatus { get; set; }
        [StringLength(100)]
        public string TransactionID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? RefundedAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RefundedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(AppointmentID))]
        [InverseProperty(nameof(Appointments.Payments))]
        public virtual Appointments Appointment { get; set; }
        [ForeignKey(nameof(PatientID))]
        [InverseProperty(nameof(Patients.Payments))]
        public virtual Patients Patient { get; set; }
        [InverseProperty("Payment")]
        public virtual ICollection<Invoices> Invoices { get; set; }
    }
}
