using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class DailyDashBoardIPSubModule
    {
        public List<TodayvsYesterdayModel> TodayvsYesterdayModel { get; set; }
        public List<IPBillCashCreditModel> IPBillCashCreditModel { get; set; }
        public List<IPCollectionModel> IPCollectionModel { get; set; }
        public List<RevenueCollectionModel> RevenueCollectionModel { get; set; }
        public List<AdmissionAgeWiseModel> AdmissionAgeWiseModel { get; set; }
        public List<CompanyWiseCountModel> CompanyWiseCountModel { get; set; }
        public List<ReferDoctorWiseCountModel> ReferDoctorWiseCountModel { get; set; }
        public List<DoctorWiseCountModel> DoctorWiseCountModel { get; set; }
        public List<DepartmentWiseDischargeRevenueModel> DepartmentWiseDischargeRevenueModel { get; set; }
        public List<WardWiseOccupancyModel> WardWiseOccupancyModel { get; set; }
        public List<DischargeCountTrendModel> DischargeCountTrendModel { get; set; }
        public List<RevenueTrendModel> RevenueTrendModel { get; set; }
        public List<AdmissionTrendModel> AdmissionTrendModel { get; set; }
    }

    public class TodayvsYesterdayModel
    {
        public long TodaysAdmissions { get; set; }
        public long CurrentOccupancy { get; set; }
        public long TodaysDischarge { get; set; }
        public long DischargeClearance { get; set; }
        public long DischargePending { get; set; }
        public long OPTOIP { get; set; }

        //public decimal TodaysAdmissionsDiff { get; set; }
        //public decimal CurrentOccupancyDiff { get; set; }
        //public decimal TodaysDischargeDiff { get; set; }
        //public decimal DischargePendingDiff { get; set; }
        //public decimal OPTOIPDiff { get; set; }
    }

    public class IPBillCashCreditModel
    {
        public long IPBillCash { get; set; }
        public long IPBillCredit { get; set; }
        public long RefundCount { get; set; }
        //public decimal IPBillCashDiff { get; set; }
        //public decimal IPBillCreditDiff { get; set; }
        //public decimal RefundCountDiff { get; set; }
    }

    public class IPCollectionModel
    {
        //public decimal Cash { get; set; }
        //public decimal Card { get; set; }
        //public decimal Cheque { get; set; }
        //public decimal UPI { get; set; }
        //public decimal Bank { get; set; }
        //public decimal TotalIPCollection { get; set; }
    }

    public class RevenueCollectionModel
    {
        //public decimal Gross { get; set; }
        //public decimal Discount { get; set; }
        //public decimal Net { get; set; }
        //public decimal PaidAmount { get; set; }
        //public decimal Outstanding { get; set; }
    }

    public class AdmissionAgeWiseModel
    {
        public string? AgeGroup { get; set; }
        public long PatientCount { get; set; }
    }

    public class CompanyWiseCountModel
    {
        public string? CompanyName { get; set; }
        public long PatientCount { get; set; }
    }

    public class ReferDoctorWiseCountModel
    {
        public string? ReferDoctorName { get; set; }
        public long Count { get; set; }
    }

    public class DoctorWiseCountModel
    {
        public string? DoctorName { get; set; }
        public long Count { get; set; }
    }

    public class DepartmentWiseDischargeRevenueModel
    {
        public string? Department { get; set; }
        public long Count { get; set; }
        public decimal Gross { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmt { get; set; }
    }

    public class WardWiseOccupancyModel
    {
        public string? WardName { get; set; }
        public long TotalBeds { get; set; }
        public long OccupiedBeds { get; set; }
        public long VacantBeds { get; set; }
        public decimal OccupancyPercent { get; set; }
    }

    public class DischargeCountTrendModel
    {
        public DateTime TrendDate { get; set; }
        public long DischargeCount { get; set; }
    }

    public class RevenueTrendModel
    {
        public DateTime TrendDate { get; set; }
        public decimal Revenue { get; set; }
        public double Discount { get; set; }
        public decimal NetRevenue { get; set; }
    }

    public class AdmissionTrendModel
    {
        public DateTime TrendDate { get; set; }
        public long OPtoIPCount { get; set; }
        public long ERtoIPCount { get; set; }
    }
}
