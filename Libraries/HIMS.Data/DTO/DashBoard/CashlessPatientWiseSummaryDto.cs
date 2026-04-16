using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.DashBoard
{
    public  class CashlessPatientWiseSummaryDto
    {
        public DateTime? BillDate { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? VisitCompanyName { get; set; }
        public DateTime? VisitDate { get; set; }
        public decimal? BillAmount { get; set; }
        public double? DiscAmount { get; set; }
        public decimal? NetBillAmount { get; set; }
        public decimal? PharSalesAmt { get; set; }
        public decimal? BillPharNetAmount { get; set; }
        public decimal? PaidAmtPaidByPaidGov { get; set; }
        public decimal? BalAmount_ful { get; set; }
        public decimal? Sys_BalanceAmt { get; set; }
        public decimal? PaidByPatient { get; set; }
        public decimal? BalAmount { get; set; }
        public long? GovtCompanyId { get; set; }
        public string? FirstCompanyName { get; set; }
        public decimal? GovtApprovedAmt { get; set; }
        public string? GovtRefNo { get; set; }
        public long? CompanyApprovedId { get; set; }
        public string? SecondCompanyName { get; set; }
        public decimal? CompanyApprovedAmt { get; set; }
        public string? CompRefNo { get; set; }
        public string? BillCount { get; set; }
        public string? PBillNo { get; set; }
        public long? OPIPID { get; set; }
        public string? OPIPType { get; set; }
    }
    public class CashlessCountSummaryDto
    {
        public DateTime VisitDate { get; set; }
        public string Section { get; set; }
        public double TotalCount { get; set; }
        public long SelfCount { get; set; }
        public long CompanyCount { get; set; }
        public long ApprovedCount { get; set; }
        public long PendingCount { get; set; }

    }
    public class CashlessCompanyWiseSummaryDto
    {
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public double PatientCount { get; set; }
        public decimal? DraftBill { get; set; }
        public decimal? FinalAmount { get; set; }
        public decimal? PharmacyAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? RemainingAmount { get; set; }

    }
    public class CashlessPatientBillDto
    {
        public DateTime? BillDate { get; set; }
        public string? PbillNo { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? VisitCompanyName { get; set; }
        public DateTime? VisitDate { get; set; }
        public decimal? BillAmount { get; set; }
        public double? DiscAmount { get; set; }
        public decimal? NetBillAmount { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalAmount { get; set; }
        public long? GovtCompanyId { get; set; }
        public string? FirstCompanyName { get; set; }
        public decimal? GovtApprovedAmt { get; set; }
        public string? GovtRefNo { get; set; }
        public long? CompanyApprovedId { get; set; }
        public string? SecondCompanyName { get; set; }
        public decimal? CompanyApprovedAmt { get; set; }
        public string? CompRefNo { get; set; }
        public long? BillNo { get; set; }
        public string? OPDIPDype { get; set; }


    }

    public class CashlessPatientApprovalPendingListDto
    {
        public long? CompanyId { get; set; }
        public string? PatientType { get; set; }
        public string? CompanyName { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public decimal? NetBillAmount { get; set; }
        public long PendingCount { get; set; }

    }

    public class CashlessMonthlyCompanyWiseSummaryDto
    {
        public string? CompanyName { get; set; }
        public decimal? Jan { get; set; }
        public decimal? Feb { get; set; }
        public string? Mar { get; set; }
        public string? Apr { get; set; }
        public string? May { get; set; }
        public string? Jun { get; set; }
        public string? Jul { get; set; }
        public string? Aug { get; set; }
        public string? Sep { get; set; }
        public string? Oct { get; set; }
        public string? Nov { get; set; }
        public string? Dec { get; set; }

    }

}
