namespace HIMS.Data.DTO.IPPatient
{
    public class RefundOfAdvancesListDto
    {
        public long AdvanceDetailID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public long AdvanceId { get; set; }
        public string AdvanceNo { get; set; }
        public long OPD_IPD_Id { get; set; }
        public long ReasonOfAdvanceId { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal UsedAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public decimal NetBallAmt { get; set; }

    }

}
