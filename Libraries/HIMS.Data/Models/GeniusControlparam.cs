using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeniusControlparam
    {
        public decimal Controlid { get; set; }
        public string Machineid { get; set; } = null!;
        public string Controlparamid { get; set; } = null!;
        public string? Sampletypeid { get; set; }
        public string? Controlparamname { get; set; }
        public double? Mean { get; set; }
        public double? Sd { get; set; }
        public double? Lowlimit { get; set; }
        public double? Highlimit { get; set; }
        public string? Unit { get; set; }
        public double? Cv { get; set; }
        public string? Valid { get; set; }
        public decimal? Seq { get; set; }
        public string? Runcount { get; set; }
        public double? Lotmean { get; set; }
        public double? Lotsd { get; set; }
        public double? Lotcv { get; set; }
        public double? Calcmean { get; set; }
        public double? Calcsd { get; set; }
        public double? Calccv { get; set; }
        public double? Prece { get; set; }
    }
}
