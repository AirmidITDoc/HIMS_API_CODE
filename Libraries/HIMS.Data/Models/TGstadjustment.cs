using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TGstadjustment
    {
        public long GstadgId { get; set; }
        public long? StoreId { get; set; }
        public long? StkId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public double? OldCgstper { get; set; }
        public double? OldSgstper { get; set; }
        public double? OldIgstper { get; set; }
        public double? Cgstper { get; set; }
        public double? Sgstper { get; set; }
        public double? Igstper { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
