using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Shifts
    {
        public Shifts()
        {
            Appointments = new HashSet<Appointments>();
            DoctorSchedules = new HashSet<DoctorSchedules>();
        }

        [Key]
        public int ShiftID { get; set; }
        [Required]
        [StringLength(50)]
        public string ShiftName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int? MaxSlots { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [InverseProperty("Shift")]
        public virtual ICollection<Appointments> Appointments { get; set; }
        [InverseProperty("Shift")]
        public virtual ICollection<DoctorSchedules> DoctorSchedules { get; set; }
    }
}
