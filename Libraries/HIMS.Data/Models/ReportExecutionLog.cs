using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ReportExecutionLog
    {
        public int LogId { get; set; }
        public long? BatchId { get; set; }
        public int? ReportId { get; set; }
        public string? ReportName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Status { get; set; }
        public string? ErrorMessage { get; set; }
        public int? RecordsDeleted { get; set; }
        public int? RecordsInserted { get; set; }
        public string ExecutedBy { get; set; } = null!;

        public virtual ReportBatchLog? Batch { get; set; }
        public virtual ReportConfig? Report { get; set; }
    }
}
