using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Doctor;
using System;
using System.Windows.Forms;

namespace HospitalManagement.Presenters.Doctor
{
    public class ExaminationPresenter
    {
        private readonly IExaminationView _view;
        private readonly IDoctorService _doctorService;
        private int _appointmentId;
        private PatientExamInfo _currentPatient;

        public ExaminationPresenter(IExaminationView view, int appointmentId)
        {
            _view = view;
            _appointmentId = appointmentId;
            _doctorService = new DoctorService();
        }

        public void LoadPatient()
        {
            try
            {
                _view.ShowLoading(true);
                
                _currentPatient = _doctorService.GetPatientForExam(_appointmentId);
                
                if (_currentPatient != null)
                {
                    _view.LoadPatientInfo(_currentPatient);
                    _view.Symptoms = _currentPatient.Symptoms ?? "";
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

        public void SaveExamination()
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(_view.Diagnosis))
                {
                    _view.ShowError("Vui lòng nhập chẩn đoán.");
                    return;
                }

                _view.ShowLoading(true);

                var data = new ExaminationData
                {
                    Symptoms = _view.Symptoms,
                    Diagnosis = _view.Diagnosis,
                    Notes = _view.Notes,
                    TreatmentPlan = _view.TreatmentPlan,
                    NextAppointmentDate = _view.NextAppointmentDate
                };

                var examId = _doctorService.CompleteExamination(_appointmentId, data);

                if (examId > 0)
                {
                    // _view.ShowSuccess("Đã lưu kết quả khám bệnh!");
                    
                    var result = MessageBox.Show(
                        "Đã lưu kết quả khám bệnh. Bạn có muốn kê đơn thuốc cho bệnh nhân này không?",
                        "Kê đơn thuốc",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        _view.NavigateToPrescription(examId);
                    }
                    else
                    {
                        _view.CloseView();
                    }
                }
                else
                {
                    _view.ShowError("Không thể lưu kết quả. Vui lòng thử lại.");
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
        public bool AssignService(string serviceName, int? targetDoctorId)
        {
            try
            {
                _view.ShowLoading(true);

                var success = _doctorService.AssignService(_appointmentId, serviceName, targetDoctorId);

                if (success)
                {
                    _view.ShowSuccess($"Đã chỉ định dịch vụ: {serviceName}\nBệnh nhân đã được chuyển sang danh sách chờ dịch vụ.");
                    _view.CloseView();
                    return true;
                }
                else
                {
                    _view.ShowError("Không thể chỉ định dịch vụ. Vui lòng thử lại.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi: {ex.Message}");
                return false;
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }
    }
}
