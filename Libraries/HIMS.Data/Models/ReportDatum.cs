namespace HIMS.Data.Models
{
    public partial class ReportDatum
    {
        public long ReportDataId { get; set; }
        public int? ReportId { get; set; }
        public string ReportName { get; set; } = null!;
        public DateTime ReportDate { get; set; }
        public string? Category { get; set; }
        public decimal? TodayAmount { get; set; }
        public decimal? YesterdayAmount { get; set; }
        public decimal? Dbyamount { get; set; }
        public decimal? Mtd { get; set; }
        public decimal? Pmt { get; set; }
        public decimal? Ytd { get; set; }
        public decimal? Lytd { get; set; }
        public decimal? Lysd { get; set; }
        public DateTime? LoadDate { get; set; }
    }
}
