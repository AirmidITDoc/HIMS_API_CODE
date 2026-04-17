namespace HIMS.Services.Pharmacy
{
    public class PharSalesCurrentSumryListDto
    {
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public double SalesQty { get; set; }
        public long StoreId { get; set; }

    }
}
