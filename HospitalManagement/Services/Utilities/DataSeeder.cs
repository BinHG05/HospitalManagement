using System;
using System.Linq;
using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;

namespace HospitalManagement.Services.Utilities
{
    public static class DataSeeder
    {
        public static void SeedSchedules()
        {
            try
            {
                using (var context = new HospitalDbContext())
                {
                    // Check if we have schedules for the next 7 days
                    var today = DateTime.Today;
                    
                    // Fetch doctors and shifts
                    var doctors = context.Doctors.Where(d => d.IsActive == true).ToList();
                    var shifts = context.Shifts.ToList();

                    if (!doctors.Any() || !shifts.Any()) return;

                    // Create schedules for next 7 days
                    for (int i = 0; i < 7; i++)
                    {
                        var date = today.AddDays(i);
                        if (date.DayOfWeek == DayOfWeek.Sunday) continue;

                        foreach (var doctor in doctors)
                        {
                            foreach (var shift in shifts)
                            {
                                // Check if schedule exists
                                bool exists = context.DoctorSchedules.Any(ds => 
                                    ds.DoctorID == doctor.DoctorID && 
                                    ds.ScheduleDate == date && 
                                    ds.ShiftID == shift.ShiftID);

                                if (!exists)
                                {
                                    var schedule = new DoctorSchedules
                                    {
                                        DoctorID = doctor.DoctorID,
                                        DepartmentID = doctor.DepartmentID ?? 1,
                                        ShiftID = shift.ShiftID,
                                        ScheduleDate = date,
                                        AvailableSlots = shift.MaxSlots ?? 20, // Follow Model: Use Shift's MaxSlots
                                        IsActive = true,
                                        Status = "Approved", // Auto-created schedules should be Approved
                                        CreatedAt = DateTime.Now,
                                        RequestedAt = DateTime.Now,
                                        ApprovedAt = DateTime.Now
                                    };
                                    context.DoctorSchedules.Add(schedule);
                                }
                            }
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Seeding Error: {ex.Message}");
            }
        }
    }
}
