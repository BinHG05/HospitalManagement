using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Patient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagement.Services.Implementations
{
    public class PatientService : IPatientService
    {
        public Patients GetPatientByUserId(int userId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Patients
                    .Include(p => p.User)
                    .FirstOrDefault(p => p.UserID == userId);
            }
        }

        public PatientProfileInfo GetPatientProfile(int patientId)
        {
            using (var context = new HospitalDbContext())
            {
                var patient = context.Patients
                    .Include(p => p.User)
                    .FirstOrDefault(p => p.PatientID == patientId);

                if (patient == null) return null;

                return new PatientProfileInfo
                {
                    PatientId = patient.PatientID,
                    FullName = patient.User?.FullName,
                    Email = patient.User?.Email,
                    Phone = patient.User?.Phone,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    Address = patient.Address,
                    BloodType = patient.BloodType,
                    InsuranceNumber = patient.InsuranceNumber,
                    EmergencyContact = patient.EmergencyContact,
                    EmergencyPhone = patient.EmergencyPhone,
                    CreatedAt = patient.CreatedAt
                };
            }
        }

        public IEnumerable<MedicalHistoryDisplayInfo> GetMedicalHistory(int patientId)
        {
            using (var context = new HospitalDbContext())
            {
                var history = context.MedicalHistory
                    .Include(h => h.Doctor)
                    .Include(h => h.Doctor.User)
                    .Include(h => h.Doctor.Department)
                    .Where(h => h.PatientID == patientId)
                    .OrderByDescending(h => h.VisitDate)
                    .ToList();

                return history.Select(h => new MedicalHistoryDisplayInfo
                {
                    RecordId = h.RecordID.HasValue ? h.RecordID.Value : 0,
                    VisitDate = h.VisitDate,
                    DoctorName = h.Doctor?.User?.FullName ?? "N/A",
                    DepartmentName = h.Doctor?.Department?.DepartmentName ?? "N/A",
                    Diagnosis = h.Diagnosis,
                    Treatment = h.Treatment,
                    NextAppointmentDate = h.NextAppointmentDate
                }).ToList();
            }
        }

        public bool UpdatePatientInfo(int patientId, Patients updatedInfo)
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var patient = context.Patients.Find(patientId);
                    if (patient == null) return false;

                    patient.DateOfBirth = updatedInfo.DateOfBirth;
                    patient.Gender = updatedInfo.Gender;
                    patient.Address = updatedInfo.Address;
                    patient.BloodType = updatedInfo.BloodType;
                    patient.InsuranceNumber = updatedInfo.InsuranceNumber;
                    patient.EmergencyContact = updatedInfo.EmergencyContact;
                    patient.EmergencyPhone = updatedInfo.EmergencyPhone;

                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
