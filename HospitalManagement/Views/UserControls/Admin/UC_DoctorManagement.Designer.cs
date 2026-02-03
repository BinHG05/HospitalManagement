using System.Windows.Forms;
using System.Drawing;

namespace HospitalManagement.Views.UserControls.Admin
{
    partial class UC_DoctorManagement
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvDoctors = new System.Windows.Forms.DataGridView();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblDept = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lblSpec = new System.Windows.Forms.Label();
            this.txtSpecialization = new System.Windows.Forms.TextBox();
            this.lblLicense = new System.Windows.Forms.Label();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.lblExp = new System.Windows.Forms.Label();
            this.numExp = new System.Windows.Forms.NumericUpDown();
            this.lblFee = new System.Windows.Forms.Label();
            this.txtFee = new System.Windows.Forms.TextBox();
            this.lblQual = new System.Windows.Forms.Label();
            this.txtQualifications = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();

            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).BeginInit();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExp)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 30);
            this.lblTitle.Text = "Quản lý Bác sĩ";

            // 
            // btnAddNew
            // 
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnAddNew.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddNew.ForeColor = System.Drawing.Color.White;
            this.btnAddNew.Location = new System.Drawing.Point(870, 15);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(150, 35);
            this.btnAddNew.Text = "➕ Thêm Bác sĩ";

            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.txtSearch);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpSearch.Location = new System.Drawing.Point(25, 60);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(500, 100);
            this.grpSearch.Text = "Tìm kiếm & Lọc";

            // lblFilterDept
            this.lblFilterDept = new System.Windows.Forms.Label();
            this.lblFilterDept.AutoSize = true;
            this.lblFilterDept.Location = new System.Drawing.Point(20, 65);
            this.lblFilterDept.Name = "lblFilterDept";
            this.lblFilterDept.Size = new System.Drawing.Size(43, 19);
            this.lblFilterDept.Text = "Khoa:";

            // cmbFilterDept
            this.cmbFilterDept = new System.Windows.Forms.ComboBox();
            this.cmbFilterDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterDept.FormattingEnabled = true;
            this.cmbFilterDept.Location = new System.Drawing.Point(80, 62);
            this.cmbFilterDept.Name = "cmbFilterDept";
            this.cmbFilterDept.Size = new System.Drawing.Size(290, 25);
            this.cmbFilterDept.TabIndex = 2;

            this.grpSearch.Controls.Add(this.lblFilterDept);
            this.grpSearch.Controls.Add(this.cmbFilterDept);

            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(20, 30);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(350, 25);
            // this.txtSearch.PlaceholderText = ""; // Not supported in .NET Framework WinForms

            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(380, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.Text = "Tìm";
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnSearch.ForeColor = System.Drawing.Color.White;

            // 
            // dgvDoctors
            // 
            this.dgvDoctors.ColumnHeadersHeight = 30;
            this.dgvDoctors.Location = new System.Drawing.Point(25, 150);
            this.dgvDoctors.Name = "dgvDoctors";
            this.dgvDoctors.Size = new System.Drawing.Size(550, 500);
            this.dgvDoctors.ReadOnly = true;
            this.dgvDoctors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoctors.MultiSelect = false;
            this.dgvDoctors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblUser);
            this.grpInfo.Controls.Add(this.cmbUser);
            this.grpInfo.Controls.Add(this.lblDept);
            this.grpInfo.Controls.Add(this.cmbDepartment);
            this.grpInfo.Controls.Add(this.lblSpec);
            this.grpInfo.Controls.Add(this.txtSpecialization);
            this.grpInfo.Controls.Add(this.lblLicense);
            this.grpInfo.Controls.Add(this.txtLicense);
            this.grpInfo.Controls.Add(this.lblExp);
            this.grpInfo.Controls.Add(this.numExp);
            this.grpInfo.Controls.Add(this.lblFee);
            this.grpInfo.Controls.Add(this.txtFee);
            this.grpInfo.Controls.Add(this.lblQual);
            this.grpInfo.Controls.Add(this.txtQualifications);
            this.grpInfo.Controls.Add(this.chkIsActive);
            this.grpInfo.Controls.Add(this.btnSave);
            this.grpInfo.Controls.Add(this.btnDelete);
            this.grpInfo.Controls.Add(this.btnClear);
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpInfo.Location = new System.Drawing.Point(600, 60);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(400, 590);
            this.grpInfo.Text = "Thông tin chi tiết";

            // Layout helper vars
            int left = 20;
            int right = 150;
            int w = 220;
            int y = 30;
            int gap = 45;

            // User (Account)
            this.lblUser.Text = "Tài khoản (User):";
            this.lblUser.Location = new System.Drawing.Point(left, y);
            this.cmbUser.Location = new System.Drawing.Point(right, y);
            this.cmbUser.Size = new System.Drawing.Size(w, 25);
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += gap;

            // Department
            this.lblDept.Text = "Phòng ban:";
            this.lblDept.Location = new System.Drawing.Point(left, y);
            this.cmbDepartment.Location = new System.Drawing.Point(right, y);
            this.cmbDepartment.Size = new System.Drawing.Size(w, 25);
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += gap;

            // Specialization
            this.lblSpec.Text = "Chuyên khoa:";
            this.lblSpec.Location = new System.Drawing.Point(left, y);
            this.txtSpecialization.Location = new System.Drawing.Point(right, y);
            this.txtSpecialization.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // License
            this.lblLicense.Text = "Số chứng chỉ:";
            this.lblLicense.Location = new System.Drawing.Point(left, y);
            this.txtLicense.Location = new System.Drawing.Point(right, y);
            this.txtLicense.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // Experience
            this.lblExp.Text = "Kinh nghiệm (năm):";
            this.lblExp.Location = new System.Drawing.Point(left, y);
            this.numExp.Location = new System.Drawing.Point(right, y);
            this.numExp.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // Fee
            this.lblFee.Text = "Phí khám (VND):";
            this.lblFee.Location = new System.Drawing.Point(left, y);
            this.txtFee.Location = new System.Drawing.Point(right, y);
            this.txtFee.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // Qualifications
            this.lblQual.Text = "Bằng cấp/Mô tả:";
            this.lblQual.Location = new System.Drawing.Point(left, y);
            this.txtQualifications.Location = new System.Drawing.Point(left, y + 25);
            this.txtQualifications.Size = new System.Drawing.Size(350, 80);
            this.txtQualifications.Multiline = true;
            y += 115; // larger gap

            // Active
            this.chkIsActive.Text = "Đang hoạt động";
            this.chkIsActive.Location = new System.Drawing.Point(left, y);
            this.chkIsActive.Checked = true;
            y += gap;

            // Buttons
            this.btnSave.Text = "Thêm mới";
            this.btnSave.Location = new System.Drawing.Point(left, y);
            this.btnSave.Size = new System.Drawing.Size(110, 35);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 168, 107);
            this.btnSave.ForeColor = System.Drawing.Color.White;

            this.btnDelete.Text = "Xóa/Vô hiệu";
            this.btnDelete.Location = new System.Drawing.Point(left + 120, y);
            this.btnDelete.Size = new System.Drawing.Size(110, 35);
            this.btnDelete.BackColor = System.Drawing.Color.Gray;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Enabled = false;

            this.btnClear.Text = "Làm mới";
            this.btnClear.Location = new System.Drawing.Point(left + 240, y);
            this.btnClear.Size = new System.Drawing.Size(100, 35);

            // Add all to GroupBox
            this.grpInfo.Controls.Add(this.lblTitle); // wait, title shouldn't be in grpInfo
            // Add labels
            this.grpInfo.Controls.AddRange(new System.Windows.Forms.Control[] { 
                this.lblUser, this.lblDept, this.lblSpec, this.lblLicense, 
                this.lblExp, this.lblFee, this.lblQual
            });

            // 
            // UC_DoctorManagement
            // 
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.dgvDoctors);
            this.Controls.Add(this.grpInfo);
            this.Name = "UC_DoctorManagement";
            this.Size = new System.Drawing.Size(1050, 700);
            this.BackColor = System.Drawing.Color.White;

            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoctors)).EndInit();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblFilterDept;
        private System.Windows.Forms.ComboBox cmbFilterDept;
        private System.Windows.Forms.DataGridView dgvDoctors;
        private System.Windows.Forms.GroupBox grpInfo;
        
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.TextBox txtSpecialization;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Label lblExp;
        private System.Windows.Forms.NumericUpDown numExp;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.TextBox txtFee;
        private System.Windows.Forms.Label lblQual;
        private System.Windows.Forms.TextBox txtQualifications;
        private System.Windows.Forms.CheckBox chkIsActive;
        
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAddNew;
    }
}
