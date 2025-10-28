using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusSampledatum
    {
        public string Srno { get; set; } = null!;
        public DateTime Sdate { get; set; }
        public string Sampleid { get; set; } = null!;
        public string Testid { get; set; } = null!;
        public string Machineid { get; set; } = null!;
        public string Suffix { get; set; } = null!;
        public string Transferflag { get; set; } = null!;
        public string Tmpvalue { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Rundate { get; set; }
        public string? Result { get; set; }
        public string? Finalflag { get; set; }
    }
}
