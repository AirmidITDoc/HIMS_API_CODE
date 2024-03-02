using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMaterialConsumptionHeader
    {
        public long MaterialConsumptionId { get; set; }
        public string? ConsumptionNo { get; set; }
        public DateTime? ConsumptionDate { get; set; }
        public DateTime? ConsumptionTime { get; set; }
        public long? FromStoreId { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public string? Remark { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? AdmId { get; set; }
    }
}
