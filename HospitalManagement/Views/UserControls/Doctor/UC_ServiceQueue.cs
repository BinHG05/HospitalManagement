using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Services.Implementations;
using HospitalManagement.Models.DTOs;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_ServiceQueue : UserControl
    {
        private readonly IServiceRequestService _serviceRequestService;
        private int _currentDoctorId;

        public UC_ServiceQueue(int doctorId)
        {
            InitializeComponent();
            _serviceRequestService = new ServiceRequestService();
            _currentDoctorId = doctorId;
            LoadQueue();
        }

        public void LoadQueue()
        {
            try
            {
                var requests = _serviceRequestService.GetPendingRequestsForDoctor(_currentDoctorId);
                dgvQueue.DataSource = requests;
                
                // Format DataGridView
                if (dgvQueue.Columns["RequestId"] != null) dgvQueue.Columns["RequestId"].Visible = false;
                if (dgvQueue.Columns["PatientName"] != null) dgvQueue.Columns["PatientName"].HeaderText = "Bệnh nhân";
                if (dgvQueue.Columns["ServiceName"] != null) dgvQueue.Columns["ServiceName"].HeaderText = "Dịch vụ";
                if (dgvQueue.Columns["Status"] != null) dgvQueue.Columns["Status"].HeaderText = "Trạng thái";
                if (dgvQueue.Columns["QueueNumber"] != null) dgvQueue.Columns["QueueNumber"].HeaderText = "STT";
                if (dgvQueue.Columns["RequestedAt"] != null) dgvQueue.Columns["RequestedAt"].HeaderText = "Giờ yêu cầu";
                if (dgvQueue.Columns["DoctorNotes"] != null) dgvQueue.Columns["DoctorNotes"].HeaderText = "Ghi chú BS";
                if (dgvQueue.Columns["ResultDetails"] != null) dgvQueue.Columns["ResultDetails"].HeaderText = "Kết quả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hàng đợi: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadQueue();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (dgvQueue.SelectedRows.Count > 0)
            {
                var requestId = (int)dgvQueue.SelectedRows[0].Cells["RequestId"].Value;
                OnExecuteRequest?.Invoke(this, requestId);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một yêu cầu để thực hiện.");
            }
        }

        public event EventHandler<int> OnExecuteRequest;
    }
}
