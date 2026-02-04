using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Doctor;
using HospitalManagement.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Presenters.Doctor
{
    public class ExaminationPresenter
    {
        private readonly IExaminationView _view;
        private readonly IDoctorService _doctorService;
        private readonly IServiceRequestService _serviceRequestService;
        private int _appointmentId;
        private PatientExamInfo _currentPatient;

        public ExaminationPresenter(IExaminationView view, int appointmentId)
        {
            _view = view;
            _appointmentId = appointmentId;
            _doctorService = new DoctorService();
            _serviceRequestService = new ServiceRequestService();
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

                // Check and show service status
                RefreshServiceStatus();
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

        public void RefreshServiceStatus()
        {
            var services = _doctorService.GetActiveAssignedServices(_appointmentId);
            _view.LoadServiceRequests(services);

            var isCompletedOrNone = _doctorService.GetPatientServiceStatus(_appointmentId);
            
            if (!isCompletedOrNone)
            {
                _view.SetCompleteButtonEnabled(false);
                _view.ShowServiceStatus("Đang chờ kết quả dịch vụ chuyên khoa...");
            }
            else if (services.Any())
            {
                _view.SetCompleteButtonEnabled(true);
                _view.ShowServiceStatus("Đã có đủ kết quả cận lâm sàng.");
            }
            else
            {
                _view.SetCompleteButtonEnabled(true);
                _view.ShowServiceStatus("");
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

        public bool AssignServices(List<int> serviceIds, string serviceNames)
        {
            try
            {
                _view.ShowLoading(true);

                // Lấy thông tin bác sĩ hiện tại đang khám (từ _currentPatient hoặc session)
                // Giả sử appointments.DoctorID là bác sĩ chỉ định.
                using (var context = new HospitalManagement.Models.EF.HospitalDbContext())
                {
                    var appointment = context.Appointments.Find(_appointmentId);
                    int requestingDoctorId = appointment?.DoctorID ?? 0;

                    foreach (var serviceId in serviceIds)
                    {
                        _serviceRequestService.CreateServiceRequest(_appointmentId, serviceId, requestingDoctorId, _view.Notes, false);
                    }
                }

                _view.ShowSuccess($"Đã chỉ định các dịch vụ: {serviceNames}\nHồ sơ bệnh nhân đã được tự động chuyển đến các bác sĩ chuyên khoa.\n\nBạn sẽ có thể hoàn tất việc chẩn đoán sau khi có kết quả từ các khoa này.");
                
                RefreshServiceStatus();
                return true;
            }
            catch (Exception ex)
            {
                _view.ShowError($"Lỗi khi chỉ định dịch vụ: {ex.Message}");
                return false;
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }
    }
}
