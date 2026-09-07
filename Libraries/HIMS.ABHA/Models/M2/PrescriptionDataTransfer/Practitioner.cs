using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class Practitioner
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "Practitioner";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonPropertyName("name")]
        public List<HumanName> Name { get; set; }
    }
}
