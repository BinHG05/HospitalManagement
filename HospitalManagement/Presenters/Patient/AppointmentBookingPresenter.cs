using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Patient;
using System;

namespace HospitalManagement.Presenters.Patient
{
    public class AppointmentBookingPresenter
    {
        private readonly IAppointmentBookingView _view;
        private readonly IAppointmentService _appointmentService;
        private int _patientId;
        private DateTime _currentWeekStart;

        public AppointmentBookingPresenter(IAppointmentBookingView view, int patientId)
        {
            _view = view;
            _patientId = patientId;
            _appointmentService = new AppointmentService();
            _currentWeekStart = GetWeekStart(DateTime.Today);
        }

        public void Initialize()
        {
            LoadDepartments();
            LoadPatientInfo();
        }

        private void LoadPatientInfo()
        {
            try
            {
                var patientService = new PatientService();
                var profile = patientService.GetPatientProfile(_patientId);
                if (profile != null)
                {
                    _view.UpdatePatientProfile(profile);
                }
            }
            catch { /* Ignore, not critical for core flow but nice for confirmation */ }
        }

        public void LoadDepartments()
        {
            try
            {
                _view.ShowLoading(true);
                var departments = _appointmentService.GetAllDepartments();
                _view.LoadDepartments(departments);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải danh sách khoa: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void LoadWeeklySchedule(int departmentId)
        {
            try
            {
                _view.ShowLoading(true);
                var schedules = _appointmentService.GetDepartmentWeeklySchedule(departmentId, _currentWeekStart);
                _view.ShowWeeklySchedule(schedules);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải lịch khoa: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void LoadTimeSlots(int departmentId, DateTime date)
        {
            try
            {
                _view.ShowLoading(true);
                var slots = _appointmentService.GetTimeSlots(departmentId, date);
                _view.ShowTimeSlots(slots);
                _view.GoToTimeSlotSelection();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải khung giờ: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void LoadQueueNumbers(int departmentId, DateTime date, int shiftId, int maxPatients)
        {
            try
            {
                // [NEW] Dynamic selection of least-busy doctor
                var bestScheduleId = _appointmentService.GetBestScheduleForSlot(departmentId, date, shiftId);
                
                if (bestScheduleId <= 0)
                {
                    _view.ShowError("Không có bác sĩ nào trực trong khung giờ này.");
                    return;
                }

                _view.SetSelectedSchedule(bestScheduleId);

                var bookedNumbers = _appointmentService.GetBookedQueueNumbers(bestScheduleId, date);
                var suggestedNumber = _appointmentService.GetNextAvailableQueueNumber(bestScheduleId, date, maxPatients);
                
                _view.ShowQueueNumbers(bookedNumbers, suggestedNumber, maxPatients);
                _view.GoToQueueSelection();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải số thứ tự: {ex.Message}");
            }
        }

        public void BookAppointment()
        {
            try
            {
                _view.ShowLoading(true);

                var scheduleId = _view.SelectedScheduleId;
                var date = _view.SelectedDate;
                var queueNumber = _view.SelectedQueueNumber;
                var reason = _view.Reason;

                if (scheduleId <= 0 || queueNumber <= 0)
                {
                    _view.ShowError("Vui lòng chọn đầy đủ thông tin đặt lịch.");
                    return;
                }

                if (date > DateTime.Today.AddMonths(3))
                {
                     _view.ShowError("Chỉ có thể đặt lịch trong vòng 3 tháng tới.");
                     return;
                }

                var appointmentId = _appointmentService.BookAppointment(
                    _patientId, scheduleId, date, queueNumber, reason);

                if (appointmentId > 0)
                {
                    // Tính tiền thực tế để hiển thị ở bước thanh toán sau
                    var patientService = new PatientService();
                    var profile = patientService.GetPatientProfile(_patientId);
                    
                    decimal fee = 150000;
                    if (profile != null && !string.IsNullOrWhiteSpace(profile.InsuranceNumber))
                    {
                        fee = fee * 0.5m;
                    }

                    _view.ShowPaymentPrompt(appointmentId, $"{fee:N0} VND");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void ConfirmPayment(int appointmentId, string method)
        {
            try
            {
                _view.ShowLoading(true);
                bool success = _appointmentService.ConfirmAppointment(appointmentId, method);
                
                if (success)
                {
                    _view.ShowSuccess("Thanh toán thành công! Lịch khám của bạn đã được xác nhận.");
                    _view.ClearSelection();
                    _view.GoBackToWeeklyView();
                }
                else
                {
                    _view.ShowError("Lỗi xác nhận thanh toán. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi thanh toán: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void NavigateToPreviousWeek()
        {
            var newWeekStart = _currentWeekStart.AddDays(-7);
            // Only allow if the week contains at least one future date
            if (newWeekStart.AddDays(6) >= DateTime.Today)
            {
                _currentWeekStart = newWeekStart;
            }
        }

        public void NavigateToNextWeek()
        {
            var nextWeek = _currentWeekStart.AddDays(7);
            var maxDate = DateTime.Today.AddMonths(3);

            if (nextWeek > maxDate)
            {
                _view.ShowError("Lịch khám cho thời gian này chưa được mở. Vui lòng quay lại sau.");
                return;
            }

            _currentWeekStart = nextWeek;
        }

        public void NavigateToWeekOf(DateTime date)
        {
            _currentWeekStart = GetWeekStart(date);
        }

        public DateTime CurrentWeekStart => _currentWeekStart;

        private DateTime GetWeekStart(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }
    }
}
