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
}
