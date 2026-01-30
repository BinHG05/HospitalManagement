using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagement.Models.Entities;
using HospitalManagement.Presenters.Admin;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Views.UserControls.Admin
{
    public partial class UC_ShiftApproval : UserControl, IShiftApprovalView
    {
        private ShiftApprovalPresenter _presenter;
        private List<int> _selectedScheduleIds = new List<int>();

        public UC_ShiftApproval(int adminUserId)
        {
            InitializeComponent();
            _presenter = new ShiftApprovalPresenter(this, adminUserId);
            SetupControls();
            SetupEvents();
        }

        private void SetupControls()
        {
            // Setup Month ComboBox
            for (int i = 1; i <= 12; i++)
                cmbMonth.Items.Add(i);
            cmbMonth.SelectedItem = DateTime.Now.Month;

            // Setup Year ComboBox
            for (int y = DateTime.Now.Year - 1; y <= DateTime.Now.Year + 1; y++)
                cmbYear.Items.Add(y);
            cmbYear.SelectedItem = DateTime.Now.Year;
        }

        private void SetupEvents()
        {
            this.Load += (s, e) =>
            {
                _presenter.LoadPendingRequests();
                _presenter.LoadShiftQuotaSummary();
                _presenter.LoadDoctorQuotaSummary();
            };

            // Tab change
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                switch (tabControl.SelectedIndex)
                {
                    case 0: _presenter.LoadPendingRequests(); break;
                    case 1: _presenter.LoadShiftQuotaSummary(); break;
                    case 2: _presenter.LoadDoctorQuotaSummary(); break;
                }
            };

            // Date/Month change
            dtpShiftQuota.ValueChanged += (s, e) => _presenter.LoadShiftQuotaSummary();
            cmbMonth.SelectedIndexChanged += (s, e) => _presenter.LoadDoctorQuotaSummary();
            cmbYear.SelectedIndexChanged += (s, e) => _presenter.LoadDoctorQuotaSummary();

            // Safe Grid Configuration via DataBindingComplete
            dgvPending.DataBindingComplete += dgvPending_DataBindingComplete;
            dgvShiftQuota.DataBindingComplete += dgvShiftQuota_DataBindingComplete;
            dgvDoctorQuota.DataBindingComplete += dgvDoctorQuota_DataBindingComplete;

            // Buttons
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click += BtnReject_Click;
            btnApproveAll.Click += BtnApproveAll_Click;

            // Selection
            dgvPending.SelectionChanged += DgvPending_SelectionChanged;
        }

        private void dgvPending_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvPending.DataSource == null || dgvPending.Columns.Count == 0) return;
            
            ConfigureColumn(dgvPending, "ScheduleID", visible: false);
            ConfigureColumn(dgvPending, "DoctorName", headerText: "Bác sĩ");
            ConfigureColumn(dgvPending, "Department", headerText: "Khoa");
            ConfigureColumn(dgvPending, "Date", headerText: "Ngày");
            ConfigureColumn(dgvPending, "DayOfWeek", headerText: "Thứ");
            ConfigureColumn(dgvPending, "ShiftName", headerText: "Ca");
            ConfigureColumn(dgvPending, "Time", headerText: "Giờ");
            ConfigureColumn(dgvPending, "RequestedAt", headerText: "Đăng ký lúc");
        }

        private void dgvShiftQuota_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvShiftQuota.DataSource == null || dgvShiftQuota.Columns.Count == 0) return;
            ConfigureColumn(dgvShiftQuota, "DepartmentName", headerText: "Khoa");
            ConfigureColumn(dgvShiftQuota, "ShiftName", headerText: "Ca");
            ConfigureColumn(dgvShiftQuota, "Registered", headerText: "Đã ĐK");
            ConfigureColumn(dgvShiftQuota, "Min", headerText: "Tối thiểu");
            ConfigureColumn(dgvShiftQuota, "Max", headerText: "Tối đa");
            ConfigureColumn(dgvShiftQuota, "Status", headerText: "Trạng thái");

            foreach (DataGridViewRow row in dgvShiftQuota.Rows)
            {
                var status = row.Cells["Status"]?.Value?.ToString() ?? "";
                if (status.Contains("Thiếu"))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
            }
        }

        private void dgvDoctorQuota_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvDoctorQuota.DataSource == null || dgvDoctorQuota.Columns.Count == 0) return;
            
            // Use BeginInvoke to defer column configuration
            BeginInvoke(new Action(() =>
            {
                try
                {
                    ConfigureColumn(dgvDoctorQuota, "DoctorID", visible: false);
                    ConfigureColumn(dgvDoctorQuota, "Icon", headerText: ""); // Remove width setting
                    ConfigureColumn(dgvDoctorQuota, "DoctorName", headerText: "Bác sĩ");
                    ConfigureColumn(dgvDoctorQuota, "Department", headerText: "Khoa");
                    ConfigureColumn(dgvDoctorQuota, "Approved", headerText: "Đã duyệt");
                    ConfigureColumn(dgvDoctorQuota, "Pending", headerText: "Chờ duyệt");
                    ConfigureColumn(dgvDoctorQuota, "Total", headerText: "Tổng");
                    ConfigureColumn(dgvDoctorQuota, "Min", headerText: "Tối thiểu");
                    ConfigureColumn(dgvDoctorQuota, "Max", headerText: "Tối đa");
                    ConfigureColumn(dgvDoctorQuota, "Remaining", headerText: "Trạng thái");

                    // Set AutoSizeMode for Icon column instead of fixed width
                    if (dgvDoctorQuota.Columns.Contains("Icon"))
                    {
                        dgvDoctorQuota.Columns["Icon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                    foreach (DataGridViewRow row in dgvDoctorQuota.Rows)
                    {
                        if (row.Cells.Count > 0 && dgvDoctorQuota.Columns.Contains("Icon"))
                        {
                            var iconCell = row.Cells["Icon"];
                            if (iconCell?.Value != null)
                            {
                                string icon = iconCell.Value.ToString();
                                if (icon == "🔴")
                                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                                else if (icon == "🟢")
                                    row.DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 200);
                            }
                        }
                    }
                }
                catch
                {
                    // Ignore errors during column configuration
                }
            }));
        }

        private void DgvPending_SelectionChanged(object sender, EventArgs e)
        {
            _selectedScheduleIds.Clear();
            foreach (DataGridViewRow row in dgvPending.SelectedRows)
            {
                if (row.Cells["ScheduleID"]?.Value != null)
                    _selectedScheduleIds.Add((int)row.Cells["ScheduleID"].Value);
            }

            btnApprove.Enabled = _selectedScheduleIds.Count > 0;
            btnReject.Enabled = _selectedScheduleIds.Count > 0;
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (_selectedScheduleIds.Count == 0) return;

            if (_selectedScheduleIds.Count == 1)
            {
                _presenter.ApproveRequest(_selectedScheduleIds[0]);
            }
            else
            {
                var result = MessageBox.Show(
                    $"Duyệt {_selectedScheduleIds.Count} yêu cầu đã chọn?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    _presenter.ApproveMultiple(_selectedScheduleIds);
            }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (_selectedScheduleIds.Count == 0) return;

            // Create simple input dialog
            string reason = "";
            using (var form = new Form())
            {
                form.Text = "Từ chối yêu cầu";
                form.Size = new Size(400, 150);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var lbl = new Label { Text = "Nhập lý do từ chối (tùy chọn):", Location = new Point(15, 15), AutoSize = true };
                var txt = new TextBox { Location = new Point(15, 40), Size = new Size(355, 25) };
                var btnOk = new Button { Text = "OK", Location = new Point(215, 75), Size = new Size(75, 30), DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Hủy", Location = new Point(295, 75), Size = new Size(75, 30), DialogResult = DialogResult.Cancel };

                form.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
                form.AcceptButton = btnOk;
                form.CancelButton = btnCancel;

                if (form.ShowDialog(this) == DialogResult.OK)
                    reason = txt.Text;
                else
                    return;
            }

            foreach (var id in _selectedScheduleIds)
            {
                _presenter.RejectRequest(id, reason);
            }
        }

        private void BtnApproveAll_Click(object sender, EventArgs e)
        {
            var allIds = new List<int>();
            foreach (DataGridViewRow row in dgvPending.Rows)
            {
                if (row.Cells["ScheduleID"]?.Value != null)
                    allIds.Add((int)row.Cells["ScheduleID"].Value);
            }

            if (allIds.Count == 0)
            {
                ShowMessage("Không có yêu cầu nào để duyệt.");
                return;
            }

            var result = MessageBox.Show(
                $"Duyệt tất cả {allIds.Count} yêu cầu?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                _presenter.ApproveMultiple(allIds);
        }

        #region IShiftApprovalView Implementation

        public DateTime SelectedDate => dtpShiftQuota.Value;
        public int? SelectedMonth => cmbMonth.SelectedItem as int?;
        public int? SelectedYear => cmbYear.SelectedItem as int?;

        public void SetPendingRequests(IEnumerable<ShiftRequestInfo> requests)
        {
            // Handle empty list
            if (requests == null || !requests.Any())
            {
                dgvPending.DataSource = null;
                return;
            }

            var displayList = requests.Select(r => new
            {
                ScheduleID = r.ScheduleID,
                DoctorName = r.DoctorName,
                Department = r.Department,
                Date = r.Date.ToString("dd/MM/yyyy"),
                DayOfWeek = r.Date.ToString("dddd"),
                ShiftName = r.ShiftName,
                Time = r.Time,
                RequestedAt = r.RequestedAt?.ToString("dd/MM HH:mm") ?? ""
            }).ToList();

            dgvPending.DataSource = displayList;
        }

        public void SetShiftQuotaSummary(IEnumerable<ShiftQuotaInfo> quotas)
        {
            // Handle empty list
            if (quotas == null || !quotas.Any())
            {
                dgvShiftQuota.DataSource = null;
                return;
            }

            var displayList = quotas.Select(q => new
            {
                q.DepartmentName,
                q.ShiftName,
                Registered = $"{q.CurrentRegistered}/{q.MaxDoctors}",
                Min = q.MinDoctors,
                Max = q.MaxDoctors,
                Status = q.StatusIcon
            }).ToList();

            dgvShiftQuota.DataSource = displayList;
        }

        public void SetDoctorQuotaSummary(IEnumerable<DoctorQuotaInfo> quotas)
        {
            // Handle empty list - avoid null column access
            if (quotas == null || !quotas.Any())
            {
                dgvDoctorQuota.DataSource = null;
                return;
            }

            var displayList = quotas.Select(q => new
            {
                q.DoctorID,
                Icon = q.StatusIcon,
                DoctorName = q.DoctorName,
                Department = q.Department,
                Approved = q.ApprovedShifts,
                Pending = q.PendingShifts,
                Total = q.TotalShifts,
                Min = q.MinRequired,
                Max = q.MaxAllowed,
                Remaining = q.RemainingRequired > 0 ? $"Còn thiếu {q.RemainingRequired}" : q.StatusText
            }).ToList();

            dgvDoctorQuota.DataSource = displayList;
        }

        private void ConfigureColumn(DataGridView dgv, string columnName, string headerText = null, int? width = null, bool? visible = null)
        {
            try
            {
                if (dgv == null || dgv.Columns == null || dgv.Columns.Count == 0) return;
                if (string.IsNullOrEmpty(columnName)) return;
                if (!dgv.Columns.Contains(columnName)) return;

                var col = dgv.Columns[columnName];
                if (col == null) return;

                if (headerText != null) 
                    col.HeaderText = headerText;
                if (width.HasValue && width.Value > 0) 
                    col.Width = width.Value;
                if (visible.HasValue) 
                    col.Visible = visible.Value;
            }
            catch
            {
                // Silently ignore column configuration errors
            }
        }

        public void SetPendingCount(int count)
        {
            lblPendingCount.Text = $"⏳ {count} yêu cầu đang chờ duyệt";
            btnApproveAll.Visible = count > 0;
        }

        public void ShowLoading(bool isLoading)
        {
            Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            this.Enabled = !isLoading;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void RefreshData()
        {
            _presenter.LoadPendingRequests();
            _presenter.LoadShiftQuotaSummary();
            _presenter.LoadDoctorQuotaSummary();
        }

        #endregion
    }
}
