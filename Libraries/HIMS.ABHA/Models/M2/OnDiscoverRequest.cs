namespace HIMS.ABHA.Models.M2
{
    public class OnDiscoverRequest
    {
        public string TransactionId { get; set; } = string.Empty;
        public List<PatientLinkEntry> Patient { get; set; } = new List<PatientLinkEntry>();
        public List<string> MatchedBy { get; set; } = new List<string>();
        public ResponseRef Response { get; set; } = new ResponseRef();
        public string XCmId { get; set; } = string.Empty;
    }

    public class ResponseRef
    {
        public string RequestId { get; set; } = string.Empty;
    }


    public class OnDiscoverRequestDto
    {
        public string transactionId { get; set; }
        public PatientDto[] patient { get; set; }
        public string[] matchedBy { get; set; }
        public Response response { get; set; }
    }

    public class Response
    {
        public string requestId { get; set; }
    }

    public class PatientDto
    {
        public string referenceNumber { get; set; }
        public string display { get; set; }
        public Carecontext[] careContexts { get; set; }
        public string hiType { get; set; }
        public int count { get; set; }
    }

    public class Carecontext
    {
        public string referenceNumber { get; set; }
        public string display { get; set; }
    }

}
