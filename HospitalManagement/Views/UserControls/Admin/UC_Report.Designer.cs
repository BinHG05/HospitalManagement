namespace HospitalManagement.Views.UserControls.Admin
{
    partial class UC_Report
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            
            this.tableLayoutPanelMetric = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardRevenue = new System.Windows.Forms.Panel();
            this.lblRevenueVal = new System.Windows.Forms.Label();
            this.lblRevenueTitle = new System.Windows.Forms.Label();
            
            this.pnlCardPatients = new System.Windows.Forms.Panel();
            this.lblPatientVal = new System.Windows.Forms.Label();
            this.lblPatientTitle = new System.Windows.Forms.Label();
            
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartSources = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelCharts = new System.Windows.Forms.TableLayoutPanel();

            this.pnlHeader.SuspendLayout();
            this.tableLayoutPanelMetric.SuspendLayout();
            this.pnlCardRevenue.SuspendLayout();
            this.pnlCardPatients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSources)).BeginInit();
            this.panelCharts.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnFilter);
            this.pnlHeader.Controls.Add(this.lblTo);
            this.pnlHeader.Controls.Add(this.dtpTo);
            this.pnlHeader.Controls.Add(this.dtpFrom);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 60;
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(15);

            // lblTitle
            this.lblTitle.Text = "📊 Báo cáo thống kê";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);

            // dtpFrom
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(300, 18);
            this.dtpFrom.Size = new System.Drawing.Size(120, 25);
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);

            // lblTo
            this.lblTo.Text = "đến";
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(430, 22);

            // dtpTo
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(470, 18);
            this.dtpTo.Size = new System.Drawing.Size(120, 25);
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);

            // btnFilter
            this.btnFilter.Text = "Xem báo cáo";
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFilter.Location = new System.Drawing.Point(610, 15);
            this.btnFilter.Size = new System.Drawing.Size(120, 32);
            this.btnFilter.UseVisualStyleBackColor = false;

            // 
            // tableLayoutPanelMetric
            // 
            this.tableLayoutPanelMetric.ColumnCount = 2;
            this.tableLayoutPanelMetric.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMetric.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMetric.Controls.Add(this.pnlCardRevenue, 0, 0);
            this.tableLayoutPanelMetric.Controls.Add(this.pnlCardPatients, 1, 0);
            this.tableLayoutPanelMetric.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelMetric.Height = 120;
            this.tableLayoutPanelMetric.Padding = new System.Windows.Forms.Padding(10);

            // Card Revenue
            this.pnlCardRevenue.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.pnlCardRevenue.Controls.Add(this.lblRevenueVal);
            this.pnlCardRevenue.Controls.Add(this.lblRevenueTitle);
            this.pnlCardRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardRevenue.Margin = new System.Windows.Forms.Padding(10);
            
            this.lblRevenueTitle.Text = "TỔNG DOANH THU";
            this.lblRevenueTitle.ForeColor = System.Drawing.Color.White;
            this.lblRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRevenueTitle.Location = new System.Drawing.Point(15, 15);
            this.lblRevenueTitle.AutoSize = true;

            this.lblRevenueVal.Text = "0 đ";
            this.lblRevenueVal.ForeColor = System.Drawing.Color.White;
            this.lblRevenueVal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblRevenueVal.Location = new System.Drawing.Point(15, 45);
            this.lblRevenueVal.AutoSize = true;

            // Card Patients
            this.pnlCardPatients.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.pnlCardPatients.Controls.Add(this.lblPatientVal);
            this.pnlCardPatients.Controls.Add(this.lblPatientTitle);
            this.pnlCardPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardPatients.Margin = new System.Windows.Forms.Padding(10);

            this.lblPatientTitle.Text = "TỔNG BỆNH NHÂN";
            this.lblPatientTitle.ForeColor = System.Drawing.Color.White;
            this.lblPatientTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatientTitle.Location = new System.Drawing.Point(15, 15);
            this.lblPatientTitle.AutoSize = true;

            this.lblPatientVal.Text = "0";
            this.lblPatientVal.ForeColor = System.Drawing.Color.White;
            this.lblPatientVal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblPatientVal.Location = new System.Drawing.Point(15, 45);
            this.lblPatientVal.AutoSize = true;

            // 
            // panelCharts
            // 
            this.panelCharts.ColumnCount = 2;
            this.panelCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.panelCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.panelCharts.Controls.Add(this.chartRevenue, 0, 0);
            this.panelCharts.Controls.Add(this.chartSources, 1, 0);
            this.panelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCharts.Padding = new System.Windows.Forms.Padding(10);

            // Chart Revenue
            chartArea1.Name = "ChartArea1";
            this.chartRevenue.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartRevenue.Legends.Add(legend1);
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Doanh thu";
            this.chartRevenue.Series.Add(series1);
            this.chartRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartRevenue.Text = "Biểu đồ doanh thu";

            // Chart Sources
            chartArea2.Name = "ChartArea1";
            this.chartSources.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartSources.Legends.Add(legend2);
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Nguồn thu";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            this.chartSources.Series.Add(series2);
            this.chartSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartSources.Text = "Nguồn thu";

            // Add controls
            this.Controls.Add(this.panelCharts);
            this.Controls.Add(this.tableLayoutPanelMetric);
            this.Controls.Add(this.pnlHeader);
            
            this.Size = new System.Drawing.Size(1000, 700);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tableLayoutPanelMetric.ResumeLayout(false);
            this.pnlCardRevenue.ResumeLayout(false);
            this.pnlCardRevenue.PerformLayout();
            this.pnlCardPatients.ResumeLayout(false);
            this.pnlCardPatients.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSources)).EndInit();
            this.panelCharts.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btnFilter;
        
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMetric;
        private System.Windows.Forms.Panel pnlCardRevenue;
        private System.Windows.Forms.Label lblRevenueVal;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Panel pnlCardPatients;
        private System.Windows.Forms.Label lblPatientVal;
        private System.Windows.Forms.Label lblPatientTitle;
        
        private System.Windows.Forms.TableLayoutPanel panelCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSources;
    }
}
