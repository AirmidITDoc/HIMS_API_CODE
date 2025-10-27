namespace HIMS.Data.Models
{
    public partial class Refund
    {
        public Refund()
        {
            TRefundDetails = new HashSet<TRefundDetail>();
        }

        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long? UnitId { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public byte? TransactionId { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsRefundFlag { get; set; }
        public long? AddBy { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TRefundDetail> TRefundDetails { get; set; }
    }
}
