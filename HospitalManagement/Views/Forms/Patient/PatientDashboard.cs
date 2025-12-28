using HospitalManagement.Infrastructure.Common;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters;
using HospitalManagement.Views.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms.Patient
{
    public partial class PatientDashboard : Form, IDashboardView
    {
        private readonly DashboardPresenter _presenter;
        private Button _activeMenuButton;

        public Users CurrentUser { get; set; }

        public PatientDashboard(Users user)
        {
            InitializeComponent();
            
            CurrentUser = user;
            _presenter = new DashboardPresenter(this);
            
            InitializeUserInfo();
            SetActiveButton(btnHome);
            LoadHomeContent();
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
        }

        public void LoadContent(string contentName)
        {
            contentPanel.Controls.Clear();

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
                Size = new Size(contentPanel.Width - 60, 120),
                Location = new Point(10, 10),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var welcomeText = new Label
            {
                Text = $"Xin chào, {CurrentUser.FullName}!",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(25, 25),
                AutoSize = true
            };

            var welcomeSubtext = new Label
            {
                Text = "Chào mừng bạn đến với Hệ thống Quản lý Bệnh viện",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(127, 140, 141),
                Location = new Point(25, 60),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { welcomeText, welcomeSubtext });
            return card;
        }

        private void CreateQuickActionCards()
        {
            string[] icons = { "📅", "📋", "💳", "❤️" };
            string[] titles = { "Đặt lịch khám", "Lịch sử khám", "Thanh toán", "Hồ sơ sức khỏe" };
            Color[] colors = { 
                Color.FromArgb(0, 102, 204), 
                Color.FromArgb(0, 168, 107), 
                Color.FromArgb(241, 196, 15), 
                Color.FromArgb(231, 76, 60) 
            };

            for (int i = 0; i < 4; i++)
            {
                var card = CreateQuickActionCard(icons[i], titles[i], colors[i]);
                card.Location = new Point(10 + (i * 220), 150);
                contentPanel.Controls.Add(card);
            }
        }

        private Panel CreateQuickActionCard(string icon, string title, Color accentColor)
        {
            var card = new Panel
            {
                Size = new Size(200, 150),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            var iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 36F),
                Location = new Point(20, 20),
                AutoSize = true
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(20, 100),
                AutoSize = true
            };

            var accentBar = new Panel
            {
                Size = new Size(200, 4),
                Location = new Point(0, 146),
                BackColor = accentColor
            };

            card.Controls.AddRange(new Control[] { iconLabel, titleLabel, accentBar });

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(248, 249, 250);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void SetActiveButton(Button button)
        {
            // Reset previous active button
            if (_activeMenuButton != null)
            {
                _activeMenuButton.BackColor = Color.Transparent;
                _activeMenuButton.ForeColor = Color.FromArgb(200, 255, 255, 255);
            }

            // Set new active button
            _activeMenuButton = button;
            button.BackColor = Color.FromArgb(0, 102, 204);
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
