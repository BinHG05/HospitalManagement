using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        public Doctors GetDoctorByUserId(int userId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Doctors
                    .Include(d => d.User)
                    .Include(d => d.Department)
                    .FirstOrDefault(d => d.UserID == userId);
            }
        }

        public IEnumerable<QueuePatientInfo> GetTodayQueue(int doctorId)
        {
            using (var context = new HospitalDbContext())
            {
                var today = DateTime.Today;

                var appointments = context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Patient.User)
                    .Where(a => a.DoctorID == doctorId
                             && a.AppointmentDate == today
                             && (a.Status == "pending" || a.Status == "confirmed"))
                    .OrderBy(a => a.AppointmentNumber)
                    .ToList();

                return appointments.Select(a => new QueuePatientInfo
                {
                    AppointmentId = a.AppointmentID,
                    QueueNumber = a.AppointmentNumber,
                    PatientName = a.Patient?.User?.FullName ?? "N/A",
                    Gender = a.Patient?.Gender,
                    Age = a.Patient?.DateOfBirth != null
                        ? (int?)((DateTime.Today - a.Patient.DateOfBirth.Value).TotalDays / 365.25)
                        : null,
                    Symptoms = a.Symptoms,
                    Status = a.Status,
                    CreatedAt = a.CreatedAt
                }).ToList();
            }
        }

        public PatientExamInfo GetPatientForExam(int appointmentId)
        {
            using (var context = new HospitalDbContext())
            {
                var appointment = context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Patient.User)
                    .FirstOrDefault(a => a.AppointmentID == appointmentId);

                if (appointment?.Patient == null) return null;

                var patient = appointment.Patient;
                var totalVisits = context.MedicalHistory.Count(h => h.PatientID == patient.PatientID);
                var lastRecord = context.MedicalHistory
                    .Where(h => h.PatientID == patient.PatientID)
                    .OrderByDescending(h => h.VisitDate)
                    .FirstOrDefault();

                return new PatientExamInfo
                {
                    AppointmentId = appointmentId,
                    PatientId = patient.PatientID,
                    PatientName = patient.User?.FullName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    BloodType = patient.BloodType,
                    Address = patient.Address,
                    Symptoms = appointment.Symptoms,
                    InsuranceNumber = patient.InsuranceNumber,
                    TotalVisits = totalVisits,
                    LastDiagnosis = lastRecord?.Diagnosis
                };
            }
        }

        public bool CallPatient(int appointmentId)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var appointment = context.Appointments.Find(appointmentId);
                    if (appointment == null) return false;

                    appointment.Status = "confirmed";
                    appointment.UpdatedAt = DateTime.Now;
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool CompleteExamination(int appointmentId, ExaminationData data)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var appointment = context.Appointments
                        .Include(a => a.Patient)
                        .FirstOrDefault(a => a.AppointmentID == appointmentId);

                    if (appointment == null) return false;

                    // Create examination record
                    var examination = new Examinations
                    {
                        AppointmentID = appointmentId,
                        DoctorID = appointment.DoctorID,
                        PatientID = appointment.PatientID,
                        ExaminationDate = DateTime.Now,
                        Symptoms = data.Symptoms,
                        PreliminaryDiagnosis = data.Diagnosis,
                        Notes = data.Notes,
                        Status = "completed",
                        CreatedAt = DateTime.Now
                    };
                    context.Examinations.Add(examination);
                    context.SaveChanges();

                    // Create medical record
                    var record = new MedicalRecords
                    {
                        PatientID = appointment.PatientID,
                        ExaminationID = examination.ExaminationID,
                        Diagnosis = data.Diagnosis,
                        TreatmentPlan = data.TreatmentPlan,
                        RecordDate = DateTime.Now,
                        CreatedBy = appointment.DoctorID,
                        CreatedAt = DateTime.Now
                    };
                    context.MedicalRecords.Add(record);

                    // Create medical history
                    var history = new MedicalHistory
                    {
                        PatientID = appointment.PatientID,
                        RecordID = record.RecordID,
                        VisitDate = DateTime.Now,
                        DoctorID = appointment.DoctorID,
                        Diagnosis = data.Diagnosis,
                        Treatment = data.TreatmentPlan,
                        NextAppointmentDate = data.NextAppointmentDate,
                        CreatedAt = DateTime.Now
                    };
                    context.MedicalHistory.Add(history);

                    // Update appointment status
                    appointment.Status = "completed";
                    appointment.UpdatedAt = DateTime.Now;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CompleteExamination Error: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<DoctorScheduleInfo> GetDoctorSchedule(int doctorId, DateTime weekStart)
        {
            var weekEnd = weekStart.AddDays(6);

            using (var context = new HospitalDbContext())
            {
                var schedules = context.DoctorSchedules
                    .Include(ds => ds.Shift)
                    .Where(ds => ds.DoctorID == doctorId
                              && ds.ScheduleDate >= weekStart.Date
                              && ds.ScheduleDate <= weekEnd.Date)
                    .ToList();

                var result = new List<DoctorScheduleInfo>();

                foreach (var schedule in schedules)
                {
                    var bookedCount = context.Appointments
                        .Count(a => a.ScheduleID == schedule.ScheduleID
                                 && (a.Status == "pending" || a.Status == "confirmed"));

                    var completedCount = context.Appointments
                        .Count(a => a.ScheduleID == schedule.ScheduleID && a.Status == "completed");

                    result.Add(new DoctorScheduleInfo
                    {
                        ScheduleId = schedule.ScheduleID,
                        ScheduleDate = schedule.ScheduleDate,
                        ShiftName = schedule.Shift?.ShiftName ?? "N/A",
                        StartTime = schedule.Shift?.StartTime ?? TimeSpan.Zero,
                        EndTime = schedule.Shift?.EndTime ?? TimeSpan.Zero,
                        TotalSlots = schedule.AvailableSlots ?? 50,
                        BookedSlots = bookedCount,
                        CompletedSlots = completedCount,
                        IsActive = schedule.IsActive ?? true
                    });
                }

                return result.OrderBy(s => s.ScheduleDate).ThenBy(s => s.StartTime).ToList();
            }
        }
    }
}
