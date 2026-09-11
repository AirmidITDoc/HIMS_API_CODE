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
}