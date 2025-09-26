using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class UpdateGRNSupplierModel
    { 
       public DateTime? Grndate { get; set; }
        public string? Grntime { get; set; }
        public long? SupplierId { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime? InvDate { get; set; }
        public long Grnid { get; set; }
    }


    public class UpdateCurrentStockModel
    {
        public long? BarCodeSeqNo { get; set; }
        public long? StockId { get; set; }
        public long? ItemId { get; set; }
        public long? StoreId { get; set; }

    }

}