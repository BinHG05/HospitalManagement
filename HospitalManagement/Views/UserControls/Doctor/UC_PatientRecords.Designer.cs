namespace HospitalManagement.Views.UserControls.Doctor
{
    partial class UC_PatientRecords
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
            this.panelSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearchHint = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvPatients = new System.Windows.Forms.DataGridView();
            this.colPatientId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDOB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBloodType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.panelDetailHeader = new System.Windows.Forms.Panel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.panelPatientCard = new System.Windows.Forms.Panel();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.panelHistoryHeader = new System.Windows.Forms.Panel();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.colHistDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistDoctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistDiagnosis = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.panelHeader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
            this.panelDetails.SuspendLayout();
            this.panelDetailHeader.SuspendLayout();
            this.panelPatientCard.SuspendLayout();
            this.panelHistoryHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();

            // =============================================
            // panelHeader - Blue header
            // =============================================
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(980, 55);
            this.panelHeader.TabIndex = 0;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(25, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Hồ sơ bệnh nhân";
            this.lblTitle.UseCompatibleTextRendering = true;

            // =============================================
            // panelSearch - Search bar
            // =============================================
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 55);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.panelSearch.Size = new System.Drawing.Size(980, 70);
            this.panelSearch.TabIndex = 1;

            this.lblSearchHint.AutoSize = true;
            this.lblSearchHint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearchHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSearchHint.Location = new System.Drawing.Point(25, 22);
            this.lblSearchHint.Text = "Tim kiem:";

            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(110, 18);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(350, 32);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);

            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(470, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 32);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tim kiem";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.panelSearch.Controls.Add(this.lblSearchHint);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.btnSearch);

            // =============================================
            // splitContainer - Main content
            // =============================================
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 125);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(980, 475);
            this.splitContainer.SplitterDistance = 580;
            this.splitContainer.TabIndex = 2;

            // Left panel - Patient list
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.splitContainer.Panel1.Controls.Add(this.dgvPatients);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(15);

            // dgvPatients
            this.dgvPatients.AllowUserToAddRows = false;
            this.dgvPatients.AllowUserToDeleteRows = false;
            this.dgvPatients.BackgroundColor = System.Drawing.Color.White;
            this.dgvPatients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPatients.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colPatientId,
                this.colFullName,
                this.colPhone,
                this.colGender,
                this.colDOB,
                this.colBloodType
            });
            this.dgvPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatients.Location = new System.Drawing.Point(15, 15);
            this.dgvPatients.Name = "dgvPatients";
            this.dgvPatients.ReadOnly = true;
            this.dgvPatients.RowHeadersVisible = false;
            this.dgvPatients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatients.Size = new System.Drawing.Size(550, 445);
            this.dgvPatients.TabIndex = 0;
            this.dgvPatients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatients_CellClick);

            this.colPatientId.HeaderText = "Ma BN";
            this.colPatientId.Name = "colPatientId";
            this.colPatientId.ReadOnly = true;
            this.colPatientId.Width = 60;

            this.colFullName.HeaderText = "Ho ten";
            this.colFullName.Name = "colFullName";
            this.colFullName.ReadOnly = true;
            this.colFullName.Width = 150;

            this.colPhone.HeaderText = "SDT";
            this.colPhone.Name = "colPhone";
            this.colPhone.ReadOnly = true;
            this.colPhone.Width = 100;

            this.colGender.HeaderText = "Gioi tinh";
            this.colGender.Name = "colGender";
            this.colGender.ReadOnly = true;
            this.colGender.Width = 70;

            this.colDOB.HeaderText = "Ngay sinh";
            this.colDOB.Name = "colDOB";
            this.colDOB.ReadOnly = true;
            this.colDOB.Width = 90;

            this.colBloodType.HeaderText = "Nhom mau";
            this.colBloodType.Name = "colBloodType";
            this.colBloodType.ReadOnly = true;
            this.colBloodType.Width = 70;

            // =============================================
            // Right panel - Patient details
            // =============================================
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel2.Controls.Add(this.panelDetails);

            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Padding = new System.Windows.Forms.Padding(0);
            
            // Detail header
            this.panelDetailHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelDetailHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetailHeader.Size = new System.Drawing.Size(396, 45);

            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.White;
            this.lblDetailsTitle.Location = new System.Drawing.Point(15, 10);
            this.lblDetailsTitle.Text = "Chi tiet benh nhan";
            this.lblDetailsTitle.UseCompatibleTextRendering = true;

            this.panelDetailHeader.Controls.Add(this.lblDetailsTitle);

            // Patient info card
            this.panelPatientCard.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelPatientCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPatientCard.Location = new System.Drawing.Point(0, 45);
            this.panelPatientCard.Size = new System.Drawing.Size(396, 120);
            this.panelPatientCard.Padding = new System.Windows.Forms.Padding(15);

            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblPatientName.Location = new System.Drawing.Point(15, 15);
            this.lblPatientName.Text = "Chon benh nhan";

            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatientInfo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblPatientInfo.Location = new System.Drawing.Point(15, 45);
            this.lblPatientInfo.MaximumSize = new System.Drawing.Size(360, 0);
            this.lblPatientInfo.Text = "Chon mot benh nhan tu danh sach de xem chi tiet";

            this.panelPatientCard.Controls.Add(this.lblPatientName);
            this.panelPatientCard.Controls.Add(this.lblPatientInfo);

            // History header
            this.panelHistoryHeader.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.panelHistoryHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHistoryHeader.Location = new System.Drawing.Point(0, 165);
            this.panelHistoryHeader.Size = new System.Drawing.Size(396, 35);

            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblHistoryTitle.ForeColor = System.Drawing.Color.White;
            this.lblHistoryTitle.Location = new System.Drawing.Point(15, 8);
            this.lblHistoryTitle.Text = "Lich su kham benh";
            this.lblHistoryTitle.UseCompatibleTextRendering = true;

            this.panelHistoryHeader.Controls.Add(this.lblHistoryTitle);

            // History grid
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colHistDate,
                this.colHistDoctor,
                this.colHistDiagnosis
            });
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(0, 200);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.Size = new System.Drawing.Size(396, 275);
            this.dgvHistory.TabIndex = 0;

            this.colHistDate.HeaderText = "Ngay kham";
            this.colHistDate.Name = "colHistDate";
            this.colHistDate.ReadOnly = true;
            this.colHistDate.Width = 80;

            this.colHistDoctor.HeaderText = "Bac si";
            this.colHistDoctor.Name = "colHistDoctor";
            this.colHistDoctor.ReadOnly = true;
            this.colHistDoctor.Width = 120;

            this.colHistDiagnosis.HeaderText = "Chan doan";
            this.colHistDiagnosis.Name = "colHistDiagnosis";
            this.colHistDiagnosis.ReadOnly = true;
            this.colHistDiagnosis.Width = 180;

            this.panelDetails.Controls.Add(this.dgvHistory);
            this.panelDetails.Controls.Add(this.panelHistoryHeader);
            this.panelDetails.Controls.Add(this.panelPatientCard);
            this.panelDetails.Controls.Add(this.panelDetailHeader);

            // =============================================
            // UC_PatientRecords
            // =============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_PatientRecords";
            this.Size = new System.Drawing.Size(980, 600);

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetailHeader.ResumeLayout(false);
            this.panelDetailHeader.PerformLayout();
            this.panelPatientCard.ResumeLayout(false);
            this.panelPatientCard.PerformLayout();
            this.panelHistoryHeader.ResumeLayout(false);
            this.panelHistoryHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearchHint;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvPatients;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatientId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDOB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBloodType;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Panel panelDetailHeader;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Panel panelPatientCard;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Panel panelHistoryHeader;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistDoctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistDiagnosis;
    }
}
