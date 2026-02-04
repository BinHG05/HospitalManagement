using System.Windows.Forms;
using System.Drawing;

namespace HospitalManagement.Views.UserControls.Admin
{
    partial class UC_ServiceManagement
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
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();

            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(210, 30);
            this.lblTitle.Text = "Quản lý Dịch vụ";

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
            this.btnAddNew.Text = "➕ Thêm Dịch vụ";

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
            // 
            // dgvServices
            // 
            this.dgvServices.ColumnHeadersHeight = 30;
            this.dgvServices.Location = new System.Drawing.Point(25, 150);
            this.dgvServices.Name = "dgvServices";
            this.dgvServices.Size = new System.Drawing.Size(600, 500); // 550 -> 600
            this.dgvServices.ReadOnly = true;
            this.dgvServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServices.MultiSelect = false;
            this.dgvServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblName);
            this.grpInfo.Controls.Add(this.txtName);
            this.grpInfo.Controls.Add(this.lblPrice);
            this.grpInfo.Controls.Add(this.numPrice);
            this.grpInfo.Controls.Add(this.lblDesc);
            this.grpInfo.Controls.Add(this.txtDescription);
            this.grpInfo.Controls.Add(this.btnSave);
            this.grpInfo.Controls.Add(this.btnDelete);
            this.grpInfo.Controls.Add(this.btnClear);
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpInfo.Location = new System.Drawing.Point(640, 60); // 600 -> 640
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(380, 350); // 400 -> 380
            this.grpInfo.Text = "Thông tin dịch vụ";
            this.grpInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));

            // 
            // lblName
            // 
            this.lblName.Text = "Tên dịch vụ:";
            this.lblName.Location = new System.Drawing.Point(20, 40);
            this.lblName.AutoSize = true;

            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(140, 38);
            this.txtName.Size = new System.Drawing.Size(230, 25);

            // 
            // lblPrice
            // 
            this.lblPrice.Text = "Đơn giá (VNĐ):";
            this.lblPrice.Location = new System.Drawing.Point(20, 80);
            this.lblPrice.AutoSize = true;

            // 
            // numPrice
            // 
            this.numPrice.Location = new System.Drawing.Point(140, 78);
            this.numPrice.Size = new System.Drawing.Size(230, 25);
            this.numPrice.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
            this.numPrice.DecimalPlaces = 0;
            this.numPrice.ThousandsSeparator = true;

            // 
            // lblDesc
            // 
            this.lblDesc.Text = "Mô tả:";
            this.lblDesc.Location = new System.Drawing.Point(20, 120);
            this.lblDesc.AutoSize = true;

            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(140, 120);
            this.txtDescription.Size = new System.Drawing.Size(230, 100);
            this.txtDescription.Multiline = true;

            // 
            // btnSave
            // 
            this.btnSave.Text = "Thêm mới";
            this.btnSave.Location = new System.Drawing.Point(20, 250);
            this.btnSave.Size = new System.Drawing.Size(110, 35);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 168, 107);
            this.btnSave.ForeColor = System.Drawing.Color.White;

            // 
            // btnDelete
            // 
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new System.Drawing.Point(140, 250);
            this.btnDelete.Size = new System.Drawing.Size(110, 35);
            this.btnDelete.BackColor = System.Drawing.Color.Gray;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Enabled = false;

            // 
            // btnClear
            // 
            this.btnClear.Text = "Làm mới";
            this.btnClear.Location = new System.Drawing.Point(260, 250);
            this.btnClear.Size = new System.Drawing.Size(100, 35);

            // 
            // UC_ServiceManagement
            // 
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.dgvServices);
            this.Controls.Add(this.grpInfo);
            this.Name = "UC_ServiceManagement";
            this.Size = new System.Drawing.Size(1050, 700);
            this.BackColor = System.Drawing.Color.White;

            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.GroupBox grpInfo;
        
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDescription;
        
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAddNew;
    }
}
