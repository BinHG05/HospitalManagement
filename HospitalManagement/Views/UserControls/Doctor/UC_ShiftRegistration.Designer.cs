using System.Windows.Forms;
using System.Drawing;

namespace HospitalManagement.Views.UserControls.Doctor
{
    partial class UC_ShiftRegistration
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
            // Main Layout
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            
            // Left Panel - Registration Form
            this.grpRegistration = new System.Windows.Forms.GroupBox();
            this.pnlQuota = new System.Windows.Forms.Panel();
            this.lblQuotaTitle = new System.Windows.Forms.Label();
            this.lblQuotaValue = new System.Windows.Forms.Label();
            this.progressQuota = new System.Windows.Forms.ProgressBar();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblShift = new System.Windows.Forms.Label();
            this.dgvShifts = new System.Windows.Forms.DataGridView();
            this.btnRegister = new System.Windows.Forms.Button();

            // Right Panel - My Registrations
            this.grpMyRegistrations = new System.Windows.Forms.GroupBox();
            this.dgvMyRegistrations = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpRegistration.SuspendLayout();
            this.pnlQuota.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).BeginInit();
            this.grpMyRegistrations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyRegistrations)).BeginInit();
            this.SuspendLayout();

            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer.SplitterDistance = 450;
            this.splitContainer.TabIndex = 0;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpRegistration);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpMyRegistrations);

            // 
            // grpRegistration
            // 
            this.grpRegistration.Controls.Add(this.btnRegister);
            this.grpRegistration.Controls.Add(this.dgvShifts);
            this.grpRegistration.Controls.Add(this.lblShift);
            this.grpRegistration.Controls.Add(this.dtpDate);
            this.grpRegistration.Controls.Add(this.lblDate);
            this.grpRegistration.Controls.Add(this.pnlQuota);
            this.grpRegistration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRegistration.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.grpRegistration.Name = "grpRegistration";
            this.grpRegistration.Padding = new Padding(15);
            this.grpRegistration.TabIndex = 0;
            this.grpRegistration.Text = "📝 Đăng Ký Ca Trực";

            // 
            // pnlQuota - Monthly Quota Display
            // 
            this.pnlQuota.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.pnlQuota.Location = new System.Drawing.Point(15, 35);
            this.pnlQuota.Size = new System.Drawing.Size(410, 80);
            this.pnlQuota.Name = "pnlQuota";

            // lblQuotaTitle
            this.lblQuotaTitle.AutoSize = true;
            this.lblQuotaTitle.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblQuotaTitle.Location = new System.Drawing.Point(10, 10);
            this.lblQuotaTitle.Text = "📊 Định mức tháng này:";

            // lblQuotaValue
            this.lblQuotaValue.AutoSize = true;
            this.lblQuotaValue.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblQuotaValue.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblQuotaValue.Location = new System.Drawing.Point(10, 35);
            this.lblQuotaValue.Text = "0 / 15 ca (Tối đa: 25)";

            // progressQuota
            this.progressQuota.Location = new System.Drawing.Point(10, 60);
            this.progressQuota.Size = new System.Drawing.Size(390, 12);
            this.progressQuota.Maximum = 100;
            this.progressQuota.Value = 0;

            this.pnlQuota.Controls.Add(this.lblQuotaTitle);
            this.pnlQuota.Controls.Add(this.lblQuotaValue);
            this.pnlQuota.Controls.Add(this.progressQuota);

            // lblDate
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(15, 130);
            this.lblDate.Text = "Chọn ngày làm việc:";

            // dtpDate
            this.dtpDate.Location = new System.Drawing.Point(15, 155);
            this.dtpDate.Size = new System.Drawing.Size(200, 27);
            this.dtpDate.Format = DateTimePickerFormat.Short;
            this.dtpDate.MinDate = System.DateTime.Today;

            // lblShift
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(15, 195);
            this.lblShift.Text = "Chọn ca trực:";

            // dgvShifts - Available shifts grid
            this.dgvShifts.AllowUserToAddRows = false;
            this.dgvShifts.AllowUserToDeleteRows = false;
            this.dgvShifts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShifts.BackgroundColor = System.Drawing.Color.White;
            this.dgvShifts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShifts.Location = new System.Drawing.Point(15, 220);
            this.dgvShifts.MultiSelect = false;
            this.dgvShifts.Name = "dgvShifts";
            this.dgvShifts.ReadOnly = true;
            this.dgvShifts.RowHeadersVisible = false;
            this.dgvShifts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShifts.Size = new System.Drawing.Size(410, 180);
            this.dgvShifts.TabIndex = 0;

            // btnRegister
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(15, 420);
            this.btnRegister.Size = new System.Drawing.Size(200, 45);
            this.btnRegister.Text = "✅ Đăng ký ca này";
            this.btnRegister.UseVisualStyleBackColor = false;

            // 
            // grpMyRegistrations
            // 
            this.grpMyRegistrations.Controls.Add(this.btnCancel);
            this.grpMyRegistrations.Controls.Add(this.dgvMyRegistrations);
            this.grpMyRegistrations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMyRegistrations.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.grpMyRegistrations.Name = "grpMyRegistrations";
            this.grpMyRegistrations.Padding = new Padding(15);
            this.grpMyRegistrations.TabIndex = 0;
            this.grpMyRegistrations.Text = "📅 Lịch đăng ký của tôi";

            // dgvMyRegistrations
            this.dgvMyRegistrations.AllowUserToAddRows = false;
            this.dgvMyRegistrations.AllowUserToDeleteRows = false;
            this.dgvMyRegistrations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyRegistrations.BackgroundColor = System.Drawing.Color.White;
            this.dgvMyRegistrations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyRegistrations.Location = new System.Drawing.Point(15, 35);
            this.dgvMyRegistrations.Name = "dgvMyRegistrations";
            this.dgvMyRegistrations.ReadOnly = true;
            this.dgvMyRegistrations.RowHeadersVisible = false;
            this.dgvMyRegistrations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMyRegistrations.Size = new System.Drawing.Size(550, 400);
            this.dgvMyRegistrations.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvMyRegistrations.TabIndex = 0;

            // btnCancel
            this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(400, 450);
            this.btnCancel.Size = new System.Drawing.Size(165, 40);
            this.btnCancel.Text = "❌ Hủy đăng ký";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Enabled = false;

            // 
            // UC_ShiftRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "UC_ShiftRegistration";
            this.Size = new System.Drawing.Size(1050, 700);

            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpRegistration.ResumeLayout(false);
            this.grpRegistration.PerformLayout();
            this.pnlQuota.ResumeLayout(false);
            this.pnlQuota.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShifts)).EndInit();
            this.grpMyRegistrations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyRegistrations)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpRegistration;
        private System.Windows.Forms.GroupBox grpMyRegistrations;

        private System.Windows.Forms.Panel pnlQuota;
        private System.Windows.Forms.Label lblQuotaTitle;
        private System.Windows.Forms.Label lblQuotaValue;
        private System.Windows.Forms.ProgressBar progressQuota;

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.DataGridView dgvShifts;
        private System.Windows.Forms.Button btnRegister;

        private System.Windows.Forms.DataGridView dgvMyRegistrations;
        private System.Windows.Forms.Button btnCancel;
    }
}
