namespace HospitalManagement.Views.UserControls.Doctor
{
    partial class UC_DoctorSchedule
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelWeekNav = new System.Windows.Forms.Panel();
            this.btnPrevWeek = new System.Windows.Forms.Button();
            this.lblWeekInfo = new System.Windows.Forms.Label();
            this.btnNextWeek = new System.Windows.Forms.Button();
            this.panelCalendar = new System.Windows.Forms.TableLayoutPanel();
            this.panelLegend = new System.Windows.Forms.Panel();

            this.panelHeader.SuspendLayout();
            this.panelWeekNav.SuspendLayout();
            this.panelLegend.SuspendLayout();
            this.SuspendLayout();

            // =============================================
            // panelHeader
            // =============================================
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.panelHeader.Size = new System.Drawing.Size(950, 55);
            this.panelHeader.TabIndex = 0;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "📆 Lịch làm việc";

            // =============================================
            // panelWeekNav
            // =============================================
            this.panelWeekNav.BackColor = System.Drawing.Color.White;
            this.panelWeekNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWeekNav.Location = new System.Drawing.Point(0, 55);
            this.panelWeekNav.Name = "panelWeekNav";
            this.panelWeekNav.Size = new System.Drawing.Size(950, 50);
            this.panelWeekNav.TabIndex = 1;

            this.btnPrevWeek.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevWeek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevWeek.FlatAppearance.BorderSize = 0;
            this.btnPrevWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevWeek.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrevWeek.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnPrevWeek.Location = new System.Drawing.Point(320, 10);
            this.btnPrevWeek.Name = "btnPrevWeek";
            this.btnPrevWeek.Size = new System.Drawing.Size(90, 30);
            this.btnPrevWeek.Text = "◀ Tuần";
            this.btnPrevWeek.Click += new System.EventHandler(this.btnPrevWeek_Click);

            this.lblWeekInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWeekInfo.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblWeekInfo.Location = new System.Drawing.Point(420, 13);
            this.lblWeekInfo.Name = "lblWeekInfo";
            this.lblWeekInfo.Size = new System.Drawing.Size(180, 25);
            this.lblWeekInfo.Text = "20/01 - 26/01/2026";
            this.lblWeekInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.btnNextWeek.BackColor = System.Drawing.Color.Transparent;
            this.btnNextWeek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextWeek.FlatAppearance.BorderSize = 0;
            this.btnNextWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextWeek.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNextWeek.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnNextWeek.Location = new System.Drawing.Point(610, 10);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(90, 30);
            this.btnNextWeek.Text = "Tuần ▶";
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);

            this.panelWeekNav.Controls.Add(this.btnPrevWeek);
            this.panelWeekNav.Controls.Add(this.lblWeekInfo);
            this.panelWeekNav.Controls.Add(this.btnNextWeek);

            // =============================================
            // panelCalendar
            // =============================================
            this.panelCalendar.BackColor = System.Drawing.Color.White;
            this.panelCalendar.ColumnCount = 7;
            this.panelCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCalendar.Location = new System.Drawing.Point(0, 105);
            this.panelCalendar.Name = "panelCalendar";
            this.panelCalendar.RowCount = 3;
            this.panelCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.panelCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelCalendar.Size = new System.Drawing.Size(950, 445);
            this.panelCalendar.TabIndex = 2;
            this.panelCalendar.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panelCalendar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // =============================================
            // panelLegend
            // =============================================
            this.panelLegend.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLegend.Location = new System.Drawing.Point(0, 550);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(950, 50);
            this.panelLegend.TabIndex = 3;

            var legendMorning = CreateLegendItem("Ca sáng (7:30-11:30)", System.Drawing.Color.FromArgb(59, 130, 246), 20);
            var legendAfternoon = CreateLegendItem("Ca chiều (13:30-17:30)", System.Drawing.Color.FromArgb(16, 185, 129), 250);
            var legendOff = CreateLegendItem("Nghỉ", System.Drawing.Color.FromArgb(148, 163, 184), 500);
            
            this.panelLegend.Controls.Add(legendMorning);
            this.panelLegend.Controls.Add(legendAfternoon);
            this.panelLegend.Controls.Add(legendOff);

            // =============================================
            // UC_DoctorSchedule
            // =============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.Controls.Add(this.panelCalendar);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.panelWeekNav);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_DoctorSchedule";
            this.Size = new System.Drawing.Size(950, 600);

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelWeekNav.ResumeLayout(false);
            this.panelLegend.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel CreateLegendItem(string text, System.Drawing.Color color, int x)
        {
            var panel = new System.Windows.Forms.Panel
            {
                Location = new System.Drawing.Point(x, 10),
                Size = new System.Drawing.Size(200, 30)
            };

            var colorBox = new System.Windows.Forms.Panel
            {
                Location = new System.Drawing.Point(0, 5),
                Size = new System.Drawing.Size(20, 20),
                BackColor = color
            };

            var label = new System.Windows.Forms.Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(25, 5),
                Font = new System.Drawing.Font("Segoe UI", 9F),
                ForeColor = System.Drawing.Color.FromArgb(15, 23, 42),
                Text = text
            };

            panel.Controls.Add(colorBox);
            panel.Controls.Add(label);
            return panel;
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelWeekNav;
        private System.Windows.Forms.Button btnPrevWeek;
        private System.Windows.Forms.Label lblWeekInfo;
        private System.Windows.Forms.Button btnNextWeek;
        private System.Windows.Forms.TableLayoutPanel panelCalendar;
        private System.Windows.Forms.Panel panelLegend;
    }
}
