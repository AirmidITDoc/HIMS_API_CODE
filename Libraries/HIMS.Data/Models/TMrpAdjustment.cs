using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMrpAdjustment
    {
        public long MrpAdjId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public decimal? OldMrp { get; set; }
        public decimal? OldLandedRate { get; set; }
        public decimal? OldPurRate { get; set; }
        public float? Qty { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? PurRate { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? AddedDateTime { get; set; }
    }
}
