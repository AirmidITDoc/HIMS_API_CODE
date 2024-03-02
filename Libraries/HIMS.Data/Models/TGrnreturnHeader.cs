using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TGrnreturnHeader
    {
        public long GrnreturnId { get; set; }
        public string? GrnreturnNo { get; set; }
        public long? Grnid { get; set; }
        public DateTime? GrnreturnDate { get; set; }
        public DateTime? GrnreturnTime { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? GrnReturnAmount { get; set; }
        public decimal? TotalDiscAmount { get; set; }
        public decimal? TotalVatAmount { get; set; }
        public decimal? TotalOtherTaxAmount { get; set; }
        public decimal? TotalOctroiAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? CashCredit { get; set; }
        public string? Remark { get; set; }
        public bool? IsVerified { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public bool? IsClosed { get; set; }
        public string? Prefix { get; set; }
        public string? GrnType { get; set; }
        public bool? IsGrnTypeFlag { get; set; }
    }
}
