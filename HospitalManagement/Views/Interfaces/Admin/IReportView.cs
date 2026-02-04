using System;
using System.Collections.Generic;

namespace HospitalManagement.Views.Interfaces.Admin
{
    public interface IReportView
    {
        // Inputs
        DateTime FromDate { get; }
        DateTime ToDate { get; }

        // Data Display
        void SetTotalRevenue(decimal amount);
        void SetTotalPatients(int count);
        
        // Charts Data
        void SetRevenueChartData(IEnumerable<dynamic> data); // dynamic or specific DTO
        void SetServiceTypeData(decimal consultation, decimal service, decimal medicine);

        // State
        void ShowLoading(bool isLoading);
        void ShowError(string message);
    }
}
