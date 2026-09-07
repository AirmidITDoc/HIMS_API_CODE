using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class Binary
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "Binary";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}
