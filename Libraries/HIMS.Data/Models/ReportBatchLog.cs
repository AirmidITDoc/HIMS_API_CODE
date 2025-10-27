namespace HIMS.Data.Models
{
    public partial class ReportBatchLog
    {
        public ReportBatchLog()
        {
            ReportExecutionLogs = new HashSet<ReportExecutionLog>();
        }

        public long BatchId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Status { get; set; }
        public int? TotalReports { get; set; }
        public int? ReportsSucceeded { get; set; }
        public int? ReportsFailed { get; set; }
        public string ExecutedBy { get; set; } = null!;

        public virtual ICollection<ReportExecutionLog> ReportExecutionLogs { get; set; }
    }
}
