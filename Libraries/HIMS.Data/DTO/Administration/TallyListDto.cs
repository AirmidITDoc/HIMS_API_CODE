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
        public decimal NEFTPayAmount { get; set; }
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
        public decimal NEFTPayAmount { get; set; }
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
        public decimal NEFTPayAmount { get; set; }
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

        public decimal NEFTPayAmount { get; set; }

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

        public decimal NEFTPayAmount { get; set; }

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

        public decimal NEFTPayAmount { get; set; }

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

        public decimal NEFTPayAmount { get; set; }

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
}