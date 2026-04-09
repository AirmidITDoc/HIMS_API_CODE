using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class IpdDrugSchedule
    {
        public long IpdDrugScheduleId { get; set; }
        public long MedChartId { get; set; }
        public int DoseNo { get; set; }
        public DateTime DoseTime { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public string? Comment { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual TNursingMedicationChart1 MedChart { get; set; } = null!;
    }
}
