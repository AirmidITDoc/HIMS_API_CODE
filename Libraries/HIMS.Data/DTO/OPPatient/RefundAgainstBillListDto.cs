namespace HIMS.Data.DTO.OPPatient
{
    public class RefundAgainstBillListDto
    {
        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public bool? Opdipdtype { get; set; }
        public long? Opdipdid { get; set; }
        public decimal? RefundAmount { get; set; }
        public long PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public string? Remark { get; set; }
        public string? PaymentRemark { get; set; }
        public long? TransactionId { get; set; }
        public long? TransactionType { get; set; }
        public long? BillId { get; set; }

    }
}
