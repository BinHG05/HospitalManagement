using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class DoctorSchedules
    {
        public DoctorSchedules()
        {
            Appointments = new HashSet<Appointments>();
        }

        [Key]
        public int ScheduleID { get; set; }
        public int? DoctorID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ShiftID { get; set; }
        [Column(TypeName = "date")]
        public DateTime ScheduleDate { get; set; }
        public int? AvailableSlots { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        // Approval Workflow Fields
        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        [Column(TypeName = "datetime")]
        public DateTime? RequestedAt { get; set; }

        public int? ApprovedByUserID { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ApprovedAt { get; set; }

        [StringLength(500)]
        public string RejectionReason { get; set; }
        [StringLength(50)]
        public string RoomNumber { get; set; }

        [ForeignKey(nameof(ApprovedByUserID))]
        public virtual Users ApprovedByUser { get; set; }

        [ForeignKey(nameof(DepartmentID))]
        [InverseProperty(nameof(Departments.DoctorSchedules))]
        public virtual Departments Department { get; set; }
        [ForeignKey(nameof(DoctorID))]
        [InverseProperty(nameof(Doctors.DoctorSchedules))]
        public virtual Doctors Doctor { get; set; }
        [ForeignKey(nameof(ShiftID))]
        [InverseProperty(nameof(Shifts.DoctorSchedules))]
        public virtual Shifts Shift { get; set; }
        [InverseProperty("Schedule")]
        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}
