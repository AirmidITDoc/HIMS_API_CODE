using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCanItemMaster
    {
        public long ItemId { get; set; }
        public string? ItemShortName { get; set; }
        public string? ItemName { get; set; }
        public long? ItemCategaryId { get; set; }
        public long? PurchaseUomid { get; set; }
        public string? ConversionFactor { get; set; }
        public bool? Isdeleted { get; set; }
        public long? Addedby { get; set; }
        public long? UpDatedBy { get; set; }
        public bool? IsBatchRequired { get; set; }
        public double? Cgst { get; set; }
        public double? Sgst { get; set; }
        public double? Igst { get; set; }
        public string? ProdLocation { get; set; }
        public decimal? Price { get; set; }
        public decimal? EmpPrice { get; set; }
    }
}
