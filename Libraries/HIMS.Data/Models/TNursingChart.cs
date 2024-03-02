using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TNursingChart
    {
        public long NursingChartId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Mdate { get; set; }
        public DateTime? Mtime { get; set; }
        public string? NursingT { get; set; }
        public string? NursingP { get; set; }
        public string? NursingR { get; set; }
        public string? NursingBp { get; set; }
        public string? NursingIvo { get; set; }
        public string? NursingUo { get; set; }
        public string? NursingDo { get; set; }
        public string? NursingTotalInput { get; set; }
        public string? NursingTotalOutput { get; set; }
        public string? NurseName { get; set; }
        public long? IsAddedBy { get; set; }
        public bool? IsCancelled { get; set; }
    }
}
