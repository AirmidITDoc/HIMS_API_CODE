using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAdvHeader
    {
        public long? TadvAdvanceId { get; set; }
        public DateTime? TadvDate { get; set; }
        public long? TadvRefId { get; set; }
        public byte? TadvOpdIpdType { get; set; }
        public long? TadvOpdIpdId { get; set; }
        public double? TadvAdvanceAmount { get; set; }
        public double? TadvAdvanceUsedAmount { get; set; }
        public double? TadvBalanceAmount { get; set; }
        public long? TadvAddedBy { get; set; }
        public bool? TadvIsCancelled { get; set; }
        public long? TadvIsCancelledBy { get; set; }
        public DateTime? TadvIsCancelledDate { get; set; }
    }
}
