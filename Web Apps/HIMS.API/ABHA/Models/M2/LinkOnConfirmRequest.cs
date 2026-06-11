namespace HIMS.API.ABHA.Models.M2
{
    public class LinkOnConfirmRequest
    {
        public List<PatientLinkEntry> Patient { get; set; } = new List<PatientLinkEntry>();
        public ResponseRef Response { get; set; } = new ResponseRef();
    }
    public class ConsentHipOnNotifyRequest
    {
        public ConsentAcknowledgement Acknowledgement { get; set; } = new ConsentAcknowledgement();
        public ResponseRef Response { get; set; } = new ResponseRef();
    }

    public class ConsentAcknowledgement
    {
        public string Status { get; set; } = string.Empty;
        public string ConsentId { get; set; } = string.Empty;
    }
}
