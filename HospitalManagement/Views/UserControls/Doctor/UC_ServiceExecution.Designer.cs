namespace HospitalManagement.Views.UserControls.Doctor
{
    partial class UC_ServiceExecution
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.Label lblNotesTitle;
        private System.Windows.Forms.TextBox txtDoctorNotes;
        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblFileTitle;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.lblNotesTitle = new System.Windows.Forms.Label();
            this.txtDoctorNotes = new System.Windows.Forms.TextBox();
            this.lblResultTitle = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblFileTitle = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thực Hiện Dịch Vụ";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPatientName.Location = new System.Drawing.Point(20, 60);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(100, 21);
            this.lblPatientName.TabIndex = 1;
            this.lblPatientName.Text = "Bệnh nhân: ";
            // 
            // lblServiceName
            // 
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblServiceName.Location = new System.Drawing.Point(20, 90);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(76, 21);
            this.lblServiceName.TabIndex = 2;
            this.lblServiceName.Text = "Dịch vụ: ";
            // 
            // lblNotesTitle
            // 
            this.lblNotesTitle.AutoSize = true;
            this.lblNotesTitle.Location = new System.Drawing.Point(20, 130);
            this.lblNotesTitle.Name = "lblNotesTitle";
            this.lblNotesTitle.Size = new System.Drawing.Size(201, 19);
            this.lblNotesTitle.TabIndex = 3;
            this.lblNotesTitle.Text = "Ghi chú từ bác sĩ chỉ định:";
            // 
            // txtDoctorNotes
            // 
            this.txtDoctorNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.txtDoctorNotes.Location = new System.Drawing.Point(25, 155);
            this.txtDoctorNotes.Multiline = true;
            this.txtDoctorNotes.Name = "txtDoctorNotes";
            this.txtDoctorNotes.ReadOnly = true;
            this.txtDoctorNotes.Size = new System.Drawing.Size(750, 60);
            this.txtDoctorNotes.TabIndex = 4;
            // 
            // lblResultTitle
            // 
            this.lblResultTitle.AutoSize = true;
            this.lblResultTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResultTitle.Location = new System.Drawing.Point(20, 230);
            this.lblResultTitle.Name = "lblResultTitle";
            this.lblResultTitle.Size = new System.Drawing.Size(158, 19);
            this.lblResultTitle.TabIndex = 5;
            this.lblResultTitle.Text = "Kết quả chẩn đoán:";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(25, 255);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(750, 150);
            this.txtResult.TabIndex = 6;
            // 
            // lblFileTitle
            // 
            this.lblFileTitle.AutoSize = true;
            this.lblFileTitle.Location = new System.Drawing.Point(20, 420);
            this.lblFileTitle.Name = "lblFileTitle";
            this.lblFileTitle.Size = new System.Drawing.Size(161, 19);
            this.lblFileTitle.TabIndex = 7;
            this.lblFileTitle.Text = "Tệp đính kèm (nếu có):";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(25, 445);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(640, 25);
            this.txtFilePath.TabIndex = 8;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(675, 445);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 25);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "Duyệt...";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(25, 500);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 45);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "✅ Lưu Kết Quả";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(240, 500);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(120, 45);
            this.btnBack.TabIndex = 11;
            this.btnBack.Text = "Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // UC_ServiceExecution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblFileTitle);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblResultTitle);
            this.Controls.Add(this.txtDoctorNotes);
            this.Controls.Add(this.lblNotesTitle);
            this.Controls.Add(this.lblServiceName);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "UC_ServiceExecution";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
