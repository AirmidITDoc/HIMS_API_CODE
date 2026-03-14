using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class CashlessDashboard
    {
        public List<CashlessPatientSummary> CashlessPatientSummary { get; set; }
        public List<CompanyPatientCount> CompanyPatientCounts { get; set; }
        public List<CompanyBillSummary> CompanyBillSummaries { get; set; }
        public List<CashlessDailyTrend> DailyTrend { get; set; }
    }
    public class CashlessPatientSummary
    {
        public long CashlessPatientCount { get; set; }
        public string lbl { get; set; }
    }

    public class CompanyPatientCount
    {
        public string CompanyName { get; set; }
        public long CashlessPatientCount { get; set; }
    }

    public class CompanyBillSummary
    {
        public string CompanyName { get; set; }
        public long CashlessPatientCount { get; set; }
        public decimal BillAmount { get; set; }
        public double DiscAmount { get; set; }
        public decimal CompDiscAmount { get; set; }
        public decimal NetBillAmount { get; set; }
    }

    public class CashlessDailyTrend
    {
        public string Date { get; set; }
        public long CashlessPatientCount { get; set; }
    }
}
