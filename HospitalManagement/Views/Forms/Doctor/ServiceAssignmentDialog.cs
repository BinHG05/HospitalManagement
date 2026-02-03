using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using HospitalManagement.Services.Implementations;
using HospitalManagement.Services.Interfaces;

namespace HospitalManagement.Views.Forms.Doctor
{
    public partial class ServiceAssignmentDialog : Form
    {
        private readonly IDoctorService _doctorService;
        
        public class ServiceItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        public int SelectedServiceId { get; private set; }
        public string SelectedServiceName { get; private set; }

        public ServiceAssignmentDialog()
        {
            InitializeComponent();
            _doctorService = new DoctorService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var services = _doctorService.GetAllServices();
                cboServices.Items.Clear();
                foreach (var s in services)
                {
                    cboServices.Items.Add(new ServiceItem { Id = s.ServiceID, Name = s.ServiceName });
                }
                if (cboServices.Items.Count > 0) cboServices.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách dịch vụ: " + ex.Message);
            }

            // Ẩn phần chọn bác sĩ vì hệ thống tự động gán
            if (lblDoctor != null) lblDoctor.Visible = false; // "Bác sĩ thực hiện"
            if (cboDoctors != null) cboDoctors.Visible = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cboServices.SelectedItem is ServiceItem service)
            {
                SelectedServiceId = service.Id;
                SelectedServiceName = service.Name;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
