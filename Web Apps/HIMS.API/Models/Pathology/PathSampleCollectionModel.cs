namespace HIMS.API.Models.Pathology
{
    public class PathSampleCollectionModel
    {

        public long? PathReportId { get; set; }
        public string? PathDate { get; set; }
        public string? PathTime { get; set; }
        public byte? IsSampleCollection { get; set; }

        public long? SampleNo { get; set; }

       

    }
}
