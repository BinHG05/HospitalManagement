using System.Windows.Forms;
using System.Drawing;

namespace HospitalManagement.Views.UserControls.Admin
{
    partial class UC_ShiftAssignment
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
            // Main Layout using SplitContainer
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            
            // Left Panel (Assignment Form)
            this.grpAssignment = new System.Windows.Forms.GroupBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.cmbDoctors = new System.Windows.Forms.ComboBox();
            this.lblShift = new System.Windows.Forms.Label();
            this.cmbShifts = new System.Windows.Forms.ComboBox();
            this.btnAssign = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button(); // NEW Button

            // Right Panel (Schedules List)
            this.grpSchedule = new System.Windows.Forms.GroupBox();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpAssignment.SuspendLayout();
            this.grpSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.SuspendLayout();

            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(1050, 700);
            this.splitContainer.SplitterDistance = 350; // Width of Left Panel
            this.splitContainer.TabIndex = 0;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpAssignment);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpSchedule);
            // 
            // grpAssignment (Left Panel)
            // 
            this.grpAssignment.Controls.Add(this.btnAddNew);
            this.grpAssignment.Controls.Add(this.btnAssign);
            this.grpAssignment.Controls.Add(this.cmbShifts);
            this.grpAssignment.Controls.Add(this.lblShift);
            this.grpAssignment.Controls.Add(this.cmbDoctors);
            this.grpAssignment.Controls.Add(this.lblDoctor);
            this.grpAssignment.Controls.Add(this.dtpDate);
            this.grpAssignment.Controls.Add(this.lblDate);
            this.grpAssignment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAssignment.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.grpAssignment.Location = new System.Drawing.Point(0, 0);
            this.grpAssignment.Name = "grpAssignment";
            this.grpAssignment.Padding = new Padding(20);
            this.grpAssignment.Size = new System.Drawing.Size(350, 700);
            this.grpAssignment.TabIndex = 0;
            this.grpAssignment.TabStop = false;
            this.grpAssignment.Text = "Phân Ca Trực";

            // lblDate
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(20, 50);
            this.lblDate.Name = "lblDate";
            this.lblDate.Text = "Ngày làm việc:";

            // dtpDate
            this.dtpDate.Location = new System.Drawing.Point(20, 80);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(300, 27);
            this.dtpDate.Format = DateTimePickerFormat.Short;

            // lblDoctor
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Location = new System.Drawing.Point(20, 130);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Text = "Chọn Bác sĩ:";

            // cmbDoctors
            this.cmbDoctors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctors.FormattingEnabled = true;
            this.cmbDoctors.Location = new System.Drawing.Point(20, 160);
            this.cmbDoctors.Name = "cmbDoctors";
            this.cmbDoctors.Size = new System.Drawing.Size(300, 28);

            // lblShift
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(20, 210);
            this.lblShift.Name = "lblShift";
            this.lblShift.Text = "Chọn Ca trực:";

            // cmbShifts
            this.cmbShifts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShifts.FormattingEnabled = true;
            this.cmbShifts.Location = new System.Drawing.Point(20, 240);
            this.cmbShifts.Name = "cmbShifts";
            this.cmbShifts.Size = new System.Drawing.Size(300, 28);

            // btnAssign
            // 
            this.btnAssign.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnAssign.ForeColor = System.Drawing.Color.White;
            this.btnAssign.Location = new System.Drawing.Point(20, 310);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(140, 40);
            this.btnAssign.TabIndex = 4;
            this.btnAssign.Text = "Phân công";
            this.btnAssign.UseVisualStyleBackColor = false;

            // btnAddNew
            // 
            this.btnAddNew.BackColor = System.Drawing.Color.FromArgb(46, 204, 113); // Green
            this.btnAddNew.ForeColor = System.Drawing.Color.White;
            this.btnAddNew.Location = new System.Drawing.Point(180, 310);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(140, 40);
            this.btnAddNew.TabIndex = 5;
            this.btnAddNew.Text = "Thêm mới";
            this.btnAddNew.UseVisualStyleBackColor = false;

            // 
            // grpSchedule (Right Panel)
            // 
            this.grpSchedule.Controls.Add(this.btnDelete);
            this.grpSchedule.Controls.Add(this.dgvSchedule);
            this.grpSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSchedule.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.grpSchedule.Location = new System.Drawing.Point(0, 0);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Padding = new Padding(10);
            this.grpSchedule.Size = new System.Drawing.Size(696, 700);
            this.grpSchedule.TabIndex = 1;
            this.grpSchedule.TabStop = false;
            this.grpSchedule.Text = "Danh Sách Phân Công Trong Ngày";

            // dgvSchedule
            // 
            this.dgvSchedule.AllowUserToAddRows = false;
            this.dgvSchedule.AllowUserToDeleteRows = false;
            this.dgvSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedule.BackgroundColor = System.Drawing.Color.White;
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Location = new System.Drawing.Point(10, 40);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.ReadOnly = true;
            this.dgvSchedule.RowHeadersVisible = false;
            this.dgvSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedule.Size = new System.Drawing.Size(670, 580);
            this.dgvSchedule.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvSchedule.TabIndex = 0;

            // btnDelete
            // 
            this.btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Enabled = false; // Initially disabled
            this.btnDelete.Location = new System.Drawing.Point(540, 640);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 40);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Hủy phân công";
            this.btnDelete.UseVisualStyleBackColor = false;

            // 
            // UC_ShiftAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "UC_ShiftAssignment";
            this.Size = new System.Drawing.Size(1050, 700);
            
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpAssignment.ResumeLayout(false);
            this.grpAssignment.PerformLayout();
            this.grpSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpAssignment;
        private System.Windows.Forms.GroupBox grpSchedule;
        
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.ComboBox cmbDoctors;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.ComboBox cmbShifts;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnAddNew;
        
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.Button btnDelete;
    }
}
