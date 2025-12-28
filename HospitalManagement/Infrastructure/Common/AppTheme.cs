using System.Drawing;

namespace HospitalManagement.Infrastructure.Common
{
    /// <summary>
    /// UMC-style color theme for the application
    /// </summary>
    public static class AppColors
    {
        // Primary Colors
        public static readonly Color Primary = Color.FromArgb(0, 102, 204);       // #0066CC - Blue (Medical)
        public static readonly Color PrimaryDark = Color.FromArgb(0, 76, 153);    // Darker blue for hover
        public static readonly Color PrimaryLight = Color.FromArgb(230, 242, 255); // Light blue background

        // Secondary Colors
        public static readonly Color Secondary = Color.FromArgb(0, 168, 107);     // #00A86B - Green (Health)
        public static readonly Color SecondaryDark = Color.FromArgb(0, 128, 80);

        // Background Colors
        public static readonly Color Background = Color.FromArgb(245, 247, 250);  // #F5F7FA - Light Gray
        public static readonly Color CardBackground = Color.White;
        public static readonly Color SidebarBackground = Color.FromArgb(44, 62, 80); // Dark sidebar

        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(44, 62, 80);    // #2C3E50 - Dark Gray
        public static readonly Color TextSecondary = Color.FromArgb(127, 140, 141);
        public static readonly Color TextLight = Color.White;

        // Accent Colors
        public static readonly Color Accent = Color.FromArgb(231, 76, 60);        // #E74C3C - Red (Alerts)
        public static readonly Color Warning = Color.FromArgb(241, 196, 15);      // Yellow
        public static readonly Color Success = Color.FromArgb(46, 204, 113);      // Green

        // Status Colors
        public static readonly Color StatusPending = Color.FromArgb(241, 196, 15);
        public static readonly Color StatusConfirmed = Color.FromArgb(52, 152, 219);
        public static readonly Color StatusCompleted = Color.FromArgb(46, 204, 113);
        public static readonly Color StatusCancelled = Color.FromArgb(231, 76, 60);
    }

    /// <summary>
    /// Font configurations for the application
    /// </summary>
    public static class AppFonts
    {
        public static readonly Font Title = new Font("Segoe UI", 24, FontStyle.Bold);
        public static readonly Font Subtitle = new Font("Segoe UI", 18, FontStyle.Bold);
        public static readonly Font Heading = new Font("Segoe UI", 14, FontStyle.Bold);
        public static readonly Font Body = new Font("Segoe UI", 11, FontStyle.Regular);
        public static readonly Font BodyBold = new Font("Segoe UI", 11, FontStyle.Bold);
        public static readonly Font Small = new Font("Segoe UI", 9, FontStyle.Regular);
        public static readonly Font Button = new Font("Segoe UI", 11, FontStyle.Bold);
    }

    /// <summary>
    /// Common UI dimensions
    /// </summary>
    public static class AppDimensions
    {
        public const int SidebarWidth = 250;
        public const int HeaderHeight = 60;
        public const int ButtonHeight = 40;
        public const int TextBoxHeight = 35;
        public const int CardPadding = 20;
        public const int BorderRadius = 8;
    }
}
