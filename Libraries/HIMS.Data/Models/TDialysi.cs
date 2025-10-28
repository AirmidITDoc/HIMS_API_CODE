using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDialysi
    {
        public long DialysisId { get; set; }
        public DateTime? DialysisDate { get; set; }
        public DateTime? DialysisTime { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? RegId { get; set; }
        public DateTime? DialysisStartTime { get; set; }
        public DateTime? DialysisStartEnd { get; set; }
        public byte? TotalNo { get; set; }
        public string? TechinicianName { get; set; }
        public string? Comments { get; set; }
        public DateTime? FutureAptDate { get; set; }
        public string? BillNo { get; set; }
    }
}
