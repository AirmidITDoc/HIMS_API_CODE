namespace HIMS.Data.Models
{
    public partial class ReportConfig
    {
        public ReportConfig()
        {
            ReportExecutionLogs = new HashSet<ReportExecutionLog>();
        }

        public int ReportId { get; set; }
        public string ReportName { get; set; } = null!;
        public string ReportType { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<ReportExecutionLog> ReportExecutionLogs { get; set; }
    }
}
