using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class PharmacyDashboard
    {
        public TodaysCollectionSummary CollectionCountSummary { get; set; }
        public TodaysPaymentSummary PaymentCountSummary { get; set; }
        public List<PatientCategoryWiseSummary> PatientCategoryWiseSummary { get; set; }
        public CurrentStockSummary CurrentStockSummary { get; set; }
        public List<TopSellingMedicines> TopSellingMedicines { get; set; }
        //public List<ExpiringMedicines> ExpiringMedicines { get; set; }
    }

    public class TodaysCollectionSummary
    {
        public int CountPatient { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PaidAmount { get; set; } 
        public decimal CreditAmount { get; set; }
        public long RefundAmount { get; set; }
        public long AdvAmount { get; set; }
        public long AdvusedAmount { get; set; }
        public long AdvRefundAmount { get; set; }
    }
    public class TodaysPaymentSummary
    {
        public int CountPatient { get; set; }
        public decimal CashPay { get; set; }
        public decimal CardPay { get; set; }
        public decimal AdvUsed { get; set; }
        public decimal OnlinePay { get; set; }
        public decimal TDSPay { get; set; }
        public decimal WFPay { get; set; }
    }

    public class PatientCategoryWiseSummary
    {
        public long OPIPType { get; set; }
        public long CountPatient { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public long RefundAmount { get; set; }
    }
    public class CurrentStockSummary
    {
        public long StoreId { get; set; }
        public decimal MRPTotal { get; set; }
        public decimal PurRateWfTotal { get; set; }
        public decimal LandedTotal { get; set; }
    }

    public class TopSellingMedicines
    {
        public string ItemName { get; set; }
        public long Qty { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class ExpiringMedicines
    {
        public double ExpYear { get; set; }
        public double ExpMonth { get; set; }
        public long Cnt { get; set; }
    }
}
