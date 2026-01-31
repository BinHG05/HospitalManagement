using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Views.Forms
{
    public class Form_EditPatient : Form
    {
        private int _patientId;
        private TextBox txtInsurance;
        private TextBox txtAddress;
        private ComboBox cmbGender;
        private DateTimePicker dtpDob;
        private ComboBox cmbBloodType;
        private Button btnSave;
        private Button btnCancel;
        private Label lblName;

        public Form_EditPatient(int patientId)
        {
            _patientId = patientId;
            InitializeComponent();
            LoadPatientData();
        }

        private void InitializeComponent()
        {
            this.Text = "Cập nhật thông tin bệnh nhân";
            this.Size = new Size(400, 450);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);

            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            this.Controls.Add(panel);

            lblName = new Label { Location = new Point(20, 10), Size = new Size(340, 30), Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(59, 130, 246) };
            
            var lblIns = new Label { Text = "Số BHYT (Cố định):", Location = new Point(20, 50), AutoSize = true };
            txtInsurance = new TextBox { Location = new Point(20, 75), Size = new Size(340, 25), ReadOnly = true, BackColor = Color.FromArgb(248, 250, 252) };

            var lblDob = new Label { Text = "Ngày sinh:", Location = new Point(20, 110), AutoSize = true };
            dtpDob = new DateTimePicker { Location = new Point(20, 135), Size = new Size(340, 25), Format = DateTimePickerFormat.Short };

            var lblGen = new Label { Text = "Giới tính:", Location = new Point(20, 170), AutoSize = true };
            cmbGender = new ComboBox { Location = new Point(20, 195), Size = new Size(160, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGender.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });

            var lblBlood = new Label { Text = "Nhóm máu:", Location = new Point(200, 170), AutoSize = true };
            cmbBloodType = new ComboBox { Location = new Point(200, 195), Size = new Size(160, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbBloodType.Items.AddRange(new string[] { "A", "B", "AB", "O", "N/A" });

            var lblAddr = new Label { Text = "Địa chỉ:", Location = new Point(20, 230), AutoSize = true };
            txtAddress = new TextBox { Location = new Point(20, 255), Size = new Size(340, 60), Multiline = true };

            btnSave = new Button 
            { 
                Text = "Lưu thay đổi", 
                Location = new Point(20, 335), 
                Size = new Size(160, 40), 
                BackColor = Color.FromArgb(59, 130, 246), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button 
            { 
                Text = "Hủy bỏ", 
                Location = new Point(200, 335), 
                Size = new Size(160, 40),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(71, 85, 105),
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            panel.Controls.AddRange(new Control[] { lblName, lblIns, txtInsurance, lblDob, dtpDob, lblGen, cmbGender, lblBlood, cmbBloodType, lblAddr, txtAddress, btnSave, btnCancel });
        }

        private void LoadPatientData()
        {
            try
            {
                using (var db = new HospitalDbContext())
                {
                    var patient = db.Patients.Include("User").FirstOrDefault(p => p.PatientID == _patientId);
                    if (patient != null)
                    {
                        lblName.Text = patient.User.FullName;
                        txtInsurance.Text = patient.InsuranceNumber;
                        txtAddress.Text = patient.Address;
                        dtpDob.Value = patient.DateOfBirth ?? DateTime.Now;
                        
                        var gender = (patient.Gender == "Nam" || patient.Gender == "male") ? "Nam" : (patient.Gender == "Nữ" || patient.Gender == "female") ? "Nữ" : "Khác";
                        cmbGender.SelectedItem = gender;
                        
                        cmbBloodType.SelectedItem = patient.BloodType ?? "N/A";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new HospitalDbContext())
                {
                    var patient = db.Patients.Find(_patientId);
                    if (patient != null)
                    {
                        patient.InsuranceNumber = txtInsurance.Text.Trim();
                        patient.Address = txtAddress.Text.Trim();
                        patient.DateOfBirth = dtpDob.Value;
                        patient.Gender = cmbGender.SelectedItem.ToString();
                        patient.BloodType = cmbBloodType.SelectedItem.ToString() == "N/A" ? null : cmbBloodType.SelectedItem.ToString();
                        
                        db.SaveChanges();
                        MessageBox.Show("Đã cập nhật thông tin bệnh nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }
    }
}
