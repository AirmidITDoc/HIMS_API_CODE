using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2
{
    public class LinkCareContextRequest
    {
        [JsonPropertyName("abhaNumber")]
        public string AbhaNumber { get; set; } = string.Empty;
        [JsonPropertyName("abhaAddress")]
        public string AbhaAddress { get; set; } = string.Empty;
        [JsonPropertyName("patient")]
        public List<PatientLinkEntry> Patient { get; set; } = new List<PatientLinkEntry>();
        //public string HipId { get; set; } = string.Empty;
        [JsonPropertyName("linkToken")]
        public string LinkToken { get; set; } = string.Empty;
        //public string XCmId { get; set; } = string.Empty;
    }

    public class PatientLinkEntry
    {
        [JsonPropertyName("referenceNumber")]
        public string ReferenceNumber { get; set; } = string.Empty;
        [JsonPropertyName("display")]
        public string Display { get; set; } = string.Empty;
        [JsonPropertyName("careContexts")]
        public List<CareContext> CareContexts { get; set; } = new List<CareContext>();
        [JsonPropertyName("hiType")]
        public string HiType { get; set; } = string.Empty;
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class CareContext
    {
        [JsonPropertyName("referenceNumber")]
        public string ReferenceNumber { get; set; } = string.Empty;
        [JsonPropertyName("display")]
        public string Display { get; set; } = string.Empty;
    }
}
