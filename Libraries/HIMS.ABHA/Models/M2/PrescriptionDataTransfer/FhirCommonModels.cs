using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class Meta
    {
        [JsonPropertyName("versionId")]
        public string VersionId { get; set; }

        [JsonPropertyName("lastUpdated")]
        public string LastUpdated { get; set; }

        [JsonPropertyName("profile")]
        public List<string> Profile { get; set; }

        [JsonPropertyName("security")]
        public List<Security> Security { get; set; }
    }

    public class Security
    {
        [JsonPropertyName("system")]
        public string System { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("display")]
        public string Display { get; set; }
    }

    public class Identifier
    {
        [JsonPropertyName("system")]
        public string System { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("type")]
        public CodeableConcept Type { get; set; }
    }

    public class CodeableConcept
    {
        [JsonPropertyName("coding")]
        public List<Coding> Coding { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Coding
    {
        [JsonPropertyName("system")]
        public string System { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("display")]
        public string Display { get; set; }
    }

    public class Reference
    {
        [JsonPropertyName("reference")]
        public string ReferenceValue { get; set; }

        [JsonPropertyName("display")]
        public string Display { get; set; }
    }
}