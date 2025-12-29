using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Patient;
using System;

namespace HospitalManagement.Presenters.Patient
{
    public class HealthRecordPresenter
    {
        private readonly IHealthRecordView _view;
        private readonly IPatientService _patientService;
        private int _patientId;

        public HealthRecordPresenter(IHealthRecordView view, int patientId)
        {
            _view = view;
            _patientId = patientId;
            _patientService = new PatientService();
        }

        public void LoadProfile()
        {
            try
            {
                _view.ShowLoading(true);

                var profile = _patientService.GetPatientProfile(_patientId);
                if (profile != null)
                {
                    _view.LoadPatientInfo(profile);
                }
                else
                {
                    _view.ShowError("Không tìm thấy thông tin bệnh nhân.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải thông tin: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        public void LoadMedicalHistory()
        {
            try
            {
                _view.ShowLoading(true);

                var history = _patientService.GetMedicalHistory(_patientId);
                _view.LoadMedicalHistory(history);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi tải lịch sử khám: {ex.Message}");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }
    }
}
