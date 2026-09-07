using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class Encounter
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "Encounter";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("class")]
        public Coding Class { get; set; }

        [JsonPropertyName("subject")]
        public Reference Subject { get; set; }

        [JsonPropertyName("period")]
        public Period Period { get; set; }
    }

    public class Period
    {
        [JsonPropertyName("start")]
        public string Start { get; set; }

        [JsonPropertyName("end")]
        public string End { get; set; }
    }
}
