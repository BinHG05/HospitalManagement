using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters.Patient;
using HospitalManagement.Services.Interfaces;
using HospitalManagement.Views.Interfaces.Patient;
using HospitalManagement.Views.Forms.Patient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Patient
{
    public partial class UC_AppointmentBooking : UserControl, IAppointmentBookingView
    {
        private AppointmentBookingPresenter _presenter;
        private int _selectedDepartmentId;
        private DateTime _selectedDate;
        private int _selectedScheduleId;
        private int _selectedQueueNumber;
        private int _currentMaxPatients = 15; // Fixed 15 per slot
        private string _selectedShift; // "morning" or "afternoon"
        private string _selectedTimeSlot;
        private List<TimeSlotInfo> _loadedSlots = new List<TimeSlotInfo>();
        private int _currentRangeStart = 1;
        private int _currentRangeEnd = 15;
        private string _patientName;
        private PatientProfileInfo _currentProfile;
        private string _selectedRoomNumber;

        public int SelectedDepartmentId => _selectedDepartmentId;
        public DateTime SelectedDate => _selectedDate;
        public int SelectedScheduleId => _selectedScheduleId;
        public int SelectedQueueNumber => _selectedQueueNumber;
        public string Reason => txtSymptoms.Text;

        // Time slot definitions
        private readonly string[] MorningSlots = { "07:30-08:30", "08:30-09:30", "09:30-10:30", "10:30-11:30" };
        private readonly string[] AfternoonSlots = { "13:30-14:30", "14:30-15:30", "15:30-16:30", "16:30-17:30" };

        public UC_AppointmentBooking()
        {
            InitializeComponent();
            dtpSelectDate.MinDate = DateTime.Today;
            dtpSelectDate.MaxDate = DateTime.Today.AddMonths(3); // [NEW] Enforce 3-month rolling window
            dtpSelectDate.Value = DateTime.Today;
        }


        public void Initialize(int patientId)
        {
            _presenter = new AppointmentBookingPresenter(this, patientId);
            _presenter.Initialize();
            _selectedDate = DateTime.Today;
            UpdateWeekLabel();
            LoadWeeklyCalendar();
        }

        #region IAppointmentBookingView Implementation

        public void LoadDepartments(IEnumerable<Departments> departments)
        {
            cmbDepartment.Items.Clear();
            cmbDepartment.Items.Add(new ComboBoxItem { Text = "-- Chọn khoa --", Value = 0 });

            foreach (var dept in departments)
            {
                cmbDepartment.Items.Add(new ComboBoxItem
                {
                    Text = dept.DepartmentName,
                    Value = dept.DepartmentID
                });
            }

            cmbDepartment.SelectedIndex = 0;
            cmbDepartment.DisplayMember = "Text";
        }

        public void ShowWeeklySchedule(IEnumerable<DepartmentScheduleInfo> schedules)
        {
            // Not used in new design - we use LoadWeeklyCalendar instead
        }

        public void ShowTimeSlots(IEnumerable<TimeSlotInfo> slots)
        {
            _loadedSlots = slots.ToList();
            
            // If we have a shift selected, refresh the view
            if (!string.IsNullOrEmpty(_selectedShift))
            {
                ShowTimeSlotsForShift(_selectedShift);
            }
        }

        public void ShowQueueNumbers(IEnumerable<int> bookedNumbers, int suggestedNumber, int maxPatients)
        {
            _currentMaxPatients = 60; // Increased capacity
            
            // Adjust suggested number to be within range
            // If suggestedNumber is < startRange, we need to find the first available in range.
            // But 'suggestedNumber' comes from Service which looks at all schedule.
            // We should re-calculate locally.
            
            var bookedSet = new HashSet<int>(bookedNumbers);
            
            // Find first available in current range
            int localSuggested = -1;
            for (int i = _currentRangeStart; i <= _currentRangeEnd; i++)
            {
                if (!bookedSet.Contains(i))
                {
                    localSuggested = i;
                    break;
                }
            }
            
            _selectedQueueNumber = localSuggested != -1 ? localSuggested : 0;

            flowQueueNumbers.Controls.Clear();
            if (localSuggested != -1)
            {
                lblSuggestedQueue.Text = $"✓ Hệ thống đề xuất: STT {localSuggested} (nhỏ nhất trong khung giờ này)";
                lblSuggestedQueue.ForeColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                lblSuggestedQueue.Text = "⚠️ Khung giờ này đã hết số.";
                lblSuggestedQueue.ForeColor = Color.Red;
            }

            for (int i = _currentRangeStart; i <= _currentRangeEnd; i++)
            {
                var btn = CreateQueueButton(i, bookedSet.Contains(i), i == localSuggested);
                flowQueueNumbers.Controls.Add(btn);
            }

            panelQueueSelection.Visible = true;
        }

        public void ShowLoading(bool isLoading)
        {
            panelLoading.Visible = isLoading;
            panelLoading.BringToFront();
        }

        public void ShowPaymentPrompt(int appointmentId, string amount)
        {
            MessageBox.Show(
                $"Đặt lịch khám thành công!\n" +
                $"Hệ thống sẽ chuyển bạn đến mục Thanh toán để hoàn tất việc giữ chỗ.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ClearSelection();

            // Find dashboard and switch
            Form parent = this.FindForm();
            if (parent is PatientDashboard dashboard)
            {
                dashboard.SwitchToSection("Thanh toán");
            }
        }

        public void UpdatePatientProfile(PatientProfileInfo profile)
        {
            _currentProfile = profile;
            _patientName = profile?.FullName;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ClearSelection()
        {
            _selectedScheduleId = 0;
            _selectedQueueNumber = 0;
            _selectedShift = null;
            _selectedTimeSlot = null;
            txtSymptoms.Text = "";
            cmbDepartment.SelectedIndex = 0;
            panelShiftSelection.Visible = false;
            panelTimeSlots.Visible = false;
            panelQueueSelection.Visible = false;
            LoadWeeklyCalendar();
        }

        public void GoToTimeSlotSelection()
        {
            panelShiftSelection.Visible = true;
        }

        public void GoToQueueSelection()
        {
            panelQueueSelection.Visible = true;
        }

        public void GoBackToWeeklyView()
        {
            panelShiftSelection.Visible = false;
            panelTimeSlots.Visible = false;
            panelQueueSelection.Visible = false;
        }

        #endregion

        #region Weekly Calendar

        private void LoadWeeklyCalendar()
        {
            panelWeeklyCalendar.SuspendLayout();
            panelWeeklyCalendar.Controls.Clear();

            var weekStart = _presenter?.CurrentWeekStart ?? GetCurrentWeekStart();
            string[] dayNames = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };

            for (int i = 0; i < 7; i++)
            {
                var date = weekStart.AddDays(i);
                var dayCard = CreateDayCard(date, dayNames[i]);
                panelWeeklyCalendar.Controls.Add(dayCard, i, 0);
            }
            
            panelWeeklyCalendar.ResumeLayout(true);
        }

        private DateTime GetCurrentWeekStart()
        {
            var today = DateTime.Today;
            int diff = (7 + (int)today.DayOfWeek - (int)DayOfWeek.Monday) % 7;
            return today.AddDays(-diff);
        }

        private Panel CreateDayCard(DateTime date, string dayName)
        {
            bool isPast = date.Date < DateTime.Today;
            bool isSelected = date.Date == _selectedDate.Date;
            bool isToday = date.Date == DateTime.Today;

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(2),
                Tag = date
            };
            
            // Enable double buffering via reflection for smoother rendering
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, panel, new object[] { true });

            var lblDay = new Label
            {
                Text = dayName,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 28,
                Name = "lblDay"
            };

            var lblDate = new Label
            {
                Text = date.ToString("dd/MM"),
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 25,
                Name = "lblDate"
            };

            var lblStatus = new Label
            {
                Text = isPast ? "Đã qua" : (isToday ? "Hôm nay" : "Có lịch"),
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Name = "lblStatus"
            };

            panel.Controls.Add(lblStatus);
            panel.Controls.Add(lblDate);
            panel.Controls.Add(lblDay);
            
            // Apply initial appearance
            ApplyDayCardAppearance(panel, isPast, isSelected, isToday);

            if (!isPast)
            {
                EventHandler onClick = (s, e) => SelectDay(date);
                panel.Click += onClick;
                lblDay.Click += onClick;
                lblDate.Click += onClick;
                lblStatus.Click += onClick;
            }

            return panel;
        }

        private void SelectDay(DateTime date)
        {
            var previousDate = _selectedDate;
            _selectedDate = date;
            
            // Update DateTimePicker without triggering ValueChanged event loop
            if (dtpSelectDate.Value.Date != date.Date)
            {
                dtpSelectDate.ValueChanged -= dtpSelectDate_ValueChanged;
                dtpSelectDate.Value = date;
                dtpSelectDate.ValueChanged += dtpSelectDate_ValueChanged;
            }
            
            // Update only the affected cards (previous and new selection)
            UpdateCalendarSelection(previousDate, date);
            
            OnDaySelected();
        }

        private void UpdateCalendarSelection(DateTime previousDate, DateTime newDate)
        {
            foreach (Control ctrl in panelWeeklyCalendar.Controls)
            {
                if (ctrl is Panel panel && panel.Tag is DateTime cardDate)
                {
                    bool isPast = cardDate.Date < DateTime.Today;
                    bool isSelected = cardDate.Date == newDate.Date;
                    bool isToday = cardDate.Date == DateTime.Today;
                    
                    // Only update if this is the old or new selection
                    if (cardDate.Date == previousDate.Date || cardDate.Date == newDate.Date)
                    {
                        ApplyDayCardAppearance(panel, isPast, isSelected, isToday);
                    }
                }
            }
        }

        private void ApplyDayCardAppearance(Panel panel, bool isPast, bool isSelected, bool isToday)
        {
            // Set panel appearance
            panel.BackColor = isPast ? Color.FromArgb(241, 245, 249) :
                              isSelected ? Color.FromArgb(59, 130, 246) :
                              isToday ? Color.FromArgb(16, 185, 129) : Color.White;
            panel.Cursor = isPast ? Cursors.No : Cursors.Hand;
            
            // Set text colors
            Color textColor = isPast ? Color.FromArgb(148, 163, 184) :
                              (isSelected || isToday) ? Color.White : Color.FromArgb(15, 23, 42);
            Color subTextColor = isPast ? Color.FromArgb(148, 163, 184) :
                                 (isSelected || isToday) ? Color.White : Color.FromArgb(100, 116, 139);
            Color statusColor = isPast ? Color.FromArgb(148, 163, 184) :
                                (isSelected || isToday) ? Color.White : Color.FromArgb(16, 185, 129);
            
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl.Name == "lblDay") ctrl.ForeColor = textColor;
                else if (ctrl.Name == "lblDate") ctrl.ForeColor = subTextColor;
                else if (ctrl.Name == "lblStatus") ctrl.ForeColor = statusColor;
            }
        }

        private void OnDaySelected()
        {
            if (_selectedDepartmentId > 0)
            {
                panelShiftSelection.Visible = true;
                panelTimeSlots.Visible = false;
                panelQueueSelection.Visible = false;
                ResetShiftSelection();
            }
        }

        private void ResetShiftSelection()
        {
            btnMorningShift.BackColor = Color.FromArgb(241, 245, 249);
            btnMorningShift.ForeColor = Color.FromArgb(15, 23, 42);
            btnAfternoonShift.BackColor = Color.FromArgb(241, 245, 249);
            btnAfternoonShift.ForeColor = Color.FromArgb(15, 23, 42);
            _selectedShift = null;
        }

        #endregion

        #region Shift & Time Slot Selection

        private void btnMorningShift_Click(object sender, EventArgs e)
        {
            SelectShift("morning");
            ShowTimeSlotsForShift("morning");
        }

        private void btnAfternoonShift_Click(object sender, EventArgs e)
        {
            SelectShift("afternoon");
            ShowTimeSlotsForShift("afternoon");
        }

        private void SelectShift(string shift)
        {
            _selectedShift = shift;
            
            btnMorningShift.BackColor = shift == "morning" ? Color.FromArgb(59, 130, 246) : Color.FromArgb(241, 245, 249);
            btnMorningShift.ForeColor = shift == "morning" ? Color.White : Color.FromArgb(15, 23, 42);
            
            btnAfternoonShift.BackColor = shift == "afternoon" ? Color.FromArgb(59, 130, 246) : Color.FromArgb(241, 245, 249);
            btnAfternoonShift.ForeColor = shift == "afternoon" ? Color.White : Color.FromArgb(15, 23, 42);
        }

        private void ShowTimeSlotsForShift(string shift)
        {
            flowTimeSlots.Controls.Clear();
            btnConfirmBooking.Enabled = false; 
            btnConfirmBooking.BackColor = Color.Gray;

            // Đảm bảo panel hiển thị trước khi kiểm tra schedule
            panelTimeSlots.Visible = true;
            panelQueueSelection.Visible = false;

            string[] displaySlots = shift == "morning" ? MorningSlots : AfternoonSlots;
            bool hasAnyAvailable = false;

            for (int i = 0; i < displaySlots.Length; i++)
            {
                string slotText = displaySlots[i];
                TimeSpan slotStartTime = TimeSpan.Parse(slotText.Split('-')[0]);

                // Tìm lịch khám từ DB bao phủ khung giờ này
                var shiftSchedule = _loadedSlots.FirstOrDefault(s => 
                    s.StartTime <= slotStartTime && s.EndTime > slotStartTime
                );

                if (shiftSchedule == null) continue;

                hasAnyAvailable = true;
                
                var btn = new Button
                {
                    Text = slotText,
                    Size = new Size(130, 35),
                    Font = new Font("Segoe UI", 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(15, 23, 42),
                    Cursor = Cursors.Hand,
                    Margin = new Padding(5, 0, 5, 0),
                    Tag = new Tuple<TimeSlotInfo, int>(shiftSchedule, i) // Store Schedule AND Index
                };
                
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                btn.FlatAppearance.BorderSize = 1;
                btn.Click += TimeSlotButton_Click;
                
                flowTimeSlots.Controls.Add(btn);
            }

            if (!hasAnyAvailable)
            {
                var lblMs = new Label 
                { 
                    Text = "Không có lịch khám nào trong ca này.", 
                    AutoSize = true, 
                    ForeColor = Color.Red,
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    Padding = new Padding(10)
                };
                flowTimeSlots.Controls.Add(lblMs);
            }
            
            // Auto scroll to time slots
            panelContent.VerticalScroll.Value = Math.Min(panelContent.VerticalScroll.Maximum, 320);
            UpdateBookingSummary();
        }

        private void TimeSlotButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            var tagData = btn.Tag as Tuple<TimeSlotInfo, int>;
            if (tagData == null) return;

            var slotInfo = tagData.Item1;
            int slotIndex = tagData.Item2;

            _selectedTimeSlot = btn.Text; // "07:30-08:30"
            _selectedScheduleId = slotInfo.ScheduleId;
            _selectedRoomNumber = slotInfo.RoomNumber;

            // Highlight selected
            foreach (Control ctrl in flowTimeSlots.Controls)
            {
                if (ctrl is Button b)
                {
                    b.BackColor = b == btn ? Color.FromArgb(59, 130, 246) : Color.FromArgb(241, 245, 249);
                    b.ForeColor = b == btn ? Color.White : Color.FromArgb(15, 23, 42);
                }
            }

            // Load queue numbers
            // Calculate range based on index
            int startRange = (slotIndex * 15) + 1;
            int endRange = (slotIndex + 1) * 15;

            lblQueueTitle.Text = $"Chọn số thứ tự - {_selectedTimeSlot} (STT {startRange}-{endRange})";
            
            // Pass the range to the presenter (or handle filtering in View)
            // Ideally, we fetch ALL for the schedule, then Filter here.
            // But LoadQueueNumbers calls Presenter -> Service.
            // Hack: We'll modify ShowQueueNumbers to take a range, or filter here?
            // Better: Store the range and let ShowQueueNumbers use it? 
            // Or just fetch all and filter in ShowQueueNumbers.
            
            _currentRangeStart = startRange;
            _currentRangeEnd = endRange;
            
            _presenter.LoadQueueNumbers(_selectedScheduleId, _selectedDate, 60); // Load up to max context
            
            // Still disable confirm
            btnConfirmBooking.Enabled = false;
            btnConfirmBooking.BackColor = Color.Gray;
        }
        
        // ... (Queue Button Creation omitted for brevity, logic remains same) ...

        private Button CreateQueueButton(int number, bool isBooked, bool isSuggested)
        {
            var btn = new Button
            {
                Size = new Size(42, 42),
                Text = number.ToString(),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(3),
                Tag = number
            };

            btn.FlatAppearance.BorderSize = 1;

            if (isBooked)
            {
                btn.BackColor = Color.FromArgb(239, 68, 68);
                btn.ForeColor = Color.White;
                btn.Enabled = false;
                btn.Cursor = Cursors.No;
            }
            else if (isSuggested)
            {
                btn.BackColor = Color.FromArgb(16, 185, 129);
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(15, 23, 42);
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                btn.Cursor = Cursors.Hand;
            }

            if (!isBooked)
            {
                btn.Click += (s, e) => OnQueueNumberSelected(number);
            }

            return btn;
        }

        private void OnQueueNumberSelected(int number)
        {
            _selectedQueueNumber = number;

            foreach (Control ctrl in flowQueueNumbers.Controls)
            {
                if (ctrl is Button btn && btn.Enabled)
                {
                    int btnNumber = (int)btn.Tag;
                    if (btnNumber == number)
                    {
                        btn.BackColor = Color.FromArgb(16, 185, 129);
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.BackColor = Color.White;
                        btn.ForeColor = Color.FromArgb(15, 23, 42);
                    }
                }
            }
            
            // Enable Confirm Button
            btnConfirmBooking.Enabled = true;
            btnConfirmBooking.BackColor = Color.FromArgb(16, 185, 129); // Green
            
            UpdateBookingSummary();
        }

        private void UpdateBookingSummary()
        {
            if (string.IsNullOrEmpty(_selectedShift) && string.IsNullOrEmpty(_selectedTimeSlot)) return;

            string info = $"Khoa: {cmbDepartment.Text} | Ngày: {_selectedDate:dd/MM/yyyy}";
            if (!string.IsNullOrEmpty(_selectedTimeSlot)) info += $" | Giờ: {_selectedTimeSlot}";
            if (_selectedQueueNumber > 0) info += $" | STT: {_selectedQueueNumber}";

            lblQueueTitle.Text = $"Thông tin đặt lịch: {info}";
            
            // Highlight the deadline or immediate payment requirement
            if (_selectedDate.Date == DateTime.Today)
            {
                lblSuggestedQueue.Text = $"⚠️ LƯU Ý: Lịch khám hôm nay yêu cầu thanh toán NGAY LẬP TỨC để xác nhận.";
                lblSuggestedQueue.ForeColor = Color.FromArgb(220, 38, 38);
            }
            else
            {
                lblSuggestedQueue.Text = $"⚠️ LƯU Ý: Bạn cần thanh toán trước 19:30 tối hôm nay để giữ lịch hẹn này.";
                lblSuggestedQueue.ForeColor = Color.FromArgb(220, 38, 38);
            }
            lblSuggestedQueue.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        #endregion

        #region Event Handlers

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartment.SelectedItem is ComboBoxItem item && item.Value > 0)
            {
                _selectedDepartmentId = item.Value;
                LoadWeeklyCalendar();
                
                // If date already selected, show shift selection and load slots
                if (_selectedDate >= DateTime.Today)
                {
                    _presenter.LoadTimeSlots(_selectedDepartmentId, _selectedDate);
                    panelShiftSelection.Visible = true;
                    panelTimeSlots.Visible = false;
                    panelQueueSelection.Visible = false;
                    ResetShiftSelection();
                }
            }
            else
            {
                _selectedDepartmentId = 0;
                panelShiftSelection.Visible = false;
                panelTimeSlots.Visible = false;
                panelQueueSelection.Visible = false;
            }
        }

        private void dtpSelectDate_ValueChanged(object sender, EventArgs e)
        {
            _selectedDate = dtpSelectDate.Value.Date;
            
            // Update week if date is in different week
            var weekStart = GetCurrentWeekStart();
            if (_presenter != null)
            {
                _presenter.NavigateToWeekOf(_selectedDate);
            }
            
            UpdateWeekLabel();
            LoadWeeklyCalendar();
            
            if (_selectedDepartmentId > 0)
            {
                OnDaySelected();
            }
        }

        private void btnPrevWeek_Click(object sender, EventArgs e)
        {
            // Only allow if previous week contains future dates
            var prevWeekStart = _presenter.CurrentWeekStart.AddDays(-7);
            if (prevWeekStart.AddDays(6) >= DateTime.Today)
            {
                _presenter.NavigateToPreviousWeek();
                UpdateWeekLabel();
                LoadWeeklyCalendar();
            }
        }

        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            _presenter.NavigateToNextWeek();
            UpdateWeekLabel();
            LoadWeeklyCalendar();
        }

        private void UpdateWeekLabel()
        {
            if (_presenter != null)
            {
                var weekStart = _presenter.CurrentWeekStart;
                var weekEnd = weekStart.AddDays(6);
                lblWeekInfo.Text = $"{weekStart:dd/MM} - {weekEnd:dd/MM/yyyy}";
                
                // Disable prev week button if current week contains only past dates
                var canGoPrev = weekStart.AddDays(-1) >= DateTime.Today || weekStart > DateTime.Today;
                btnPrevWeek.Enabled = weekStart > DateTime.Today;
                btnPrevWeek.ForeColor = btnPrevWeek.Enabled ? Color.FromArgb(59, 130, 246) : Color.FromArgb(148, 163, 184);
            }
        }

        private void btnConfirmBooking_Click(object sender, EventArgs e)
        {
            if (_selectedDepartmentId == 0)
            {
                ShowError("Vui lòng chọn khoa khám.");
                return;
            }

            if (_selectedDate < DateTime.Today)
            {
                ShowError("Không thể đặt lịch cho ngày trong quá khứ.");
                return;
            }

            if (string.IsNullOrEmpty(_selectedShift))
            {
                ShowError("Vui lòng chọn ca khám.");
                return;
            }

            if (string.IsNullOrEmpty(_selectedTimeSlot))
            {
                ShowError("Vui lòng chọn khung giờ.");
                return;
            }

            if (_selectedQueueNumber <= 0)
            {
                ShowError("Vui lòng chọn số thứ tự.");
                return;
            }

            // MÀN HÌNH XÁC NHẬN MỚI
            using (var formConfirm = new Form_BookingConfirmation(
                _currentProfile, 
                cmbDepartment.Text, 
                _selectedDate, 
                _selectedTimeSlot, 
                _selectedQueueNumber,
                _selectedRoomNumber))
            {
                var result = formConfirm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _presenter.BookAppointment();
                }
                else if (result == DialogResult.Retry)
                {
                    // Người dùng vừa cập nhật hồ sơ, tải lại thông tin và mở lại form xác nhận
                    _presenter.Initialize(); // Để nạp lại profile mới
                    btnConfirmBooking_Click(sender, e);
                }
            }
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        #endregion

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
