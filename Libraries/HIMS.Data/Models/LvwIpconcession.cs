using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwIpconcession
    {
        public long ChargesId { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public double? DocPercentage { get; set; }
        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
        public string? ChargesDate { get; set; }
        public bool? IsGenerated { get; set; }
        public bool? IsCancelled { get; set; }
        public byte? OpdIpdType { get; set; }
        public long BillNo { get; set; }
        public string? PbillNo { get; set; }
        public long? CashCounterId { get; set; }
        public decimal? BillTotalAmount { get; set; }
        public double? BillConcessionAmount { get; set; }
        public decimal? BillNetPayableAmount { get; set; }
    }
}
