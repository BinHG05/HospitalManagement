using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms.Patient
{
    public partial class Form_QRPayment : Form
    {
        public Form_QRPayment(string amount, string invoiceNumber)
        {
            InitializeComponent(amount, invoiceNumber);
        }

        private void InitializeComponent(string amount, string invoiceNumber)
        {
            this.Text = "Thanh toán chuyển khoản qua QR";
            this.Size = new Size(400, 600);
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
                Text = "QUÉT MÃ QR ĐỂ THANH TOÁN",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            pnlHeader.Controls.Add(lblTitle);

            PictureBox picQR = new PictureBox
            {
                Location = new Point(25, 80),
                Size = new Size(350, 420),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.None
            };

            string qrPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", "vcb_qr_payment.png");
            if (System.IO.File.Exists(qrPath))
            {
                picQR.Image = Image.FromFile(qrPath);
            }
            else
            {
                picQR.BackColor = Color.FromArgb(241, 245, 249);
                Label lblError = new Label {
                    Text = "Không tìm thấy tệp QR\nAssets/Icons/vcb_qr_payment.png",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Red
                };
                picQR.Controls.Add(lblError);
            }

            Label lblInfo = new Label
            {
                Text = $"Hóa đơn: {invoiceNumber}\nSố tiền cần trả: {amount} đ",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(25, 510),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblStatus = new Label
            {
                Text = "⏳ Đang chờ hệ thống xác nhận...",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(25, 550),
                Size = new Size(350, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.Add(lblStatus);
            this.Controls.Add(lblInfo);
            this.Controls.Add(picQR);
            this.Controls.Add(pnlHeader);

            // Close automatically after 4 seconds to simulate success
            Timer timer = new Timer { Interval = 4000 };
            timer.Tick += (s, e) => {
                timer.Stop();
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            timer.Start();
        }
    }
}
