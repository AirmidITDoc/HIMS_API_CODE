using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class MobilePharmacyDashboard
    {
        public MobileTodaysCollectionSummary CollectionCountSummary { get; set; }
        public MobileTodaysPaymentSummary PaymentCountSummary { get; set; }
        public List<MobilePatientCategoryWiseSummary> PatientCategoryWiseSummary { get; set; }
        public MobileCurrentStockSummary CurrentStockSummary { get; set; }
        public List<MobileTopSellingMedicines> TopSellingMedicines { get; set; }
        public List<MobileExpiringMedicines> ExpiringMedicines { get; set; }
    }

    public class MobileTodaysCollectionSummary
    {
        public int CountPatient { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PendingAmountReceived { get; set; }
        public decimal CreditAmount { get; set; }
        public long RefundAmount { get; set; }
        public long AdvAmount { get; set; }
        public long AdvusedAmount { get; set; }
        public long AdvRefundAmount { get; set; }
        public long UnUsedAdvanceAmount { get; set; }
    }
    public class MobileTodaysPaymentSummary
    {
        public decimal CashPay { get; set; }
        public decimal CardPay { get; set; }
        public decimal ChequePay { get; set; }
        public decimal OnlinePay { get; set; }
        public decimal TDSPay { get; set; }
        public decimal WFPay { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class MobilePatientCategoryWiseSummary
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
    public class  MobileCurrentStockSummary
    {
        public long StoreId { get; set; }
        public decimal MRPTotal { get; set; }
        public decimal PurRateWfTotal { get; set; }
        public decimal LandedTotal { get; set; }
    }

    public class MobileTopSellingMedicines
    {
        public string ItemName { get; set; }
        public double Qty { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class MobileExpiringMedicines
    {
        public double ThisMonthExp { get; set; }
        public double NextMonthExp { get; set; }
        public long AfterNextMonthExp { get; set; }
    }
}
