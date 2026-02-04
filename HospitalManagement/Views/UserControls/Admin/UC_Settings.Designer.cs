namespace HospitalManagement.Views.UserControls.Admin
{
    partial class UC_Settings
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.btnSaveInfo = new System.Windows.Forms.Button();
            this.txtHotline = new System.Windows.Forms.TextBox();
            this.lblHotline = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grpSecurity = new System.Windows.Forms.GroupBox();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.txtConfirmPass = new System.Windows.Forms.TextBox();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.lblNewPass = new System.Windows.Forms.Label();
            this.txtCurrentPass = new System.Windows.Forms.TextBox();
            this.lblCurrentPass = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.grpSecurity.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(15);

            // lblTitle
            this.lblTitle.Text = "🛠️ Cài đặt hệ thống";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);

            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.btnSaveInfo);
            this.grpGeneral.Controls.Add(this.txtHotline);
            this.grpGeneral.Controls.Add(this.lblHotline);
            this.grpGeneral.Controls.Add(this.txtAddress);
            this.grpGeneral.Controls.Add(this.lblAddress);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpGeneral.Location = new System.Drawing.Point(50, 80);
            this.grpGeneral.Size = new System.Drawing.Size(500, 300);
            this.grpGeneral.Text = "🏥 Thông tin Bệnh viện";
            
            // TextBoxes and Labels
            this.lblName.Text = "Tên Bệnh viện:";
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location = new System.Drawing.Point(30, 50);
            this.lblName.AutoSize = true;

            this.txtName.Location = new System.Drawing.Point(30, 75);
            this.txtName.Size = new System.Drawing.Size(440, 27);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblAddress.Text = "Địa chỉ:";
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAddress.Location = new System.Drawing.Point(30, 115);
            this.lblAddress.AutoSize = true;

            this.txtAddress.Location = new System.Drawing.Point(30, 140);
            this.txtAddress.Size = new System.Drawing.Size(440, 27);
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.lblHotline.Text = "Hotline:";
            this.lblHotline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHotline.Location = new System.Drawing.Point(30, 180);
            this.lblHotline.AutoSize = true;

            this.txtHotline.Location = new System.Drawing.Point(30, 205);
            this.txtHotline.Size = new System.Drawing.Size(440, 27);
            this.txtHotline.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.btnSaveInfo.Text = "Lưu thông tin";
            this.btnSaveInfo.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSaveInfo.ForeColor = System.Drawing.Color.White;
            this.btnSaveInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveInfo.Location = new System.Drawing.Point(30, 250);
            this.btnSaveInfo.Size = new System.Drawing.Size(150, 35);
            this.btnSaveInfo.UseVisualStyleBackColor = false;

            // 
            // grpSecurity
            // 
            this.grpSecurity.Controls.Add(this.btnChangePass);
            this.grpSecurity.Controls.Add(this.txtConfirmPass);
            this.grpSecurity.Controls.Add(this.lblConfirmPass);
            this.grpSecurity.Controls.Add(this.txtNewPass);
            this.grpSecurity.Controls.Add(this.lblNewPass);
            this.grpSecurity.Controls.Add(this.txtCurrentPass);
            this.grpSecurity.Controls.Add(this.lblCurrentPass);
            this.grpSecurity.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpSecurity.Location = new System.Drawing.Point(600, 80);
            this.grpSecurity.Size = new System.Drawing.Size(400, 300);
            this.grpSecurity.Text = "🔒 Bảo mật";

            // Security Fields
            this.lblCurrentPass.Text = "Mật khẩu hiện tại:";
            this.lblCurrentPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentPass.Location = new System.Drawing.Point(30, 50);
            this.lblCurrentPass.AutoSize = true;

            this.txtCurrentPass.Location = new System.Drawing.Point(30, 75);
            this.txtCurrentPass.Size = new System.Drawing.Size(340, 27);
            this.txtCurrentPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCurrentPass.PasswordChar = '●';

            this.lblNewPass.Text = "Mật khẩu mới:";
            this.lblNewPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNewPass.Location = new System.Drawing.Point(30, 115);
            this.lblNewPass.AutoSize = true;

            this.txtNewPass.Location = new System.Drawing.Point(30, 140);
            this.txtNewPass.Size = new System.Drawing.Size(340, 27);
            this.txtNewPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPass.PasswordChar = '●';

            this.lblConfirmPass.Text = "Xác nhận mật khẩu:";
            this.lblConfirmPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConfirmPass.Location = new System.Drawing.Point(30, 180);
            this.lblConfirmPass.AutoSize = true;

            this.txtConfirmPass.Location = new System.Drawing.Point(30, 205);
            this.txtConfirmPass.Size = new System.Drawing.Size(340, 27);
            this.txtConfirmPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPass.PasswordChar = '●';

            this.btnChangePass.Text = "Đổi mật khẩu";
            this.btnChangePass.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnChangePass.ForeColor = System.Drawing.Color.White;
            this.btnChangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChangePass.Location = new System.Drawing.Point(30, 250);
            this.btnChangePass.Size = new System.Drawing.Size(150, 35);
            this.btnChangePass.UseVisualStyleBackColor = false;

            // Add controls
            this.Controls.Add(this.grpSecurity);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.pnlHeader);
            
            this.Size = new System.Drawing.Size(1050, 700);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpSecurity.ResumeLayout(false);
            this.grpSecurity.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.GroupBox grpSecurity;
        
        // General Controls
        private System.Windows.Forms.Button btnSaveInfo;
        private System.Windows.Forms.TextBox txtHotline;
        private System.Windows.Forms.Label lblHotline;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;

        // Security Controls
        private System.Windows.Forms.Button btnChangePass;
        private System.Windows.Forms.TextBox txtConfirmPass;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.Label lblNewPass;
        private System.Windows.Forms.TextBox txtCurrentPass;
        private System.Windows.Forms.Label lblCurrentPass;
    }
}
