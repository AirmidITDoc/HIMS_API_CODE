using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIssueToDepartmentDetail
    {
        public long IssueDepId { get; set; }
        public long? IssueId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? IssueQty { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public double? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public decimal? UnitPurRate { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public float? ReturnQty { get; set; }
        public long? StkId { get; set; }
    }
}
