using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Departments
    {
        public Departments()
        {
            Appointments = new HashSet<Appointments>();
            DoctorSchedules = new HashSet<DoctorSchedules>();
            Doctors = new HashSet<Doctors>();
            MedicalServices = new HashSet<MedicalServices>();
        }

        [Key]
        public int DepartmentID { get; set; }
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Location { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        public int? HeadDoctorID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(HeadDoctorID))]
        [InverseProperty("Departments")]
        public virtual Doctors HeadDoctor { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Appointments> Appointments { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DoctorSchedules> DoctorSchedules { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Doctors> Doctors { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<MedicalServices> MedicalServices { get; set; }
    }
}
