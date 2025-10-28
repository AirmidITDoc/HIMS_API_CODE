using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusMachinemaster
    {
        public string Machineid { get; set; } = null!;
        public string? Machinename { get; set; }
        public string? Commport { get; set; }
        public string? Settings { get; set; }
        public string? Graphpresent { get; set; }
        public string? Graphstartchar { get; set; }
        public string? Issampleid { get; set; }
        public string? Export { get; set; }
        public string? Nooftests { get; set; }
        public string? Accessionno { get; set; }
        public string? Testid { get; set; }
        public string? Category { get; set; }
        public string? Compname { get; set; }
        public string? Commmode { get; set; }
    }
}
