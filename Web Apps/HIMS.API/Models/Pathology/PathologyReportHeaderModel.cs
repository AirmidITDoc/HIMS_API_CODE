using HIMS.API.Models.OutPatient;

namespace HIMS.API.Models.Pathology
{
    public class PathologyLabReportHeaderModel
    {
        public long PathReportId { get; set; }
        public DateTime? SampleReceviedDateTime { get; set; }
        public long? SampleReceviedUserId { get; set; }
        public bool? IsSampleReceivedStatus { get; set; }


    }
    public class PathologyLabReportHeader
    {
        public List<PathologyLabReportHeaderModel> PathologyLabReport { get; set; }

    }
    public class PathologyReportHeaderCancel
    {
        public long PathReportId { get; set; }
        public string? SampleReceviedCancelReason { get; set; }
       


    }

}
