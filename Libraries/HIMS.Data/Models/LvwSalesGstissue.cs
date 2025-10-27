namespace HIMS.Data.Models
{
    public partial class LvwSalesGstissue
    {
        public long SalesId { get; set; }
        public string? SalesNo { get; set; }
        public decimal? Mrpamount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public float? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public double? Acgstamt { get; set; }
        public double? AcgstamtDiff { get; set; }
        public float? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
    }
}
