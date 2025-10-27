namespace HIMS.Data.Models
{
    public partial class StockLog
    {
        public long Id { get; set; }
        public long StockId { get; set; }
        public long Qty { get; set; }
        public int OpType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Remark { get; set; }

        public virtual TCurrentStock2322 Stock { get; set; } = null!;
    }
}
