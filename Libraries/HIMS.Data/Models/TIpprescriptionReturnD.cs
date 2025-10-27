namespace HIMS.Data.Models
{
    public partial class TIpprescriptionReturnD
    {
        public long PresDetailsId { get; set; }
        public long? PresReId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? Qty { get; set; }
        public bool? IsClosed { get; set; }

        public virtual TIpprescriptionReturnH? PresRe { get; set; }
    }
}
