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

        public void LoadQueueNumbers(int scheduleId, DateTime date, int maxPatients)
        {
            try
            {
                var bookedNumbers = _appointmentService.GetBookedQueueNumbers(scheduleId, date);
                var suggestedNumber = _appointmentService.GetNextAvailableQueueNumber(scheduleId, date, maxPatients);
                
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

                var appointmentId = _appointmentService.BookAppointment(
                    _patientId, scheduleId, date, queueNumber, reason);

                if (appointmentId > 0)
                {
                    // Show Payment Dialog instead of Success immediately
                    _view.ShowPaymentPrompt(appointmentId, "150,000 VND");
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
            _currentWeekStart = _currentWeekStart.AddDays(7);
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
