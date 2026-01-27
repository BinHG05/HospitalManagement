namespace HospitalManagement.Views.UserControls.Patient
{
    partial class UC_AppointmentBooking
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
            this.panelContent = new System.Windows.Forms.Panel();
            
            // Department selection
            this.panelDepartment = new System.Windows.Forms.Panel();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.dtpSelectDate = new System.Windows.Forms.DateTimePicker();
            this.lblSelectDate = new System.Windows.Forms.Label();
            
            // Week navigation
            this.panelWeekNav = new System.Windows.Forms.Panel();
            this.btnPrevWeek = new System.Windows.Forms.Button();
            this.lblWeekInfo = new System.Windows.Forms.Label();
            this.btnNextWeek = new System.Windows.Forms.Button();
            
            // Weekly calendar - 7 days
            this.panelWeeklyCalendar = new System.Windows.Forms.TableLayoutPanel();
            
            // Shift selection (Sáng/Chiều)
            this.panelShiftSelection = new System.Windows.Forms.Panel();
            this.lblShiftTitle = new System.Windows.Forms.Label();
            this.btnMorningShift = new System.Windows.Forms.Button();
            this.btnAfternoonShift = new System.Windows.Forms.Button();
            
            // Time slots
            this.panelTimeSlots = new System.Windows.Forms.Panel();
            this.lblTimeSlotsTitle = new System.Windows.Forms.Label();
            this.flowTimeSlots = new System.Windows.Forms.FlowLayoutPanel();
            
            // Queue selection
            this.panelQueueSelection = new System.Windows.Forms.Panel();
            this.lblQueueTitle = new System.Windows.Forms.Label();
            this.lblSuggestedQueue = new System.Windows.Forms.Label();
            this.flowQueueNumbers = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSymptoms = new System.Windows.Forms.Label();
            this.txtSymptoms = new System.Windows.Forms.TextBox();
            this.btnConfirmBooking = new System.Windows.Forms.Button();
            this.btnCancelBooking = new System.Windows.Forms.Button();
            
            // Loading
            this.panelLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            
            this.panelHeader.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelDepartment.SuspendLayout();
            this.panelWeekNav.SuspendLayout();
            this.panelShiftSelection.SuspendLayout();
            this.panelTimeSlots.SuspendLayout();
            this.panelQueueSelection.SuspendLayout();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            
            // =============================================
            // panelHeader
            // =============================================
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.panelHeader.Size = new System.Drawing.Size(950, 55);
            this.panelHeader.TabIndex = 0;
            
            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📅 Đặt lịch khám bệnh";
            this.lblTitle.UseCompatibleTextRendering = true;
            
            // =============================================
            // panelContent - Scrollable main content
            // =============================================
            this.panelContent.AutoScroll = true;
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 55);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(25);
            this.panelContent.Size = new System.Drawing.Size(950, 545);
            this.panelContent.TabIndex = 1;
            
            // =============================================
            // panelDepartment - Department + Date selection
            // =============================================
            this.panelDepartment.BackColor = System.Drawing.Color.White;
            this.panelDepartment.Location = new System.Drawing.Point(25, 15);
            this.panelDepartment.Name = "panelDepartment";
            this.panelDepartment.Size = new System.Drawing.Size(900, 50);
            this.panelDepartment.TabIndex = 0;
            this.panelDepartment.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblDepartment.Location = new System.Drawing.Point(0, 14);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Text = "Chọn Khoa:";
            
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDepartment.Location = new System.Drawing.Point(100, 10);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(250, 28);
            this.cmbDepartment.TabIndex = 1;
            this.cmbDepartment.SelectedIndexChanged += new System.EventHandler(this.cmbDepartment_SelectedIndexChanged);
            
            this.lblSelectDate.AutoSize = true;
            this.lblSelectDate.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblSelectDate.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblSelectDate.Location = new System.Drawing.Point(400, 14);
            this.lblSelectDate.Name = "lblSelectDate";
            this.lblSelectDate.Text = "Chọn ngày:";
            
            this.dtpSelectDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpSelectDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSelectDate.Location = new System.Drawing.Point(500, 10);
            this.dtpSelectDate.MinDate = System.DateTime.Today;
            this.dtpSelectDate.Name = "dtpSelectDate";
            this.dtpSelectDate.Size = new System.Drawing.Size(140, 28);
            this.dtpSelectDate.TabIndex = 2;
            this.dtpSelectDate.ValueChanged += new System.EventHandler(this.dtpSelectDate_ValueChanged);
            
            this.panelDepartment.Controls.Add(this.lblDepartment);
            this.panelDepartment.Controls.Add(this.cmbDepartment);
            this.panelDepartment.Controls.Add(this.lblSelectDate);
            this.panelDepartment.Controls.Add(this.dtpSelectDate);
            
            // =============================================
            // panelWeekNav - Week navigation
            // =============================================
            this.panelWeekNav.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelWeekNav.Location = new System.Drawing.Point(25, 75);
            this.panelWeekNav.Name = "panelWeekNav";
            this.panelWeekNav.Size = new System.Drawing.Size(900, 45);
            this.panelWeekNav.TabIndex = 1;
            this.panelWeekNav.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            
            this.btnPrevWeek.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevWeek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevWeek.FlatAppearance.BorderSize = 0;
            this.btnPrevWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevWeek.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrevWeek.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnPrevWeek.Location = new System.Drawing.Point(280, 8);
            this.btnPrevWeek.Name = "btnPrevWeek";
            this.btnPrevWeek.Size = new System.Drawing.Size(90, 30);
            this.btnPrevWeek.TabIndex = 0;
            this.btnPrevWeek.Text = "◀ Tuần";
            this.btnPrevWeek.Click += new System.EventHandler(this.btnPrevWeek_Click);
            
            this.lblWeekInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblWeekInfo.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblWeekInfo.Location = new System.Drawing.Point(380, 12);
            this.lblWeekInfo.Name = "lblWeekInfo";
            this.lblWeekInfo.Size = new System.Drawing.Size(160, 25);
            this.lblWeekInfo.Text = "30/12 - 05/01/2026";
            this.lblWeekInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            this.btnNextWeek.BackColor = System.Drawing.Color.Transparent;
            this.btnNextWeek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextWeek.FlatAppearance.BorderSize = 0;
            this.btnNextWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextWeek.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNextWeek.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnNextWeek.Location = new System.Drawing.Point(550, 8);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(90, 30);
            this.btnNextWeek.TabIndex = 2;
            this.btnNextWeek.Text = "Tuần ▶";
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            
            this.panelWeekNav.Controls.Add(this.btnPrevWeek);
            this.panelWeekNav.Controls.Add(this.lblWeekInfo);
            this.panelWeekNav.Controls.Add(this.btnNextWeek);
            
            // =============================================
            // panelWeeklyCalendar - 7-day TableLayoutPanel
            // =============================================
            this.panelWeeklyCalendar.BackColor = System.Drawing.Color.White;
            this.panelWeeklyCalendar.ColumnCount = 7;
            this.panelWeeklyCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelWeeklyCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelWeeklyCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelWeeklyCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelWeeklyCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelWeeklyCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelWeeklyCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.panelWeeklyCalendar.Location = new System.Drawing.Point(25, 130);
            this.panelWeeklyCalendar.Name = "panelWeeklyCalendar";
            this.panelWeeklyCalendar.RowCount = 1;
            this.panelWeeklyCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelWeeklyCalendar.Size = new System.Drawing.Size(900, 110);
            this.panelWeeklyCalendar.TabIndex = 2;
            this.panelWeeklyCalendar.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panelWeeklyCalendar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            
            // =============================================
            // panelShiftSelection - Sáng/Chiều buttons
            // =============================================
            this.panelShiftSelection.BackColor = System.Drawing.Color.White;
            this.panelShiftSelection.Location = new System.Drawing.Point(25, 250);
            this.panelShiftSelection.Name = "panelShiftSelection";
            this.panelShiftSelection.Size = new System.Drawing.Size(900, 60);
            this.panelShiftSelection.TabIndex = 3;
            this.panelShiftSelection.Visible = false;
            this.panelShiftSelection.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            
            this.lblShiftTitle.AutoSize = true;
            this.lblShiftTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblShiftTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblShiftTitle.Location = new System.Drawing.Point(0, 18);
            this.lblShiftTitle.Name = "lblShiftTitle";
            this.lblShiftTitle.Text = "Chọn ca khám:";
            
            this.btnMorningShift.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnMorningShift.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMorningShift.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnMorningShift.FlatAppearance.BorderSize = 2;
            this.btnMorningShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMorningShift.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnMorningShift.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnMorningShift.Location = new System.Drawing.Point(130, 10);
            this.btnMorningShift.Name = "btnMorningShift";
            this.btnMorningShift.Size = new System.Drawing.Size(180, 40);
            this.btnMorningShift.TabIndex = 1;
            this.btnMorningShift.Text = "🌅 Ca sáng (7:30-11:30)";
            this.btnMorningShift.Click += new System.EventHandler(this.btnMorningShift_Click);
            
            this.btnAfternoonShift.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnAfternoonShift.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAfternoonShift.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnAfternoonShift.FlatAppearance.BorderSize = 2;
            this.btnAfternoonShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAfternoonShift.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAfternoonShift.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnAfternoonShift.Location = new System.Drawing.Point(330, 10);
            this.btnAfternoonShift.Name = "btnAfternoonShift";
            this.btnAfternoonShift.Size = new System.Drawing.Size(180, 40);
            this.btnAfternoonShift.TabIndex = 2;
            this.btnAfternoonShift.Text = "🌆 Ca chiều (13:30-17:30)";
            this.btnAfternoonShift.Click += new System.EventHandler(this.btnAfternoonShift_Click);
            
            this.panelShiftSelection.Controls.Add(this.lblShiftTitle);
            this.panelShiftSelection.Controls.Add(this.btnMorningShift);
            this.panelShiftSelection.Controls.Add(this.btnAfternoonShift);
            
            // =============================================
            // panelTimeSlots - Time slot buttons
            // =============================================
            this.panelTimeSlots.BackColor = System.Drawing.Color.White;
            this.panelTimeSlots.Location = new System.Drawing.Point(25, 320);
            this.panelTimeSlots.Name = "panelTimeSlots";
            this.panelTimeSlots.Size = new System.Drawing.Size(900, 100);
            this.panelTimeSlots.TabIndex = 4;
            this.panelTimeSlots.Visible = false;
            this.panelTimeSlots.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            
            this.lblTimeSlotsTitle.AutoSize = true;
            this.lblTimeSlotsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTimeSlotsTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTimeSlotsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTimeSlotsTitle.Name = "lblTimeSlotsTitle";
            this.lblTimeSlotsTitle.Text = "Chọn khung giờ:";
            
            this.flowTimeSlots.Location = new System.Drawing.Point(0, 28);
            this.flowTimeSlots.Name = "flowTimeSlots";
            this.flowTimeSlots.Size = new System.Drawing.Size(900, 60);
            this.flowTimeSlots.TabIndex = 1;
            this.flowTimeSlots.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            
            this.panelTimeSlots.Controls.Add(this.lblTimeSlotsTitle);
            this.panelTimeSlots.Controls.Add(this.flowTimeSlots);
            
            // =============================================
            // panelQueueSelection - Queue number + confirm
            // =============================================
            this.panelQueueSelection.BackColor = System.Drawing.Color.White;
            this.panelQueueSelection.Location = new System.Drawing.Point(25, 400);
            this.panelQueueSelection.Name = "panelQueueSelection";
            this.panelQueueSelection.Size = new System.Drawing.Size(900, 250);
            this.panelQueueSelection.TabIndex = 5;
            this.panelQueueSelection.Visible = false;
            this.panelQueueSelection.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            
            this.lblQueueTitle.AutoSize = true;
            this.lblQueueTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblQueueTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblQueueTitle.Location = new System.Drawing.Point(0, 0);
            this.lblQueueTitle.Name = "lblQueueTitle";
            this.lblQueueTitle.Text = "Chọn số thứ tự:";
            
            this.lblSuggestedQueue.AutoSize = true;
            this.lblSuggestedQueue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSuggestedQueue.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.lblSuggestedQueue.Location = new System.Drawing.Point(0, 25);
            this.lblSuggestedQueue.Name = "lblSuggestedQueue";
            this.lblSuggestedQueue.Text = "✓ Đề xuất: STT 1 (nhỏ nhất còn trống)";
            
            this.flowQueueNumbers.Location = new System.Drawing.Point(0, 50);
            this.flowQueueNumbers.Name = "flowQueueNumbers";
            this.flowQueueNumbers.Size = new System.Drawing.Size(700, 50);
            this.flowQueueNumbers.TabIndex = 2;
            
            this.lblSymptoms.AutoSize = true;
            this.lblSymptoms.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSymptoms.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblSymptoms.Location = new System.Drawing.Point(0, 108);
            this.lblSymptoms.Name = "lblSymptoms";
            this.lblSymptoms.Text = "Triệu chứng (tùy chọn):";
            
            this.txtSymptoms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSymptoms.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSymptoms.Location = new System.Drawing.Point(0, 130);
            this.txtSymptoms.Multiline = true;
            this.txtSymptoms.Name = "txtSymptoms";
            this.txtSymptoms.Size = new System.Drawing.Size(400, 45);
            this.txtSymptoms.TabIndex = 3;
            
            this.btnConfirmBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnConfirmBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmBooking.FlatAppearance.BorderSize = 0;
            this.btnConfirmBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmBooking.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmBooking.ForeColor = System.Drawing.Color.White;
            this.btnConfirmBooking.Location = new System.Drawing.Point(580, 130);
            this.btnConfirmBooking.Name = "btnConfirmBooking";
            this.btnConfirmBooking.Size = new System.Drawing.Size(180, 50);
            this.btnConfirmBooking.TabIndex = 4;
            this.btnConfirmBooking.Text = "✓ XÁC NHẬN ĐẶT LỊCH";
            this.btnConfirmBooking.Click += new System.EventHandler(this.btnConfirmBooking_Click);
            
            this.btnCancelBooking.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.btnCancelBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelBooking.FlatAppearance.BorderSize = 0;
            this.btnCancelBooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelBooking.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelBooking.ForeColor = System.Drawing.Color.White;
            this.btnCancelBooking.Location = new System.Drawing.Point(750, 130);
            this.btnCancelBooking.Name = "btnCancelBooking";
            this.btnCancelBooking.Size = new System.Drawing.Size(100, 45);
            this.btnCancelBooking.TabIndex = 5;
            this.btnCancelBooking.Text = "HỦY";
            this.btnCancelBooking.Click += new System.EventHandler(this.btnCancelBooking_Click);
            
            this.panelQueueSelection.Controls.Add(this.lblQueueTitle);
            this.panelQueueSelection.Controls.Add(this.lblSuggestedQueue);
            this.panelQueueSelection.Controls.Add(this.flowQueueNumbers);
            this.panelQueueSelection.Controls.Add(this.lblSymptoms);
            this.panelQueueSelection.Controls.Add(this.txtSymptoms);
            this.panelQueueSelection.Controls.Add(this.btnConfirmBooking);
            this.panelQueueSelection.Controls.Add(this.btnCancelBooking);
            
            // =============================================
            // panelLoading
            // =============================================
            this.panelLoading.BackColor = System.Drawing.Color.FromArgb(200, 255, 255, 255);
            this.panelLoading.Controls.Add(this.lblLoading);
            this.panelLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLoading.Location = new System.Drawing.Point(0, 0);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(950, 600);
            this.panelLoading.TabIndex = 10;
            this.panelLoading.Visible = false;
            
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblLoading.ForeColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.lblLoading.Text = "⏳ Đang tải...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // =============================================
            // Add controls to panelContent
            // =============================================
            this.panelContent.Controls.Add(this.panelQueueSelection);
            this.panelContent.Controls.Add(this.panelTimeSlots);
            this.panelContent.Controls.Add(this.panelShiftSelection);
            this.panelContent.Controls.Add(this.panelWeeklyCalendar);
            this.panelContent.Controls.Add(this.panelWeekNav);
            this.panelContent.Controls.Add(this.panelDepartment);
            
            // =============================================
            // UC_AppointmentBooking
            // =============================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_AppointmentBooking";
            this.Size = new System.Drawing.Size(950, 600);
            
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelDepartment.ResumeLayout(false);
            this.panelDepartment.PerformLayout();
            this.panelWeekNav.ResumeLayout(false);
            this.panelShiftSelection.ResumeLayout(false);
            this.panelShiftSelection.PerformLayout();
            this.panelTimeSlots.ResumeLayout(false);
            this.panelTimeSlots.PerformLayout();
            this.panelQueueSelection.ResumeLayout(false);
            this.panelQueueSelection.PerformLayout();
            this.panelLoading.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelDepartment;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.DateTimePicker dtpSelectDate;
        private System.Windows.Forms.Label lblSelectDate;
        private System.Windows.Forms.Panel panelWeekNav;
        private System.Windows.Forms.Button btnPrevWeek;
        private System.Windows.Forms.Label lblWeekInfo;
        private System.Windows.Forms.Button btnNextWeek;
        private System.Windows.Forms.TableLayoutPanel panelWeeklyCalendar;
        private System.Windows.Forms.Panel panelShiftSelection;
        private System.Windows.Forms.Label lblShiftTitle;
        private System.Windows.Forms.Button btnMorningShift;
        private System.Windows.Forms.Button btnAfternoonShift;
        private System.Windows.Forms.Panel panelTimeSlots;
        private System.Windows.Forms.Label lblTimeSlotsTitle;
        private System.Windows.Forms.FlowLayoutPanel flowTimeSlots;
        private System.Windows.Forms.Panel panelQueueSelection;
        private System.Windows.Forms.Label lblQueueTitle;
        private System.Windows.Forms.Label lblSuggestedQueue;
        private System.Windows.Forms.FlowLayoutPanel flowQueueNumbers;
        private System.Windows.Forms.Label lblSymptoms;
        private System.Windows.Forms.TextBox txtSymptoms;
        private System.Windows.Forms.Button btnConfirmBooking;
        private System.Windows.Forms.Button btnCancelBooking;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label lblLoading;
    }
}
