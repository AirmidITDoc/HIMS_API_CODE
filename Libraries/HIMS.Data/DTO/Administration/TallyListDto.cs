using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class TallyListDto
    {
        public String BillDate { get; set; }
        public string CashCounterName { get; set; }

        public decimal NetPayableAmt { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
       // public decimal NEFTPayAmount { get; set; }
        public decimal PayTMAmount { get; set; }
    }

    public class OPRefundBillListCashCounterDto
    {
        public String BillDate { get; set; }
        public string CashCounterName { get; set; }

        public decimal NetPayableAmt { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
        //public decimal NEFTPayAmount { get; set; }
        public decimal PayTMAmount { get; set; }
    }

    public class IPAdvRefundPatientWisePaymentDto
    {
        public DateTime BillDate { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal AdvanceUsedAmount { get; set; }
        //public decimal NEFTPayAmount { get; set; }
        public decimal PayTMAmount { get; set; }
    }

    public class IPBillListPatientWisePaymentDto
    {
        public string PaymentDate { get; set; }

        public string PatientName { get; set; }

        public string RegNo { get; set; }

        public string IPDNo { get; set; }

        public string RefundNo { get; set; }

        public decimal CashPayAmount { get; set; }

        public decimal ChequePayAmount { get; set; }

        public decimal CardPayAmount { get; set; }

        //public decimal NEFTPayAmount { get; set; }

        public decimal PayTMAmount { get; set; }

        public string Remark { get; set; }
    }

    public class IPAdvPatientWisePaymentDto
    {
        public string AdvDate { get; set; }

        public string PatientName { get; set; }

        public string RegNo { get; set; }

        public string IPDNo { get; set; }

        public string AdvanceNo { get; set; }

        public decimal AdvanceAmount { get; set; }

        public decimal CashPayAmount { get; set; }

        public decimal ChequePayAmount { get; set; }

        public decimal CardPayAmount { get; set; }

       // public decimal NEFTPayAmount { get; set; }

        public decimal PayTMAmount { get; set; }
    }

    public class IPBillListPatientWiseDto
    {
        public string PatientName { get; set; }

        public string RegNo { get; set; }

        public decimal TotalAmt { get; set; }

        public double ConcessionAmt { get; set; }

        public decimal NetPayableAmt { get; set; }

        public string PaymentDate { get; set; }

        public decimal PaidAmount { get; set; }

        public string PBillNo { get; set; }

        public string BillDate { get; set; }

        public string CashCounterName { get; set; }

        public string IPDNo { get; set; }

        public string ConcessionReason { get; set; }
    }
    public class IPBillListCashCounterDto
    {
        public string BillDate { get; set; }

        public string CashCounterName { get; set; }

        public decimal NetPayableAmt { get; set; }

        public decimal CashPayAmount { get; set; }

        public decimal ChequePayAmount { get; set; }

        public decimal CardPayAmount { get; set; }

       // public decimal NEFTPayAmount { get; set; }

        public decimal PayTMAmount { get; set; }

        public decimal AdvanceUsedAmount { get; set; }
    }


    public class IPBillRefundBillListPatientWisePaymentDto
    {
        public string PaymentDate { get; set; }

        public string PatientName { get; set; }

        public string RegNo { get; set; }

        public string IPDNo { get; set; }

        public string RefundNo { get; set; }

        public decimal CashPayAmount { get; set; }

        public decimal ChequePayAmount { get; set; }

        public decimal CardPayAmount { get; set; }

       // public decimal NEFTPayAmount { get; set; }

        public decimal PayTMAmount { get; set; }

        public string ReceiptNo { get; set; }

        public string Remark { get; set; }
    }

    public class PurchaseWiseSupplierDto
    {
        public string SupplierName { get; set; }

        public DateTime GRNDate { get; set; }

        public string GrnNumber { get; set; }

        public string InvoiceNo { get; set; }

        public double CGSTPer { get; set; }
        public decimal CGSTAmt { get; set; }

        public double SGSTPer { get; set; }
        public decimal SGSTAmt { get; set; }

        public double IGSTPer { get; set; }
        public decimal IGSTAmt { get; set; }

        public double VatPercentage { get; set; }
        public decimal VatAmount { get; set; }

       public double MRP { get; set; }

        public decimal PTR { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal CrDrAmount { get; set; }

        public decimal TotalBillAmount { get; set; }
    }

    public class TallyPhar2SalesDto
    {
        public DateTime MDate { get; set; }
        public decimal CashPay { get; set; }
        public string DEBIT { get; set; }
        public string CREDIT { get; set; }
        public string SrNo { get; set; }
    }

    public class TallyPhar2PaymentDto
    {
        public DateTime MDate { get; set; }
        public decimal CashPay { get; set; }
        public string DEBIT { get; set; }
        public string CREDIT { get; set; }
        public string SrNo { get; set; }
    }

    
    public class TallyPhar2SalesReturnDto
    {
        public DateTime MDate { get; set; }
        public decimal CashPay { get; set; }
        public string DEBIT { get; set; }
        public string CREDIT { get; set; }
        public string SrNo { get; set; }
    }

    public class TallyPhar2ReceiptDto
    {
        public DateTime MDate { get; set; }
        public decimal CashPay { get; set; }
        public string DEBIT { get; set; }
        public string CREDIT { get; set; }
        public string SrNo { get; set; }
    }

    public class TallyIPBillListMediforteDto
    {
        public long BillNo { get; set; }
        public DateTime? BillDate { get; set; }

        public long? RegNo { get; set; }
        public string? PatientName { get; set; }

        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }

        public string? PbillNo { get; set; }
        public string? PrintBillNo { get; set; }

        public long? GovtCompanyId { get; set; }

        public string? GovtCompanyName { get; set; }
        public string? GovtRefNo { get; set; }
        public decimal? GovtApprovedAmt { get; set; }

        public string? CompnayCompanyName { get; set; }
        public string? CompRefNo { get; set; }
        public decimal? CompanyApprovedAmt { get; set; }

        public long? InterimOrFinal { get; set; }
    }

    public class TallyIPBillDetailListMediforteDto
    {
        public string Lbl { get; set; }
        public long AdmissionId { get; set; }
        public DateTime BillDate { get; set; }
        public string PBillNo { get; set; }
        public DateTime ChargesDate { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }
        public double ChargesTotalAmt { get; set; }
    }

    public class TallyOPBillListMediforteDto
    {
        public long BillNo { get; set; }
        public DateTime BillDate { get; set; }

        public long RegNo { get; set; }
        public string PatientName { get; set; }

        public decimal TotalAmt { get; set; }
        public double ConcessionAmt { get; set; }
        public decimal NetPayableAmt { get; set; }
        public decimal PaidAmt { get; set; }
        public decimal BalanceAmt { get; set; }
        public decimal AdvanceUsedAmount { get; set; }

        public string PbillNo { get; set; }
        public string PrintBillNo { get; set; }

        public long GovtCompanyId { get; set; }

        public string GovtCompanyName { get; set; }
        public string GovtRefNo { get; set; }
        public decimal GovtApprovedAmt { get; set; }

        public string CompnayCompanyName { get; set; }
        public string CompRefNo { get; set; }
        public decimal CompanyApprovedAmt { get; set; }

        public long? InterimOrFinal { get; set; }
    }

    public class TallyOPBillDetailListMediforteDto
    {
        public string Lbl { get; set; }
        public long AdmissionId { get; set; }
        public DateTime BillDate { get; set; }
        public string PBillNo { get; set; }
        public DateTime ChargesDate { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }
        public double ChargesTotalAmt { get; set; }
        public string DoctorName { get; set; }
    }
}