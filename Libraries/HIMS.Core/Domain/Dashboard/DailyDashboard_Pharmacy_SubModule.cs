using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class DailyDashboard_Pharmacy_SubModule
    {
        public PharmacyRXSummary RXSummary { get; set; }
        public PharmacyWalkingSalesSummary WalkingSales { get; set; }
        public PharmacyDischargeSummary DischargeSummary { get; set; }
        public PharmacyIPIssuedSummary IPIssued { get; set; }

        public PharmacyStockSummary StockSummary { get; set; }
        public PharmacyBillSummary BillSummary { get; set; }

        public List<PharmacyDepartmentWiseSales> DepartmentWiseSales { get; set; }
        public List<PharmacyDoctorWiseSales> DoctorWiseSales { get; set; }
        public List<PharmacyRefDoctorWiseSales> RefDoctorWiseSales { get; set; }

        public List<PharmacyWalkInPrescriptionTrend> WalkInPrescriptionTrend { get; set; }
        public List<PharmacyRevenueTrend> RevenueTrend { get; set; }

        public PharmacyCollectionSummary CollectionSummary { get; set; }
        public PharmacyRevenueSummary RevenueSummary { get; set; }
    }

    public class PharmacyRXSummary
    {
        public long RXClosedToday { get; set; }
        public long RXOpenToday { get; set; }
        public decimal RXClosedDiff { get; set; }
        public decimal RXOpenDiff { get; set; }
    }

    public class PharmacyWalkingSalesSummary
    {
        public long WalkingSalesToday { get; set; }
        public decimal WalkingSalesDiff { get; set; }
    }

    public class PharmacyDischargeSummary
    {
        public long DischargedToday { get; set; }
        public long NonDischargedToday { get; set; }
        public decimal DischargedDiff { get; set; }
        public decimal NonDischargedDiff { get; set; }
    }

    public class PharmacyIPIssuedSummary
    {
        public long IPIssuedToday { get; set; }
        public decimal IPIssuedDiff { get; set; }
    }

    public class PharmacyStockSummary
    {
        public long NearExpiryItemsCount { get; set; }
        public long OutOfStockForOpenPrescriptionCount { get; set; }
    }

    public class PharmacyBillSummary
    {
        public long DiscountApprovedCount { get; set; }
        public long DiscountPendingCount { get; set; }
        public long OutstandingBillsCount { get; set; }
        public long DueBillsCount { get; set; }
        public long CancelledBillsCount { get; set; }
    }

    public class PharmacyDepartmentWiseSales
    {
        public string DepartmentName { get; set; }
        public long BillCount { get; set; }
        public decimal NetAmount { get; set; }
    }

    public class PharmacyDoctorWiseSales
    {
        public string DoctorName { get; set; }
        public long BillCount { get; set; }
        public decimal NetAmount { get; set; }
    }

    public class PharmacyRefDoctorWiseSales
    {
        public string RefDoctorName { get; set; }
        public long BillCount { get; set; }
        public decimal NetAmount { get; set; }
    }

    public class PharmacyWalkInPrescriptionTrend
    {
        public DateTime TrendDate { get; set; }
        public long WalkInCount { get; set; }
        public long PrescriptionClosedCount { get; set; }
        public long PrescriptionOpenCount { get; set; }
    }
    public class PharmacyRevenueTrend
    {
        public DateTime TrendDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
    public class PharmacyCollectionSummary
    {
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal UPIAmount { get; set; }
        public decimal NEFTPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal TotalCollection { get; set; }
    }
    public class PharmacyRevenueSummary
    {
        public decimal TotalAmount { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}
