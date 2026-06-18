namespace HIMS.ABHA.Models.M2
{
    public class LinkCareContextRequest
    {
        public string AbhaNumber { get; set; } = string.Empty;
        public string AbhaAddress { get; set; } = string.Empty;
        public List<PatientLinkEntry> Patient { get; set; } = new List<PatientLinkEntry>();
        public string HipId { get; set; } = string.Empty;
        public string LinkToken { get; set; } = string.Empty;
        public string XCmId { get; set; } = string.Empty;
    }

    public class PatientLinkEntry
    {
        public string ReferenceNumber { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
        public List<CareContext> CareContexts { get; set; } = new List<CareContext>();
        public string HiType { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class CareContext
    {
        public string ReferenceNumber { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;
    }
}
