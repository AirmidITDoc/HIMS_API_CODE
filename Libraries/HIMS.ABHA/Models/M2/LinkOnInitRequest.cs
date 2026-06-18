namespace HIMS.ABHA.Models.M2
{
    public class LinkOnInitRequest
    {
        public string TransactionId { get; set; } = string.Empty;
        public LinkInitDetail Link { get; set; } = new LinkInitDetail();
        public ResponseRef Response { get; set; } = new ResponseRef();
        public string XCmId { get; set; } = string.Empty;
    }

    public class LinkInitDetail
    {
        public string ReferenceNumber { get; set; }= string.Empty;
        public string AuthenticationType { get; set; }=string.Empty;
        public LinkMeta Meta { get; set; } = new LinkMeta();
    }

    public class LinkMeta
    {
        public string CommunicationMedium { get; set; } = string.Empty;
        public string CommunicationHint { get; set; } = string.Empty;
        public string CommunicationExpiry { get; set; } = string.Empty;
    }
}
