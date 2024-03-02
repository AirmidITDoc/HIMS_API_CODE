using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GetSalesDraftBillItemDet
    {
        public long? DsalesId { get; set; }
        public long ItemId { get; set; }
        public double? QtyPerDay { get; set; }
        public int BalQty { get; set; }
        public bool? IsBatchRequired { get; set; }
    }
}
