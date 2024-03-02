using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusControl
    {
        public decimal Controlid { get; set; }
        public string Machineid { get; set; } = null!;
        public string? Lotno { get; set; }
        public string? Controlname { get; set; }
        public string? Controlalias { get; set; }
        public string? Controlbc { get; set; }
        public string? Sampletypeid { get; set; }
        public string? Controltype { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public string? Mfgname { get; set; }
        public string? Valid { get; set; }
        public decimal? Seqno { get; set; }
        public string? Comments { get; set; }
    }
}
