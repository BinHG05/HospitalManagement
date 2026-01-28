using HospitalManagement.Infrastructure.Common;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters;
using HospitalManagement.Views.Interfaces;
using HospitalManagement.Views.UserControls.Patient;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms.Patient
{
    public partial class PatientDashboard : Form, IDashboardView
    {
        private readonly DashboardPresenter _presenter;
        private Button _activeMenuButton;
        private Timer _statusTimer;
        private System.Collections.Generic.HashSet<int> _notifiedAppointments = new System.Collections.Generic.HashSet<int>();

        public Users CurrentUser { get; set; }

        public PatientDashboard(Users user)
        {
            InitializeComponent();
            
            CurrentUser = user;
            _presenter = new DashboardPresenter(this);
            
            // Thiết lập phong cách Luxury
            lblLogo.ForeColor = Color.FromArgb(251, 191, 36); // Gold
            lblUserName.ForeColor = Color.FromArgb(248, 250, 252); // Silver/White
            lblUserIcon.BackColor = Color.FromArgb(251, 191, 36); // Icon Gold

            InitializeUserInfo();
            SetActiveButton(btnHome);
            LoadHomeContent();
            InitializeStatusTimer();
        }

        private void InitializeStatusTimer()
        {
            _statusTimer = new Timer();
            _statusTimer.Interval = 5000; // Check every 5 seconds
            _statusTimer.Tick += StatusTimer_Tick;
            _statusTimer.Start();
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                var patientId = GetPatientId();
                if (patientId <= 0) return;

                using (var context = new Models.EF.HospitalDbContext())
                {
                    var today = DateTime.Today;
                    var activeAppointment = context.Appointments
                        .Where(a => a.PatientID == patientId 
                                 && a.AppointmentDate == today 
                                 && a.Status == "examining")
                        .OrderByDescending(a => a.UpdatedAt)
                        .FirstOrDefault();

                    if (activeAppointment != null && !_notifiedAppointments.Contains(activeAppointment.AppointmentID))
                    {
                        _notifiedAppointments.Add(activeAppointment.AppointmentID);
                        ShowTurnNotification(activeAppointment);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Status polling error: {ex.Message}");
            }
        }

        private void ShowTurnNotification(Appointments appointment)
        {
            // Flash the form or show a message
            string message = $"🔔 ĐÃ ĐẾN LƯỢT KHÁM CỦA BẠN!\n\n" +
                            $"Vui lòng di chuyển đến phòng khám của bác sĩ ngay lập tức.\n" +
                            $"Số thứ tự của bạn: {appointment.AppointmentNumber}";
            
            MessageBox.Show(this, message, "Thông báo gọi khám", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeUserInfo()
        {
            lblUserName.Text = CurrentUser.FullName ?? "User";
            lblHeaderDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        #region IDashboardView Implementation

        public void LoadHomeContent()
        {
            contentPanel.Controls.Clear();

            // Welcome card
            var welcomeCard = CreateWelcomeCard();
            contentPanel.Controls.Add(welcomeCard);

            // Quick action cards
            CreateQuickActionCards();

            // Status Board (Real-time monitoring for patients)
            var statusBoard = new UserControls.Patient.UC_HospitalStatusBoard();
            statusBoard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            statusBoard.Location = new Point(0, 330); // Below quick actions (130 + 160 + gap)
            statusBoard.Size = new Size(contentPanel.Width - 10, contentPanel.Height - 340);
            contentPanel.Controls.Add(statusBoard);
        }

        public void LoadContent(string contentName)
        {
            contentPanel.Controls.Clear();

            switch (contentName)
            {
                case "Đặt lịch khám":
                    LoadAppointmentBooking();
                    break;
                case "Lịch sử khám":
                    LoadAppointmentHistory();
                    break;
                case "Hồ sơ sức khỏe":
                    LoadHealthRecord();
                    break;
                case "Thanh toán":
                    LoadPayment();
                    break;
                default:
                    ShowPlaceholder(contentName);
                    break;
            }
        }

        private void LoadHealthRecord()
        {
            var patientId = GetPatientId();
            
            var healthRecord = new UserControls.Patient.UC_HealthRecord();
            healthRecord.Dock = DockStyle.Fill;
            healthRecord.Initialize(patientId);
            
            contentPanel.Controls.Add(healthRecord);
        }

        private void LoadPayment()
        {
            var patientId = GetPatientId();
            
            var payment = new UserControls.Patient.UC_Payment();
            payment.Dock = DockStyle.Fill;
            payment.Initialize(patientId);
            
            contentPanel.Controls.Add(payment);
        }

        private void LoadAppointmentHistory()
        {
            var patientId = GetPatientId();
            
            var appointmentHistory = new UserControls.Patient.UC_AppointmentHistory();
            appointmentHistory.Dock = DockStyle.Fill;
            appointmentHistory.Initialize(patientId);
            
            contentPanel.Controls.Add(appointmentHistory);
        }

        private void LoadAppointmentBooking()
        {
            // Get patient ID from current user
            var patientId = GetPatientId();
            
            var appointmentBooking = new UserControls.Patient.UC_AppointmentBooking();
            appointmentBooking.Dock = DockStyle.Fill;
            appointmentBooking.Initialize(patientId);
            
            contentPanel.Controls.Add(appointmentBooking);
        }

        private int GetPatientId()
        {
            // Query patient ID from Users
            using (var context = new Models.EF.HospitalDbContext())
            {
                var patient = context.Patients.FirstOrDefault(p => p.UserID == CurrentUser.UserID);
                return patient?.PatientID ?? 0;
            }
        }

        private void ShowPlaceholder(string contentName)
        {
            var placeholder = new Label
            {
                Text = $"🚧 {contentName}\n\nTính năng này đang được phát triển...",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(127, 140, 141),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            contentPanel.Controls.Add(placeholder);
        }

        public void UpdateHeaderTitle(string title)
        {
            lblHeaderTitle.Text = title;
        }

        public void ShowLogoutConfirmation()
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }

        #endregion

        #region UI Helper Methods

        private Panel CreateWelcomeCard()
        {
            var card = new Panel
            {
                Size = new Size(contentPanel.Width - 50, 130),
                Location = new Point(0, 0),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Padding = new Padding(30)
            };

            // Blue accent on left
            var accentBar = new Panel
            {
                Size = new Size(5, 130),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(59, 130, 246)
            };

            var welcomeText = new Label
            {
                Text = $"Xin chào, {CurrentUser.FullName}! 👋",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(30, 25),
                AutoSize = true
            };

            var welcomeSubtext = new Label
            {
                Text = "Chào mừng bạn đến với Hệ thống Quản lý Bệnh viện MedCare",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(30, 70),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { accentBar, welcomeText, welcomeSubtext });
            return card;
        }

        private void CreateQuickActionCards()
        {
            string[] icons = { "📅", "📋", "💳", "📋" };
            string[] titles = { "Đặt lịch khám", "Lịch sử khám", "Thanh toán", "Hồ sơ sức khỏe" };
            string[] descriptions = { "Đặt lịch hẹn mới", "Xem các cuộc hẹn", "Thanh toán hóa đơn", "Xem hồ sơ y tế" };
            Color[] colors = { 
                Color.FromArgb(59, 130, 246),   // Blue
                Color.FromArgb(16, 185, 129),   // Emerald
                Color.FromArgb(245, 158, 11),   // Amber
                Color.FromArgb(239, 68, 68)     // Red
            };

            int cardWidth = 220;
            int cardGap = 20;
            int startX = 0;

            for (int i = 0; i < 4; i++)
            {
                var card = CreateQuickActionCard(icons[i], titles[i], descriptions[i], colors[i]);
                card.Location = new Point(startX + (i * (cardWidth + cardGap)), 150);
                contentPanel.Controls.Add(card);
            }
        }

        private Panel CreateQuickActionCard(string icon, string title, string description, Color accentColor)
        {
            var card = new Panel
            {
                Size = new Size(220, 160),
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Padding = new Padding(20)
            };

            // Accent strip at top
            var accentBar = new Panel
            {
                Size = new Size(220, 5),
                Location = new Point(0, 0),
                BackColor = accentColor
            };

            var iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 32F),
                Location = new Point(20, 25),
                AutoSize = true
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(20, 90),
                AutoSize = true
            };

            var descLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(20, 115),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { accentBar, iconLabel, titleLabel, descLabel });

            // Hover effects
            card.MouseEnter += (s, e) => {
                card.BackColor = Color.FromArgb(248, 250, 252);
            };
            card.MouseLeave += (s, e) => {
                card.BackColor = Color.White;
            };

            // Apply hover to child controls too
            foreach (Control ctrl in card.Controls)
            {
                ctrl.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(248, 250, 252);
                ctrl.MouseLeave += (s, e) => card.BackColor = Color.White;
            }

            return card;
        }

        private void SetActiveButton(Button button)
        {
            // Modern color scheme - Tailwind Slate/Blue
            Color activeColor = Color.FromArgb(59, 130, 246);     // Blue-500
            Color inactiveTextColor = Color.FromArgb(203, 213, 225); // Slate-300

            // Reset previous active button
            if (_activeMenuButton != null)
            {
                _activeMenuButton.BackColor = Color.Transparent;
                _activeMenuButton.ForeColor = inactiveTextColor;
            }

            // Set new active button with accent
            _activeMenuButton = button;
            button.BackColor = activeColor;
            button.ForeColor = Color.White;
        }

        #endregion

        #region Event Handlers

        private void btnHome_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnHome);
            _presenter.NavigateTo("Trang chủ");
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAppointment);
            _presenter.NavigateTo("Đặt lịch khám");
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnHistory);
            _presenter.NavigateTo("Lịch sử khám");
        }

        private void btnHealth_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnHealth);
            _presenter.NavigateTo("Hồ sơ sức khỏe");
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnPayment);
            _presenter.NavigateTo("Thanh toán");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _presenter.Logout();
        }

        #endregion
    }
}
