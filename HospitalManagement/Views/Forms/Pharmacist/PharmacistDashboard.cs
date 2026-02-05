using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters;
using HospitalManagement.Views.Interfaces;
using HospitalManagement.Views.UserControls.Pharmacist;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms.Pharmacist
{
    public partial class PharmacistDashboard : Form, IDashboardView
    {
        private readonly DashboardPresenter _presenter;
        private Button _activeMenuButton;

        public Users CurrentUser { get; set; }

        public PharmacistDashboard(Users user)
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
            lblUserName.Text = CurrentUser.FullName ?? "Pharmacist";
            lblUserRole.Text = "Dược sĩ";
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

            if (contentName == "Cấp phát thuốc")
            {
                var uc = new UC_PharmacySales();
                uc.Dock = DockStyle.Fill;
                contentPanel.Controls.Add(uc);
                return;
            }

            // Placeholder for others
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
                Text = $"Xin chào, {CurrentUser.FullName}! 👋",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(25, 25),
                AutoSize = true
            };

            var welcomeSubtext = new Label
            {
                Text = "Hệ thống quản lý cấp phát thuốc MedCare",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(25, 65),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { welcomeText, welcomeSubtext });
            return card;
        }

        private void CreateQuickActionCards()
        {
            string[] icons = { "💊", "📦", "📊" };
            string[] titles = { "Cấp phát thuốc", "Kho thuốc", "Báo cáo" };
            Color[] colors = { 
                Color.FromArgb(14, 165, 233), // Sky
                Color.FromArgb(16, 185, 129), // Emerald
                Color.FromArgb(245, 158, 11)  // Amber
            };

            for (int i = 0; i < 3; i++)
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
                ForeColor = Color.FromArgb(15, 23, 42),
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

            // Click events for navigation
            Action<object, EventArgs> clickHandler = (s, e) => {
                // Determine which button to highlight
                if (title == "Cấp phát thuốc") SetActiveButton(btnSales);
                
                // Navigate
                _presenter.NavigateTo(title);
            };

            card.Click += new EventHandler(clickHandler);
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Click += new EventHandler(clickHandler);
            }

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(248, 250, 252);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void SetActiveButton(Button button)
        {
            if (_activeMenuButton != null)
            {
                _activeMenuButton.BackColor = Color.Transparent;
                _activeMenuButton.ForeColor = Color.FromArgb(148, 163, 184);
            }

            _activeMenuButton = button;
            button.BackColor = Color.FromArgb(14, 165, 233);
            button.ForeColor = Color.White;
        }

        #endregion

        #region Event Handlers

        private void btnHome_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnHome);
            _presenter.NavigateTo("Trang chủ");
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSales);
            _presenter.NavigateTo("Cấp phát thuốc");
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnStock);
            _presenter.NavigateTo("Quản lý kho");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _presenter.Logout();
        }

        #endregion
    }
}
