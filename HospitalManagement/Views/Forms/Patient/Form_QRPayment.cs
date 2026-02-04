using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms.Patient
{
    public partial class Form_QRPayment : Form
    {
        public Form_QRPayment(string amount, string invoiceNumber, string paymentMethod = "bank_transfer")
        {
            InitializeComponent(amount, invoiceNumber, paymentMethod);
        }

        private void InitializeComponent(string amount, string invoiceNumber, string paymentMethod)
        {
            string titleText = "THANH TOÁN QUA QR";
            string qrFileName = "vcb_qr_payment.png";
            string codeLabel = "Nội dung chuyển khoản:";

            if (paymentMethod == "ewallet" || paymentMethod == "Ví điện tử")
            {
                titleText = "THANH TOÁN VÍ ĐIỆN TỬ QUA QR";
                qrFileName = "momo_payment.png";
                codeLabel = "Mã thanh toán:";
                this.Text = "Thanh toán Ví điện tử";
            }
            else
            {
                this.Text = "Thanh toán chuyển khoản qua QR";
            }

            this.Size = new Size(400, 600); // Tăng nhẹ chiều cao để tạo khoảng trống dưới đáy
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(15, 23, 42)
            };

            Label lblTitle = new Label
            {
                Text = titleText,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            pnlHeader.Controls.Add(lblTitle);

            PictureBox picQR = new PictureBox
            {
                Location = new Point(25, 75),
                Size = new Size(350, 330),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.None
            };

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            // Handle both running from bin/Debug and from root during development
            string qrPath = System.IO.Path.Combine(baseDir, "Assets", "Icons", qrFileName);
            
            if (!System.IO.File.Exists(qrPath))
            {
                // Try fallback for dev environment
                qrPath = System.IO.Path.Combine(baseDir, "..", "..", "Assets", "Icons", qrFileName);
            }

            if (System.IO.File.Exists(qrPath))
            {
                picQR.Image = Image.FromFile(qrPath);
            }
            else
            {
                picQR.BackColor = Color.FromArgb(241, 245, 249);
                Label lblError = new Label {
                    Text = $"Không tìm thấy tệp QR\n{qrPath}", // Show full path in error
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Red
                };
                picQR.Controls.Add(lblError);
            }

            Label lblInfo = new Label
            {
                Text = $"{codeLabel} {invoiceNumber}\nSố tiền: {amount} đ",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(25, 410),
                Size = new Size(350, 45),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button btnCancel = new Button
            {
                Text = "QUAY LẠI",
                Size = new Size(350, 35),
                Location = new Point(25, 510), 
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(100, 116, 139),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            Button btnConfirm = new Button
            {
                Text = "XÁC NHẬN",
                Size = new Size(350, 45),
                Location = new Point(25, 455),
                BackColor = Color.FromArgb(16, 185, 129), 
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += async (s, e) => {
                // [NEW] Simulate verification process
                btnConfirm.Enabled = false;
                btnConfirm.Text = "⏳ ĐANG XÁC THỰC GIAO DỊCH...";
                btnConfirm.BackColor = Color.FromArgb(243, 156, 18); // Orange
                btnCancel.Enabled = false;

                // Create a timer to simulate network delay (2 seconds)
                // Using Task.Delay if C# 5.0+ or Timer
                var timer = new Timer();
                timer.Interval = 2000;
                timer.Tick += (ts, te) => {
                    timer.Stop();
                    // Verification success
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                };
                timer.Start();
            };


            this.Controls.Add(btnConfirm);
            this.Controls.Add(btnCancel);
            this.Controls.Add(lblInfo);
            this.Controls.Add(picQR);
            this.Controls.Add(pnlHeader);
        }
    }
}
