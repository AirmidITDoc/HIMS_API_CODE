namespace HIMS.Data.Models
{
    public partial class MisRefreshProcessLog
    {
        public long? LogId { get; set; }
        public int? LogSeq { get; set; }
        public string? ProcessName { get; set; }
        public string? ProcedureName { get; set; }
        public DateTime? CurrTimeStamp { get; set; }
        public DateTime? ProcessRunDate { get; set; }
        public string? Task { get; set; }
        public string? TaskStage { get; set; }
        public string? Remark { get; set; }
    }
}
