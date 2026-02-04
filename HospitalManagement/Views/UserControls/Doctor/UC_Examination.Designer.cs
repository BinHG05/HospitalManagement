namespace HospitalManagement.Views.UserControls.Doctor
{
    partial class UC_Examination
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
            this.btnBack = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            
            // Left panel - Patient info
            this.panelPatientInfo = new System.Windows.Forms.Panel();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblPatientDetails = new System.Windows.Forms.Label();
            
            // Right panel - Examination form
            this.panelExamForm = new System.Windows.Forms.Panel();
            this.lblSymptoms = new System.Windows.Forms.Label();
            this.txtSymptoms = new System.Windows.Forms.TextBox();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblTreatment = new System.Windows.Forms.Label();
            this.txtTreatment = new System.Windows.Forms.TextBox();
            this.lblNextAppt = new System.Windows.Forms.Label();
            this.dtpNextAppt = new System.Windows.Forms.DateTimePicker();
            this.chkNextAppt = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAssignService = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            
            // Service Results
            this.lblServiceSummary = new System.Windows.Forms.Label();
            this.dgvServiceStatus = new System.Windows.Forms.DataGridView();
            
            this.panelLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelPatientInfo.SuspendLayout();
            this.panelExamForm.SuspendLayout();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.panelHeader.Controls.Add(this.btnBack);
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
            this.lblTitle.Location = new System.Drawing.Point(60, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🩺 Khám bệnh ";
            this.lblTitle.UseCompatibleTextRendering = true;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(10, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 30);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 50);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(900, 450);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.TabIndex = 1;
            // 
            // panelPatientInfo
            // 
            this.panelPatientInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelPatientInfo.Controls.Add(this.lblPatientDetails);
            this.panelPatientInfo.Controls.Add(this.lblPatientName);
            this.panelPatientInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPatientInfo.Padding = new System.Windows.Forms.Padding(20);
            this.panelPatientInfo.Name = "panelPatientInfo";
            this.panelPatientInfo.Size = new System.Drawing.Size(300, 450);
            this.panelPatientInfo.TabIndex = 0;
            this.splitContainer.Panel1.Controls.Add(this.panelPatientInfo);
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblPatientName.Location = new System.Drawing.Point(20, 20);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(150, 25);
            this.lblPatientName.TabIndex = 0;
            this.lblPatientName.Text = "Tên bệnh nhân";
            // 
            // lblPatientDetails
            // 
            this.lblPatientDetails.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatientDetails.Location = new System.Drawing.Point(20, 55);
            this.lblPatientDetails.Name = "lblPatientDetails";
            this.lblPatientDetails.Size = new System.Drawing.Size(260, 380);
            this.lblPatientDetails.TabIndex = 1;
            this.lblPatientDetails.Text = "Thông tin bệnh nhân...";
            // 
            // 
            // panelExamForm
            // 
            this.panelExamForm.BackColor = System.Drawing.Color.White;
            this.panelExamForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExamForm.Padding = new System.Windows.Forms.Padding(10);
            this.panelExamForm.Name = "panelExamForm";
            this.panelExamForm.Size = new System.Drawing.Size(596, 450);
            this.panelExamForm.TabIndex = 0;
            this.splitContainer.Panel2.Controls.Add(this.panelExamForm);

            // MAIN FLOW LAYOUT
            var flpMain = new System.Windows.Forms.FlowLayoutPanel();
            flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flpMain.WrapContents = false;
            flpMain.AutoScroll = true;
            flpMain.Padding = new System.Windows.Forms.Padding(10);
            this.panelExamForm.Controls.Add(flpMain);

            int textWidth = 520; // Slightly wider for better fit

            // 1. Symptoms
            this.lblSymptoms.AutoSize = true;
            this.lblSymptoms.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSymptoms.Text = "Triệu chứng:";
            flpMain.Controls.Add(this.lblSymptoms);

            this.txtSymptoms.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSymptoms.Multiline = true;
            this.txtSymptoms.Size = new System.Drawing.Size(textWidth, 60);
            this.txtSymptoms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            flpMain.Controls.Add(this.txtSymptoms);
            flpMain.SetFlowBreak(this.txtSymptoms, true);

            // 2. Diagnosis
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblDiagnosis.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDiagnosis.Text = "Chẩn đoán: *";
            this.lblDiagnosis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            flpMain.Controls.Add(this.lblDiagnosis);

            this.txtDiagnosis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiagnosis.Multiline = true;
            this.txtDiagnosis.Size = new System.Drawing.Size(textWidth, 60);
            this.txtDiagnosis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            flpMain.Controls.Add(this.txtDiagnosis);
            flpMain.SetFlowBreak(this.txtDiagnosis, true);

            // 3. Treatment
            this.lblTreatment.AutoSize = true;
            this.lblTreatment.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblTreatment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTreatment.Text = "Phương pháp điều trị:";
            flpMain.Controls.Add(this.lblTreatment);

            this.txtTreatment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTreatment.Multiline = true;
            this.txtTreatment.Size = new System.Drawing.Size(textWidth, 60);
            this.txtTreatment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            flpMain.Controls.Add(this.txtTreatment);
            flpMain.SetFlowBreak(this.txtTreatment, true);

            // 4. Notes
            this.lblNotes.AutoSize = true;
            this.lblNotes.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNotes.Text = "Ghi chú:";
            flpMain.Controls.Add(this.lblNotes);

            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.Multiline = true;
            this.txtNotes.Size = new System.Drawing.Size(textWidth, 50);
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            flpMain.Controls.Add(this.txtNotes);
            flpMain.SetFlowBreak(this.txtNotes, true);

            // 5. Service Request Section (Header + Grid)
            var pnlServiceHeader = new System.Windows.Forms.FlowLayoutPanel();
            pnlServiceHeader.AutoSize = true;
            pnlServiceHeader.Margin = new System.Windows.Forms.Padding(0, 20, 0, 5);
            pnlServiceHeader.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            
            this.lblServiceSummary.AutoSize = true;
            this.lblServiceSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Bold);
            this.lblServiceSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(145)))), ((int)(((byte)(0)))));
            this.lblServiceSummary.Text = "Đang chờ kết quả dịch vụ...";
            this.lblServiceSummary.Visible = false;
            
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnRefresh.Size = new System.Drawing.Size(90, 26);
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.Visible = false;

            pnlServiceHeader.Controls.Add(this.lblServiceSummary);
            pnlServiceHeader.Controls.Add(this.btnRefresh);
            flpMain.Controls.Add(pnlServiceHeader);

            this.dgvServiceStatus.BackgroundColor = System.Drawing.Color.White;
            this.dgvServiceStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServiceStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiceStatus.ReadOnly = true;
            this.dgvServiceStatus.RowHeadersVisible = false;
            this.dgvServiceStatus.Size = new System.Drawing.Size(textWidth, 120);
            this.dgvServiceStatus.TabIndex = 20;
            this.dgvServiceStatus.Visible = false;
            this.dgvServiceStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            flpMain.Controls.Add(this.dgvServiceStatus);
            flpMain.SetFlowBreak(this.dgvServiceStatus, true);

            // 6. Next Appointment
            var pnlNextAppt = new System.Windows.Forms.FlowLayoutPanel();
            pnlNextAppt.AutoSize = true;
            pnlNextAppt.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            pnlNextAppt.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;

            this.chkNextAppt.AutoSize = true;
            this.chkNextAppt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkNextAppt.Text = "Hẹn tái khám:";
            this.chkNextAppt.CheckedChanged += new System.EventHandler(this.chkNextAppt_CheckedChanged);
            
            this.dtpNextAppt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNextAppt.Size = new System.Drawing.Size(200, 25);
            this.dtpNextAppt.Enabled = false;
            this.dtpNextAppt.MinDate = System.DateTime.Today.AddDays(1);
            
            pnlNextAppt.Controls.Add(this.chkNextAppt);
            pnlNextAppt.Controls.Add(this.dtpNextAppt);
            flpMain.Controls.Add(pnlNextAppt);
            flpMain.SetFlowBreak(pnlNextAppt, true);

            // 7. Action Buttons
            var pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            pnlButtons.AutoSize = true;
            pnlButtons.Margin = new System.Windows.Forms.Padding(0, 30, 0, 20);
            pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;

            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(107)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.Text = "💾 Lưu kết quả";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnAssignService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(145)))), ((int)(((byte)(0)))));
            this.btnAssignService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAssignService.FlatAppearance.BorderSize = 0;
            this.btnAssignService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssignService.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAssignService.ForeColor = System.Drawing.Color.White;
            this.btnAssignService.Size = new System.Drawing.Size(160, 40);
            this.btnAssignService.Text = "🔬 Chỉ định DV";
            this.btnAssignService.UseVisualStyleBackColor = false;
            this.btnAssignService.Click += new System.EventHandler(this.btnAssignService_Click);

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            pnlButtons.Controls.Add(this.btnSave);
            pnlButtons.Controls.Add(this.btnAssignService);
            pnlButtons.Controls.Add(this.btnCancel);
            flpMain.Controls.Add(pnlButtons);
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
            this.lblLoading.Text = "⏳ Đang tải...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_Examination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_Examination";
            this.Size = new System.Drawing.Size(900, 500);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelPatientInfo.ResumeLayout(false);
            this.panelPatientInfo.PerformLayout();
            this.panelExamForm.ResumeLayout(false);
            this.panelExamForm.PerformLayout();
            this.panelLoading.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelPatientInfo;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientDetails;
        private System.Windows.Forms.Panel panelExamForm;
        private System.Windows.Forms.Label lblSymptoms;
        private System.Windows.Forms.TextBox txtSymptoms;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblTreatment;
        private System.Windows.Forms.TextBox txtTreatment;
        private System.Windows.Forms.Label lblNextAppt;
        private System.Windows.Forms.DateTimePicker dtpNextAppt;
        private System.Windows.Forms.CheckBox chkNextAppt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAssignService;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblServiceSummary;
        private System.Windows.Forms.DataGridView dgvServiceStatus;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
