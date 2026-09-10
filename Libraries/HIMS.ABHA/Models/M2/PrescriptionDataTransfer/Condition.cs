using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class Condition
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "Condition";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("code")]
        public CodeableConcept Code { get; set; }

        [JsonPropertyName("subject")]
        public Reference Subject { get; set; }

        [JsonPropertyName("recordedDate")]
        public string RecordedDate { get; set; }
    }
}
