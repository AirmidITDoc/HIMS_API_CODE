using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M3
{
    public class ConsentInitRequest
    {
        [JsonPropertyName("consent")]
        public ConsentDetail Consent { get; set; } = new ConsentDetail();
    }

    public class ConsentDetail
    {
        [JsonPropertyName("purpose")]
        public ConsentPurpose Purpose { get; set; } = new ConsentPurpose();

        [JsonPropertyName("patient")]
        public ConsentPatient Patient { get; set; } = new ConsentPatient();

        [JsonPropertyName("hiu")]
        public ConsentHiu Hiu { get; set; } = new ConsentHiu();

        [JsonPropertyName("hip")]
        public ConsentHip Hip { get; set; } = new ConsentHip();

        [JsonPropertyName("careContexts")]
        public List<CareContext> CareContexts { get; set; } = new List<CareContext>();

        [JsonPropertyName("requester")]
        public ConsentRequester Requester { get; set; } = new ConsentRequester();

        [JsonPropertyName("hiTypes")]
        public List<string> HiTypes { get; set; } = new List<string>();

        [JsonPropertyName("permission")]
        public ConsentPermission Permission { get; set; } = new ConsentPermission();
    }

    public class ConsentPurpose
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("refUri")]
        public string RefUri { get; set; } = string.Empty;
    }

    public class ConsentPatient
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }

    public class ConsentHiu
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }

    public class ConsentHip
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }

    public class CareContext
    {
        [JsonPropertyName("patientReference")]
        public string PatientReference { get; set; } = string.Empty;

        [JsonPropertyName("careContextReference")]
        public string CareContextReference { get; set; } = string.Empty;
    }

    public class ConsentRequester
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("identifier")]
        public ConsentRequesterIdentifier Identifier { get; set; } = new ConsentRequesterIdentifier();
    }

    public class ConsentRequesterIdentifier
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;

        [JsonPropertyName("system")]
        public string System { get; set; } = string.Empty;
    }

    public class ConsentPermission
    {
        [JsonPropertyName("accessMode")]
        public string AccessMode { get; set; } = string.Empty;

        [JsonPropertyName("dateRange")]
        public DateRange DateRange { get; set; } = new DateRange();

        [JsonPropertyName("dataEraseAt")]
        public string DataEraseAt { get; set; } = string.Empty;

        [JsonPropertyName("frequency")]
        public ConsentFrequency Frequency { get; set; } = new ConsentFrequency();
    }

    public class DateRange
    {
        [JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;

        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;
    }

    public class ConsentFrequency
    {
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("repeats")]
        public int Repeats { get; set; }
    }

    public class ConsentRequestStatusRequest
    {
        [JsonPropertyName("consentRequestId")]
        public string ConsentRequestId { get; set; } = string.Empty;
    }

    public class ConsentHiuOnNotifyRequest
    {
        [JsonPropertyName("acknowledgement")]
        public List<ConsentAcknowledgement> Acknowledgement { get; set; } = new List<ConsentAcknowledgement>();

        [JsonPropertyName("response")]
        public ConsentOnNotifyResponse Response { get; set; } = new ConsentOnNotifyResponse();
    }

    public class ConsentAcknowledgement
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("consentId")]
        public string ConsentId { get; set; } = string.Empty;
    }

    public class ConsentOnNotifyResponse
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
    }

    public class ConsentFetchRequest
    {
        [JsonPropertyName("consentId")]
        public string ConsentId { get; set; } = string.Empty;
    }

    public class HiuHealthInformationRequest
    {
        [JsonPropertyName("hiRequest")]
        public HiRequestDetail HiRequest { get; set; } = new HiRequestDetail();
    }

    public class HiRequestDetail
    {
        [JsonPropertyName("consent")]
        public ConsentPatient Consent { get; set; } = new ConsentPatient(); // { "id": "..." } - reuse same shape

        [JsonPropertyName("dateRange")]
        public DateRange DateRange { get; set; } = new DateRange();

        [JsonPropertyName("dataPushUrl")]
        public string DataPushUrl { get; set; } = string.Empty;

        [JsonPropertyName("keyMaterial")]
        public KeyMaterial KeyMaterial { get; set; } = new KeyMaterial();
    }

    public class KeyMaterial
    {
        [JsonPropertyName("cryptoAlg")]
        public string CryptoAlg { get; set; } = string.Empty;

        [JsonPropertyName("curve")]
        public string Curve { get; set; } = string.Empty;

        [JsonPropertyName("dhPublicKey")]
        public DhPublicKey DhPublicKey { get; set; } = new DhPublicKey();

        [JsonPropertyName("nonce")]
        public string Nonce { get; set; } = string.Empty;
    }

    public class DhPublicKey
    {
        [JsonPropertyName("expiry")]
        public string Expiry { get; set; } = string.Empty;

        [JsonPropertyName("parameters")]
        public string Parameters { get; set; } = string.Empty;

        [JsonPropertyName("keyValue")]
        public string KeyValue { get; set; } = string.Empty;
    }

    public class DataFlowNotificationRequest
    {
        [JsonPropertyName("notification")]
        public DataFlowNotification Notification { get; set; } = new DataFlowNotification();
    }

    public class DataFlowNotification
    {
        [JsonPropertyName("consentId")]
        public string ConsentId { get; set; } = string.Empty;

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; } = string.Empty;

        [JsonPropertyName("doneAt")]
        public string DoneAt { get; set; } = string.Empty;

        [JsonPropertyName("notifier")]
        public Notifier Notifier { get; set; } = new Notifier();

        [JsonPropertyName("statusNotification")]
        public StatusNotification StatusNotification { get; set; } = new StatusNotification();
    }

    public class Notifier
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty; // "HIU"

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }

    public class StatusNotification
    {
        [JsonPropertyName("sessionStatus")]
        public string SessionStatus { get; set; } = string.Empty;

        [JsonPropertyName("hipId")]
        public string HipId { get; set; } = string.Empty;

        [JsonPropertyName("statusResponses")]
        public List<StatusResponse> StatusResponses { get; set; } = new List<StatusResponse>();
    }

    public class StatusResponse
    {
        [JsonPropertyName("careContextReference")]
        public string CareContextReference { get; set; } = string.Empty;

        [JsonPropertyName("hiStatus")]
        public string HiStatus { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }

}
