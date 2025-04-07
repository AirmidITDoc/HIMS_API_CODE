namespace HIMS.API.Models.OutPatient
{
    public class OPCasepaperModel
    {
        public long Id { get; set; }
        public long? VisitId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
    }
}
