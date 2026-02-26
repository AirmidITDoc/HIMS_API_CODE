namespace HIMS.API.Models.Pathology
{
    public class PathologyLabReportHeaderModel
    {
        public long PathReportId { get; set; }
        public DateTime? SampleReceviedDateTime { get; set; }
        public long? SampleReceviedUserId { get; set; }
        public bool? IsSampleReceivedStatus { get; set; }


    }
}
