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
            public decimal Price { get; set; } // [NEW]
            public override string ToString() => Name;
        }

        public List<ServiceItem> SelectedServices { get; private set; } = new List<ServiceItem>();

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
                clbServices.Items.Clear();
                foreach (var s in services)
                {
                    clbServices.Items.Add(new ServiceItem 
                    { 
                        Id = s.ServiceID, 
                        Name = s.ServiceName,
                        Price = s.Price 
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách dịch vụ: " + ex.Message);
            }

            // Ẩn phần chọn bác sĩ vì hệ thống tự động gán
            if (lblDoctor != null) lblDoctor.Visible = false; 
            if (cboDoctors != null) cboDoctors.Visible = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SelectedServices.Clear();
            foreach (var item in clbServices.CheckedItems)
            {
                if (item is ServiceItem service)
                {
                    SelectedServices.Add(service);
                }
            }

            if (SelectedServices.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
