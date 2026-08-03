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
        public List<CashlessRevenueSummary> RevenueSummaries { get; set; }
        public List<CashlessCollectionSummary> CollectionSummaries { get; set; }
    }
    public class CashlessPatientSummary
    {
        public string Section { get; set; }
        public double TotalCount { get; set; }
        public long SelfCount { get; set; }
        public long CompanyCount { get; set; }
        public long ApprovedCount { get; set; }
        public long PendingCount { get; set; }
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

    public class CashlessRevenueSummary
    {
        public string lbl { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BalanceAmount { get; set; }
    }

    public class CashlessCollectionSummary
    {  
        public string lbl { get; set; }
        public decimal CashCollection { get; set; }
        public decimal ChequeCollection { get; set; }
        public decimal CardCollection { get; set; }
        public decimal UPICollection { get; set; }
        public decimal NEFTCollection { get; set; }
        public decimal TotalCollection { get; set; }

    }
}
