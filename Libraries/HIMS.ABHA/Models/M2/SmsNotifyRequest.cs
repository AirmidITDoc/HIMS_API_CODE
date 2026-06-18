namespace HIMS.ABHA.Models.M2
{
    public class SmsNotifyRequest
    {
        public string RequestId { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public SmsNotification Notification { get; set; } = new SmsNotification();
    }

    public class SmsNotification
    {
        public string PhoneNo { get; set; } = string.Empty;
        public HipRef Hip { get; set; } = new HipRef();
    }

    public class HipRef
    {
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }
}
