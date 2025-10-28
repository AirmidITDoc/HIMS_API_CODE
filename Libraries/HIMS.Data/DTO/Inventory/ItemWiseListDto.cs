namespace HIMS.Data.DTO.Inventory
{
    public class ItemWiseListDto
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string ConversionFactor { get; set; }
        public double ReceivedQty { get; set; }
        public double Sales_Qty { get; set; }
        public double Current_BalQty { get; set; }
    }
}
