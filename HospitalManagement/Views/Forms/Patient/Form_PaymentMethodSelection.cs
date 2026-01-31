using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagement.Views.Forms.Patient
{
    public partial class Form_PaymentMethodSelection : Form
    {
        public string SelectedMethod { get; private set; }

        public Form_PaymentMethodSelection()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Chọn phương thức thanh toán";
            this.Size = new Size(400, 480);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(15, 23, 42)
            };

            Label lblTitle = new Label
            {
                Text = "VUI LÒNG CHỌN PHƯƠNG THỨC",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            pnlHeader.Controls.Add(lblTitle);

            FlowLayoutPanel flowMethods = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 20, 30, 30),
                AutoScroll = false
            };

            AddPaymentButton(flowMethods, "💰 Tiền mặt", "Tiền mặt");
            AddPaymentButton(flowMethods, "🏦 Chuyển khoản", "Chuyển khoản");
            AddPaymentButton(flowMethods, "💳 Thẻ tín dụng", "Thẻ tín dụng");
            AddPaymentButton(flowMethods, "📱 Ví điện tử", "Ví điện tử");

            Button btnCancel = new Button
            {
                Text = "HỦY BỎ",
                Size = new Size(340, 45),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(100, 116, 139),
                Margin = new Padding(0, 10, 0, 0),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            flowMethods.Controls.Add(btnCancel);

            this.Controls.Add(flowMethods);
            this.Controls.Add(pnlHeader);
        }

        private void AddPaymentButton(FlowLayoutPanel container, string text, string value)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(340, 60),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = Color.FromArgb(15, 23, 42),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Margin = new Padding(0, 5, 0, 5),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            
            btn.MouseEnter += (s, e) => {
                btn.BackColor = Color.FromArgb(59, 130, 246);
                btn.ForeColor = Color.White;
            };
            btn.MouseLeave += (s, e) => {
                btn.BackColor = Color.FromArgb(241, 245, 249);
                btn.ForeColor = Color.FromArgb(15, 23, 42);
            };

            btn.Click += (s, e) => {
                this.SelectedMethod = value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            container.Controls.Add(btn);
        }
    }
}
