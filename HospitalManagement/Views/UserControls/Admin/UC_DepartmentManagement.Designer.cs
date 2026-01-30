using System.Windows.Forms;
using System.Drawing;

namespace HospitalManagement.Views.UserControls.Admin
{
    partial class UC_DepartmentManagement
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
            this.dgvDepartments = new System.Windows.Forms.DataGridView();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();

            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).BeginInit();
            this.grpInfo.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(210, 30);
            this.lblTitle.Text = "Quản lý Phòng ban";

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
            this.btnAddNew.Size = new System.Drawing.Size(160, 35);
            this.btnAddNew.Text = "➕ Thêm Phòng ban";

            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.txtSearch);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpSearch.Location = new System.Drawing.Point(25, 60);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(500, 70);
            this.grpSearch.Text = "Tìm kiếm";

            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(20, 30);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(350, 25);
            // this.txtSearch.PlaceholderText = "Nhập tên phòng ban..."; // Removed for simple compilation

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
            // dgvDepartments
            // 
            this.dgvDepartments.ColumnHeadersHeight = 30;
            this.dgvDepartments.Location = new System.Drawing.Point(25, 150);
            this.dgvDepartments.Name = "dgvDepartments";
            this.dgvDepartments.Size = new System.Drawing.Size(550, 500);
            this.dgvDepartments.ReadOnly = true;
            this.dgvDepartments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartments.MultiSelect = false;
            this.dgvDepartments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblName);
            this.grpInfo.Controls.Add(this.txtName);
            this.grpInfo.Controls.Add(this.lblDesc);
            this.grpInfo.Controls.Add(this.txtDescription);
            this.grpInfo.Controls.Add(this.btnSave);
            this.grpInfo.Controls.Add(this.btnDelete);
            this.grpInfo.Controls.Add(this.btnClear);
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpInfo.Location = new System.Drawing.Point(600, 60);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(400, 300); // Shorter than simpler forms
            this.grpInfo.Text = "Thông tin chi tiết";

            // 
            // lblName
            // 
            this.lblName.Text = "Tên phòng ban:";
            this.lblName.Location = new System.Drawing.Point(20, 40);
            this.lblName.AutoSize = true;

            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(140, 38);
            this.txtName.Size = new System.Drawing.Size(230, 25);

            // 
            // lblDesc
            // 
            this.lblDesc.Text = "Mô tả:";
            this.lblDesc.Location = new System.Drawing.Point(20, 80);
            this.lblDesc.AutoSize = true;

            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(140, 80);
            this.txtDescription.Size = new System.Drawing.Size(230, 100);
            this.txtDescription.Multiline = true;

            // 
            // btnSave
            // 
            this.btnSave.Text = "Thêm mới";
            this.btnSave.Location = new System.Drawing.Point(20, 210);
            this.btnSave.Size = new System.Drawing.Size(110, 35);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 168, 107);
            this.btnSave.ForeColor = System.Drawing.Color.White;

            // 
            // btnDelete
            // 
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new System.Drawing.Point(140, 210);
            this.btnDelete.Size = new System.Drawing.Size(110, 35);
            this.btnDelete.BackColor = System.Drawing.Color.Gray;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Enabled = false;

            // 
            // btnClear
            // 
            this.btnClear.Text = "Làm mới";
            this.btnClear.Location = new System.Drawing.Point(260, 210);
            this.btnClear.Size = new System.Drawing.Size(100, 35);

            // 
            // UC_DepartmentManagement
            // 
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.dgvDepartments);
            this.Controls.Add(this.grpInfo);
            this.Name = "UC_DepartmentManagement";
            this.Size = new System.Drawing.Size(1050, 700);
            this.BackColor = System.Drawing.Color.White;

            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).EndInit();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvDepartments;
        private System.Windows.Forms.GroupBox grpInfo;
        
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDescription;
        
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAddNew;
    }
}
