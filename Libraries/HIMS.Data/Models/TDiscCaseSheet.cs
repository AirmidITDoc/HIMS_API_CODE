using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDiscCaseSheet
    {
        public long DiscCaseId { get; set; }
        public long? OpIpId { get; set; }
        public bool? IsMlc { get; set; }
        public string? Mlcno { get; set; }
        public string? ExtCinj { get; set; }
        public string? ExtCinjIcdcode { get; set; }
        public string? ProvisionalDiag1 { get; set; }
        public string? ProvisionalDiag2 { get; set; }
        public string? ProvisionalDiag3 { get; set; }
        public string? FinalDiag1 { get; set; }
        public string? FinalDiag2 { get; set; }
        public string? FinalDiag3 { get; set; }
        public string? Icdcode1 { get; set; }
        public string? Icdcode2 { get; set; }
        public string? Icdcode3 { get; set; }
        public string? Cod1 { get; set; }
        public string? Cod2 { get; set; }
        public string? Cod3 { get; set; }
        public long? IsAddedBy { get; set; }
        public long? IsUpdatedBy { get; set; }
    }
}
