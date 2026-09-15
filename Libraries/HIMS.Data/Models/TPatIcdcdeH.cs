using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPatIcdcdeH
    {
        public long Hid { get; set; }
        public DateTime? ReqDate { get; set; }
        public DateTime? ReqTime { get; set; }
        public byte? OpIpType { get; set; }
        public long? OpIpId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? Icdcode1 { get; set; }
        public string? Icdcode2 { get; set; }
        public string? Icdcode3 { get; set; }
        public string? CauseofDeath1 { get; set; }
        public string? CauseofDeath2 { get; set; }
        public string? CauseofDeath3 { get; set; }
        public string? ProvisionalDiagnosis1 { get; set; }
        public string? ProvisionalDiagnosis2 { get; set; }
        public string? ProvisionalDiagnosis3 { get; set; }
        public string? FinalDiagnosis1 { get; set; }
        public string? FinalDiagnosis2 { get; set; }
        public string? FinalDiagnosis3 { get; set; }
    }
}
