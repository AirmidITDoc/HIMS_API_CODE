using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class SalessModel
    {


        public long SalesId { get; set; }
        //   public string? SalesNo { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }

        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public long? ConcessionReasonId { get; set; }
        public long? ConcessionAuthorizationId { get; set; }
   //     public long? CashCounterId { get; set; }
        public bool? IsSellted { get; set; }
        public bool? IsPrint { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public string? ExternalPatientName { get; set; }
        public string? DoctorName { get; set; }
        public long? StoreId { get; set; }
        public long? IsPrescription { get; set; }
        public long? AddedBy { get; set; }
        public string? CreditReason { get; set; }
        public long? CreditReasonId { get; set; }
    //    public decimal? RefundAmt { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public float? DiscperH { get; set; }
        public bool? IsPurBill { get; set; }
        public bool? IsBillCheck { get; set; }
     //   public bool? IsRefundFlag { get; set; }
        public string? SalesHeadName { get; set; }
        public long? SalesTypeId { get; set; }
        public long? RegId { get; set; }
//        public string? PatientName { get; set; }
 //       public string? RegNo { get; set; }
        public string? ExtMobileNo { get; set; }
        //public decimal? RoundOff { get; set; }
        public string? ExtAddress { get; set; }

        public List<SalesDetailModel> TSalesDetails { get; set; }
    }

    public class CurrentStocksModel
    {
        public int ItemId { get; set; }
        public int IssueQty { get; set; }
        public int IStkId { get; set; }
        public int StoreID { get; set; }
    }
    public class PaymentpharModel
    {
        public long? PaymentId { get; set; }
        public long? BillNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public DateTime? CardDate { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public long? TransactionType { get; set; }
        public string? Remark { get; set; }
        public long? AddBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public int? OPDIPDType { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime? PayTmdate { get; set; }
    }

    public class IPPrescriptionsModel
    {
        public int opipid { get; set; }
        public bool Isclosed { get; set; }
      
    }
    public class SalesDraftHeaderModel
    {
        public int DSalesId { get; set; }
        public bool Isclosed { get; set; }

    }
    public class SaleReqModel
    {
        public SalessModel Sales { get; set; }
        public List<CurrentStocksModel> TCurrentStock { get; set; }
        public PaymentpharModel? Payment { get; set; }
        public IPPrescriptionsModel Prescription { get; set; }
        public SalesDraftHeaderModel SalesDraft { get; set; }

    }
}
