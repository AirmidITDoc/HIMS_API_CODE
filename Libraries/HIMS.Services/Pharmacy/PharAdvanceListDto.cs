namespace HIMS.Services.Pharmacy
{
    public class PharAdvanceListDto
    {
        public long AdvanceDetailId { get; set; }
        public string Date { get; set; }
        public long? AdvanceId { get; set; }
        public long OPD_IPD_Id { get; set; }
        public string Reason { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? UsedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public string AdvanceNo { get; set; }
        public string UserName { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal NEFTPayAmount { get; set; }
        public decimal? PayTmamount { get; set; }
        public long? TransactionType { get; set; }
        public decimal? OnlineAmount { get; set; }
        public decimal? TDSAmount { get; set; }
        public decimal? WFAmount { get; set; }




    }
}
