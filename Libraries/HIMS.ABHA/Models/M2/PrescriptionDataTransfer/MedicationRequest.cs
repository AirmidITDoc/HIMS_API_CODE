using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2.PrescriptionDataTransfer
{
    public class MedicationRequest
    {
        [JsonPropertyName("resourceType")]
        public string ResourceType { get; set; } = "MedicationRequest";

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("intent")]
        public string Intent { get; set; }

        [JsonPropertyName("medicationCodeableConcept")]
        public CodeableConcept MedicationCodeableConcept { get; set; }

        [JsonPropertyName("subject")]
        public Reference Subject { get; set; }

        [JsonPropertyName("authoredOn")]
        public string AuthoredOn { get; set; }

        [JsonPropertyName("requester")]
        public Reference Requester { get; set; }

        [JsonPropertyName("reasonCode")]
        public List<CodeableConcept> ReasonCode { get; set; }

        [JsonPropertyName("reasonReference")]
        public List<Reference> ReasonReference { get; set; }

        [JsonPropertyName("dosageInstruction")]
        public List<DosageInstruction> DosageInstruction { get; set; }
    }

    public class DosageInstruction
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("additionalInstruction")]
        public List<CodeableConcept> AdditionalInstruction { get; set; }

        [JsonPropertyName("timing")]
        public Timing Timing { get; set; }

        [JsonPropertyName("route")]
        public CodeableConcept Route { get; set; }

        [JsonPropertyName("method")]
        public CodeableConcept Method { get; set; }
    }

    public class Timing
    {
        [JsonPropertyName("repeat")]
        public TimingRepeat Repeat { get; set; }
    }

    public class TimingRepeat
    {
        [JsonPropertyName("duration")]
        public decimal Duration { get; set; }

        [JsonPropertyName("durationUnit")]
        public string DurationUnit { get; set; }

        [JsonPropertyName("frequency")]
        public decimal Frequency { get; set; }

        [JsonPropertyName("period")]
        public decimal Period { get; set; }

        [JsonPropertyName("periodUnit")]
        public string PeriodUnit { get; set; }
    }
}
