using System;
using System.IO;
using System.Windows.Forms;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Services.Implementations;
using HospitalManagement.Models.DTOs;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_ServiceExecution : UserControl
    {
        private readonly IServiceResultService _serviceResultService;
        private readonly IServiceRequestService _serviceRequestService;
        private int _requestId;
        private int _doctorId;

        public UC_ServiceExecution(int doctorId)
        {
            InitializeComponent();
            _serviceResultService = new ServiceResultService();
            _serviceRequestService = new ServiceRequestService();
            _doctorId = doctorId;
        }

        public void SetRequest(int requestId)
        {
            _requestId = requestId;
            LoadRequestInfo();
        }

        private void LoadRequestInfo()
        {
            try
            {
                var request = _serviceRequestService.GetRequestById(_requestId);
                if (request != null)
                {
                    lblPatientName.Text = "Bệnh nhân: " + request.PatientName;
                    lblServiceName.Text = "Dịch vụ: " + request.ServiceName;
                    txtDoctorNotes.Text = request.DoctorNotes;
                    txtResult.Clear();
                    txtFilePath.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin yêu cầu: " + ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.pdf|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = ofd.FileName;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResult.Text))
            {
                MessageBox.Show("Vui lòng nhập kết quả chẩn đoán.");
                return;
            }

            try
            {
                var result = _serviceResultService.CreateResult(_requestId, txtResult.Text, txtFilePath.Text, _doctorId);
                if (result != null)
                {
                    MessageBox.Show("Lưu kết quả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnComplete?.Invoke(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu kết quả: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            OnCancel?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnComplete;
        public event EventHandler OnCancel;
    }
}
