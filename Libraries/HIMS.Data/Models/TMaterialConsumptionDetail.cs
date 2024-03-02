using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMaterialConsumptionDetail
    {
        public long MaterialConDetId { get; set; }
        public long? MaterialConsumptionId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public long? Qty { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public decimal? PerUnitPurchaseRate { get; set; }
        public decimal? PerUnitMrprate { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Remark { get; set; }
        public long? AdmId { get; set; }
    }
}
