
namespace HospitalManagement.Views.UserControls.Patient
{
    partial class UC_HealthRecord
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabProfile = new System.Windows.Forms.TabPage();
            this.panelProfileContent = new System.Windows.Forms.Panel();
            this.panelMedicalInfo = new System.Windows.Forms.Panel();
            this.lblBloodTypeValue = new System.Windows.Forms.Label();
            this.lblBloodType = new System.Windows.Forms.Label();
            this.lblInsuranceValue = new System.Windows.Forms.Label();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.lblMedicalHeader = new System.Windows.Forms.Label();
            this.panelContactInfo = new System.Windows.Forms.Panel();
            this.lblEmergencyValue = new System.Windows.Forms.Label();
            this.lblEmergency = new System.Windows.Forms.Label();
            this.lblAddressValue = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblEmailValue = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhoneValue = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblContactHeader = new System.Windows.Forms.Label();
            this.panelPersonalHeader = new System.Windows.Forms.Panel();
            this.lblJoinDateValue = new System.Windows.Forms.Label();
            this.lblPatientIdValue = new System.Windows.Forms.Label();
            this.lblAgeValue = new System.Windows.Forms.Label();
            this.lblGenderValue = new System.Windows.Forms.Label();
            this.lblDobValue = new System.Windows.Forms.Label();
            this.lblFullNameValue = new System.Windows.Forms.Label();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.colVisitDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiagnosis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTreatment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabProfile.SuspendLayout();
            this.panelProfileContent.SuspendLayout();
            this.panelMedicalInfo.SuspendLayout();
            this.panelContactInfo.SuspendLayout();
            this.panelPersonalHeader.SuspendLayout();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblTitle.Location = new System.Drawing.Point(25, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 Hồ sơ sức khỏe";
            this.lblTitle.UseCompatibleTextRendering = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabProfile);
            this.tabControl.Controls.Add(this.tabHistory);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(20, 10);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 640);
            this.tabControl.TabIndex = 1;
            // 
            // tabProfile
            // 
            this.tabProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabProfile.Controls.Add(this.panelProfileContent);
            this.tabProfile.Location = new System.Drawing.Point(4, 41);
            this.tabProfile.Name = "tabProfile";
            this.tabProfile.Size = new System.Drawing.Size(992, 595);
            this.tabProfile.TabIndex = 0;
            this.tabProfile.Text = "Thông tin chi tiết";
            // 
            // panelProfileContent
            // 
            this.panelProfileContent.AutoScroll = true;
            this.panelProfileContent.Controls.Add(this.panelMedicalInfo);
            this.panelProfileContent.Controls.Add(this.panelContactInfo);
            this.panelProfileContent.Controls.Add(this.panelPersonalHeader);
            this.panelProfileContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProfileContent.Location = new System.Drawing.Point(0, 0);
            this.panelProfileContent.Name = "panelProfileContent";
            this.panelProfileContent.Padding = new System.Windows.Forms.Padding(20);
            this.panelProfileContent.Size = new System.Drawing.Size(992, 595);
            this.panelProfileContent.TabIndex = 0;
            // 
            // panelMedicalInfo
            // 
            this.panelMedicalInfo.BackColor = System.Drawing.Color.White;
            this.panelMedicalInfo.Controls.Add(this.lblBloodTypeValue);
            this.panelMedicalInfo.Controls.Add(this.lblBloodType);
            this.panelMedicalInfo.Controls.Add(this.lblInsuranceValue);
            this.panelMedicalInfo.Controls.Add(this.lblInsurance);
            this.panelMedicalInfo.Controls.Add(this.lblMedicalHeader);
            this.panelMedicalInfo.Location = new System.Drawing.Point(20, 395);
            this.panelMedicalInfo.Name = "panelMedicalInfo";
            this.panelMedicalInfo.Padding = new System.Windows.Forms.Padding(20);
            this.panelMedicalInfo.Size = new System.Drawing.Size(930, 150);
            this.panelMedicalInfo.TabIndex = 2;
            // 
            // lblBloodTypeValue
            // 
            this.lblBloodTypeValue.AutoSize = true;
            this.lblBloodTypeValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblBloodTypeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblBloodTypeValue.Location = new System.Drawing.Point(550, 60);
            this.lblBloodTypeValue.Name = "lblBloodTypeValue";
            this.lblBloodTypeValue.Size = new System.Drawing.Size(30, 20);
            this.lblBloodTypeValue.Text = "-";
            // 
            // lblBloodType
            // 
            this.lblBloodType.AutoSize = true;
            this.lblBloodType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBloodType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBloodType.Location = new System.Drawing.Point(450, 60);
            this.lblBloodType.Name = "lblBloodType";
            this.lblBloodType.Size = new System.Drawing.Size(89, 19);
            this.lblBloodType.Text = "Nhóm máu:";
            // 
            // lblInsuranceValue
            // 
            this.lblInsuranceValue.AutoSize = true;
            this.lblInsuranceValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblInsuranceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblInsuranceValue.Location = new System.Drawing.Point(150, 60);
            this.lblInsuranceValue.Name = "lblInsuranceValue";
            this.lblInsuranceValue.Size = new System.Drawing.Size(84, 20);
            this.lblInsuranceValue.Text = "-";
            // 
            // lblInsurance
            // 
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblInsurance.Location = new System.Drawing.Point(30, 60);
            this.lblInsurance.Name = "lblInsurance";
            this.lblInsurance.Size = new System.Drawing.Size(73, 19);
            this.lblInsurance.Text = "Số BHYT:";
            // 
            // lblMedicalHeader
            // 
            this.lblMedicalHeader.AutoSize = true;
            this.lblMedicalHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMedicalHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblMedicalHeader.Location = new System.Drawing.Point(20, 20);
            this.lblMedicalHeader.Name = "lblMedicalHeader";
            this.lblMedicalHeader.Size = new System.Drawing.Size(164, 21);
            this.lblMedicalHeader.Text = "🛡️ Thông tin y tế";
            // 
            // panelContactInfo
            // 
            this.panelContactInfo.BackColor = System.Drawing.Color.White;
            this.panelContactInfo.Controls.Add(this.lblEmergencyValue);
            this.panelContactInfo.Controls.Add(this.lblEmergency);
            this.panelContactInfo.Controls.Add(this.lblAddressValue);
            this.panelContactInfo.Controls.Add(this.lblAddress);
            this.panelContactInfo.Controls.Add(this.lblEmailValue);
            this.panelContactInfo.Controls.Add(this.lblEmail);
            this.panelContactInfo.Controls.Add(this.lblPhoneValue);
            this.panelContactInfo.Controls.Add(this.lblPhone);
            this.panelContactInfo.Controls.Add(this.lblContactHeader);
            this.panelContactInfo.Location = new System.Drawing.Point(20, 175);
            this.panelContactInfo.Name = "panelContactInfo";
            this.panelContactInfo.Padding = new System.Windows.Forms.Padding(20);
            this.panelContactInfo.Size = new System.Drawing.Size(930, 200);
            this.panelContactInfo.TabIndex = 1;
            // 
            // lblEmergencyValue
            // 
            this.lblEmergencyValue.AutoSize = true;
            this.lblEmergencyValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblEmergencyValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEmergencyValue.Location = new System.Drawing.Point(550, 100);
            this.lblEmergencyValue.Name = "lblEmergencyValue";
            this.lblEmergencyValue.Size = new System.Drawing.Size(30, 20);
            this.lblEmergencyValue.Text = "-";
            // 
            // lblEmergency
            // 
            this.lblEmergency.AutoSize = true;
            this.lblEmergency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmergency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblEmergency.Location = new System.Drawing.Point(450, 100);
            this.lblEmergency.Name = "lblEmergency";
            this.lblEmergency.Size = new System.Drawing.Size(73, 19);
            this.lblEmergency.Text = "Khẩn cấp:";
            // 
            // lblAddressValue
            // 
            this.lblAddressValue.AutoSize = true;
            this.lblAddressValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblAddressValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblAddressValue.Location = new System.Drawing.Point(150, 140);
            this.lblAddressValue.Name = "lblAddressValue";
            this.lblAddressValue.Size = new System.Drawing.Size(30, 20);
            this.lblAddressValue.Text = "-";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAddress.Location = new System.Drawing.Point(30, 140);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(58, 19);
            this.lblAddress.Text = "Địa chỉ:";
            // 
            // lblEmailValue
            // 
            this.lblEmailValue.AutoSize = true;
            this.lblEmailValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblEmailValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEmailValue.Location = new System.Drawing.Point(150, 100);
            this.lblEmailValue.Name = "lblEmailValue";
            this.lblEmailValue.Size = new System.Drawing.Size(30, 20);
            this.lblEmailValue.Text = "-";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblEmail.Location = new System.Drawing.Point(30, 100);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(49, 19);
            this.lblEmail.Text = "Email:";
            // 
            // lblPhoneValue
            // 
            this.lblPhoneValue.AutoSize = true;
            this.lblPhoneValue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPhoneValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPhoneValue.Location = new System.Drawing.Point(150, 60);
            this.lblPhoneValue.Name = "lblPhoneValue";
            this.lblPhoneValue.Size = new System.Drawing.Size(30, 20);
            this.lblPhoneValue.Text = "-";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPhone.Location = new System.Drawing.Point(30, 60);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(77, 19);
            this.lblPhone.Text = "Điện thoại:";
            // 
            // lblContactHeader
            // 
            this.lblContactHeader.AutoSize = true;
            this.lblContactHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblContactHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblContactHeader.Location = new System.Drawing.Point(20, 20);
            this.lblContactHeader.Name = "lblContactHeader";
            this.lblContactHeader.Size = new System.Drawing.Size(155, 21);
            this.lblContactHeader.Text = "📞 Thông tin liên hệ";
            // 
            // panelPersonalHeader
            // 
            this.panelPersonalHeader.BackColor = System.Drawing.Color.White;
            this.panelPersonalHeader.Controls.Add(this.lblJoinDateValue);
            this.panelPersonalHeader.Controls.Add(this.lblPatientIdValue);
            this.panelPersonalHeader.Controls.Add(this.lblAgeValue);
            this.panelPersonalHeader.Controls.Add(this.lblGenderValue);
            this.panelPersonalHeader.Controls.Add(this.lblDobValue);
            this.panelPersonalHeader.Controls.Add(this.lblFullNameValue);
            this.panelPersonalHeader.Controls.Add(this.lblAvatar);
            this.panelPersonalHeader.Location = new System.Drawing.Point(20, 20);
            this.panelPersonalHeader.Name = "panelPersonalHeader";
            this.panelPersonalHeader.Size = new System.Drawing.Size(930, 140);
            this.panelPersonalHeader.TabIndex = 0;
            // 
            // lblJoinDateValue
            // 
            this.lblJoinDateValue.AutoSize = true;
            this.lblJoinDateValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJoinDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblJoinDateValue.Location = new System.Drawing.Point(140, 105);
            this.lblJoinDateValue.Name = "lblJoinDateValue";
            this.lblJoinDateValue.Size = new System.Drawing.Size(100, 15);
            this.lblJoinDateValue.Text = "-";
            // 
            // lblPatientIdValue
            // 
            this.lblPatientIdValue.AutoSize = true;
            this.lblPatientIdValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPatientIdValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblPatientIdValue.Location = new System.Drawing.Point(140, 80);
            this.lblPatientIdValue.Name = "lblPatientIdValue";
            this.lblPatientIdValue.Size = new System.Drawing.Size(30, 19);
            this.lblPatientIdValue.Text = "-";
            // 
            // lblAgeValue
            // 
            this.lblAgeValue.AutoSize = true;
            this.lblAgeValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblAgeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAgeValue.Location = new System.Drawing.Point(400, 50);
            this.lblAgeValue.Name = "lblAgeValue";
            this.lblAgeValue.Size = new System.Drawing.Size(30, 19);
            this.lblAgeValue.Text = "-";
            // 
            // lblGenderValue
            // 
            this.lblGenderValue.AutoSize = true;
            this.lblGenderValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblGenderValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGenderValue.Location = new System.Drawing.Point(320, 50);
            this.lblGenderValue.Name = "lblGenderValue";
            this.lblGenderValue.Size = new System.Drawing.Size(30, 19);
            this.lblGenderValue.Text = "-";
            // 
            // lblDobValue
            // 
            this.lblDobValue.AutoSize = true;
            this.lblDobValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblDobValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDobValue.Location = new System.Drawing.Point(140, 50);
            this.lblDobValue.Name = "lblDobValue";
            this.lblDobValue.Size = new System.Drawing.Size(30, 19);
            this.lblDobValue.Text = "-";
            // 
            // lblFullNameValue
            // 
            this.lblFullNameValue.AutoSize = true;
            this.lblFullNameValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblFullNameValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblFullNameValue.Location = new System.Drawing.Point(135, 15);
            this.lblFullNameValue.Name = "lblFullNameValue";
            this.lblFullNameValue.Size = new System.Drawing.Size(30, 32);
            this.lblFullNameValue.Text = "-";
            // 
            // lblAvatar
            // 
            this.lblAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblAvatar.Font = new System.Drawing.Font("Segoe UI", 36F);
            this.lblAvatar.Location = new System.Drawing.Point(20, 20);
            this.lblAvatar.Name = "lblAvatar";
            this.lblAvatar.Size = new System.Drawing.Size(100, 100);
            this.lblAvatar.TabIndex = 0;
            this.lblAvatar.Text = "👤";
            this.lblAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.dgvHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 41);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(992, 595);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "Lịch sử khám bệnh";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvHistory.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvHistory.ColumnHeadersHeight = 45;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colVisitDate,
                this.colDoctor,
                this.colDepartment,
                this.colDiagnosis,
                this.colTreatment
            });
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.EnableHeadersVisualStyles = false;
            this.dgvHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.dgvHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.RowTemplate.Height = 50;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(992, 595);
            this.dgvHistory.TabIndex = 0;
            
            // Header Style
            this.dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.dgvHistory.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            
            // Cell Style
            this.dgvHistory.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvHistory.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.dgvHistory.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvHistory.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.dgvHistory.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(252, 254, 255);
            // 
            // colVisitDate
            // 
            this.colVisitDate.HeaderText = "Ngày khám";
            this.colVisitDate.Name = "colVisitDate";
            this.colVisitDate.ReadOnly = true;
            this.colVisitDate.FillWeight = 15;
            // 
            // colDoctor
            // 
            this.colDoctor.HeaderText = "Bác sĩ điều trị";
            this.colDoctor.Name = "colDoctor";
            this.colDoctor.ReadOnly = true;
            this.colDoctor.FillWeight = 20;
            // 
            // colDepartment
            // 
            this.colDepartment.HeaderText = "Khoa";
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.ReadOnly = true;
            this.colDepartment.FillWeight = 15;
            // 
            // colDiagnosis
            // 
            this.colDiagnosis.HeaderText = "Chẩn đoán bệnh";
            this.colDiagnosis.Name = "colDiagnosis";
            this.colDiagnosis.ReadOnly = true;
            this.colDiagnosis.FillWeight = 25;
            this.colDiagnosis.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.colDiagnosis.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // 
            // colTreatment
            // 
            this.colTreatment.HeaderText = "Phác đồ & Hướng điều trị";
            this.colTreatment.Name = "colTreatment";
            this.colTreatment.ReadOnly = true;
            this.colTreatment.FillWeight = 25;
            this.colTreatment.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            // 
            // panelLoading
            // 
            this.panelLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelLoading.Controls.Add(this.lblLoading);
            this.panelLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLoading.Location = new System.Drawing.Point(0, 0);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(1000, 700);
            this.panelLoading.TabIndex = 10;
            this.panelLoading.Visible = false;
            // 
            // lblLoading
            // 
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblLoading.Location = new System.Drawing.Point(0, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(1000, 700);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "⏳ Đang tải thông tin...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_HealthRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "UC_HealthRecord";
            this.Size = new System.Drawing.Size(1000, 700);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabProfile.ResumeLayout(false);
            this.panelProfileContent.ResumeLayout(false);
            this.panelMedicalInfo.ResumeLayout(false);
            this.panelMedicalInfo.PerformLayout();
            this.panelContactInfo.ResumeLayout(false);
            this.panelContactInfo.PerformLayout();
            this.panelPersonalHeader.ResumeLayout(false);
            this.panelPersonalHeader.PerformLayout();
            this.tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.panelLoading.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabProfile;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Panel panelProfileContent;
        private System.Windows.Forms.Panel panelPersonalHeader;
        private System.Windows.Forms.Label lblFullNameValue;
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.Label lblDobValue;
        private System.Windows.Forms.Label lblGenderValue;
        private System.Windows.Forms.Label lblAgeValue;
        private System.Windows.Forms.Label lblPatientIdValue;
        private System.Windows.Forms.Label lblJoinDateValue;
        private System.Windows.Forms.Panel panelContactInfo;
        private System.Windows.Forms.Label lblContactHeader;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblPhoneValue;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblEmailValue;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblAddressValue;
        private System.Windows.Forms.Label lblEmergency;
        private System.Windows.Forms.Label lblEmergencyValue;
        private System.Windows.Forms.Panel panelMedicalInfo;
        private System.Windows.Forms.Label lblMedicalHeader;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.Label lblInsuranceValue;
        private System.Windows.Forms.Label lblBloodType;
        private System.Windows.Forms.Label lblBloodTypeValue;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVisitDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiagnosis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTreatment;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
