using System.Windows.Forms;
using System.Drawing;

namespace HospitalManagement.Views.UserControls.Admin
{
    partial class UC_ShiftApproval
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

        private void InitializeComponent()
        {
            // Main Tab Control
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPending = new System.Windows.Forms.TabPage();
            this.tabShiftQuota = new System.Windows.Forms.TabPage();
            this.tabDoctorQuota = new System.Windows.Forms.TabPage();

            // Tab 1: Pending Requests
            this.pnlPendingHeader = new System.Windows.Forms.Panel();
            this.lblPendingCount = new System.Windows.Forms.Label();
            this.btnApproveAll = new System.Windows.Forms.Button();
            this.dgvPending = new System.Windows.Forms.DataGridView();
            this.pnlPendingActions = new System.Windows.Forms.Panel();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();

            // Tab 2: Shift Quota Summary
            this.pnlShiftQuotaHeader = new System.Windows.Forms.Panel();
            this.lblShiftQuotaDate = new System.Windows.Forms.Label();
            this.dtpShiftQuota = new System.Windows.Forms.DateTimePicker();
            this.dgvShiftQuota = new System.Windows.Forms.DataGridView();

            // Tab 3: Doctor Quota Summary
            this.pnlDoctorQuotaHeader = new System.Windows.Forms.Panel();
            this.lblDoctorQuotaMonth = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.dgvDoctorQuota = new System.Windows.Forms.DataGridView();

            this.tabControl.SuspendLayout();
            this.tabPending.SuspendLayout();
            this.tabShiftQuota.SuspendLayout();
            this.tabDoctorQuota.SuspendLayout();
            this.pnlPendingHeader.SuspendLayout();
            this.pnlPendingActions.SuspendLayout();
            this.pnlShiftQuotaHeader.SuspendLayout();
            this.pnlDoctorQuotaHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPending)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftQuota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctorQuota)).BeginInit();
            this.SuspendLayout();

            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPending);
            this.tabControl.Controls.Add(this.tabShiftQuota);
            this.tabControl.Controls.Add(this.tabDoctorQuota);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1050, 700);
            this.tabControl.TabIndex = 0;

            // 
            // tabPending
            // 
            this.tabPending.Controls.Add(this.dgvPending);
            this.tabPending.Controls.Add(this.pnlPendingActions);
            this.tabPending.Controls.Add(this.pnlPendingHeader);
            this.tabPending.Location = new System.Drawing.Point(4, 29);
            this.tabPending.Name = "tabPending";
            this.tabPending.Padding = new System.Windows.Forms.Padding(10);
            this.tabPending.Size = new System.Drawing.Size(1042, 667);
            this.tabPending.TabIndex = 0;
            this.tabPending.Text = "📋 Yêu cầu chờ duyệt";
            this.tabPending.UseVisualStyleBackColor = true;

            // pnlPendingHeader
            this.pnlPendingHeader.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pnlPendingHeader.Controls.Add(this.btnApproveAll);
            this.pnlPendingHeader.Controls.Add(this.lblPendingCount);
            this.pnlPendingHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPendingHeader.Height = 60;
            this.pnlPendingHeader.Padding = new Padding(15);

            // lblPendingCount
            this.lblPendingCount.AutoSize = true;
            this.lblPendingCount.Font = new System.Drawing.Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblPendingCount.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblPendingCount.Location = new System.Drawing.Point(15, 18);
            this.lblPendingCount.Text = "⏳ 0 yêu cầu đang chờ duyệt";

            // btnApproveAll
            this.btnApproveAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnApproveAll.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnApproveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApproveAll.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnApproveAll.ForeColor = System.Drawing.Color.White;
            this.btnApproveAll.Location = new System.Drawing.Point(850, 12);
            this.btnApproveAll.Size = new System.Drawing.Size(170, 36);
            this.btnApproveAll.Text = "✅ Duyệt tất cả";
            this.btnApproveAll.UseVisualStyleBackColor = false;

            // dgvPending
            this.dgvPending.AllowUserToAddRows = false;
            this.dgvPending.AllowUserToDeleteRows = false;
            this.dgvPending.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPending.BackgroundColor = System.Drawing.Color.White;
            this.dgvPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPending.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPending.MultiSelect = true;
            this.dgvPending.Name = "dgvPending";
            this.dgvPending.ReadOnly = true;
            this.dgvPending.RowHeadersVisible = false;
            this.dgvPending.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPending.TabIndex = 0;

            // pnlPendingActions
            this.pnlPendingActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPendingActions.Height = 60;
            this.pnlPendingActions.Controls.Add(this.btnApprove);
            this.pnlPendingActions.Controls.Add(this.btnReject);
            this.pnlPendingActions.Padding = new Padding(10);

            // btnApprove
            this.btnApprove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnApprove.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.Location = new System.Drawing.Point(700, 12);
            this.btnApprove.Size = new System.Drawing.Size(150, 36);
            this.btnApprove.Text = "✅ Duyệt";
            this.btnApprove.UseVisualStyleBackColor = false;

            // btnReject
            this.btnReject.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnReject.ForeColor = System.Drawing.Color.White;
            this.btnReject.Location = new System.Drawing.Point(870, 12);
            this.btnReject.Size = new System.Drawing.Size(150, 36);
            this.btnReject.Text = "❌ Từ chối";
            this.btnReject.UseVisualStyleBackColor = false;

            // 
            // tabShiftQuota
            // 
            this.tabShiftQuota.Controls.Add(this.dgvShiftQuota);
            this.tabShiftQuota.Controls.Add(this.pnlShiftQuotaHeader);
            this.tabShiftQuota.Location = new System.Drawing.Point(4, 29);
            this.tabShiftQuota.Name = "tabShiftQuota";
            this.tabShiftQuota.Padding = new System.Windows.Forms.Padding(10);
            this.tabShiftQuota.Size = new System.Drawing.Size(1042, 667);
            this.tabShiftQuota.TabIndex = 1;
            this.tabShiftQuota.Text = "📊 Thống kê Ca";
            this.tabShiftQuota.UseVisualStyleBackColor = true;

            // pnlShiftQuotaHeader
            this.pnlShiftQuotaHeader.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pnlShiftQuotaHeader.Controls.Add(this.dtpShiftQuota);
            this.pnlShiftQuotaHeader.Controls.Add(this.lblShiftQuotaDate);
            this.pnlShiftQuotaHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlShiftQuotaHeader.Height = 60;
            this.pnlShiftQuotaHeader.Padding = new Padding(15);

            // lblShiftQuotaDate
            this.lblShiftQuotaDate.AutoSize = true;
            this.lblShiftQuotaDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblShiftQuotaDate.Location = new System.Drawing.Point(15, 18);
            this.lblShiftQuotaDate.Text = "Xem thống kê ngày:";

            // dtpShiftQuota
            this.dtpShiftQuota.Location = new System.Drawing.Point(180, 15);
            this.dtpShiftQuota.Size = new System.Drawing.Size(200, 27);
            this.dtpShiftQuota.Format = DateTimePickerFormat.Short;

            // dgvShiftQuota
            this.dgvShiftQuota.AllowUserToAddRows = false;
            this.dgvShiftQuota.AllowUserToDeleteRows = false;
            this.dgvShiftQuota.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShiftQuota.BackgroundColor = System.Drawing.Color.White;
            this.dgvShiftQuota.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShiftQuota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShiftQuota.Name = "dgvShiftQuota";
            this.dgvShiftQuota.ReadOnly = true;
            this.dgvShiftQuota.RowHeadersVisible = false;
            this.dgvShiftQuota.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShiftQuota.TabIndex = 0;

            // 
            // tabDoctorQuota
            // 
            this.tabDoctorQuota.Controls.Add(this.dgvDoctorQuota);
            this.tabDoctorQuota.Controls.Add(this.pnlDoctorQuotaHeader);
            this.tabDoctorQuota.Location = new System.Drawing.Point(4, 29);
            this.tabDoctorQuota.Name = "tabDoctorQuota";
            this.tabDoctorQuota.Padding = new System.Windows.Forms.Padding(10);
            this.tabDoctorQuota.Size = new System.Drawing.Size(1042, 667);
            this.tabDoctorQuota.TabIndex = 2;
            this.tabDoctorQuota.Text = "👨‍⚕️ Thống kê Bác sĩ";
            this.tabDoctorQuota.UseVisualStyleBackColor = true;

            // pnlDoctorQuotaHeader
            this.pnlDoctorQuotaHeader.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pnlDoctorQuotaHeader.Controls.Add(this.cmbYear);
            this.pnlDoctorQuotaHeader.Controls.Add(this.cmbMonth);
            this.pnlDoctorQuotaHeader.Controls.Add(this.lblDoctorQuotaMonth);
            this.pnlDoctorQuotaHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDoctorQuotaHeader.Height = 60;
            this.pnlDoctorQuotaHeader.Padding = new Padding(15);

            // lblDoctorQuotaMonth
            this.lblDoctorQuotaMonth.AutoSize = true;
            this.lblDoctorQuotaMonth.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDoctorQuotaMonth.Location = new System.Drawing.Point(15, 18);
            this.lblDoctorQuotaMonth.Text = "Xem thống kê tháng:";

            // cmbMonth
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.Location = new System.Drawing.Point(180, 15);
            this.cmbMonth.Size = new System.Drawing.Size(100, 27);

            // cmbYear
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.Location = new System.Drawing.Point(300, 15);
            this.cmbYear.Size = new System.Drawing.Size(100, 27);

            // dgvDoctorQuota
            this.dgvDoctorQuota.AllowUserToAddRows = false;
            this.dgvDoctorQuota.AllowUserToDeleteRows = false;
            this.dgvDoctorQuota.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoctorQuota.BackgroundColor = System.Drawing.Color.White;
            this.dgvDoctorQuota.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoctorQuota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoctorQuota.Name = "dgvDoctorQuota";
            this.dgvDoctorQuota.ReadOnly = true;
            this.dgvDoctorQuota.RowHeadersVisible = false;
            this.dgvDoctorQuota.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoctorQuota.TabIndex = 0;

            // 
            // UC_ShiftApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "UC_ShiftApproval";
            this.Size = new System.Drawing.Size(1050, 700);

            this.tabControl.ResumeLayout(false);
            this.tabPending.ResumeLayout(false);
            this.tabShiftQuota.ResumeLayout(false);
            this.tabDoctorQuota.ResumeLayout(false);
            this.pnlPendingHeader.ResumeLayout(false);
            this.pnlPendingHeader.PerformLayout();
            this.pnlPendingActions.ResumeLayout(false);
            this.pnlShiftQuotaHeader.ResumeLayout(false);
            this.pnlShiftQuotaHeader.PerformLayout();
            this.pnlDoctorQuotaHeader.ResumeLayout(false);
            this.pnlDoctorQuotaHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPending)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftQuota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctorQuota)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPending;
        private System.Windows.Forms.TabPage tabShiftQuota;
        private System.Windows.Forms.TabPage tabDoctorQuota;

        // Tab Pending
        private System.Windows.Forms.Panel pnlPendingHeader;
        private System.Windows.Forms.Label lblPendingCount;
        private System.Windows.Forms.Button btnApproveAll;
        private System.Windows.Forms.DataGridView dgvPending;
        private System.Windows.Forms.Panel pnlPendingActions;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;

        // Tab Shift Quota
        private System.Windows.Forms.Panel pnlShiftQuotaHeader;
        private System.Windows.Forms.Label lblShiftQuotaDate;
        private System.Windows.Forms.DateTimePicker dtpShiftQuota;
        private System.Windows.Forms.DataGridView dgvShiftQuota;

        // Tab Doctor Quota
        private System.Windows.Forms.Panel pnlDoctorQuotaHeader;
        private System.Windows.Forms.Label lblDoctorQuotaMonth;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.DataGridView dgvDoctorQuota;
    }
}
