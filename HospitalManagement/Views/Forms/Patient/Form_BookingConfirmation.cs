using HospitalManagement.Views.Interfaces.Patient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms.Patient
{
    public class Form_BookingConfirmation : Form
    {
        private PatientProfileInfo _profile;
        private string _deptName;
        private DateTime _date;
        private string _timeSlot;
        private int _queueNumber;

        public Form_BookingConfirmation(PatientProfileInfo profile, string deptName, DateTime date, string timeSlot, int queueNumber)
        {
            _profile = profile;
            _deptName = deptName;
            _date = date;
            _timeSlot = timeSlot;
            _queueNumber = queueNumber;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Thông tin phiếu khám";
            this.Size = new Size(520, 850);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(241, 245, 249); // Mẫu xám nhạt nền ngoài
            this.Font = new Font("Segoe UI", 10F);

            // Container chính màu trắng như tờ phiếu
            var card = new Panel 
            { 
                Location = new Point(20, 20),
                Size = new Size(465, 770),
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            this.Controls.Add(card);

            // 1. Header Bệnh viện
            var lblHospName = new Label
            {
                Text = "Bệnh viện Đa khoa MedCare",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 25,
                Dock = DockStyle.Top
            };
            var lblHospAddr = new Label
            {
                Text = "123 Đường Số 1, Phường Bến Nghé, Quận 1, TP. Hồ Chí Minh",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 20,
                Dock = DockStyle.Top
            };
            card.Controls.Add(lblHospAddr);
            card.Controls.Add(lblHospName);

            // 2. Tiêu đề Phiếu
            var lblTitle = new Label
            {
                Text = "PHIẾU KHÁM BỆNH",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 235), // Màu xanh dương
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 40,
                Location = new Point(20, 80),
                Size = new Size(425, 40)
            };
            card.Controls.Add(lblTitle);

            // 3. Mã phiếu (Mock)
            var lblRecordId = new Label
            {
                Text = $"Mã phiếu: APP{DateTime.Now:yyyyMMdd}{_queueNumber:D3}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(71, 85, 105),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 120),
                Size = new Size(425, 20)
            };
            card.Controls.Add(lblRecordId);

            // 4. Tên Khoa & Vị trí
            var lblDept = new Label
            {
                Text = _deptName.ToUpper(),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 160),
                Size = new Size(425, 30)
            };
            card.Controls.Add(lblDept);

            var lblLocation = new Label
            {
                Text = "Phòng 102 - Tầng 1 Khu A",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(34, 197, 94), // Màu xanh lá
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 190),
                Size = new Size(425, 25)
            };
            card.Controls.Add(lblLocation);

            // 5. Hình tròn STT
            var pnlSTT = new Panel
            {
                Size = new Size(90, 90),
                Location = new Point(187, 230),
                BackColor = Color.Transparent
            };
            pnlSTT.Paint += (s, e) => {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawEllipse(new Pen(Color.FromArgb(34, 197, 94), 2), 5, 5, 80, 80);
            };
            var lblSTTText = new Label
            {
                Text = "STT",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 15),
                Size = new Size(90, 20)
            };
            var lblSTTNum = new Label
            {
                Text = _queueNumber.ToString(),
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(34, 197, 94),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 35),
                Size = new Size(90, 40)
            };
            pnlSTT.Controls.Add(lblSTTText);
            pnlSTT.Controls.Add(lblSTTNum);
            card.Controls.Add(pnlSTT);

            // 6. Thông tin chi tiết
            int detailY = 340;
            AddVoucherDetail(card, "Họ tên:", _profile.FullName.ToUpper(), detailY);
            AddVoucherDetail(card, "Mã người bệnh:", $"BN-{_profile.PatientId:D6}", detailY += 25);
            AddVoucherDetail(card, "Ngày sinh:", $"{_profile.DateOfBirth?.ToString("dd/MM/yyyy")}    Giới tính: {(_profile.Gender == "Nam" ? "Nam" : "Nữ")}", detailY += 25);
            AddVoucherDetail(card, "Ngày khám:", $"{_date:dd/MM/yyyy} ({(_timeSlot.StartsWith("0") || _timeSlot.StartsWith("11") ? "Sáng" : "Chiều")})", detailY += 25);
            AddVoucherDetail(card, "Giờ dự kiến:", _timeSlot, detailY += 25);
            
            decimal fee = string.IsNullOrWhiteSpace(_profile.InsuranceNumber) ? 150000 : 75000;
            var lblFeeVal = AddVoucherDetail(card, "Tiền khám:", $"{fee:N0} đồng", detailY += 25);
            lblFeeVal.ForeColor = Color.FromArgb(34, 197, 94); // Giá tiền màu xanh lá

            AddVoucherDetail(card, "Đối tượng:", string.IsNullOrWhiteSpace(_profile.InsuranceNumber) ? "Không BHYT" : $"BHYT ({_profile.InsuranceNumber})", detailY += 25);

            // 7. Lời dặn
            var lblNote = new Label
            {
                Text = $"Vui lòng đến trực tiếp phòng khám {_deptName.ToUpper()} trước hẹn 15-30 phút để chuẩn bị khám bệnh.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.Black,
                Location = new Point(30, detailY + 40),
                Size = new Size(405, 45)
            };
            card.Controls.Add(lblNote);

            var lblFootnote = new Label
            {
                Text = "Ghi chú: Số thứ tự (STT) này chỉ có giá trị trong ngày khám.",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(30, detailY + 90),
                Size = new Size(405, 20)
            };
            card.Controls.Add(lblFootnote);

            // 8. Nút bấm
            var btnConfirm = new Button
            {
                Text = "XÁC NHẬN & THANH TOÁN",
                Size = new Size(405, 45),
                Location = new Point(30, 640),
                BackColor = Color.FromArgb(16, 185, 129),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };
            card.Controls.Add(btnConfirm);

            var btnEdit = new Button
            {
                Text = "Thay đổi thông tin cá nhân",
                Size = new Size(405, 30),
                Location = new Point(30, 695),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(37, 99, 235),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.Click += (s, e) => {
                using (var editForm = new Form_CompleteProfile(_profile.UserId)) {
                    if (editForm.ShowDialog() == DialogResult.OK) {
                        this.DialogResult = DialogResult.Retry;
                        this.Close();
                    }
                }
            };
            card.Controls.Add(btnEdit);
        }

        private Label AddVoucherDetail(Panel container, string label, string value, int y)
        {
            var lblL = new Label
            {
                Text = label,
                Location = new Point(30, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 116, 139)
            };
            container.Controls.Add(lblL);

            var lblR = new Label
            {
                Text = value,
                Location = new Point(145, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.Black
            };
            container.Controls.Add(lblR);
            return lblR;
        }

        private void AddDetail(GroupBox gb, string label, string value, int y, Color? valColor = null)
        {
            var lbl = new Label
            {
                Text = label,
                Location = new Point(15, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139)
            };
            gb.Controls.Add(lbl);

            var val = new Label
            {
                Text = value,
                Location = new Point(150, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = valColor ?? Color.Black
            };
            gb.Controls.Add(val);
        }
    }
}
