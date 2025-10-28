using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LMaxSupplierNameForItem
    {
        public long ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? SupplierName { get; set; }
        public long? StoreId { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? Rate { get; set; }
    }
}
