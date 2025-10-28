namespace HIMS.Data.Models
{
    public partial class GeTTPrescriptionItemDet
    {
        public long? OpdIpdIp { get; set; }
        public long ItemId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? BalQty { get; set; }
        public bool? IsBatchRequired { get; set; }
        public string? ItemName { get; set; }
    }
}
