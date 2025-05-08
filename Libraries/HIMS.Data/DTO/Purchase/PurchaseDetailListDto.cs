using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class PurchaseDetailListDto
    {
        public long PurDetId { get; set; }
        public long? ItemId { get; set; }
        public long? UOMID { get; set; }
        public string?  ItemName { get; set; }
        public double? Qty { get; set; }
        public double? Rate { get; set; }
        public double? TotalAmount { get; set; }
        public double? DiscAmount { get; set; }
        public double? DiscPer { get; set; }
        public double? VatAmount { get; set; }
        public double? VatPer { get; set; }
        public double? GrandTotalAmount { get; set; }
        public double? MRP { get; set; }
        public long? PurchaseId { get; set; }
        public double? CGSTPer { get; set; }
        public decimal? CGSTAmt { get; set; }
        public double? SGSTPer { get; set; }
        public decimal? SGSTAmt { get; set; }
        public double? IGSTPer { get; set; }
        public decimal? IGSTAmt { get; set; }
        public decimal? DefRate { get; set; }
        public decimal? VendDiscAmt { get; set; }
        public string? Specification { get; set; }


    }
}
