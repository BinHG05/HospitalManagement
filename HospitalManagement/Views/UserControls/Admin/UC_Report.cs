using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using HospitalManagement.Presenters.Admin;
using HospitalManagement.Views.Interfaces.Admin;
using System.Drawing;

namespace HospitalManagement.Views.UserControls.Admin
{
    public partial class UC_Report : UserControl, IReportView
    {
        private ReportPresenter _presenter;

        public UC_Report()
        {
            InitializeComponent();
            _presenter = new ReportPresenter(this);

            // Defaults: This Month
            var now = DateTime.Now;
            dtpFrom.Value = new DateTime(now.Year, now.Month, 1);
            dtpTo.Value = dtpFrom.Value.AddMonths(1).AddDays(-1);

            // Events
            btnFilter.Click += (s, e) => _presenter.LoadData();
            this.Load += (s, e) => _presenter.LoadData();
        }

        public DateTime FromDate => dtpFrom.Value;
        public DateTime ToDate => dtpTo.Value;

        public void SetTotalRevenue(decimal amount)
        {
            lblRevenueVal.Text = string.Format("{0:N0} đ", amount);
        }

        public void SetTotalPatients(int count)
        {
            lblPatientVal.Text = count.ToString();
        }

        public void SetRevenueChartData(IEnumerable<dynamic> data)
        {
            chartRevenue.Series["Doanh thu"].Points.Clear();
            
            foreach (var item in data)
            {
                // Reflection to get properties from dynamic type
                var date = (DateTime)item.GetType().GetProperty("Date").GetValue(item, null);
                var total = (decimal)item.GetType().GetProperty("Total").GetValue(item, null);
                
                chartRevenue.Series["Doanh thu"].Points.AddXY(date.ToString("dd/MM"), total);
            }
            
            chartRevenue.Series["Doanh thu"].Color = Color.FromArgb(46, 204, 113);
            chartRevenue.ChartAreas[0].AxisX.Interval = 1;
            chartRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chartRevenue.Titles.Clear();
            chartRevenue.Titles.Add("Biểu đồ Doanh thu theo ngày");
        }

        public void SetServiceTypeData(decimal consultation, decimal service, decimal medicine)
        {
            chartSources.Series["Nguồn thu"].Points.Clear();

            AddPiePoint("Khám bệnh", consultation);
            AddPiePoint("Dịch vụ", service);
            AddPiePoint("Thuốc", medicine);

            chartSources.Titles.Clear();
            chartSources.Titles.Add("Tỷ trọng Doanh thu");
        }

        private void AddPiePoint(string label, decimal value)
        {
            if (value > 0)
            {
                int i = chartSources.Series["Nguồn thu"].Points.AddY(value);
                chartSources.Series["Nguồn thu"].Points[i].LegendText = label;
                chartSources.Series["Nguồn thu"].Points[i].Label = "#PERCENT";
            }
        }

        public void ShowLoading(bool isLoading)
        {
            Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            btnFilter.Enabled = !isLoading;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
