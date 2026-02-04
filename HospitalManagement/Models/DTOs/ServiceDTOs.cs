using System;

namespace HospitalManagement.Models.DTOs
{
    public class ServiceRequestInfo
    {
        public int RequestId { get; set; }
        public string PatientName { get; set; }
        public string ServiceName { get; set; }
        public string Status { get; set; } // requested, in_progress, completed, cancelled
        public DateTime RequestedAt { get; set; }
        public int QueueNumber { get; set; }
        public string DoctorNotes { get; set; }
        public string ResultDetails { get; set; }
    }

    public class ServiceResultInfo
    {
        public int ResultId { get; set; }
        public string ServiceName { get; set; }
        public string ResultDetails { get; set; }
        public string ResultFile { get; set; }
        public string PerformedByName { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}
