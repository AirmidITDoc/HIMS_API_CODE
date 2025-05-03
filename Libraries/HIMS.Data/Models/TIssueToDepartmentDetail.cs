using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIssueToDepartmentDetail
    {
        public long IssueDepId { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? IssueQty { get; set; }
        public long IssueId { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public double? VatPercentage { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public string? Status { get; set; }
        public long? StockId { get; set; }
        public long? StoreId { get; set; }
    }
}
