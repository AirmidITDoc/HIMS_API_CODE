using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{

    public class grnInvoicenocheckModel
    {
        public string? InvoiceNo { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        
    }
}