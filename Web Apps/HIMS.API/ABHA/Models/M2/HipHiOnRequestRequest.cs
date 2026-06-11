namespace HIMS.API.ABHA.Models.M2
{
    public class HipHiOnRequestRequest
    {
        public HiRequestDetail HiRequest { get; set; } = new HiRequestDetail();
        public ResponseRef Response { get; set; } = new ResponseRef();
    }

    public class HiRequestDetail
    {
        public string TransactionId { get; set; } = string.Empty;
        public string SessionStatus { get; set; } = string.Empty;
    }
}
