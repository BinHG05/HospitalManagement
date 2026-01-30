namespace HospitalManagement.Views.Forms.Doctor
{
    partial class DoctorDashboard
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.btnShiftRegistration = new System.Windows.Forms.Button();
            this.btnPrescription = new System.Windows.Forms.Button();
            this.btnPatients = new System.Windows.Forms.Button();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.btnExam = new System.Windows.Forms.Button();
            this.btnQueue = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.userPanel = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserIcon = new System.Windows.Forms.Label();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblHeaderDate = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.sidebarPanel.SuspendLayout();
            this.menuPanel.SuspendLayout();
            this.userPanel.SuspendLayout();
            this.logoPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel - Modern dark Slate sidebar (matching Patient Dashboard)
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.sidebarPanel.Controls.Add(this.menuPanel);
            this.sidebarPanel.Controls.Add(this.userPanel);
            this.sidebarPanel.Controls.Add(this.logoPanel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(260, 720);
            this.sidebarPanel.TabIndex = 0;
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.menuPanel.Controls.Add(this.btnShiftRegistration);
            this.menuPanel.Controls.Add(this.btnPrescription);
            this.menuPanel.Controls.Add(this.btnPatients);
            this.menuPanel.Controls.Add(this.btnSchedule);
            this.menuPanel.Controls.Add(this.btnExam);
            this.menuPanel.Controls.Add(this.btnQueue);
            this.menuPanel.Controls.Add(this.btnHome);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuPanel.Location = new System.Drawing.Point(0, 100);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Padding = new System.Windows.Forms.Padding(15, 30, 15, 0);
            this.menuPanel.Size = new System.Drawing.Size(260, 540);
            this.menuPanel.TabIndex = 2;
            // 
            // btnShiftRegistration - Đăng ký ca trực
            // 
            this.btnShiftRegistration.BackColor = System.Drawing.Color.Transparent;
            this.btnShiftRegistration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShiftRegistration.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnShiftRegistration.FlatAppearance.BorderSize = 0;
            this.btnShiftRegistration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnShiftRegistration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnShiftRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShiftRegistration.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnShiftRegistration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnShiftRegistration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShiftRegistration.Location = new System.Drawing.Point(15, 366);
            this.btnShiftRegistration.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnShiftRegistration.Name = "btnShiftRegistration";
            this.btnShiftRegistration.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnShiftRegistration.Size = new System.Drawing.Size(230, 56);
            this.btnShiftRegistration.TabIndex = 6;
            this.btnShiftRegistration.Text = "   ✍️   Đăng ký ca trực";
            this.btnShiftRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShiftRegistration.UseVisualStyleBackColor = false;
            this.btnShiftRegistration.Click += new System.EventHandler(this.btnShiftRegistration_Click);
            // 
            // btnPrescription - Kê đơn thuốc
            // 
            this.btnPrescription.BackColor = System.Drawing.Color.Transparent;
            this.btnPrescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrescription.FlatAppearance.BorderSize = 0;
            this.btnPrescription.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnPrescription.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnPrescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrescription.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnPrescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnPrescription.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrescription.Location = new System.Drawing.Point(15, 310);
            this.btnPrescription.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnPrescription.Name = "btnPrescription";
            this.btnPrescription.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnPrescription.Size = new System.Drawing.Size(230, 56);
            this.btnPrescription.TabIndex = 5;
            this.btnPrescription.Text = "   💊   Kê đơn thuốc";
            this.btnPrescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrescription.UseVisualStyleBackColor = false;
            this.btnPrescription.Click += new System.EventHandler(this.btnPrescription_Click);
            // 
            // btnPatients - Hồ sơ bệnh nhân
            // 
            this.btnPatients.BackColor = System.Drawing.Color.Transparent;
            this.btnPatients.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPatients.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPatients.FlatAppearance.BorderSize = 0;
            this.btnPatients.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnPatients.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnPatients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatients.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnPatients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnPatients.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPatients.Location = new System.Drawing.Point(15, 254);
            this.btnPatients.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnPatients.Name = "btnPatients";
            this.btnPatients.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnPatients.Size = new System.Drawing.Size(230, 56);
            this.btnPatients.TabIndex = 4;
            this.btnPatients.Text = "   📁   Hồ sơ bệnh nhân";
            this.btnPatients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPatients.UseVisualStyleBackColor = false;
            this.btnPatients.Click += new System.EventHandler(this.btnPatients_Click);
            // 
            // btnSchedule - Lịch làm việc
            // 
            this.btnSchedule.BackColor = System.Drawing.Color.Transparent;
            this.btnSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSchedule.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSchedule.FlatAppearance.BorderSize = 0;
            this.btnSchedule.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnSchedule.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSchedule.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnSchedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSchedule.Location = new System.Drawing.Point(15, 198);
            this.btnSchedule.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnSchedule.Size = new System.Drawing.Size(230, 56);
            this.btnSchedule.TabIndex = 3;
            this.btnSchedule.Text = "   📆 Lịch làm việc";
            this.btnSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSchedule.UseVisualStyleBackColor = false;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // btnExam - Khám bệnh
            // 
            this.btnExam.BackColor = System.Drawing.Color.Transparent;
            this.btnExam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExam.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExam.FlatAppearance.BorderSize = 0;
            this.btnExam.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnExam.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExam.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnExam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnExam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExam.Location = new System.Drawing.Point(15, 142);
            this.btnExam.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnExam.Name = "btnExam";
            this.btnExam.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnExam.Size = new System.Drawing.Size(230, 56);
            this.btnExam.TabIndex = 2;
            this.btnExam.Text = "   🩺   Khám bệnh";
            this.btnExam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExam.UseVisualStyleBackColor = false;
            this.btnExam.Click += new System.EventHandler(this.btnExam_Click);
            // 
            // btnQueue - Hàng đợi khám
            // 
            this.btnQueue.BackColor = System.Drawing.Color.Transparent;
            this.btnQueue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQueue.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnQueue.FlatAppearance.BorderSize = 0;
            this.btnQueue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnQueue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnQueue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQueue.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnQueue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnQueue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQueue.Location = new System.Drawing.Point(15, 86);
            this.btnQueue.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnQueue.Name = "btnQueue";
            this.btnQueue.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnQueue.Size = new System.Drawing.Size(230, 56);
            this.btnQueue.TabIndex = 1;
            this.btnQueue.Text = "   👥   Hàng đợi khám";
            this.btnQueue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQueue.UseVisualStyleBackColor = false;
            this.btnQueue.Click += new System.EventHandler(this.btnQueue_Click);
            // 
            // btnHome - Trang chủ (Active state by default)
            // 
            this.btnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(15, 30);
            this.btnHome.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHome.Size = new System.Drawing.Size(230, 56);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "   🏠   Trang chủ";
            this.btnHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // userPanel - User info at bottom
            // 
            this.userPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.userPanel.Controls.Add(this.btnLogout);
            this.userPanel.Controls.Add(this.lblUserRole);
            this.userPanel.Controls.Add(this.lblUserName);
            this.userPanel.Controls.Add(this.lblUserIcon);
            this.userPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userPanel.Location = new System.Drawing.Point(0, 640);
            this.userPanel.Name = "userPanel";
            this.userPanel.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.userPanel.Size = new System.Drawing.Size(260, 80);
            this.userPanel.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(200, 22);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(45, 36);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "⏻";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblUserRole.Location = new System.Drawing.Point(60, 42);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(38, 15);
            this.lblUserRole.TabIndex = 2;
            this.lblUserRole.Text = "Bác sĩ";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(60, 20);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(108, 20);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "BS. Nguyễn An";
            // 
            // lblUserIcon
            // 
            this.lblUserIcon.AutoSize = true;
            this.lblUserIcon.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.lblUserIcon.ForeColor = System.Drawing.Color.White;
            this.lblUserIcon.Location = new System.Drawing.Point(15, 20);
            this.lblUserIcon.Name = "lblUserIcon";
            this.lblUserIcon.Size = new System.Drawing.Size(43, 37);
            this.lblUserIcon.TabIndex = 0;
            this.lblUserIcon.Text = "👨‍⚕️";
            // 
            // logoPanel - Logo at top
            // 
            this.logoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.logoPanel.Controls.Add(this.lblLogo);
            this.logoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoPanel.Location = new System.Drawing.Point(0, 0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.logoPanel.Size = new System.Drawing.Size(260, 100);
            this.logoPanel.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblLogo.Location = new System.Drawing.Point(20, 35);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(122, 32);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "🏥 MedCare";
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.lblHeaderDate);
            this.headerPanel.Controls.Add(this.lblHeaderTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(260, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.headerPanel.Size = new System.Drawing.Size(1020, 70);
            this.headerPanel.TabIndex = 1;
            // 
            // lblHeaderDate
            // 
            this.lblHeaderDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeaderDate.AutoSize = true;
            this.lblHeaderDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHeaderDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHeaderDate.Location = new System.Drawing.Point(780, 24);
            this.lblHeaderDate.Name = "lblHeaderDate";
            this.lblHeaderDate.Size = new System.Drawing.Size(200, 20);
            this.lblHeaderDate.TabIndex = 1;
            this.lblHeaderDate.Text = "Thứ Ba, 21 Tháng 01 2026";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(30, 20);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(126, 30);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Trang chủ";
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(260, 70);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(25);
            this.contentPanel.Size = new System.Drawing.Size(1020, 650);
            this.contentPanel.TabIndex = 2;
            // 
            // DoctorDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebarPanel);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "DoctorDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MedCare - Bác sĩ";
            this.sidebarPanel.ResumeLayout(false);
            this.menuPanel.ResumeLayout(false);
            this.userPanel.ResumeLayout(false);
            this.userPanel.PerformLayout();
            this.logoPanel.ResumeLayout(false);
            this.logoPanel.PerformLayout();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel userPanel;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserIcon;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Button btnShiftRegistration;
        private System.Windows.Forms.Button btnPrescription;
        private System.Windows.Forms.Button btnPatients;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Button btnExam;
        private System.Windows.Forms.Button btnQueue;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblHeaderDate;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel contentPanel;
    }
}
