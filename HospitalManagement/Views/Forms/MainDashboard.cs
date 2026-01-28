using HospitalManagement.Infrastructure.Common;
using HospitalManagement.Models.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms
{
    public class MainDashboard : BaseForm
    {
        private Panel sidebarPanel;
        private Panel headerPanel;
        private Panel contentPanel;
        private Users currentUser;
        private Button activeMenuButton;

        public MainDashboard(Users user)
        {
            currentUser = user;
            InitializeComponents();
        }

        protected override void ApplyBaseStyles()
        {
            base.ApplyBaseStyles();
            this.Text = "Hospital Management System";
            this.Size = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new Size(1024, 600);
        }

        private void InitializeComponents()
        {
            // Sidebar
            sidebarPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = AppDimensions.SidebarWidth,
                BackColor = AppColors.SidebarBackground
            };

            // Logo section
            var logoPanel = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(35, 50, 65)
            };

            var logoLabel = new Label
            {
                Text = "🏥 Hospital",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = AppColors.TextLight,
                AutoSize = true,
                Location = new Point(20, 25)
            };
            logoPanel.Controls.Add(logoLabel);

            // Menu items panel
            var menuPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0, 20, 0, 0)
            };

            // Create menu items based on user role
            CreateMenuItems(menuPanel);

            // User info at bottom
            var userPanel = new Panel
            {
                Height = 70,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(35, 50, 65),
                Padding = new Padding(15)
            };

            var userIcon = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI", 20),
                Location = new Point(15, 15),
                AutoSize = true
            };

            var userName = new Label
            {
                Text = currentUser.FullName,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextLight,
                Location = new Point(55, 12),
                AutoSize = true
            };

            var userRole = new Label
            {
                Text = GetRoleDisplayName(currentUser.Role),
                Font = AppFonts.Small,
                ForeColor = Color.FromArgb(150, 255, 255, 255),
                Location = new Point(55, 32),
                AutoSize = true
            };

            var btnLogout = new Button
            {
                Text = "⏻",
                Size = new Size(35, 35),
                Location = new Point(195, 17),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 14),
                ForeColor = AppColors.TextLight,
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;

            userPanel.Controls.AddRange(new Control[] { userIcon, userName, userRole, btnLogout });

            sidebarPanel.Controls.Add(menuPanel);
            sidebarPanel.Controls.Add(logoPanel);
            sidebarPanel.Controls.Add(userPanel);

            // Header
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = AppDimensions.HeaderHeight,
                BackColor = AppColors.CardBackground,
                Padding = new Padding(20, 0, 20, 0)
            };

            var headerTitle = new Label
            {
                Name = "lblHeaderTitle",
                Text = "Trang chủ",
                Font = AppFonts.Heading,
                ForeColor = AppColors.TextPrimary,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            var headerDate = new Label
            {
                Text = DateTime.Now.ToString("dddd, dd MMMM yyyy"),
                Font = AppFonts.Body,
                ForeColor = AppColors.TextSecondary,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.Width - 450, 20)
            };

            headerPanel.Controls.AddRange(new Control[] { headerTitle, headerDate });

            // Content panel
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.Background,
                Padding = new Padding(20)
            };

            // Add controls in correct order
            this.Controls.Add(contentPanel);
            this.Controls.Add(headerPanel);
            this.Controls.Add(sidebarPanel);

            // Load default content
            LoadHomeContent();
        }

        private void CreateMenuItems(Panel menuPanel)
        {
            int yPos = 0;

            // Common menu items
            var btnHome = CreateMenuButton("🏠  Trang chủ", yPos);
            btnHome.Click += (s, e) => { SetActiveMenu(btnHome, "Trang chủ"); LoadHomeContent(); };
            menuPanel.Controls.Add(btnHome);
            yPos += 50;

            // Role-specific menu items
            switch (currentUser.Role.ToLower())
            {
                case "patient":
                    AddPatientMenuItems(menuPanel, ref yPos);
                    break;
                case "doctor":
                    AddDoctorMenuItems(menuPanel, ref yPos);
                    break;
                case "admin":
                    AddAdminMenuItems(menuPanel, ref yPos);
                    break;
                default:
                    AddPatientMenuItems(menuPanel, ref yPos);
                    break;
            }

            // Set first button as active
            SetActiveMenu(btnHome, "Trang chủ");
        }

        private void AddPatientMenuItems(Panel menuPanel, ref int yPos)
        {
            var btnAppointment = CreateMenuButton("📅  Đặt lịch khám", yPos);
            btnAppointment.Click += (s, e) => { SetActiveMenu(btnAppointment, "Đặt lịch khám"); LoadPlaceholder("Đặt lịch khám"); };
            menuPanel.Controls.Add(btnAppointment);
            yPos += 50;

            var btnHistory = CreateMenuButton("📋  Lịch sử đặt khám", yPos);
            btnHistory.Click += (s, e) => { SetActiveMenu(btnHistory, "Lịch sử đặt khám"); LoadPlaceholder("Lịch sử đặt khám"); };
            menuPanel.Controls.Add(btnHistory);
            yPos += 50;

            var btnHealth = CreateMenuButton("❤️  Hồ sơ sức khỏe", yPos);
            btnHealth.Click += (s, e) => { SetActiveMenu(btnHealth, "Hồ sơ sức khỏe"); LoadPlaceholder("Hồ sơ sức khỏe"); };
            menuPanel.Controls.Add(btnHealth);
            yPos += 50;

            var btnPayment = CreateMenuButton("💳  Thanh toán", yPos);
            btnPayment.Click += (s, e) => { SetActiveMenu(btnPayment, "Thanh toán"); LoadPlaceholder("Thanh toán"); };
            menuPanel.Controls.Add(btnPayment);
            yPos += 50;
        }

        private void AddDoctorMenuItems(Panel menuPanel, ref int yPos)
        {
            var btnQueue = CreateMenuButton("👥  Hàng đợi khám", yPos);
            btnQueue.Click += (s, e) => { SetActiveMenu(btnQueue, "Hàng đợi khám"); LoadPlaceholder("Hàng đợi khám"); };
            menuPanel.Controls.Add(btnQueue);
            yPos += 50;

            var btnExam = CreateMenuButton("🩺  Khám bệnh", yPos);
            btnExam.Click += (s, e) => { SetActiveMenu(btnExam, "Khám bệnh"); LoadPlaceholder("Khám bệnh"); };
            menuPanel.Controls.Add(btnExam);
            yPos += 50;

            var btnSchedule = CreateMenuButton("📆  Lịch làm việc", yPos);
            btnSchedule.Click += (s, e) => { SetActiveMenu(btnSchedule, "Lịch làm việc"); LoadPlaceholder("Lịch làm việc"); };
            menuPanel.Controls.Add(btnSchedule);
            yPos += 50;

            var btnPatients = CreateMenuButton("📁  Hồ sơ bệnh nhân", yPos);
            btnPatients.Click += (s, e) => { SetActiveMenu(btnPatients, "Hồ sơ bệnh nhân"); LoadPlaceholder("Hồ sơ bệnh nhân"); };
            menuPanel.Controls.Add(btnPatients);
            yPos += 50;
        }

        private void AddAdminMenuItems(Panel menuPanel, ref int yPos)
        {
            var btnUsers = CreateMenuButton("👤  Quản lý User", yPos);
            btnUsers.Click += (s, e) => { SetActiveMenu(btnUsers, "Quản lý User"); LoadPlaceholder("Quản lý User"); };
            menuPanel.Controls.Add(btnUsers);
            yPos += 50;

            var btnDoctors = CreateMenuButton("👨‍⚕️  Quản lý Bác sĩ", yPos);
            btnDoctors.Click += (s, e) => { SetActiveMenu(btnDoctors, "Quản lý Bác sĩ"); LoadPlaceholder("Quản lý Bác sĩ"); };
            menuPanel.Controls.Add(btnDoctors);
            yPos += 50;

            var btnDepts = CreateMenuButton("🏢  Phòng ban", yPos);
            btnDepts.Click += (s, e) => { SetActiveMenu(btnDepts, "Phòng ban"); LoadPlaceholder("Phòng ban"); };
            menuPanel.Controls.Add(btnDepts);
            yPos += 50;

            var btnServices = CreateMenuButton("🔧  Dịch vụ", yPos);
            btnServices.Click += (s, e) => { SetActiveMenu(btnServices, "Dịch vụ"); LoadPlaceholder("Dịch vụ"); };
            menuPanel.Controls.Add(btnServices);
            yPos += 50;

            var btnReports = CreateMenuButton("📊  Báo cáo", yPos);
            btnReports.Click += (s, e) => { SetActiveMenu(btnReports, "Báo cáo"); LoadPlaceholder("Báo cáo"); };
            menuPanel.Controls.Add(btnReports);
            yPos += 50;
        }

        private Button CreateMenuButton(string text, int yPos)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(AppDimensions.SidebarWidth, 50),
                Location = new Point(0, yPos),
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.Body,
                ForeColor = Color.FromArgb(200, 255, 255, 255),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => { if (btn != activeMenuButton) btn.BackColor = Color.FromArgb(55, 75, 95); };
            btn.MouseLeave += (s, e) => { if (btn != activeMenuButton) btn.BackColor = Color.Transparent; };
            return btn;
        }

        private void SetActiveMenu(Button btn, string title)
        {
            if (activeMenuButton != null)
            {
                activeMenuButton.BackColor = Color.Transparent;
                activeMenuButton.ForeColor = Color.FromArgb(200, 255, 255, 255);
            }

            activeMenuButton = btn;
            btn.BackColor = AppColors.Primary;
            btn.ForeColor = AppColors.TextLight;

            // Update header title
            var headerTitle = headerPanel.Controls.Find("lblHeaderTitle", false);
            if (headerTitle.Length > 0)
            {
                headerTitle[0].Text = title;
            }
        }

        private void LoadHomeContent()
        {
            contentPanel.Controls.Clear();

            // Welcome card
            var welcomeCard = new Panel
            {
                Size = new Size(contentPanel.Width - 60, 120),
                Location = new Point(10, 10),
                BackColor = AppColors.CardBackground,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var welcomeText = new Label
            {
                Text = $"Xin chào, {currentUser.FullName}!",
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextPrimary,
                Location = new Point(25, 25),
                AutoSize = true
            };

            var welcomeSubtext = new Label
            {
                Text = "Chào mừng bạn đến với Hệ thống Quản lý Bệnh viện",
                Font = AppFonts.Body,
                ForeColor = AppColors.TextSecondary,
                Location = new Point(25, 60),
                AutoSize = true
            };

            welcomeCard.Controls.AddRange(new Control[] { welcomeText, welcomeSubtext });
            contentPanel.Controls.Add(welcomeCard);

            // Quick action cards
            int cardY = 150;
            int cardX = 10;
            int cardWidth = 200;
            int cardHeight = 150;

            string[] icons = { "📅", "📋", "💳", "❤️" };
            string[] titles = { "Đặt lịch khám", "Lịch sử đặt khám", "Thanh toán", "Hồ sơ sức khỏe" };
            Color[] colors = { AppColors.Primary, AppColors.Secondary, AppColors.Warning, AppColors.Accent };

            for (int i = 0; i < 4; i++)
            {
                var card = CreateQuickActionCard(icons[i], titles[i], colors[i]);
                card.Location = new Point(cardX + (i * (cardWidth + 20)), cardY);
                contentPanel.Controls.Add(card);
            }
        }

        private Panel CreateQuickActionCard(string icon, string title, Color accentColor)
        {
            var card = new Panel
            {
                Size = new Size(200, 150),
                BackColor = AppColors.CardBackground,
                Cursor = Cursors.Hand
            };

            var iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 36),
                Location = new Point(20, 20),
                AutoSize = true
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextPrimary,
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
            card.MouseLeave += (s, e) => card.BackColor = AppColors.CardBackground;

            return card;
        }

        private void LoadPlaceholder(string featureName)
        {
            contentPanel.Controls.Clear();

            var placeholder = new Label
            {
                Text = $"🚧 {featureName}\n\nTính năng này đang được phát triển...",
                Font = AppFonts.Heading,
                ForeColor = AppColors.TextSecondary,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            contentPanel.Controls.Add(placeholder);
        }

        private string GetRoleDisplayName(string role)
        {
            switch (role?.ToLower())
            {
                case "patient": return "Bệnh nhân";
                case "doctor": return "Bác sĩ";
                case "admin": return "Quản trị viên";
                default: return role;
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Retry; // Signal to show login again
                this.Close();
            }
        }
    }
}
