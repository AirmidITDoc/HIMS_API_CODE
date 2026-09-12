namespace HIMS.API.Models.AbhaIntegration
{

    public class InitiateClientModel
    {
        public int clientId { get; set; }
        public int hospitalId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string requestBy { get; set; }
        public string requestType { get; set; }
        public string callbackUrl { get; set; }
    }

    public class AbhaCallbackModel
    {
        public object patientId { get; set; }
        public string patientName { get; set; }
        public string sbxId { get; set; }
        public string gender { get; set; }
        public string mobileNumber { get; set; }
        public string dob { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string createdDate { get; set; }
        public int clientId { get; set; }
        public string abhaNumber { get; set; }
        public string json { get; set; }
        public string transactionId { get; set; }
    }


}