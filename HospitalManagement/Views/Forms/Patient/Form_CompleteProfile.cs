using HospitalManagement.Models.EF;
using HospitalManagement.Models.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms.Patient
{
    public class Form_CompleteProfile : Form
    {
        private int _userId;
        private TextBox txtInsurance;
        private TextBox txtAddress;
        private ComboBox cmbGender;
        private DateTimePicker dtpDob;
        private ComboBox cmbBloodType;
        private TextBox txtEmergencyContact;
        private TextBox txtEmergencyPhone;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTitle;

        public Form_CompleteProfile(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadUserName();
        }

        private void LoadUserName()
        {
            try
            {
                using (var db = new HospitalDbContext())
                {
                    var user = db.Users.Find(_userId);
                    if (user != null)
                    {
                        lblTitle.Text = $"HỒ SƠ: {user.FullName.ToUpper()}";
                    }

                    // Load existing patient info if any
                    var patient = db.Patients.FirstOrDefault(p => p.UserID == _userId);
                    if (patient != null)
                    {
                        if (patient.DateOfBirth.HasValue) dtpDob.Value = patient.DateOfBirth.Value;
                        if (!string.IsNullOrEmpty(patient.Gender)) cmbGender.SelectedItem = patient.Gender;
                        if (!string.IsNullOrEmpty(patient.BloodType)) cmbBloodType.SelectedItem = patient.BloodType;
                        txtInsurance.Text = patient.InsuranceNumber;
                        txtAddress.Text = patient.Address;
                        txtEmergencyContact.Text = patient.EmergencyContact;
                        txtEmergencyPhone.Text = patient.EmergencyPhone;
                        
                        btnSave.Text = "Cập nhật hồ sơ";
                    }
                }
            }
            catch { }
        }

        private void InitializeComponent()
        {
            this.Text = "Hoàn thiện hồ sơ bệnh nhân";
            this.Size = new Size(450, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);

            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30) };
            this.Controls.Add(panel);

            lblTitle = new Label 
            { 
                Text = "CẬP NHẬT THÔNG TIN CÁ NHÂN",
                Location = new Point(30, 20), 
                Size = new Size(390, 40), 
                Font = new Font("Segoe UI", 14F, FontStyle.Bold), 
                ForeColor = Color.FromArgb(59, 130, 246),
                TextAlign = ContentAlignment.MiddleCenter
            };
            
            int startY = 80;
            int gap = 65;

            var lblDob = new Label { Text = "Ngày sinh:", Location = new Point(30, startY), AutoSize = true };
            dtpDob = new DateTimePicker 
            { 
                Location = new Point(30, startY + 25), 
                Size = new Size(375, 30), 
                Format = DateTimePickerFormat.Short,
                MaxDate = DateTime.Today,
                Value = new DateTime(2000, 1, 1) // Để mặc định năm 2000 cho tiện chọn
            };

            var lblGen = new Label { Text = "Giới tính:", Location = new Point(30, startY + gap), AutoSize = true };
            cmbGender = new ComboBox { Location = new Point(30, startY + gap + 25), Size = new Size(170, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGender.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cmbGender.SelectedIndex = 0;

            var lblBlood = new Label { Text = "Nhóm máu:", Location = new Point(235, startY + gap), AutoSize = true };
            cmbBloodType = new ComboBox { Location = new Point(235, startY + gap + 25), Size = new Size(170, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbBloodType.Items.AddRange(new string[] { "A", "B", "AB", "O", "N/A" });
            cmbBloodType.SelectedIndex = 4;

            var lblIns = new Label { Text = "Số Bảo hiểm y tế (nếu có):", Location = new Point(30, startY + gap * 2), AutoSize = true };
            txtInsurance = new TextBox { Location = new Point(30, startY + gap * 2 + 25), Size = new Size(375, 30) };

            var lblAddr = new Label { Text = "Địa chỉ hiện tại:", Location = new Point(30, startY + gap * 3), AutoSize = true };
            txtAddress = new TextBox { Location = new Point(30, startY + gap * 3 + 25), Size = new Size(375, 45), Multiline = true };

            var lblEmerCon = new Label { Text = "Người liên hệ khẩn cấp:", Location = new Point(30, startY + gap * 4 + 15), AutoSize = true };
            txtEmergencyContact = new TextBox { Location = new Point(30, startY + gap * 4 + 40), Size = new Size(180, 30) };

            var lblEmerPhone = new Label { Text = "SĐT khẩn cấp:", Location = new Point(225, startY + gap * 4 + 15), AutoSize = true };
            txtEmergencyPhone = new TextBox { Location = new Point(225, startY + gap * 4 + 40), Size = new Size(180, 30) };

            btnSave = new Button 
            { 
                Text = "Hoàn tất & Tiếp tục", 
                Location = new Point(30, 480), 
                Size = new Size(375, 45), 
                BackColor = Color.FromArgb(59, 130, 246), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button 
            { 
                Text = "Để sau", 
                Location = new Point(30, 530), 
                Size = new Size(375, 30),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(148, 163, 184),
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            panel.Controls.AddRange(new Control[] { 
                lblTitle, lblDob, dtpDob, lblGen, cmbGender, lblBlood, cmbBloodType, 
                lblIns, txtInsurance, lblAddr, txtAddress, 
                lblEmerCon, txtEmergencyContact, lblEmerPhone, txtEmergencyPhone,
                btnSave, btnCancel 
            });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = new HospitalDbContext())
                {
                    var patient = db.Patients.FirstOrDefault(p => p.UserID == _userId);
                    bool isNew = false;
                    
                    if (patient == null)
                    {
                        patient = new Patients { UserID = _userId, CreatedAt = DateTime.Now };
                        isNew = true;
                    }

                    patient.DateOfBirth = dtpDob.Value;
                    patient.Gender = cmbGender.SelectedItem.ToString();
                    patient.BloodType = cmbBloodType.SelectedItem.ToString() == "N/A" ? null : cmbBloodType.SelectedItem.ToString();
                    patient.Address = txtAddress.Text.Trim();
                    patient.InsuranceNumber = txtInsurance.Text.Trim();
                    patient.EmergencyContact = txtEmergencyContact.Text.Trim();
                    patient.EmergencyPhone = txtEmergencyPhone.Text.Trim();

                    if (isNew)
                    {
                        db.Patients.Add(patient);
                    }
                    
                    db.SaveChanges();

                    string msg = isNew ? "Hồ sơ của bạn đã được khởi tạo thành công!" : "Hồ sơ của bạn đã được cập nhật thành công!";
                    MessageBox.Show(msg + " Bây giờ bạn có thể tiếp tục.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu hồ sơ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
