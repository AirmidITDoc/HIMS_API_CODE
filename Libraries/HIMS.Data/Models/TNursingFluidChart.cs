using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TNursingFluidChart
    {
        public long FluidChartId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Mdate { get; set; }
        public DateTime? Mtime { get; set; }
        public long? DurgId { get; set; }
        public DateTime? FstartTime { get; set; }
        public DateTime? FendTime { get; set; }
        public long? IsAddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public string? NurseName { get; set; }
    }
}
