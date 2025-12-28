using HospitalManagement.Infrastructure.Common;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms
{
    /// <summary>
    /// Base form with common styling for all application forms
    /// </summary>
    public class BaseForm : Form
    {
        public BaseForm()
        {
            ApplyBaseStyles();
        }

        protected virtual void ApplyBaseStyles()
        {
            this.BackColor = AppColors.Background;
            this.Font = AppFonts.Body;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        /// <summary>
        /// Create a styled primary button
        /// </summary>
        protected Button CreatePrimaryButton(string text, int width = 150)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(width, AppDimensions.ButtonHeight),
                BackColor = AppColors.Primary,
                ForeColor = AppColors.TextLight,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.Button,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = AppColors.PrimaryDark;
            btn.MouseLeave += (s, e) => btn.BackColor = AppColors.Primary;
            return btn;
        }

        /// <summary>
        /// Create a styled secondary button
        /// </summary>
        protected Button CreateSecondaryButton(string text, int width = 150)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(width, AppDimensions.ButtonHeight),
                BackColor = AppColors.CardBackground,
                ForeColor = AppColors.Primary,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.Button,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = AppColors.Primary;
            btn.FlatAppearance.BorderSize = 2;
            btn.MouseEnter += (s, e) => { btn.BackColor = AppColors.PrimaryLight; };
            btn.MouseLeave += (s, e) => { btn.BackColor = AppColors.CardBackground; };
            return btn;
        }

        /// <summary>
        /// Create a styled text box
        /// </summary>
        protected TextBox CreateStyledTextBox(int width = 250)
        {
            var txt = new TextBox
            {
                Size = new Size(width, AppDimensions.TextBoxHeight),
                Font = AppFonts.Body,
                BorderStyle = BorderStyle.FixedSingle
            };
            return txt;
        }

        /// <summary>
        /// Create a styled label
        /// </summary>
        protected Label CreateLabel(string text, Font font = null)
        {
            return new Label
            {
                Text = text,
                Font = font ?? AppFonts.Body,
                ForeColor = AppColors.TextPrimary,
                AutoSize = true
            };
        }

        /// <summary>
        /// Create a card panel with shadow effect
        /// </summary>
        protected Panel CreateCardPanel(int width, int height)
        {
            var panel = new Panel
            {
                Size = new Size(width, height),
                BackColor = AppColors.CardBackground,
                Padding = new Padding(AppDimensions.CardPadding)
            };
            return panel;
        }
    }
}
