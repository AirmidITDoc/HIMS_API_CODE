using System;
using System.Collections.Generic;

namespace HIMS.Core.Domain.Dashboard
{
    public class DailyDashBoardOpSubModule
    {
        public List<OpTopCountersModel> OpTopCountersModel { get; set; }
        public List<OpAppointmentCountModel> OpAppointmentCountModel { get; set; }
        public List<OpBillOverviewModel> OpBillOverviewModel { get; set; }
        public List<OpCollectionModel> OpCollectionModel { get; set; }
        public List<RegistrationAgeWiseModel> RegistrationAgeWiseModel { get; set; }
        public List<ConsultingDoctorWiseModel> ConsultingDoctorWiseModel { get; set; }
        public List<ReferralDoctorWiseModel> ReferralDoctorWiseModel { get; set; }
        public List<DepartmentWiseRevenueModel> DepartmentWiseRevenueModel { get; set; }
        public List<RegistrationTrendModel> RegistrationTrendModel { get; set; }
        public List<OpRevenueTrendModel> OpRevenueTrendModel { get; set; }
    }

    public class OpTopCountersModel
    {
        public long Registration { get; set; }
        public long Appointments { get; set; }
        public long AppointmentCancelled { get; set; }
        public long FollowUpVisits { get; set; }
        public long CheckedIn { get; set; }
        public long CheckedOut { get; set; }
        public long Waiting { get; set; }
        public decimal AvgWaitingTimeMinutes { get; set; }
        public decimal RegistrationDiff { get; set; }
        public decimal AppointmentsDiff { get; set; }
        public decimal AppointmentCancelledDiff { get; set; }
        public decimal FollowUpVisitsDiff { get; set; }
        public decimal CheckedInDiff { get; set; }
        public decimal CheckedOutDiff { get; set; }
        public decimal WaitingDiff { get; set; }
        public decimal AvgWaitingTimeDiff { get; set; }
    }

    public class OpAppointmentCountModel
    {
        public long Old { get; set; }
        public long New { get; set; }
        public long Referal { get; set; }
        public long Company { get; set; }
        public decimal OldDiff { get; set; }
        public decimal NewDiff { get; set; }
        public decimal ReferalDiff { get; set; }
        public decimal CompanyDiff { get; set; }
    }

    public class OpBillOverviewModel
    {
        public long OPBillCash { get; set; }
        public long OPBillCredit { get; set; }
        public long OPBillDue { get; set; }
        public long HCPCount { get; set; }
        public decimal Gross { get; set; }
        public decimal Discount { get; set; }
        public decimal Net { get; set; }
        public decimal PaidAmt { get; set; }
        public decimal Outstanding { get; set; }
    }

    public class OpCollectionModel
    {
        public decimal Cash { get; set; }
        public decimal Card { get; set; }
        public decimal Cheque { get; set; }
        public decimal UPI { get; set; }
        public decimal Bank { get; set; }
        public decimal TotalOPCollection { get; set; }
    }

    public class RegistrationAgeWiseModel
    {
        public string? AgeGroup { get; set; }
        public long PatientCount { get; set; }
    }

    public class ConsultingDoctorWiseModel
    {
        public string? Doctor { get; set; }
        public long NewPatients { get; set; }
        public long OldPatients { get; set; }
        public long TotalPatients { get; set; }
    }

    public class ReferralDoctorWiseModel
    {
        public string? Doctor { get; set; }
        public long NewPatients { get; set; }
        public long OldPatients { get; set; }
        public long TotalPatients { get; set; }
    }

    public class DepartmentWiseRevenueModel
    {
        public string? Department { get; set; }
        public long Count { get; set; }
        public decimal Gross { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmt { get; set; }
    }

    public class RegistrationTrendModel
    {
        public DateTime TrendDate { get; set; }
        public long NewPatients { get; set; }
        public long OldPatients { get; set; }
        public long Total { get; set; }
    }

    public class OpRevenueTrendModel
    {
        public DateTime TrendDate { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
    }
}