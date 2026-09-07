using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class Composition
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "Composition";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("identifier")]
        public Identifier Identifier { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("type")]
        public CodeableConcept Type { get; set; }

        [JsonPropertyName("subject")]
        public Reference Subject { get; set; }

        [JsonPropertyName("encounter")]
        public Reference Encounter { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("author")]
        public List<Reference> Author { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("custodian")]
        public Reference Custodian { get; set; }

        [JsonPropertyName("section")]
        public List<CompositionSection> Section { get; set; }
    }

    public class CompositionSection
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("code")]
        public CodeableConcept Code { get; set; }

        [JsonPropertyName("entry")]
        public List<ReferenceEntry> Entry { get; set; }
    }

    public class ReferenceEntry
    {
        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
