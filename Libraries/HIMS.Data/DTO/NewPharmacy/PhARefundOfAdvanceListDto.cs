namespace HIMS.Services.Pharmacy
{
    public class PhARefundOfAdvanceListDto
    {
        public long AdvanceDetailId { get; set; }
        public string Date { get; set; }
        public long? AdvanceId { get; set; }
        public long OPD_IPD_Id { get; set; }
        public string ReasonOfAdvanceId { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? UsedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? PrevRefundAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public decimal NetBalAmt { get; set; }
      

    }
}
