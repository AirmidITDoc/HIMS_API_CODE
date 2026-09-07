using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class Organization
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "Organization";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}
