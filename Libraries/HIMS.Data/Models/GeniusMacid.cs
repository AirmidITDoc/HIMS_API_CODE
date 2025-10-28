using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusMacid
    {
        public string Machineid { get; set; } = null!;
        public string Machineparamid { get; set; } = null!;
        public string Suffix { get; set; } = null!;
        public string? Description { get; set; }
        public double? Conversion { get; set; }
        public string? Rounding { get; set; }
        public string? Program { get; set; }
        public string? Assayno { get; set; }
        public string? SampleType { get; set; }
    }
}
