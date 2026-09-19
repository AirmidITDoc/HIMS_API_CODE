using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class Patient
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "Patient";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonPropertyName("name")]
        public List<HumanName> Name { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("birthDate")]
        public string BirthDate { get; set; }
    }

    public class HumanName
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class PatientMaster
    {
        public long RegId { get; set; }
        public string? FirstName { get; set; }
        public long? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //public string? AbhaAddress { get; set; }
    }
}
