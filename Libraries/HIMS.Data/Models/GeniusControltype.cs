using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusControltype
    {
        public string Controltype { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Seq { get; set; }
    }
}
