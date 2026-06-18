namespace HIMS.ABHA.Models.M2
{
    public class OnDiscoverRequest
    {
        public string TransactionId { get; set; }= string.Empty;
        public List<PatientLinkEntry> Patient { get; set; }= new List<PatientLinkEntry>();
        public List<string> MatchedBy { get; set; }=new List<string>();
        public ResponseRef Response { get; set; } = new ResponseRef();
        public string XCmId { get; set; }= string.Empty;
    }

    public class ResponseRef
    {
        public string RequestId { get; set; }=string.Empty;
    }
}
