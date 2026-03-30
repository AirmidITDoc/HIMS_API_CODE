using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class LabFinancialDashboard
    {
        public LabTopBox TopBoxes { get; set; }
        public List<HospitalBranch> BranchList { get; set; }
        public List<DepartmentWiseSales> DepartmentWiseSales { get; set; }
        public List<DailySalesTrend> DailySalesTrend { get; set; }
        public List<RefDoctorWiseSales> RefDoctorWiseSales { get; set; }
        public List<CPWiseSales> CPWiseSales { get; set; }
        public List<ExecutiveWiseSales> ExecutiveWiseSales { get; set; }
        public List<RadiologyDailySales> RadiologySales { get; set; }
        public List<DepartmentSummary> DepartmentSummary { get; set; }
    }
    public class LabTopBox
    {
        public long TodayRegistration { get; set; }
        public decimal TodaySales { get; set; }
        public long TodayTotalTests { get; set; }
        public long TodayPendingReports { get; set; }
    }

    public class HospitalBranch
    {
        public long HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string ServerIP { get; set; }
        public string ServerDatabasename { get; set; }
        public string UserName { get; set; }
        public string ServerPassword { get; set; }
        public decimal TodaySale { get; set; }
        public decimal MonthlySale { get; set; }

    }

    public class DepartmentWiseSales
    {
        public string Department { get; set; }
        public long TestCount { get; set; }
        public decimal CenterSale { get; set; }
        public decimal Corporate { get; set; }
        public decimal Digital { get; set; }
        public decimal Referral { get; set; }
        public decimal NetSale { get; set; }
    }


    public class DailySalesTrend
    {
        public DateTime BillDate { get; set; }
        public decimal DailySales { get; set; }
    }

 
    public class RefDoctorWiseSales
    {
        public string RefDoctorname { get; set; }
        public long TotalPatients { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
    }


    public class CPWiseSales
    {
        public string CPName { get; set; }
        public long TotalPatients { get; set; }
        public decimal TotalSales { get; set; }
    }


    public class ExecutiveWiseSales
    {
        public string ExecutiveName { get; set; }
        public decimal Gross { get; set; }
        public double Discount { get; set; }
        public decimal Reversal { get; set; }
        public decimal Net { get; set; }
    }
    public class RadiologyDailySales
    {
        public DateTime RadDate { get; set; }
        public decimal DailyRadiologySale { get; set; }
    }
    public class DepartmentSummary
    {
        public string FilterType { get; set; } 
        public long TestCount { get; set; }
        public decimal CenterSale { get; set; }
        public decimal Corporate { get; set; }
        public decimal Digital { get; set; }
        public decimal Referral { get; set; }
        public decimal NetSale { get; set; }
    }
}
