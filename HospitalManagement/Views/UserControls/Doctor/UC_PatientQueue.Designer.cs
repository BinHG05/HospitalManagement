namespace HospitalManagement.Views.UserControls.Doctor
{
    partial class UC_PatientQueue
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
            this.lblDate = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelQueue = new System.Windows.Forms.Panel();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSymptoms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.lblDetailsContent = new System.Windows.Forms.Label();
            this.btnStartExam = new System.Windows.Forms.Button();
            this.btnCallPatient = new System.Windows.Forms.Button();
            this.btnCloseDetails = new System.Windows.Forms.Button();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelQueue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            this.panelDetails.SuspendLayout();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblDate);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(900, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 Hàng đợi khám bệnh";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(280, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(150, 20);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Hôm nay: 29/12/2025";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(780, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelQueue
            // 
            this.panelQueue.Controls.Add(this.dgvQueue);
            this.panelQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQueue.Location = new System.Drawing.Point(0, 60);
            this.panelQueue.Name = "panelQueue";
            this.panelQueue.Padding = new System.Windows.Forms.Padding(20);
            this.panelQueue.Size = new System.Drawing.Size(900, 440);
            this.panelQueue.TabIndex = 1;
            // 
            // dgvQueue
            // 
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            this.dgvQueue.BackgroundColor = System.Drawing.Color.White;
            this.dgvQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colNumber,
                this.colPatientName,
                this.colAge,
                this.colGender,
                this.colSymptoms,
                this.colStatus,
                this.colActions
            });
            this.dgvQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueue.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.ReadOnly = true;
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(860, 400);
            this.dgvQueue.TabIndex = 0;
            this.dgvQueue.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQueue_CellClick);
            // 
            // Columns
            // 
            this.colNumber.HeaderText = "STT";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Width = 50;
            
            this.colPatientName.HeaderText = "Họ tên";
            this.colPatientName.Name = "colPatientName";
            this.colPatientName.ReadOnly = true;
            this.colPatientName.Width = 180;
            
            this.colAge.HeaderText = "Tuổi";
            this.colAge.Name = "colAge";
            this.colAge.ReadOnly = true;
            this.colAge.Width = 60;
            
            this.colGender.HeaderText = "Giới tính";
            this.colGender.Name = "colGender";
            this.colGender.ReadOnly = true;
            this.colGender.Width = 80;
            
            this.colSymptoms.HeaderText = "Triệu chứng";
            this.colSymptoms.Name = "colSymptoms";
            this.colSymptoms.ReadOnly = true;
            this.colSymptoms.Width = 250;
            
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 100;
            
            this.colActions.HeaderText = "Thao tác";
            this.colActions.Name = "colActions";
            this.colActions.ReadOnly = true;
            this.colActions.Text = "Khám";
            this.colActions.UseColumnTextForButtonValue = true;
            this.colActions.Width = 80;
            // 
            // panelDetails
            // 
            this.panelDetails.BackColor = System.Drawing.Color.White;
            this.panelDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetails.Controls.Add(this.btnCloseDetails);
            this.panelDetails.Controls.Add(this.btnStartExam);
            this.panelDetails.Controls.Add(this.btnCallPatient);
            this.panelDetails.Controls.Add(this.lblDetailsContent);
            this.panelDetails.Controls.Add(this.lblDetailsTitle);
            this.panelDetails.Location = new System.Drawing.Point(200, 100);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(500, 350);
            this.panelDetails.TabIndex = 3;
            this.panelDetails.Visible = false;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblDetailsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.White;
            this.lblDetailsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblDetailsTitle.Size = new System.Drawing.Size(498, 40);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "Thông tin bệnh nhân";
            this.lblDetailsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDetailsContent
            // 
            this.lblDetailsContent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDetailsContent.Location = new System.Drawing.Point(20, 50);
            this.lblDetailsContent.Name = "lblDetailsContent";
            this.lblDetailsContent.Size = new System.Drawing.Size(458, 230);
            this.lblDetailsContent.TabIndex = 1;
            this.lblDetailsContent.Text = "Thông tin...";
            // 
            // btnCallPatient
            // 
            this.btnCallPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnCallPatient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCallPatient.FlatAppearance.BorderSize = 0;
            this.btnCallPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCallPatient.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCallPatient.ForeColor = System.Drawing.Color.White;
            this.btnCallPatient.Location = new System.Drawing.Point(120, 295);
            this.btnCallPatient.Name = "btnCallPatient";
            this.btnCallPatient.Size = new System.Drawing.Size(110, 35);
            this.btnCallPatient.TabIndex = 2;
            this.btnCallPatient.Text = "📢 Gọi";
            this.btnCallPatient.UseVisualStyleBackColor = false;
            this.btnCallPatient.Click += new System.EventHandler(this.btnCallPatient_Click);
            // 
            // btnStartExam
            // 
            this.btnStartExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(107)))));
            this.btnStartExam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartExam.FlatAppearance.BorderSize = 0;
            this.btnStartExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartExam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartExam.ForeColor = System.Drawing.Color.White;
            this.btnStartExam.Location = new System.Drawing.Point(250, 295);
            this.btnStartExam.Name = "btnStartExam";
            this.btnStartExam.Size = new System.Drawing.Size(120, 35);
            this.btnStartExam.TabIndex = 3;
            this.btnStartExam.Text = "🩺 Bắt đầu khám";
            this.btnStartExam.UseVisualStyleBackColor = false;
            this.btnStartExam.Click += new System.EventHandler(this.btnStartExam_Click);
            // 
            // btnCloseDetails
            // 
            this.btnCloseDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.btnCloseDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseDetails.FlatAppearance.BorderSize = 0;
            this.btnCloseDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseDetails.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCloseDetails.ForeColor = System.Drawing.Color.White;
            this.btnCloseDetails.Location = new System.Drawing.Point(390, 295);
            this.btnCloseDetails.Name = "btnCloseDetails";
            this.btnCloseDetails.Size = new System.Drawing.Size(90, 35);
            this.btnCloseDetails.TabIndex = 4;
            this.btnCloseDetails.Text = "Đóng";
            this.btnCloseDetails.UseVisualStyleBackColor = false;
            this.btnCloseDetails.Click += new System.EventHandler(this.btnCloseDetails_Click);
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
            // UC_PatientQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.panelQueue);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_PatientQueue";
            this.Size = new System.Drawing.Size(900, 500);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelQueue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelLoading.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelQueue;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSymptoms;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblDetailsContent;
        private System.Windows.Forms.Button btnCallPatient;
        private System.Windows.Forms.Button btnStartExam;
        private System.Windows.Forms.Button btnCloseDetails;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
