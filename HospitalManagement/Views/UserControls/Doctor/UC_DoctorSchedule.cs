using HospitalManagement.Models.EF;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Doctor
{
    public partial class UC_DoctorSchedule : UserControl
    {
        private int _doctorId;
        private DateTime _currentWeekStart;

        public UC_DoctorSchedule()
        {
            InitializeComponent();
            InitializeLegend();
            _currentWeekStart = GetWeekStart(DateTime.Today);
        }

        private void InitializeLegend()
        {
            var legendMorning = CreateLegendItem("Ca sáng (7:30-11:30)", Color.FromArgb(59, 130, 246), 20);
            var legendAfternoon = CreateLegendItem("Ca chiều (13:30-17:30)", Color.FromArgb(16, 185, 129), 250);
            var legendOff = CreateLegendItem("Nghỉ", Color.FromArgb(148, 163, 184), 500);

            panelLegend.Controls.Clear();
            panelLegend.Controls.Add(legendMorning);
            panelLegend.Controls.Add(legendAfternoon);
            panelLegend.Controls.Add(legendOff);
        }

        private Panel CreateLegendItem(string text, Color color, int x)
        {
            var panel = new Panel
            {
                Location = new Point(x, 10),
                Size = new Size(200, 30)
            };

            var colorBox = new Panel
            {
                Location = new Point(0, 5),
                Size = new Size(20, 20),
                BackColor = color
            };

            var label = new Label
            {
                AutoSize = true,
                Location = new Point(25, 5),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(15, 23, 42),
                Text = text
            };

            panel.Controls.Add(colorBox);
            panel.Controls.Add(label);
            return panel;
        }

        public void Initialize(int doctorId)
        {
            _doctorId = doctorId;
            LoadWeeklySchedule();
        }

        private DateTime GetWeekStart(DateTime date)
        {
            int diff = (7 + (int)date.DayOfWeek - (int)DayOfWeek.Monday) % 7;
            return date.AddDays(-diff).Date;
        }

        private void LoadWeeklySchedule()
        {
            panelCalendar.SuspendLayout();
            panelCalendar.Controls.Clear();

            // Update week info label
            var weekEnd = _currentWeekStart.AddDays(6);
            lblWeekInfo.Text = $"{_currentWeekStart:dd/MM} - {weekEnd:dd/MM/yyyy}";

            string[] dayNames = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };

            // Add header row (day names)
            for (int col = 0; col < 7; col++)
            {
                var date = _currentWeekStart.AddDays(col);
                var headerPanel = CreateDayHeader(dayNames[col], date);
                panelCalendar.Controls.Add(headerPanel, col, 0);
            }

            // Load schedule data from database
            try
            {
                using (var context = new HospitalDbContext())
                {
                    var schedules = context.DoctorSchedules
                        .Where(s => s.DoctorID == _doctorId &&
                                    s.ScheduleDate >= _currentWeekStart &&
                                    s.ScheduleDate <= weekEnd &&
                                    s.IsActive == true)
                        .Select(s => new
                        {
                            s.ScheduleDate,
                            s.ShiftID,
                            ShiftName = s.Shift.ShiftName,
                            s.AvailableSlots,
                            DepartmentName = s.Department.DepartmentName,
                            BookedCount = context.Appointments.Count(a => 
                                a.ScheduleID == s.ScheduleID && 
                                (a.Status == "pending" || a.Status == "confirmed"))
                        })
                        .ToList();

                    // Add morning shift row (row 1)
                    for (int col = 0; col < 7; col++)
                    {
                        var date = _currentWeekStart.AddDays(col);
                        var morningSchedule = schedules.FirstOrDefault(s => 
                            s.ScheduleDate == date && s.ShiftID == 1);

                        var cell = CreateScheduleCell(morningSchedule, true, date);
                        panelCalendar.Controls.Add(cell, col, 1);
                    }

                    // Add afternoon shift row (row 2)
                    for (int col = 0; col < 7; col++)
                    {
                        var date = _currentWeekStart.AddDays(col);
                        var afternoonSchedule = schedules.FirstOrDefault(s => 
                            s.ScheduleDate == date && s.ShiftID == 2);

                        var cell = CreateScheduleCell(afternoonSchedule, false, date);
                        panelCalendar.Controls.Add(cell, col, 2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lịch làm việc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                panelCalendar.ResumeLayout(true);
            }
        }

        private Panel CreateDayHeader(string dayName, DateTime date)
        {
            bool isToday = date.Date == DateTime.Today;

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = isToday ? Color.FromArgb(59, 130, 246) : Color.FromArgb(241, 245, 249)
            };

            var label = new Label
            {
                Text = $"{dayName}\n{date:dd/MM}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, isToday ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isToday ? Color.White : Color.FromArgb(15, 23, 42)
            };

            panel.Controls.Add(label);
            return panel;
        }

        private Panel CreateScheduleCell(dynamic schedule, bool isMorning, DateTime date)
        {
            bool isPast = date.Date < DateTime.Today;
            bool hasSchedule = schedule != null;

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = isPast ? Color.FromArgb(248, 250, 252) :
                            hasSchedule ? (isMorning ? Color.FromArgb(219, 234, 254) : Color.FromArgb(209, 250, 229)) :
                            Color.White,
                Margin = new Padding(2)
            };

            if (hasSchedule)
            {
                var shiftLabel = new Label
                {
                    Text = isMorning ? "☀️ Ca sáng" : "🌙 Ca chiều",
                    Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                    ForeColor = isMorning ? Color.FromArgb(59, 130, 246) : Color.FromArgb(16, 185, 129),
                    TextAlign = ContentAlignment.TopCenter,
                    Dock = DockStyle.Top,
                    Height = 30
                };

                var deptLabel = new Label
                {
                    Text = schedule.DepartmentName ?? "N/A",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 25
                };

                int booked = schedule.BookedCount;
                int available = schedule.AvailableSlots ?? 0;

                var statsLabel = new Label
                {
                    Text = $"📋 {booked} / {available}",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = booked >= available ? Color.FromArgb(239, 68, 68) : Color.FromArgb(100, 116, 139),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };

                panel.Controls.Add(statsLabel);
                panel.Controls.Add(deptLabel);
                panel.Controls.Add(shiftLabel);
            }
            else if (!isPast)
            {
                var offLabel = new Label
                {
                    Text = "—",
                    Font = new Font("Segoe UI", 14F),
                    ForeColor = Color.FromArgb(148, 163, 184),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                panel.Controls.Add(offLabel);
            }

            return panel;
        }

        #region Event Handlers

        private void btnPrevWeek_Click(object sender, EventArgs e)
        {
            _currentWeekStart = _currentWeekStart.AddDays(-7);
            LoadWeeklySchedule();
        }

        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            _currentWeekStart = _currentWeekStart.AddDays(7);
            LoadWeeklySchedule();
        }

        #endregion
    }
}
