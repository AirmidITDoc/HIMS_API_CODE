using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusBufferresult
    {
        public string? Labno { get; set; }
        public string? Machineid { get; set; }
        public string? Machineparamid { get; set; }
        public string? Result { get; set; }
        public DateTime? Dttime { get; set; }
        public DateTime? Rundate { get; set; }
        public string? Qcflag { get; set; }
    }
}
