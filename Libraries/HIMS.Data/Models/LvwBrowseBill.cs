using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwBrowseBill
    {
        public long BillNo { get; set; }
        public long? RegId { get; set; }
        public string? PatientName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public DateTime? BillDate { get; set; }
        public long? OpdIpdId { get; set; }
        public long? IsCancelled { get; set; }
        public byte? OpdIpdType { get; set; }
        public string? PbillNo { get; set; }
        public long? Expr1 { get; set; }
        public string? RegNo { get; set; }
    }
}
