using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAdvDetail
    {
        public long? TadvDadvanceDetailId { get; set; }
        public DateTime? TadvDdate { get; set; }
        public DateTime? TadvDtime { get; set; }
        public long? TadvDadvanceId { get; set; }
        public string? TadvDadvanceNo { get; set; }
        public long? TadvDrefId { get; set; }
        public long? TadvDtransactionId { get; set; }
        public long? TadvDopdIpdId { get; set; }
        public byte? TadvDopdIpdType { get; set; }
        public decimal? TadvDadvanceAmount { get; set; }
        public decimal? TadvDusedAmount { get; set; }
        public decimal? TadvDbalanceAmount { get; set; }
        public decimal? TadvDrefundAmount { get; set; }
        public long? TadvDreasonOfAdvanceId { get; set; }
        public long? TadvDaddedBy { get; set; }
        public bool? TadvDisCancelled { get; set; }
        public long? TadvDisCancelledby { get; set; }
        public DateTime? TadvDisCancelledDate { get; set; }
        public string? TadvDreason { get; set; }
    }
}
