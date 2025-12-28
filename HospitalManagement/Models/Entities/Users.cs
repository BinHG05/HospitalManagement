using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace HospitalManagement.Models.Entities
{
    public partial class Users
    {
        public Users()
        {
            Doctors = new HashSet<Doctors>();
            Patients = new HashSet<Patients>();
        }

        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(20)]
        public string Role { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [StringLength(20)]
        public string Status { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Doctors> Doctors { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Patients> Patients { get; set; }
    }
}
