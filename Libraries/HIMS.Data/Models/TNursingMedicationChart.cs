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
        public string? NurseName { get; set; }
        public string? DoseName { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
