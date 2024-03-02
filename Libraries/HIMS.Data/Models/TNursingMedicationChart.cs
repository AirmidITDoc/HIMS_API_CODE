using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TNursingMedicationChart
    {
        public long MedChartId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Mdate { get; set; }
        public DateTime? Mtime { get; set; }
        public long? DurgId { get; set; }
        public long? DoseId { get; set; }
        public string? Route { get; set; }
        public string? Freq { get; set; }
        public long? IsAddedBy { get; set; }
        public string? NurseName { get; set; }
        public bool? IsCancelled { get; set; }
        public string? DoseName { get; set; }
    }
}
