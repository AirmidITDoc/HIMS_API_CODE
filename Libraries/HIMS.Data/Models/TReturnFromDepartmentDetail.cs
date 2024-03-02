using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TReturnFromDepartmentDetail
    {
        public long ReturnDetId { get; set; }
        public long? ReturnId { get; set; }
        public long? ItemId { get; set; }
        public decimal? UnitPurchaseRate { get; set; }
        public double? VatAmount { get; set; }
        public decimal? UnitLandedRate { get; set; }
        public decimal? TotalLandedRate { get; set; }
        public decimal? UnitMrp { get; set; }
        public double? VatPer { get; set; }
        public double? ReturnQty { get; set; }
        public double? RemainingQty { get; set; }
        public string? Remark { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? BalQty { get; set; }
        public decimal? TotalMrpamount { get; set; }
        public decimal? TotalPurAmount { get; set; }
        public long? IssueId { get; set; }
    }
}
