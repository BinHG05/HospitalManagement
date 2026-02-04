namespace HospitalManagement.Views.UserControls.Patient
{
    partial class UC_HospitalStatusBoard
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLastUpdated = new System.Windows.Forms.Label();
            this.dgvStatus = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPatient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDoctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
            this.SuspendLayout();

            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(15, 23, 42); // Navy
            this.panelTop.Controls.Add(this.lblLastUpdated);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Size = new System.Drawing.Size(800, 60);

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(251, 191, 36); // Gold
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Text = "🏥 TRẠNG THÁI PHÒNG KHÁM";

            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLastUpdated.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblLastUpdated.Location = new System.Drawing.Point(550, 20);
            this.lblLastUpdated.Size = new System.Drawing.Size(230, 25);
            this.lblLastUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLastUpdated.Text = "Cập nhật lúc: 00:00:00";

            // 
            // dgvStatus
            // 
            this.dgvStatus.AllowUserToAddRows = false;
            this.dgvStatus.AllowUserToDeleteRows = false;
            this.dgvStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSTT,
                this.colPatient,
                this.colDept,
                this.colDoctor,
                this.colStatus
            });
            this.dgvStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatus.Location = new System.Drawing.Point(0, 60);
            this.dgvStatus.Name = "dgvStatus";
            this.dgvStatus.ReadOnly = true;
            this.dgvStatus.Size = new System.Drawing.Size(800, 340);
            this.dgvStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStatus.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvStatus.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            // 
            // colSTT
            // 
            this.colSTT.HeaderText = "STT";
            this.colSTT.Width = 60;
            
            // 
            // colPatient
            // 
            this.colPatient.HeaderText = "Bệnh nhân";
            this.colPatient.Width = 200;

            // 
            // colDept
            // 
            this.colDept.HeaderText = "Chuyên khoa";
            this.colDept.Width = 150;

            // 
            // colDoctor
            // 
            this.colDoctor.HeaderText = "Bác sĩ đang khám";
            this.colDoctor.Width = 200;

            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.Width = 180;

            // 
            // UC_HospitalStatusBoard
            // 
            this.Controls.Add(this.dgvStatus);
            this.Controls.Add(this.panelTop);
            this.Name = "UC_HospitalStatusBoard";
            this.Size = new System.Drawing.Size(800, 400);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLastUpdated;
        private System.Windows.Forms.DataGridView dgvStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatient;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}
