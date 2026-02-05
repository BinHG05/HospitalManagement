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
            this.cmbFilterDepartment = new System.Windows.Forms.ComboBox();
            this.lblFilterDept = new System.Windows.Forms.Label();
            this.dgvDoctors = new System.Windows.Forms.DataGridView();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblNewAccount = new System.Windows.Forms.Label();
            this.lblNewFullName = new System.Windows.Forms.Label();
            this.txtNewFullName = new System.Windows.Forms.TextBox();
            this.lblNewUsername = new System.Windows.Forms.Label();
            this.txtNewUsername = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewEmail = new System.Windows.Forms.Label();
            this.txtNewEmail = new System.Windows.Forms.TextBox();
            this.lblNewPhone = new System.Windows.Forms.Label();
            this.txtNewPhone = new System.Windows.Forms.TextBox();
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
            this.grpSearch.Controls.Add(this.cmbFilterDepartment);
            this.grpSearch.Controls.Add(this.lblFilterDept);
            this.grpSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpSearch.Location = new System.Drawing.Point(25, 60);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(550, 80);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Tìm kiếm & Lọc";

            // 
            // lblFilterDept
            // 
            this.lblFilterDept.AutoSize = true;
            this.lblFilterDept.Location = new System.Drawing.Point(20, 30);
            this.lblFilterDept.Name = "lblFilterDept";
            this.lblFilterDept.Size = new System.Drawing.Size(43, 19);
            this.lblFilterDept.Text = "Khoa:";

            // 
            // cmbFilterDepartment
            // 
            this.cmbFilterDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterDepartment.FormattingEnabled = true;
            this.cmbFilterDepartment.Location = new System.Drawing.Point(65, 28);
            this.cmbFilterDepartment.Name = "cmbFilterDepartment";
            this.cmbFilterDepartment.Size = new System.Drawing.Size(180, 25);

            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(260, 28);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(180, 25);

            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(450, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.Text = "Tìm";
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnSearch.ForeColor = System.Drawing.Color.White;

            // 
            // dgvDoctors
            // 
            this.dgvDoctors.ColumnHeadersHeight = 30;
            this.dgvDoctors.Location = new System.Drawing.Point(25, 150);
            this.dgvDoctors.Name = "dgvDoctors";
            this.dgvDoctors.Size = new System.Drawing.Size(600, 520);
            this.dgvDoctors.ReadOnly = true;
            this.dgvDoctors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoctors.MultiSelect = false;
            this.dgvDoctors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoctors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblUser);
            this.grpInfo.Controls.Add(this.cmbUser);
            this.grpInfo.Controls.Add(this.lblNewAccount);
            this.grpInfo.Controls.Add(this.lblNewFullName);
            this.grpInfo.Controls.Add(this.txtNewFullName);
            this.grpInfo.Controls.Add(this.lblNewUsername);
            this.grpInfo.Controls.Add(this.txtNewUsername);
            this.grpInfo.Controls.Add(this.lblNewPassword);
            this.grpInfo.Controls.Add(this.txtNewPassword);
            this.grpInfo.Controls.Add(this.lblNewEmail);
            this.grpInfo.Controls.Add(this.txtNewEmail);
            this.grpInfo.Controls.Add(this.lblNewPhone);
            this.grpInfo.Controls.Add(this.txtNewPhone);
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
            this.grpInfo.Location = new System.Drawing.Point(640, 60);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(380, 610);
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Thông tin chi tiết";
            this.grpInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));

            // Layout helper vars
            int left = 15;
            int right = 140;
            int w = 220;
            int y = 25;
            int gap = 38;

            // User Selection
            this.lblUser.Text = "Tài khoản liên kết:";
            this.lblUser.Location = new System.Drawing.Point(left, y);
            this.lblUser.AutoSize = true;
            this.cmbUser.Location = new System.Drawing.Point(right, y);
            this.cmbUser.Size = new System.Drawing.Size(w, 25);
            this.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += gap;

            // New Account Section Divider
            this.lblNewAccount.Text = "--- Tạo tài khoản mới (nếu chưa có) ---";
            this.lblNewAccount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNewAccount.Location = new System.Drawing.Point(left, y);
            this.lblNewAccount.Size = new System.Drawing.Size(350, 20);
            this.lblNewAccount.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblNewAccount.ForeColor = System.Drawing.Color.DarkBlue;
            y += 22;

            // New Full Name
            this.lblNewFullName.Text = "Họ và tên:";
            this.lblNewFullName.Location = new System.Drawing.Point(left, y);
            this.lblNewFullName.AutoSize = true;
            this.txtNewFullName.Location = new System.Drawing.Point(right, y);
            this.txtNewFullName.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // New Username
            this.lblNewUsername.Text = "Tên đăng nhập:";
            this.lblNewUsername.Location = new System.Drawing.Point(left, y);
            this.lblNewUsername.AutoSize = true;
            this.txtNewUsername.Location = new System.Drawing.Point(right, y);
            this.txtNewUsername.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // New Password
            this.lblNewPassword.Text = "Mật khẩu:";
            this.lblNewPassword.Location = new System.Drawing.Point(left, y);
            this.lblNewPassword.AutoSize = true;
            this.txtNewPassword.Location = new System.Drawing.Point(right, y);
            this.txtNewPassword.Size = new System.Drawing.Size(w, 25);
            this.txtNewPassword.UseSystemPasswordChar = true;
            y += gap;

            // New Email
            this.lblNewEmail.Text = "Email:";
            this.lblNewEmail.Location = new System.Drawing.Point(left, y);
            this.lblNewEmail.AutoSize = true;
            this.txtNewEmail.Location = new System.Drawing.Point(right, y);
            this.txtNewEmail.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // New Phone
            this.lblNewPhone.Text = "Số điện thoại:";
            this.lblNewPhone.Location = new System.Drawing.Point(left, y);
            this.lblNewPhone.AutoSize = true;
            this.txtNewPhone.Location = new System.Drawing.Point(right, y);
            this.txtNewPhone.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // Department
            this.lblDept.Text = "Phòng ban:";
            this.lblDept.Location = new System.Drawing.Point(left, y);
            this.lblDept.AutoSize = true;
            this.cmbDepartment.Location = new System.Drawing.Point(right, y);
            this.cmbDepartment.Size = new System.Drawing.Size(w, 25);
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            y += gap;

            // Specialization
            this.lblSpec.Text = "Chuyên khoa:";
            this.lblSpec.Location = new System.Drawing.Point(left, y);
            this.lblSpec.AutoSize = true;
            this.txtSpecialization.Location = new System.Drawing.Point(right, y);
            this.txtSpecialization.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // License
            this.lblLicense.Text = "Số chứng chỉ:";
            this.lblLicense.Location = new System.Drawing.Point(left, y);
            this.lblLicense.AutoSize = true;
            this.txtLicense.Location = new System.Drawing.Point(right, y);
            this.txtLicense.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // Experience
            this.lblExp.Text = "Kinh nghiệm:";
            this.lblExp.Location = new System.Drawing.Point(left, y);
            this.lblExp.AutoSize = true;
            this.numExp.Location = new System.Drawing.Point(right, y);
            this.numExp.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // Fee
            this.lblFee.Text = "Phí khám (VND):";
            this.lblFee.Location = new System.Drawing.Point(left, y);
            this.lblFee.AutoSize = true;
            this.txtFee.Location = new System.Drawing.Point(right, y);
            this.txtFee.Size = new System.Drawing.Size(w, 25);
            y += gap;

            // Qualifications
            this.lblQual.Text = "Bằng cấp/Mô tả:";
            this.lblQual.Location = new System.Drawing.Point(left, y);
            this.lblQual.AutoSize = true;
            this.txtQualifications.Location = new System.Drawing.Point(right, y);
            this.txtQualifications.Size = new System.Drawing.Size(w, 50);
            this.txtQualifications.Multiline = true;
            y += gap + 15;

            // Active
            this.chkIsActive.Text = "Đang hoạt động";
            this.chkIsActive.Location = new System.Drawing.Point(left, y);
            this.chkIsActive.Checked = true;
            y += 35;

            // Buttons
            this.btnSave.Text = "Thêm mới";
            this.btnSave.Location = new System.Drawing.Point(left, y);
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 168, 107);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.btnDelete.Text = "Vô hiệu";
            this.btnDelete.Location = new System.Drawing.Point(left + 110, y);
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.BackColor = System.Drawing.Color.Gray;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Enabled = false;

            this.btnClear.Text = "Làm mới";
            this.btnClear.Location = new System.Drawing.Point(left + 220, y);
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

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
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbFilterDepartment;
        private System.Windows.Forms.Label lblFilterDept;
        private System.Windows.Forms.DataGridView dgvDoctors;
        private System.Windows.Forms.GroupBox grpInfo;
        
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.Label lblNewAccount;
        private System.Windows.Forms.Label lblNewFullName;
        private System.Windows.Forms.TextBox txtNewFullName;
        private System.Windows.Forms.Label lblNewUsername;
        private System.Windows.Forms.TextBox txtNewUsername;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblNewEmail;
        private System.Windows.Forms.TextBox txtNewEmail;
        private System.Windows.Forms.Label lblNewPhone;
        private System.Windows.Forms.TextBox txtNewPhone;

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
    }
}
