using System;

namespace HospitalManagement.Services.Interfaces
{
    public class ActiveExamInfo
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public string Status { get; set; }
        public string ServiceStatus { get; set; } // e.g., "Waiting X-Ray", "Done"
        public DateTime? StartedAt { get; set; }
    }
}
