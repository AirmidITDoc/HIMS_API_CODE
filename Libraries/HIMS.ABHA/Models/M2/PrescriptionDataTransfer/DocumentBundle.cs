using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class DocumentBundle
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "Bundle";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("identifier")]
        public Identifier Identifier { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "document";

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("entry")]
        public List<BundleEntry> Entry { get; set; } = new();
    }
}
