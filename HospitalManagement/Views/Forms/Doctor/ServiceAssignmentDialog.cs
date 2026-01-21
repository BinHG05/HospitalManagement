using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using HospitalManagement.Services.Implementations;

namespace HospitalManagement.Views.Forms.Doctor
{
    public partial class ServiceAssignmentDialog : Form
    {
        public class ServiceItem
        {
            public string Id { get; set; }
            public string Name { get; set; } // e.g., "X-Ray", "Blood Test"
            public override string ToString() => Name;
        }

        public class DoctorItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        public string SelectedService { get; private set; }
        public int? SelectedDoctorId { get; private set; }
        public string SelectedDoctorName { get; private set; }

        public ServiceAssignmentDialog()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Mock Services (In real app, load from DB)
            cboServices.Items.Add(new ServiceItem { Id = "XRAY", Name = "🔍 Chụp X-Quang" });
            cboServices.Items.Add(new ServiceItem { Id = "BLOOD", Name = "🩸 Xét nghiệm máu" });
            cboServices.Items.Add(new ServiceItem { Id = "URINE", Name = "🧪 Xét nghiệm nước tiểu" });
            cboServices.Items.Add(new ServiceItem { Id = "US", Name = "🖥️ Siêu âm" });
            cboServices.Items.Add(new ServiceItem { Id = "MRI", Name = "🧲 Chụp MRI" });
            cboServices.SelectedIndex = 0;

            // Mock Doctors (or load active via DoctorService)
            // Ideally we should filter doctors who can perform the selected service
            cboDoctors.Items.Add(new DoctorItem { Id = 0, Name = "--- Bất kỳ bác sĩ nào ---" });
            // Should load real doctors here later
            cboDoctors.SelectedIndex = 0;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cboServices.SelectedItem is ServiceItem service)
            {
                SelectedService = service.Name;
                
                if (cboDoctors.SelectedItem is DoctorItem doctor && doctor.Id > 0)
                {
                    SelectedDoctorId = doctor.Id;
                    SelectedDoctorName = doctor.Name;
                }
                else
                {
                    SelectedDoctorId = null;
                    SelectedDoctorName = "Phòng kỹ thuật";
                }

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
