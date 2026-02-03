using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Appointments
    {
        public Appointments()
        {
            Examinations = new HashSet<Examinations>();
            Payments = new HashSet<Payments>();
        }

        [Key]
        public int AppointmentID { get; set; }
        public int? PatientID { get; set; }
        public int? DoctorID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ScheduleID { get; set; }
        [Column(TypeName = "date")]
        public DateTime AppointmentDate { get; set; }
        public int? ShiftID { get; set; }
        public int AppointmentNumber { get; set; }
        [StringLength(500)]
        public string Symptoms { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [StringLength(50)]
        public string RoomNumber { get; set; }

        [ForeignKey(nameof(DepartmentID))]
        [InverseProperty(nameof(Departments.Appointments))]
        public virtual Departments Department { get; set; }
        [ForeignKey(nameof(DoctorID))]
        [InverseProperty(nameof(Doctors.Appointments))]
        public virtual Doctors Doctor { get; set; }
        [ForeignKey(nameof(PatientID))]
        [InverseProperty(nameof(Patients.Appointments))]
        public virtual Patients Patient { get; set; }
        [ForeignKey(nameof(ScheduleID))]
        [InverseProperty(nameof(DoctorSchedules.Appointments))]
        public virtual DoctorSchedules Schedule { get; set; }
        [ForeignKey(nameof(ShiftID))]
        [InverseProperty(nameof(Shifts.Appointments))]
        public virtual Shifts Shift { get; set; }
        [InverseProperty("Appointment")]
        public virtual ICollection<Examinations> Examinations { get; set; }
        [InverseProperty("Appointment")]
        public virtual ICollection<Payments> Payments { get; set; }
    }
}
