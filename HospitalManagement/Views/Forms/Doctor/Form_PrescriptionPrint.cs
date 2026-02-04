using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagement.Views.Interfaces.Doctor;

namespace HospitalManagement.Views.Forms.Doctor
{
    public partial class Form_PrescriptionPrint : Form
    {
        private PrescriptionPatientDto _patient;
        private List<PrescriptionItemDto> _items;

        public Form_PrescriptionPrint(PrescriptionPatientDto patient, IEnumerable<PrescriptionItemDto> items)
        {
            _patient = patient;
            _items = items.ToList();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(880, 700); // Reduced initial height
            this.Text = "Đơn Thuốc Điện Tử - MedCare Premium";
            this.BackColor = Color.FromArgb(226, 232, 240); 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable; // Allow resize
            this.MaximizeBox = true;
            this.AutoScroll = true; // IMPORTANT: Allow scrolling if content is large

            // Main Paper Container - AutoSize to fit content
            // Main Paper Container with manual height calculation
            // Calculate components height first
            int topBarH = 8;
            int headerH = 100;
            int titleH = 80;
            int patientH = 150;
            int diagnosisH = 45;
            
            // Need to calculate table height later, but for Panel initialization let's start with a base
            // Then update Height after
            
            Panel pnlPaper = new Panel
            {
                // Width = 780, // We will set Size directly
                // AutoSize = false, // Default is false
                Location = new Point(35, 20),
                BackColor = Color.White,
                Padding = new Padding(40, 0, 40, 40) // Bottom padding for footer
            };

            // ... (TopBar, Header, Title, Patient, Diagnosis definitions remain same) ...
            



            // TOP DECORATIVE BAR
            Panel pnlTopBar = new Panel
            {
                Size = new Size(780, 8),
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(37, 99, 235) // Elegant Blue
            };

            // HEADER SECTION
            TableLayoutPanel headerTable = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Top,
                Height = 100,
                Padding = new Padding(0, 10, 0, 0)
            };
            headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            FlowLayoutPanel pnlHospitalInfo = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, Dock = DockStyle.Fill };
            
            Label lblHospitalName = new Label
            {
                Text = "HỆ THỐNG PHÒNG KHÁM ĐA KHOA MEDCARE",
                Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true
            };
            Label lblAddress = new Label
            {
                Text = "📍 123 Đường ABC, Quận X, TP. Hồ Chí Minh\n📞 Hotline: 1900 1234 | 🌐 www.medcare.vn",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Margin = new Padding(0, 5, 0, 0)
            };

            pnlHospitalInfo.Controls.Add(lblHospitalName);
            pnlHospitalInfo.Controls.Add(lblAddress);

            Label lblId = new Label
            {
                Text = "Số: " + DateTime.Now.ToString("yyMMdd") + "/" + _patient.PatientId.ToString("D4"),
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(148, 163, 184),
                TextAlign = ContentAlignment.TopRight,
                Dock = DockStyle.Fill
            };

            headerTable.Controls.Add(pnlHospitalInfo, 0, 0);
            headerTable.Controls.Add(lblId, 1, 0);

            // TITLE
            Label lblTitle = new Label
            {
                Text = "ĐƠN THUỐC",
                Font = new Font("Segoe UI Black", 26F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Dock = DockStyle.Top,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // PATIENT INFORMATION BOX
            GroupBox gbPatient = new GroupBox
            {
                Text = " THÔNG TIN BỆNH NHÂN ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 235),
                Dock = DockStyle.Top,
                Height = 150,
                Margin = new Padding(0, 20, 0, 0),
                Padding = new Padding(20, 15, 20, 10)
            };

            TableLayoutPanel patientGrid = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 3,
                Dock = DockStyle.Fill
            };
            patientGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            patientGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            string pName = $"Họ và tên: { _patient.FullName.ToUpper() }";
            string pAge = $"Tuổi: { _patient.Age?.ToString() ?? "N/A" }";
            string pGender = $"Giới tính: { _patient.Gender }";
            string pPhone = $"SĐT: { _patient.Phone }";
            string pAddress = $"Địa chỉ: { _patient.Address }";
            string pInsurance = $"Số BHYT: { _patient.InsuranceNumber }";

            AddInfoLabel(patientGrid, pName, 0, 0, true);
            AddInfoLabel(patientGrid, pAge + "    " + pGender, 1, 0);
            AddInfoLabel(patientGrid, pPhone, 0, 1);
            AddInfoLabel(patientGrid, pInsurance, 1, 1);
            AddInfoLabel(patientGrid, pAddress, 0, 2, false, 2);

            gbPatient.Controls.Add(patientGrid);

            // DIAGNOSIS PANEL
            Panel pnlDiagnosis = new Panel { Dock = DockStyle.Top, Height = 45, Padding = new Padding(5, 10, 0, 0) };
            Label lblDiagnosis = new Label
            {
                Text = "Chẩn đoán: " + _patient.Diagnosis,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true
            };
            pnlDiagnosis.Controls.Add(lblDiagnosis);

            // MEDICINE TABLE
            int headerHeight = 45;
            int rowHeight = 45;
            int tableHeight = headerHeight + (_items.Count * rowHeight) + 30; // buffer

            Panel pnlTableContainer = new Panel
            {
                Dock = DockStyle.Top,
                Height = tableHeight,
                Padding = new Padding(0, 10, 0, 10),
                BackColor = Color.White
            };
            
            // UPDATE PAPER HEIGHT NOW
            int footerH = 350;
            int bottomPadding = 40;
            int calculatedTotalHeight = 8 + 100 + 80 + 150 + 45 + tableHeight + footerH + bottomPadding;
            pnlPaper.Size = new Size(780, calculatedTotalHeight);

            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(226, 232, 240),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowTemplate = { Height = 45 },
                ScrollBars = ScrollBars.None // Disable scrollbars inside the table
            };
            
            // Modern Header Style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = headerHeight;

            dgv.Columns.Add("No", "STT");
            dgv.Columns.Add("Name", "Tên thuốc / Hàm lượng");
            dgv.Columns.Add("Qty", "SL");
            dgv.Columns.Add("Usage", "Cách dùng & Chỉ dẫn");

            dgv.Columns["No"].Width = 45;
            dgv.Columns["Qty"].Width = 60;
            dgv.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            int count = 1;
            foreach (var item in _items)
            {
                dgv.Rows.Add(count++, item.MedicineName, item.Quantity, $"{item.Dosage}, {item.Frequency}. {item.Instructions}");
            }
            pnlTableContainer.Controls.Add(dgv);

            // FOOTER SECTION
            Panel pnlFooter = new Panel { Dock = DockStyle.Top, Height = 350, Padding = new Padding(0, 20, 0, 0) }; // Dock Top
            
            // Layout: Left (Notes & Doctor Sign) - Right (QR Code)
            TableLayoutPanel footerLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            // LEFT SIDE
            Panel pnlLeft = new Panel { Dock = DockStyle.Fill };
            
            Label lblNote = new Label
            {
                Text = "* Ghi chú: Bệnh nhân vui lòng mang đơn thuốc này đến quầy thanh toán\nvà quầy thuốc để thực hiện thủ tục nhận thuốc.",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(71, 85, 105),
                Size = new Size(400, 45),
                Location = new Point(10, 10),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblSignDate = new Label
            {
                Text = $"TP. Hồ Chí Minh, ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year}",
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                Size = new Size(300, 25),
                Location = new Point(50, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblSignTitle = new Label
            {
                Text = "BÁC SĨ CHUYÊN KHOA",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Size = new Size(300, 25),
                Location = new Point(50, 105),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblSubText = new Label
            {
                Text = "(Ký và ghi rõ họ tên)",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Size = new Size(300, 20),
                Location = new Point(50, 200),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlLeft.Controls.Add(lblNote);
            pnlLeft.Controls.Add(lblSignDate);
            pnlLeft.Controls.Add(lblSignTitle);
            pnlLeft.Controls.Add(lblSubText);

            // RIGHT SIDE (QR CODE)
            Panel pnlRight = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            
            GroupBox gbPayment = new GroupBox
            {
                Text = " THANH TOÁN ONLINE ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(185, 28, 28), // Red color
                Dock = DockStyle.Fill
            };
            
            // Use TableLayout for perfect centering
            TableLayoutPanel gbLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(0, 20, 0, 0) // Push down from GroupBox title
            };
            gbLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70F)); // QR code area
            gbLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F)); // Text area

            PictureBox pbQR = new PictureBox
            {
                Size = new Size(180, 180), 
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.None,
                Anchor = AnchorStyles.Bottom // Stick to bottom of upper cell to be close to text
            };
            
            try 
            {
                // Try to find the image in multiple locations
                string[] possiblePaths = new string[]
                {
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", "payment_qr.png"),
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Assets", "Icons", "payment_qr.png"),
                    @"e:\TTKYC_Final\HospitalManagement\HospitalManagement\Assets\Icons\payment_qr.png" 
                };

                bool loaded = false;
                foreach (var path in possiblePaths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        pbQR.Image = Image.FromFile(path);
                        loaded = true;
                        break;
                    }
                }

                if (!loaded)
                {
                     // Fallback placeholder
                     Bitmap bmp = new Bitmap(180, 180);
                     using (Graphics g = Graphics.FromImage(bmp))
                     {
                        g.Clear(Color.White);
                        g.DrawRectangle(Pens.Black, 0, 0, 179, 179);
                        g.DrawString("No QR Image", new Font("Arial", 10), Brushes.Black, 40, 80);
                     }
                     pbQR.Image = bmp;
                }
            }
            catch
            {
                // Silent fail
            }

            Label lblScan = new Label
            {
                Text = "Quét mã để thanh toán\nngay trên ứng dụng ngân hàng",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = true,
                TextAlign = ContentAlignment.TopCenter,
                Anchor = AnchorStyles.Top // Stick to top of lower cell
            };

            gbLayout.Controls.Add(pbQR, 0, 0);
            gbLayout.Controls.Add(lblScan, 0, 1);
            gbPayment.Controls.Add(gbLayout);
            pnlRight.Controls.Add(gbPayment);

            footerLayout.Controls.Add(pnlLeft, 0, 0);
            footerLayout.Controls.Add(pnlRight, 1, 0);
            pnlFooter.Controls.Add(footerLayout);

            // Actions panel outside the paper
            Panel pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(243, 244, 246)
            };

            Button btnPrint = new Button
            {
                Text = "🖨️ XÁC NHẬN VÀ IN ĐƠN THUỐC",
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(280, 40),
                Location = new Point(285, 10),
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += (s, e) => {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            pnlButtons.Controls.Add(btnPrint);

            // ASSEMBLY
            // Add in reverse order of visual appearance (Bottom to Top)
            // Last Added = Index 0 = Top Most Dock Priority
            pnlPaper.Controls.Add(pnlFooter);           // Bottom
            pnlPaper.Controls.Add(pnlTableContainer);   // Above Footer
            pnlPaper.Controls.Add(pnlDiagnosis);        // Above Table
            pnlPaper.Controls.Add(gbPatient);           // ...
            pnlPaper.Controls.Add(lblTitle);
            pnlPaper.Controls.Add(headerTable);
            pnlPaper.Controls.Add(pnlTopBar);           // Top

            this.Controls.Add(pnlPaper);
            this.Controls.Add(pnlButtons);
        }

        private void AddInfoLabel(TableLayoutPanel panel, string text, int col, int row, bool bold = false, int colSpan = 1)
        {
            Label lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10F, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = Color.FromArgb(51, 65, 85),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(lbl, col, row);
            if (colSpan > 1) panel.SetColumnSpan(lbl, colSpan);
        }

        private System.ComponentModel.IContainer components = null;
    }
}
