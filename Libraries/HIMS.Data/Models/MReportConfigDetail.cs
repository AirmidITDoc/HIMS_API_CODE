namespace HIMS.Data.Models
{
    public partial class MReportConfigDetail
    {
        public long ReportColId { get; set; }
        public long? ReportId { get; set; }
        public bool? IsDisplayColumn { get; set; }
        public string? ReportHeader { get; set; }
        public string? ReportColumn { get; set; }
        public string? ReportColumnWidth { get; set; }
        public string? ReportColumnAligment { get; set; }
        public string? ReportTotalField { get; set; }
        public string? ReportGroupByLabel { get; set; }
        public int? ReportGroupBySequenceNo { get; set; }
        public string? SummaryLabel { get; set; }
        public int? SummarySequenceNo { get; set; }
        public long? SequenceNo { get; set; }
        public string? ProcedureName { get; set; }

        public virtual MReportConfig? Report { get; set; }
    }
}
