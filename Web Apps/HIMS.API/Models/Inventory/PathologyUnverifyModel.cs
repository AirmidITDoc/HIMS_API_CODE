namespace HIMS.API.Models.Inventory
{
    public class PathologyUnverifyModel
    {
        public long PathReportId { get; set; }
        public long? UnVerifyId { get; set; }
        public string? UnVerifyComment { get; set; }
        public DateTime? UnVerifyDateTime { get; set; }

    }
}
