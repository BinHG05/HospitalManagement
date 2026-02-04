using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalManagement.Services.Interfaces; // For PatientExamInfo

namespace HospitalManagement.Views.Forms.Doctor
{
    public partial class Form_ServiceRequestPrint : Form
    {
        private PatientExamInfo _patient;
        private string _serviceName;
        private decimal _price;
        private string _diagnosis;

        public Form_ServiceRequestPrint(PatientExamInfo patient, string serviceName, decimal price, string diagnosis)
        {
            _patient = patient;
            _serviceName = serviceName;
            _price = price;
            _diagnosis = diagnosis;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(600, 780); // Reduced Form Height
            this.Text = "Phiếu Chỉ Định Dịch Vụ";
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Main Card Panel
            Panel pnlCard = new Panel
            {
                Width = 500,
                Height = 660, // Reduced from 750to 660
                BackColor = Color.White,
                Location = new Point((this.ClientSize.Width - 500) / 2, 10) // Moved up
            };

            // 1. Header Section
            Label lblHospitalName = new Label
            {
                Text = "Bệnh viện Đa khoa MedCare",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = false, Size = new Size(pnlCard.Width, 25),
                TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 15) // Up 5
            };

            Label lblAddress = new Label
            {
                Text = "123 Đường Số 1, Phường Bến Nghé, Quận 1, TP. Hồ Chí Minh",
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = Color.Gray, AutoSize = false, Size = new Size(pnlCard.Width, 20),
                TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 40)
            };

            // 2. Main Title
            Label lblTitle = new Label
            {
                Text = "PHIẾU CHỈ ĐỊNH DỊCH VỤ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 235),
                AutoSize = false, Size = new Size(pnlCard.Width, 35),
                TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 70) 
            };

            Label lblCode = new Label
            {
                Text = $"Mã phiếu: SR{DateTime.Now:yyMMdd}{DateTime.Now:HHmm}",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = false, Size = new Size(pnlCard.Width, 20),
                TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 105)
            };

            // 3. Service/Department
            Label lblServiceName = new Label
            {
                Text = _serviceName.ToUpper(),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = false, Size = new Size(pnlCard.Width, 30),
                TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 130)
            };

            Label lblRoom = new Label
            {
                Text = "Phòng Kỹ Thuật Chuyên Khoa - Tầng 2",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                AutoSize = false, Size = new Size(pnlCard.Width, 25),
                TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 160)
            };

            // 4. Circle Number (STT) 
            Panel pnlCircle = new Panel
            {
                Size = new Size(70, 70), 
                Location = new Point((pnlCard.Width - 70) / 2, 190), // Up 10
                BackColor = Color.White
            };
            pnlCircle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen p = new Pen(Color.FromArgb(16, 185, 129), 3))
                {
                    e.Graphics.DrawEllipse(p, 3, 15, 64, 50); 
                }
                 e.Graphics.DrawString("1", new Font("Segoe UI", 22, FontStyle.Bold), new SolidBrush(Color.FromArgb(16, 185, 129)), new Rectangle(0, 15, 70, 55), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            };

            Label lblSttLabel = new Label
            {
                 Text = "STT",
                 Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                 ForeColor = Color.Gray,
                 AutoSize = true,
                 Location = new Point((pnlCard.Width - 26) / 2, 185), 
                 BackColor = Color.White
            };

            // 5. Patient Info List
            int startY = 270; // Started earlier (was 280)
            int lineHeight = 25; // Compacted (was 26/30)
            int leftMargin = 50;
            
            void AddRow(string label, string value, bool isBoldValue = false, Color? colorValue = null)
            {
                Label lblL = new Label { Text = label, Font = new Font("Segoe UI", 10F, FontStyle.Regular), ForeColor = Color.FromArgb(100, 116, 139), AutoSize = true, Location = new Point(leftMargin, startY) };
                Label lblV = new Label { Text = value, Font = new Font("Segoe UI", 10F, isBoldValue ? FontStyle.Bold : FontStyle.Regular), ForeColor = colorValue ?? Color.FromArgb(15, 23, 42), AutoSize = true, Location = new Point(leftMargin + 130, startY) };
                pnlCard.Controls.Add(lblL); pnlCard.Controls.Add(lblV);
                startY += lineHeight;
            }

            AddRow("Họ tên:", _patient.PatientName.ToUpper(), true);
            AddRow("Mã người bệnh:", $"BN{_patient.InsuranceNumber ?? "0000"}".Substring(0, Math.Min(8, (_patient.InsuranceNumber ?? "0000").Length)), true); 
            AddRow("Ngày sinh:", $"{_patient.DateOfBirth:dd/MM/yyyy}      Giới tính: {_patient.Gender}");
            AddRow("Ngày chỉ định:", $"{DateTime.Now:dd/MM/yyyy (HH:mm)}");
            AddRow("Tiền dịch vụ:", $"{_price:N0} đồng", true, Color.FromArgb(16, 185, 129)); 
            AddRow("Đối tượng:", !string.IsNullOrEmpty(_patient.InsuranceNumber) ? "BHYT" : "Thu phí");
            AddRow("Chẩn đoán:", _diagnosis);

            // 6. Footer Note
            // Calculated StartY after 7 rows = 270 + (7*25) = 445.
            Label lblNote = new Label
            {
                 Text = "Vui lòng đến trực tiếp quầy thu ngân hoặc quét mã bên dưới để thanh toán.",
                 Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                 ForeColor = Color.DimGray,
                 AutoSize = false, Size = new Size(pnlCard.Width - 60, 20),
                 Location = new Point(30, startY + 5), // Y ~= 450
                 TextAlign = ContentAlignment.TopCenter
            };

            // 7. QR Code Section 
            // Panel starts at 660 - 190 = 470.
            // Gap = 470 - 450 = 20px. Safe.
            Panel pnlPayment = new Panel
            {
                Size = new Size(pnlCard.Width, 190), 
                Location = new Point(0, pnlCard.Height - 190), 
                BackColor = Color.Transparent
            };
            
            Label lblScanTitle = new Label 
            {
                Text = "QUÉT MÃ THANH TOÁN NGAY",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 235),
                AutoSize = false, Size = new Size(pnlPayment.Width, 25),
                TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 5)
            };

            PictureBox pbQR = new PictureBox
            {
                Size = new Size(110, 110), 
                SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.White,
                Location = new Point((pnlPayment.Width - 110) / 2, 35)
            };
            
             try {
                string[] paths = { System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", "vcb_qr_payment.png"), System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", "payment_qr.png") };
                foreach(var p in paths) { if (System.IO.File.Exists(p)) { pbQR.Image = Image.FromFile(p); break; } }
            } catch {}

            Label lblAmount = new Label
            {
                Text = $"{_price:N0} VND",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold), ForeColor = Color.Red,
                AutoSize = false, Size = new Size(pnlPayment.Width, 30),
                TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 150)
            };

            pnlPayment.Controls.Add(lblScanTitle); pnlPayment.Controls.Add(pbQR); pnlPayment.Controls.Add(lblAmount);
            pnlCard.Controls.AddRange(new Control[] { lblHospitalName, lblAddress, lblTitle, lblCode, lblServiceName, lblRoom, pnlCircle, lblSttLabel, lblNote, pnlPayment });
            lblSttLabel.BringToFront();

            // Action Button
            Button btnExport = new Button 
            { 
                Text = "XÁC NHẬN & IN PHIẾU", DialogResult = DialogResult.None, 
                BackColor = Color.FromArgb(16, 185, 129), ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(pnlCard.Width, 45), 
                Location = new Point(pnlCard.Left, pnlCard.Bottom + 10) 
            };
            btnExport.FlatAppearance.BorderSize = 0;

            btnExport.Click += (s, e) => {
                try {
                    Bitmap bmp = new Bitmap(pnlCard.Width, pnlCard.Height);
                    pnlCard.DrawToBitmap(bmp, new Rectangle(0, 0, pnlCard.Width, pnlCard.Height));
                    
                    string folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HospitalManagement", "ServiceRequests");
                    if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);
                    string path = System.IO.Path.Combine(folder, $"ServiceRequest_{DateTime.Now:yyyyMMddHHmmss}.png");
                    
                    bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show($"Đã in phiếu thành công!\n{path}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                } catch (Exception ex) {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message);
                }
            };

            this.Controls.Add(pnlCard);
            this.Controls.Add(btnExport);
        }
    }
}
