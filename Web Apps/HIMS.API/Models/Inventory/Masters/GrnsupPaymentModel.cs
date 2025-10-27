namespace HIMS.API.Models.Masters
{
    public class GrnsupPaymentModel
    {

        public long SupPayId { get; set; }
        public DateTime SupPayDate { get; set; }
        public string SupPayTime { get; set; }
        public long? GrnId { get; set; }
        public decimal? CashPayAmt { get; set; }
        public decimal? ChequePayAmt { get; set; }
        public DateTime? CardPayDate { get; set; }
        public DateTime? ChequePayDate { get; set; }
        public string? ChequeBankName { get; set; }
        public string? CardBankName { get; set; }
        public string? CardNo { get; set; }
        public string? ChequeNo { get; set; }
        public string? Remarks { get; set; }
        public long? IsAddedBy { get; set; }
        public long? IsUpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDatetime { get; set; }
        public string? PartyReceiptNo { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime? PayTmdate { get; set; }
        public decimal? CardPayAmt { get; set; }


    }

    public class GRNModels
    {
        public long Grnid { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalAmount { get; set; }


    }
    public class SupPayDetModel
    {
        public long SupPayId { get; set; }
        public long SupGrnId { get; set; }


    }




    public class TGRNSupPayment
    {
        public GrnsupPaymentModel GrnsupPayment { get; set; }
        public List<GRNModels> GRN { get; set; }
        public List<SupPayDetModel> SupPayDet { get; set; }


    }
}