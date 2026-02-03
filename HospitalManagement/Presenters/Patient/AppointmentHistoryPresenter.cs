using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Patient;
using System;
using System.Linq;

namespace HospitalManagement.Presenters.Patient
{
    public class AppointmentHistoryPresenter
    {
        private readonly IAppointmentHistoryView _view;
        private readonly IAppointmentService _appointmentService;
        private int _patientId;

        public AppointmentHistoryPresenter(IAppointmentHistoryView view, int patientId)
        {
            _view = view;
            _patientId = patientId;
            _appointmentService = new AppointmentService();
        }

        public void LoadAppointments(string statusFilter = "all")
        {
            try
            {
                _view.ShowLoading(true);

                var appointments = _appointmentService.GetPatientAppointments(_patientId);
                
                var displayList = appointments
                    .GroupBy(a => a.AppointmentID)
                    .Select(g => g.First())
                    .Select(a => new AppointmentDisplayInfo
                    {
                        AppointmentId = a.AppointmentID,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentNumber = a.AppointmentNumber,
                        DepartmentName = a.Department?.DepartmentName ?? "N/A",
                        DoctorName = a.Doctor?.User?.FullName ?? "N/A",
                        ShiftName = a.Shift?.ShiftName ?? "N/A",
                        ShiftStartTime = a.Shift?.StartTime,
                        ShiftEndTime = a.Shift?.EndTime,
                        Symptoms = a.Symptoms,
                        Status = a.Status,
                        RoomNumber = a.RoomNumber,
                        CreatedAt = a.CreatedAt
                    });

                // Apply filter
                if (statusFilter != "all")
                {
                    displayList = displayList.Where(a => a.Status == statusFilter);
                }

                _view.LoadAppointments(displayList.ToList());
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải danh sách lịch hẹn: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void CancelAppointment(int appointmentId)
        {
            try
            {
                _view.ShowLoading(true);

                var success = _appointmentService.CancelAppointment(appointmentId);
                
                if (success)
                {
                    _view.ShowSuccess("Đã hủy lịch hẹn thành công!");
                    _view.RefreshList();
                }
                else
                {
                    _view.ShowError("Không thể hủy lịch hẹn. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi hủy lịch: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void ViewDetails(int appointmentId)
        {
            var appointments = _appointmentService.GetPatientAppointments(_patientId);
            var appointment = appointments.FirstOrDefault(a => a.AppointmentID == appointmentId);
            
            if (appointment != null)
            {
                var display = new AppointmentDisplayInfo
                {
                    AppointmentId = appointment.AppointmentID,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentNumber = appointment.AppointmentNumber,
                    DepartmentName = appointment.Department?.DepartmentName ?? "N/A",
                    DoctorName = appointment.Doctor?.User?.FullName ?? "N/A",
                    ShiftName = appointment.Shift?.ShiftName ?? "N/A",
                    ShiftStartTime = appointment.Shift?.StartTime,
                    ShiftEndTime = appointment.Shift?.EndTime,
                    Symptoms = appointment.Symptoms,
                    Status = appointment.Status,
                    RoomNumber = appointment.RoomNumber,
                    CreatedAt = appointment.CreatedAt
                };
                
                _view.ShowAppointmentDetails(display);
            }
        }
    }
}
