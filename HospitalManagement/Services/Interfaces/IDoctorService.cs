using HospitalManagement.Models.Entities;
using System;
using System.Collections.Generic;

namespace HospitalManagement.Services.Interfaces
{
    public interface IDoctorService
    {
        // Get doctor by UserID
        Doctors GetDoctorByUserId(int userId);

        // Get today's patient queue
        IEnumerable<QueuePatientInfo> GetTodayQueue(int doctorId);
        IEnumerable<ActiveExamInfo> GetActiveExaminations(int doctorId);
        IEnumerable<HospitalActiveExamInfo> GetAllActiveExaminations();
        IEnumerable<MedicalServices> GetAllServices();
        bool AssignService(int appointmentId, int serviceId);

        // Get patient by appointment
        PatientExamInfo GetPatientForExam(int appointmentId);

        // Call next patient
        bool CallPatient(int appointmentId);
        bool MarkAsNoShow(int appointmentId);

        // Complete examination
        int CompleteExamination(int appointmentId, ExaminationData data);

        // Get doctor's schedule
        IEnumerable<DoctorScheduleInfo> GetDoctorSchedule(int doctorId, DateTime weekStart);
    }

    public class QueuePatientInfo
    {
        public int AppointmentId { get; set; }
        public int QueueNumber { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public string Symptoms { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string StatusDisplay
        {
            get
            {
                switch (Status)
                {
                    case "pending": return "Chờ khám";
                    case "confirmed": return "Đã xác nhận";
                    case "completed": return "Hoàn thành";
                    default: return Status;
                }
            }
        }
    }

    public class PatientExamInfo
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
        public string Symptoms { get; set; }
        public string InsuranceNumber { get; set; }

        // Medical history summary
        public int TotalVisits { get; set; }
        public string LastDiagnosis { get; set; }
    }

    public class ExaminationData
    {
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public string TreatmentPlan { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
    }

    public class DoctorScheduleInfo
    {
        public int ScheduleId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TotalSlots { get; set; }
        public int BookedSlots { get; set; }
        public int CompletedSlots { get; set; }
        public bool IsActive { get; set; }

        public string TimeRange => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
    }

    public class HospitalActiveExamInfo
    {
        public int AppointmentId { get; set; }
        public int QueueNumber { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public string Status { get; set; }
        public string ServiceStatus { get; set; }
        public DateTime StartedAt { get; set; }
    }
}
