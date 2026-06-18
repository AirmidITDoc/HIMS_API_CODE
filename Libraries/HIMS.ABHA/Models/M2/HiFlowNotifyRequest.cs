namespace HIMS.ABHA.Models.M2
{
    public class HiFlowNotifyRequest
    {
        public HiNotification Notification { get; set; } = new HiNotification();
        public string XCmId { get; set; } = string.Empty;
    }

    public class HiNotification
    {
        public string ConsentId { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string DoneAt { get; set; } = string.Empty;
        public NotifierRef Notifier { get; set; } = new NotifierRef();
        public StatusNotification StatusNotification { get; set; } = new StatusNotification();
    }

    public class NotifierRef
    {
        public string Type { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }

    public class StatusNotification
    {
        public string SessionStatus { get; set; } = string.Empty;
        public string HipId { get; set; } = string.Empty;
        public List<StatusResponse> StatusResponses { get; set; } = new List<StatusResponse>();
    }

    public class StatusResponse
    {
        public string CareContextReference { get; set; } = string.Empty;
        public string HiStatus { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
