using HospitalManagement.Presenters.Patient;
using HospitalManagement.Views.Interfaces.Patient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HospitalManagement.Views.UserControls.Patient
{
    public partial class UC_Payment : UserControl, IPaymentView
    {
        private PaymentPresenter _presenter;
        private List<InvoiceDisplayInfo> _invoices;
        private InvoiceDisplayInfo _selectedInvoice;

        public string SelectedStatusFilter => (cmbStatusFilter.SelectedItem as FilterItem)?.Value ?? "all";

        public UC_Payment()
        {
            InitializeComponent();
            InitializeFilters();
            InitializePaymentMethods();
        }

        public void Initialize(int patientId)
        {
            _presenter = new PaymentPresenter(this, patientId);
            _presenter.LoadInvoices();
        }

        private void InitializeFilters()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Tất cả", Value = "all" });
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Chưa thanh toán", Value = "unpaid" });
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Đã thanh toán", Value = "paid" });
            cmbStatusFilter.Items.Add(new FilterItem { Text = "Đã hủy", Value = "cancelled" });
            cmbStatusFilter.SelectedIndex = 0;
        }

        private void InitializePaymentMethods()
        {
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.Add(new FilterItem { Text = "Tiền mặt", Value = "cash" });
            cmbPaymentMethod.Items.Add(new FilterItem { Text = "Chuyển khoản", Value = "bank_transfer" });
            cmbPaymentMethod.Items.Add(new FilterItem { Text = "Thẻ tín dụng", Value = "credit_card" });
            cmbPaymentMethod.Items.Add(new FilterItem { Text = "Ví điện tử", Value = "ewallet" });
            cmbPaymentMethod.SelectedIndex = 0;
        }

        #region IPaymentView Implementation

        public void LoadInvoices(IEnumerable<InvoiceDisplayInfo> invoices)
        {
            _invoices = invoices.ToList();
            dgvInvoices.Rows.Clear();

            foreach (var invoice in _invoices)
            {
                var rowIndex = dgvInvoices.Rows.Add();
                var row = dgvInvoices.Rows[rowIndex];

                row.Cells["colInvoiceNumber"].Value = invoice.InvoiceNumber;
                row.Cells["colDate"].Value = invoice.InvoiceDate?.ToString("dd/MM/yyyy") ?? "-";
                row.Cells["colType"].Value = invoice.PaymentTypeDisplay;
                row.Cells["colAmount"].Value = invoice.FinalAmount.ToString("N0") + " đ";
                row.Cells["colStatus"].Value = invoice.StatusDisplay;
                row.Tag = invoice;

                switch (invoice.InvoiceStatus)
                {
                    case "unpaid":
                        row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(231, 76, 60);
                        row.Cells["colStatus"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        break;
                    case "paid":
                        row.Cells["colStatus"].Style.ForeColor = Color.FromArgb(0, 168, 107);
                        break;
                }
            }

            if (_invoices.Count == 0)
            {
                dgvInvoices.Rows.Add();
                dgvInvoices.Rows[0].Cells["colInvoiceNumber"].Value = "Chưa có hóa đơn";
            }
        }

        public void ShowInvoiceDetails(InvoiceDisplayInfo invoice)
        {
            _selectedInvoice = invoice;

            lblDetailsContent.Text =
                $"📄 Số hóa đơn: {invoice.InvoiceNumber}\n\n" +
                $"📅 Ngày: {invoice.InvoiceDate:dd/MM/yyyy}\n\n" +
                $"📋 Loại: {invoice.PaymentTypeDisplay}\n\n" +
                $"🏥 Khoa: {invoice.DepartmentName ?? "N/A"}\n\n" +
                $"👨‍⚕️ Bác sĩ: {invoice.DoctorName ?? "N/A"}\n\n" +
                $"💰 Tổng tiền: {invoice.TotalAmount:N0} đ\n" +
                $"🏷️ Giảm giá: {invoice.DiscountAmount:N0} đ\n" +
                $"💵 Thành tiền: {invoice.FinalAmount:N0} đ\n\n" +
                $"📊 Trạng thái: {invoice.StatusDisplay}";

            btnPay.Visible = invoice.CanPay;
            cmbPaymentMethod.Visible = invoice.CanPay;
            lblPaymentMethod.Visible = invoice.CanPay;

            panelDetails.Visible = true;
            panelDetails.BringToFront();
            panelDetails.Location = new Point(
                (this.Width - panelDetails.Width) / 2,
                (this.Height - panelDetails.Height) / 2
            );
        }

        public void ShowLoading(bool isLoading)
        {
            panelLoading.Visible = isLoading;
            panelLoading.BringToFront();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panelDetails.Visible = false;
        }

        #endregion

        #region Event Handlers

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_presenter != null)
            {
                _presenter.LoadInvoices(SelectedStatusFilter);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _presenter?.LoadInvoices(SelectedStatusFilter);
        }

        private void dgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var invoice = dgvInvoices.Rows[e.RowIndex].Tag as InvoiceDisplayInfo;
            if (invoice == null) return;

            if (e.ColumnIndex == dgvInvoices.Columns["colActions"].Index)
            {
                ShowInvoiceDetails(invoice);
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (_selectedInvoice == null) return;

            var result = MessageBox.Show(
                $"Xác nhận thanh toán hóa đơn {_selectedInvoice.InvoiceNumber}?\n" +
                $"Số tiền: {_selectedInvoice.FinalAmount:N0} đ",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var paymentMethod = (cmbPaymentMethod.SelectedItem as FilterItem)?.Value ?? "cash";
                _presenter.PayInvoice(_selectedInvoice.InvoiceId, paymentMethod);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panelDetails.Visible = false;
        }

        #endregion

        private class FilterItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
