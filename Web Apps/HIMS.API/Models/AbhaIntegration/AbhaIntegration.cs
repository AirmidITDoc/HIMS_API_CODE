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
        public string? PatientId { get; set; }
        public string PatientName { get; set; }
        public string SbxId { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Dob { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public int ClientId { get; set; }
        public string AbhaNumber { get; set; }
        public string Json { get; set; }
        public string TransactionId { get; set; }
    }


}