using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TReturnFromDepartmentHeader
    {
        public long ReturnId { get; set; }
        public string? ReturnNo { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? ReturnTime { get; set; }
        public long? FromStoreId { get; set; }
        public long? TostoreId { get; set; }
        public decimal? LandedRateTotalAmount { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public decimal? PurchaseTotalAmount { get; set; }
        public string? Remark { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public decimal? TotalVatAmount { get; set; }
    }
}
