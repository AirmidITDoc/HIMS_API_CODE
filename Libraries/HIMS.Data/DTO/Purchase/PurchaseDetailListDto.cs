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
        public String? ItemName { get; set; }
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

    }
}
