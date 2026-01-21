using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Doctor;
using System;

namespace HospitalManagement.Presenters.Doctor
{
    public class PatientQueuePresenter
    {
        private readonly IPatientQueueView _view;
        private readonly IDoctorService _doctorService;
        private int _doctorId;

        public PatientQueuePresenter(IPatientQueueView view, int doctorId)
        {
            _view = view;
            _doctorId = doctorId;
            _doctorService = new DoctorService();
        }

        public void LoadQueue()
        {
            try
            {
                _view.ShowLoading(true);
                var queue = _doctorService.GetTodayQueue(_doctorId);
                _view.LoadQueue(queue);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải hàng đợi: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void CallPatient(int appointmentId)
        {
            try
            {
                _view.ShowLoading(true);
                var success = _doctorService.CallPatient(appointmentId);
                
                if (success)
                {
                    _view.ShowSuccess("Đã gọi bệnh nhân!");
                    _view.RefreshQueue();
                }
                else
                {
                    _view.ShowError("Không thể gọi bệnh nhân.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void ViewPatientDetails(int appointmentId)
        {
            try
            {
                var patient = _doctorService.GetPatientForExam(appointmentId);
                if (patient != null)
                {
                    _view.ShowPatientDetails(patient);
                }
                else
                {
                    _view.ShowError("Không tìm thấy thông tin bệnh nhân.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi: {ex.Message}");
            }
        }

        public void StartExamination(int appointmentId)
        {
            // Ensure status is updated to 'confirmed' (or 'examining') in DB
            // so it appears in the Active Examinations list.
            var success = _doctorService.CallPatient(appointmentId);
            if (success)
            {
                _view.OpenExamination(appointmentId);
            }
            else
            {
                _view.ShowError("Có lỗi khi bắt đầu khám. Vui lòng thử lại.");
            }
        }
    }
}
