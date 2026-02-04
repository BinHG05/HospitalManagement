using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Models.EF;
using HospitalManagement.Views.Interfaces.Admin;

namespace HospitalManagement.Presenters.Admin
{
    public class ReportPresenter
    {
        private readonly IReportView _view;

        public ReportPresenter(IReportView view)
        {
            _view = view;
        }

        public void LoadData()
        {
            _view.ShowLoading(true);
            try
            {
                var fromDate = _view.FromDate.Date;
                var toDate = _view.ToDate.Date.AddDays(1).AddTicks(-1); // End of day

                using (var context = new HospitalDbContext())
                {
                    // 1. Total Revenue Statistics
                    var payments = context.Payments
                        .Where(p => p.PaymentDate >= fromDate && p.PaymentDate <= toDate && p.PaymentStatus == "completed")
                        .ToList();

                    decimal totalRevenue = payments.Sum(p => p.Amount);
                    _view.SetTotalRevenue(totalRevenue);

                    // Breakdown by Type
                    // Breakdown by Type
                    decimal consultationRev = payments.Where(p => p.PaymentType.Equals("Examination", StringComparison.OrdinalIgnoreCase) || p.PaymentType.Equals("consultation", StringComparison.OrdinalIgnoreCase)).Sum(p => p.Amount);
                    decimal serviceRev = payments.Where(p => p.PaymentType.Equals("Service", StringComparison.OrdinalIgnoreCase)).Sum(p => p.Amount);
                    decimal medicineRev = payments.Where(p => p.PaymentType.Equals("Medicine", StringComparison.OrdinalIgnoreCase)).Sum(p => p.Amount);

                    _view.SetServiceTypeData(consultationRev, serviceRev, medicineRev);

                    // Daily Revenue for Chart
                    var revenueByDay = payments
                        .GroupBy(p => p.PaymentDate.Value.Date)
                        .Select(g => new { Date = g.Key, Total = g.Sum(x => x.Amount) })
                        .OrderBy(x => x.Date)
                        .ToList();

                    _view.SetRevenueChartData(revenueByDay);

                    // 2. Patient Statistics (Unique visits per day)
                    // Count unique ExaminationIDs in specific date range
                    var patientCount = context.Examinations
                        .Count(e => e.ExaminationDate >= fromDate && e.ExaminationDate <= toDate);
                    
                    _view.SetTotalPatients(patientCount);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Lỗi tải báo cáo: " + ex.Message);
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }
    }
}
