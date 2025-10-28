using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusQcresult
    {
        public decimal Controlid { get; set; }
        public string? Controlname { get; set; }
        public string? Lotno { get; set; }
        public string Machineid { get; set; } = null!;
        public string Controlparamid { get; set; } = null!;
        public decimal? Runno { get; set; }
        public DateTime Rundate { get; set; }
        public DateTime Runtime { get; set; }
        public double? Result { get; set; }
        public string? Resulttext { get; set; }
        public string? Resultflag { get; set; }
        public string? Rackpos { get; set; }
        public string? Valid { get; set; }
        public string? Ruleflag { get; set; }
        public string? Accept { get; set; }
        public string? Comments { get; set; }
        public string? Rejectcomment { get; set; }
        public string? Verified { get; set; }
        public string? Verifiedby { get; set; }
        public DateTime? Verifieddttime { get; set; }
        public string? Enteredby { get; set; }
        public DateTime? Entrydttime { get; set; }
        public string? Sampletypeid { get; set; }
        public string? Controltype { get; set; }
    }
}
