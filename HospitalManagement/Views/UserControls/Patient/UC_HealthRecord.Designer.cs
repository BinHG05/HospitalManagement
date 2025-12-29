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
            this.tabHistory = new System.Windows.Forms.TabPage();
            
            // Profile controls
            this.panelProfile = new System.Windows.Forms.Panel();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblFullNameValue = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblEmailValue = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPhoneValue = new System.Windows.Forms.Label();
            this.lblDob = new System.Windows.Forms.Label();
            this.lblDobValue = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblGenderValue = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblAddressValue = new System.Windows.Forms.Label();
            this.lblBloodType = new System.Windows.Forms.Label();
            this.lblBloodTypeValue = new System.Windows.Forms.Label();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.lblInsuranceValue = new System.Windows.Forms.Label();
            this.lblEmergency = new System.Windows.Forms.Label();
            this.lblEmergencyValue = new System.Windows.Forms.Label();
            
            // History controls
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.colVisitDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiagnosis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            
            this.panelLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            
            this.panelHeader.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabProfile.SuspendLayout();
            this.tabHistory.SuspendLayout();
            this.panelProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 50);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 Hồ sơ bệnh án";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabProfile);
            this.tabControl.Controls.Add(this.tabHistory);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 50);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(900, 450);
            this.tabControl.TabIndex = 1;
            // 
            // tabProfile
            // 
            this.tabProfile.Controls.Add(this.panelProfile);
            this.tabProfile.Location = new System.Drawing.Point(4, 26);
            this.tabProfile.Name = "tabProfile";
            this.tabProfile.Padding = new System.Windows.Forms.Padding(20);
            this.tabProfile.Size = new System.Drawing.Size(892, 420);
            this.tabProfile.TabIndex = 0;
            this.tabProfile.Text = "Thông tin cá nhân";
            this.tabProfile.UseVisualStyleBackColor = true;
            // 
            // panelProfile
            // 
            this.panelProfile.BackColor = System.Drawing.Color.White;
            this.panelProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProfile.Location = new System.Drawing.Point(20, 20);
            this.panelProfile.Name = "panelProfile";
            this.panelProfile.Padding = new System.Windows.Forms.Padding(30);
            this.panelProfile.Size = new System.Drawing.Size(852, 380);
            this.panelProfile.TabIndex = 0;
            // 
            // Profile Labels
            // 
            int labelY = 30;
            int labelGap = 40;
            int labelX = 30;
            int valueX = 180;
            
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFullName.Location = new System.Drawing.Point(labelX, labelY);
            this.lblFullName.Text = "Họ tên:";
            
            this.lblFullNameValue.AutoSize = true;
            this.lblFullNameValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFullNameValue.Location = new System.Drawing.Point(valueX, labelY);
            this.lblFullNameValue.Text = "-";
            
            labelY += labelGap;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(labelX, labelY);
            this.lblEmail.Text = "Email:";
            
            this.lblEmailValue.AutoSize = true;
            this.lblEmailValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmailValue.Location = new System.Drawing.Point(valueX, labelY);
            this.lblEmailValue.Text = "-";
            
            labelY += labelGap;
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.Location = new System.Drawing.Point(labelX, labelY);
            this.lblPhone.Text = "Số điện thoại:";
            
            this.lblPhoneValue.AutoSize = true;
            this.lblPhoneValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhoneValue.Location = new System.Drawing.Point(valueX, labelY);
            this.lblPhoneValue.Text = "-";
            
            labelY += labelGap;
            this.lblDob.AutoSize = true;
            this.lblDob.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDob.Location = new System.Drawing.Point(labelX, labelY);
            this.lblDob.Text = "Ngày sinh:";
            
            this.lblDobValue.AutoSize = true;
            this.lblDobValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDobValue.Location = new System.Drawing.Point(valueX, labelY);
            this.lblDobValue.Text = "-";
            
            labelY += labelGap;
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGender.Location = new System.Drawing.Point(labelX, labelY);
            this.lblGender.Text = "Giới tính:";
            
            this.lblGenderValue.AutoSize = true;
            this.lblGenderValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGenderValue.Location = new System.Drawing.Point(valueX, labelY);
            this.lblGenderValue.Text = "-";
            
            // Right column
            labelY = 30;
            labelX = 450;
            valueX = 580;
            
            this.lblBloodType.AutoSize = true;
            this.lblBloodType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBloodType.Location = new System.Drawing.Point(labelX, labelY);
            this.lblBloodType.Text = "Nhóm máu:";
            
            this.lblBloodTypeValue.AutoSize = true;
            this.lblBloodTypeValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBloodTypeValue.Location = new System.Drawing.Point(valueX, labelY);
            this.lblBloodTypeValue.Text = "-";
            
            labelY += labelGap;
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInsurance.Location = new System.Drawing.Point(labelX, labelY);
            this.lblInsurance.Text = "Số BHYT:";
            
            this.lblInsuranceValue.AutoSize = true;
            this.lblInsuranceValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInsuranceValue.Location = new System.Drawing.Point(valueX, labelY);
            this.lblInsuranceValue.Text = "-";
            
            labelY += labelGap;
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAddress.Location = new System.Drawing.Point(30, 230);
            this.lblAddress.Text = "Địa chỉ:";
            
            this.lblAddressValue.AutoSize = true;
            this.lblAddressValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAddressValue.Location = new System.Drawing.Point(180, 230);
            this.lblAddressValue.Text = "-";
            
            this.lblEmergency.AutoSize = true;
            this.lblEmergency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmergency.Location = new System.Drawing.Point(30, 270);
            this.lblEmergency.Text = "Liên hệ khẩn cấp:";
            
            this.lblEmergencyValue.AutoSize = true;
            this.lblEmergencyValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmergencyValue.Location = new System.Drawing.Point(180, 270);
            this.lblEmergencyValue.Text = "-";
            
            this.panelProfile.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFullName, this.lblFullNameValue,
                this.lblEmail, this.lblEmailValue,
                this.lblPhone, this.lblPhoneValue,
                this.lblDob, this.lblDobValue,
                this.lblGender, this.lblGenderValue,
                this.lblBloodType, this.lblBloodTypeValue,
                this.lblInsurance, this.lblInsuranceValue,
                this.lblAddress, this.lblAddressValue,
                this.lblEmergency, this.lblEmergencyValue
            });
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.dgvHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 26);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(892, 420);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "Lịch sử khám bệnh";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colVisitDate,
                this.colDoctor,
                this.colDepartment,
                this.colDiagnosis
            });
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.Size = new System.Drawing.Size(892, 420);
            this.dgvHistory.TabIndex = 0;
            // 
            // colVisitDate
            // 
            this.colVisitDate.HeaderText = "Ngày khám";
            this.colVisitDate.Name = "colVisitDate";
            this.colVisitDate.ReadOnly = true;
            this.colVisitDate.Width = 120;
            // 
            // colDoctor
            // 
            this.colDoctor.HeaderText = "Bác sĩ";
            this.colDoctor.Name = "colDoctor";
            this.colDoctor.ReadOnly = true;
            this.colDoctor.Width = 180;
            // 
            // colDepartment
            // 
            this.colDepartment.HeaderText = "Khoa";
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.ReadOnly = true;
            this.colDepartment.Width = 150;
            // 
            // colDiagnosis
            // 
            this.colDiagnosis.HeaderText = "Chẩn đoán";
            this.colDiagnosis.Name = "colDiagnosis";
            this.colDiagnosis.ReadOnly = true;
            this.colDiagnosis.Width = 350;
            // 
            // panelLoading
            // 
            this.panelLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelLoading.Controls.Add(this.lblLoading);
            this.panelLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLoading.Location = new System.Drawing.Point(0, 0);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(900, 500);
            this.panelLoading.TabIndex = 10;
            this.panelLoading.Visible = false;
            // 
            // lblLoading
            // 
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblLoading.Location = new System.Drawing.Point(0, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(900, 500);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "⏳ Đang tải...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_HealthRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_HealthRecord";
            this.Size = new System.Drawing.Size(900, 500);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabProfile.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            this.panelProfile.ResumeLayout(false);
            this.panelProfile.PerformLayout();
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
        private System.Windows.Forms.Panel panelProfile;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblFullNameValue;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblEmailValue;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblPhoneValue;
        private System.Windows.Forms.Label lblDob;
        private System.Windows.Forms.Label lblDobValue;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblGenderValue;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblAddressValue;
        private System.Windows.Forms.Label lblBloodType;
        private System.Windows.Forms.Label lblBloodTypeValue;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.Label lblInsuranceValue;
        private System.Windows.Forms.Label lblEmergency;
        private System.Windows.Forms.Label lblEmergencyValue;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVisitDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiagnosis;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
