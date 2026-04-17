namespace HIMS.Services.Pharmacy
{
    public class SalesDraftBillItemListDto
    {
        public long ItemId { get; set; }
        public double? QtyPerDay { get; set; }
        public int BalQty { get; set; }
        public bool? IsBatchRequired { get; set; }
    }
}
