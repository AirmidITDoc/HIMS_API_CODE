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
        public string? VisitCompanyName { get; set; }
        public DateTime? VisitDate { get; set; }
        public decimal? BillAmount { get; set; }
        public double? DiscAmount { get; set; }
        public decimal? NetBillAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalAmount { get; set; }
        public long? GovtCompanyId { get; set; }
        public string? FirstCompanyName { get; set; }
        public decimal? GovtApprovedAmt { get; set; }
        public string? GovtRefNo { get; set; }
        public long? CompanyApprovedId { get; set; }
        public string? SecondCompanyName { get; set; }
        public decimal? CompanyApprovedAmt { get; set; }
        public string? CompRefNo { get; set; }
    }
    public class CashlessCountSummaryDto
    {
        public DateTime VisitDate { get; set; }
        public double Count { get; set; }
        public long SentApproval { get; set; }
        public long Approved { get; set; }

    }
    public class CashlessCompanyWiseSummaryDto
    {
        public string? CompanyName { get; set; }
        public double PatientCount { get; set; }
        public double DraftBill { get; set; }
        public double FinalAmount { get; set; }
        public double PharmacyAmount { get; set; }

    }
    public class CashlessPatientBillDto
    {
        public DateTime? BillDate { get; set; }
        public string? PbillNo { get; set; }
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







    }

}
