using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2
{

    public class Binary
    {
        [JsonPropertyName("resourceType")]
        public string? ResourceType { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("meta")]
        public Meta? Meta { get; set; }
        [JsonPropertyName("identifier")]
        public Identifier? Identifier { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
        [JsonPropertyName("entry")]
        public Entry[]? Entry { get; set; }
    }

    public class Meta
    {
        [JsonPropertyName("versionId")]
        public string? VersionId { get; set; }
        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; set; }
        [JsonPropertyName("profile")]
        public string[]? Profile { get; set; }
        [JsonPropertyName("security")]
        public Security[]? Security { get; set; }
    }

    public class Security
    {
        [JsonPropertyName("system")]
        public string? System { get; set; }
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("display")]
        public string? Display { get; set; }
    }

    public class Identifier
    {
        [JsonPropertyName("type")]
        public IdentifierType? Type { get; set; }
        [JsonPropertyName("system")]
        public string? System { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
    public class IdentifierType
    {
        [JsonPropertyName("coding")]
        public Class1[]? Coding { get; set; }
    }
    public class Entry
    {
        [JsonPropertyName("fullUrl")]
        public string? FullUrl { get; set; }
        [JsonPropertyName("resource")]
        public Resource? Resource { get; set; }
    }

    public class Resource
    {
        [JsonPropertyName("resourceType")]
        public string? ResourceType { get; set; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("meta")]
        public Meta1? Meta { get; set; }
        [JsonPropertyName("identifier")]
        public object? Identifier { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("type")]
        public Type? Type { get; set; }
        [JsonPropertyName("subject")]
        public Subject? Subject { get; set; }
        [JsonPropertyName("encounter")]
        public Subject? Encounter { get; set; }
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }
        [JsonPropertyName("author")]
        public Subject[]? Author { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("custodian")]
        public Custodian? Custodian { get; set; }
        [JsonPropertyName("section")]
        public Section[]? Section { get; set; }
        [JsonPropertyName("name")]
        public object? Name { get; set; }
        [JsonPropertyName("gender")]
        public string? Gender { get; set; }
        [JsonPropertyName("birthDate")]
        public string? BirthDate { get; set; }
        [JsonPropertyName("_class")]
        public Class1? _Class { get; set; }
        [JsonPropertyName("period")]
        public Period? Period { get; set; }
        [JsonPropertyName("intent")]
        public string? Intent { get; set; }
        [JsonPropertyName("medicationCodeableConcept")]
        public Medicationcodeableconcept? MedicationCodeableConcept { get; set; }
        [JsonPropertyName("authoredOn")]
        public DateTime? AuthoredOn { get; set; }
        [JsonPropertyName("requester")]
        public Subject? Requester { get; set; }
        [JsonPropertyName("reasonCode")]
        public Code[]? ReasonCode { get; set; }
        [JsonPropertyName("reasonReference")]
        public Subject[]? ReasonReference { get; set; }
        [JsonPropertyName("dosageInstruction")]
        public Dosageinstruction[]? DosageInstruction { get; set; }
        [JsonPropertyName("code")]
        public Code? Code { get; set; }
        [JsonPropertyName("recordedDate")]
        public DateTime? RecordedDate { get; set; }
        [JsonPropertyName("contentType")]
        public string? ContentType { get; set; }
        [JsonPropertyName("data")]
        public string? Data { get; set; }
    }

    public class Meta1
    {
        [JsonPropertyName("versionId")]
        public string? VersionId { get; set; }
        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; set; }
        [JsonPropertyName("profile")]
        public string[]? Profile { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("coding")]
        public Security[]? Coding { get; set; }
    }

    public class Subject
    {
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }
        [JsonPropertyName("display")]
        public string? Display { get; set; }
    }
    public class SectionEntry
    {
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Custodian
    {
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }
    }

    public class Class1
    {
        [JsonPropertyName("system")]
        public string? System { get; set; }
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("display")]
        public string? Display { get; set; }
    }

    public class Period
    {
        [JsonPropertyName("start")]
        public DateTime? Start { get; set; }
        [JsonPropertyName("end")]
        public DateTime? End { get; set; }
    }

    public class Medicationcodeableconcept
    {
        [JsonPropertyName("coding")]
        public Class1[]? Coding { get; set; }
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }

    public class Code
    {
        [JsonPropertyName("coding")]
        public Coding2[]? Coding { get; set; }
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }

    public class Coding2
    {
        [JsonPropertyName("system")]
        public string? System { get; set; }
        [JsonPropertyName("display")]
        public string? Display { get; set; }
    }

    public class Section
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("code")]
        public Route? Code { get; set; }
        [JsonPropertyName("entry")]
        public SectionEntry[]? Entry { get; set; }
    }

    public class Dosageinstruction
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        [JsonPropertyName("additionalInstruction")]
        public Route[]? AdditionalInstruction { get; set; }
        [JsonPropertyName("timing")]
        public Timing? Timing { get; set; }
        [JsonPropertyName("route")]
        public Route? Route { get; set; }
        [JsonPropertyName("method")]
        public Route? Method { get; set; }
    }

    public class Timing
    {
        [JsonPropertyName("repeat")]
        public Repeat? Repeat { get; set; }
    }

    public class Repeat
    {
        [JsonPropertyName("duration")]
        public int? Duration { get; set; }
        [JsonPropertyName("durationUnit")]
        public string? DurationUnit { get; set; }
        [JsonPropertyName("frequency")]
        public int? Frequency { get; set; }
        [JsonPropertyName("period")]
        public int? Period { get; set; }
        [JsonPropertyName("periodUnit")]
        public string? PeriodUnit { get; set; }
    }

    public class Route
    {
        [JsonPropertyName("coding")]
        public Class1[]? Coding { get; set; }
        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }
}
