using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2
{
    public class UpdateBridgeUrlRequest
    {
        public string Url { get; set; } = string.Empty;
    }
    public class LinkTokenCallbackPayload
    {
        [JsonPropertyName("abhaAddress")]
        public string? AbhaAddress { get; set; }

        [JsonPropertyName("linkToken")]
        public string? LinkToken { get; set; }

        [JsonPropertyName("error")]
        public CallbackError? Error { get; set; }

        [JsonPropertyName("response")]
        public CallbackResponseMeta Response { get; set; } = new();
        
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Error is null && !string.IsNullOrEmpty(LinkToken);
    }

    public class CallbackError
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }

    public class CallbackResponseMeta
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
    }
    public class RegisterBridgeRequest
    {
        public string FacilityId { get; set; } = string.Empty;
        public string FacilityName { get; set; } = string.Empty;
        public List<HrpEntry> HRP { get; set; } = new List<HrpEntry>();
    }

    public class HrpEntry
    {
        public string BridgeId { get; set; } = string.Empty;
        public string HipName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;   // "HIP" or "HIU"
        public bool Active { get; set; }
    }
    public class BridgeServiceDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("bridgeId")]
        public string BridgeId { get; set; } = string.Empty;
        [JsonPropertyName("serviceId")]
        public string ServiceId { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("isHip")]
        public bool IsHip { get; set; }
        [JsonPropertyName("isPhr")]
        public bool IsPhr { get; set; }
        [JsonPropertyName("endpoints")]
        public Dictionary<string, object> Endpoints { get; set; } = new Dictionary<string, object>();
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("registerTime")]
        public string RegisterTime { get; set; } = string.Empty;
        [JsonPropertyName("dateCreated")]
        public string DateCreated { get; set; } = string.Empty;
        [JsonPropertyName("dateModified")]
        public string DateModified { get; set; } = string.Empty;
    }

    public class Bridge
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public bool active { get; set; }
        public bool blocklisted { get; set; }
    }

    public class Endpoints
    {
        public List<HipEndpoint> hipEndpoints { get; set; }
        public List<HiuEndpoint> hiuEndpoints { get; set; }
        public List<HealthLockerEndpoint> healthLockerEndpoints { get; set; }
    }

    public class HealthLockerEndpoint
    {
        public string use { get; set; }
        public string connectionType { get; set; }
        public string address { get; set; }
    }

    public class HipEndpoint
    {
        public string use { get; set; }
        public string connectionType { get; set; }
        public string address { get; set; }
    }

    public class HiuEndpoint
    {
        public string use { get; set; }
        public string connectionType { get; set; }
        public string address { get; set; }
    }

    public class BridgeResponseDto
    {
        public Bridge bridge { get; set; }
        public List<Service> services { get; set; }
    }

    public class Service
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> types { get; set; }
        public Endpoints endpoints { get; set; }
        public bool active { get; set; }
    }

public class ConsentNotificationPayload
    {
        [JsonPropertyName("notification")]
        public Notification Notification { get; set; }
    }

    public class Notification
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("consentId")]
        public string ConsentId { get; set; }

        [JsonPropertyName("consentDetail")]
        public ConsentDetail ConsentDetail { get; set; }

        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        [JsonPropertyName("grantAcknowledgement")]
        public bool GrantAcknowledgement { get; set; }
    }

    public class ConsentDetail
    {
        [JsonPropertyName("schemaVersion")]
        public string SchemaVersion { get; set; }

        [JsonPropertyName("consentId")]
        public string ConsentId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("patient")]
        public Patient Patient { get; set; }

        [JsonPropertyName("careContexts")]
        public List<CareContext> CareContexts { get; set; }

        [JsonPropertyName("purpose")]
        public Purpose Purpose { get; set; }

        [JsonPropertyName("hip")]
        public Hip Hip { get; set; }

        [JsonPropertyName("consentManager")]
        public ConsentManager ConsentManager { get; set; }

        [JsonPropertyName("hiTypes")]
        public List<string> HiTypes { get; set; }

        [JsonPropertyName("permission")]
        public Permission Permission { get; set; }
    }

    public class Patient
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class CareContext
    {
        [JsonPropertyName("patientReference")]
        public string PatientReference { get; set; }

        [JsonPropertyName("careContextReference")]
        public string CareContextReference { get; set; }
    }

    public class Purpose
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("refUri")]
        public string RefUri { get; set; }
    }

    public class Hip
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class ConsentManager
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class Permission
    {
        [JsonPropertyName("accessMode")]
        public string AccessMode { get; set; }

        [JsonPropertyName("dateRange")]
        public DateRange DateRange { get; set; }

        [JsonPropertyName("dataEraseAt")]
        public DateTime DataEraseAt { get; set; }

        [JsonPropertyName("frequency")]
        public Frequency Frequency { get; set; }
    }

    public class DateRange
    {
        [JsonPropertyName("from")]
        public DateTime From { get; set; }

        [JsonPropertyName("to")]
        public DateTime To { get; set; }
    }

    public class Frequency
    {
        [JsonPropertyName("unit")]
        public string Unit { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("repeats")]
        public int Repeats { get; set; }
    }

    public class OnNotifyResponse
    {
        [JsonPropertyName("acknowledgement")]
        public Acknowledgement Acknowledgement { get; set; }

        [JsonPropertyName("response")]
        public NotifyResponse Response { get; set; }
    }

    public class Acknowledgement
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class NotifyResponse
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
    }

    public class CareContextDiscoverRequest
    {
        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("patient")]
        public Patient PatientCare { get; set; }
    }

    public class PatientCare
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("verifiedIdentifiers")]
        public List<VerifiedIdentifier> VerifiedIdentifiers { get; set; }

        [JsonPropertyName("unverifiedIdentifiers")]
        public List<VerifiedIdentifier> UnverifiedIdentifiers { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("yearOfBirth")]
        public int YearOfBirth { get; set; }
    }

    public class VerifiedIdentifier
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
