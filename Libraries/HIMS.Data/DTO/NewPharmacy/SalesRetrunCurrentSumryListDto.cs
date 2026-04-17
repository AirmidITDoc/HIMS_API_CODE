namespace HIMS.Services.Pharmacy
{
    public class SalesRetrunCurrentSumryListDto
    {
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public double ReturnQty { get; set; }
        public long StoreID { get; set; }

    }
}
