namespace HIMS.API.Models.Pharmacy
{
    public class ItemModel
    {
        public long ItemId { get; set; }
        public double? Cgst { get; set; }
        public double? Sgst { get; set; }
        public double? Igst { get; set; }
        public string? Hsncode { get; set; }
    }
}
