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
            this.Size = new Size(850, 950);
            this.Text = "Đơn Thuốc Điện Tử - MedCare Premium";
            this.BackColor = Color.FromArgb(226, 232, 240); 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Main Paper Container with subtle shadow effect
            Panel pnlPaper = new Panel
            {
                Size = new Size(780, 850),
                Location = new Point(35, 20),
                BackColor = Color.White,
                Padding = new Padding(40)
            };

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
            Panel pnlTableContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 10, 0, 10),
                BackColor = Color.White
            };

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
                ScrollBars = ScrollBars.Vertical
            };
            
            // Modern Header Style
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 40;

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
            Panel pnlFooter = new Panel { Dock = DockStyle.Bottom, Height = 280 };
            
            Label lblNote = new Label
            {
                Text = "* Ghi chú: Bệnh nhân vui lòng mang đơn thuốc này đến quầy thanh toán\nvà quầy thuốc để thực hiện thủ tục nhận thuốc.",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(71, 85, 105),
                Size = new Size(650, 45),
                Location = new Point(50, 0),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblSignDate = new Label
            {
                Text = $"TP. Hồ Chí Minh, ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year}",
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                Size = new Size(350, 25),
                Location = Point.Empty, // Will set later
                TextAlign = ContentAlignment.MiddleCenter
            };
            lblSignDate.Location = new Point(350, 100);

            Label lblSignTitle = new Label
            {
                Text = "BÁC SĨ CHUYÊN KHOA",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Size = new Size(350, 25),
                Location = new Point(350, 125),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblSubText = new Label
            {
                Text = "(Ký và ghi rõ họ tên)",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Size = new Size(350, 20),
                Location = new Point(350, 220),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlFooter.Controls.Add(lblNote);
            pnlFooter.Controls.Add(lblSignDate);
            pnlFooter.Controls.Add(lblSignTitle);
            pnlFooter.Controls.Add(lblSubText);

            // Actions panel outside the paper
            Panel pnlButtons = new Panel
            {
                Size = new Size(850, 60),
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
            pnlPaper.Controls.Add(pnlTableContainer);
            pnlPaper.Controls.Add(pnlFooter);
            pnlPaper.Controls.Add(pnlDiagnosis);
            pnlPaper.Controls.Add(gbPatient);
            pnlPaper.Controls.Add(lblTitle);
            pnlPaper.Controls.Add(headerTable);
            pnlPaper.Controls.Add(pnlTopBar);

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
