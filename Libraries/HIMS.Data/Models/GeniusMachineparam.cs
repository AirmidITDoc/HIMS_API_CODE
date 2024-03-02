using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusMachineparam
    {
        public string Machineid { get; set; } = null!;
        public string Machineparamid { get; set; } = null!;
        public string Paramcode { get; set; } = null!;
        public string Suffix { get; set; } = null!;
        public string? SampleType { get; set; }
    }
}
