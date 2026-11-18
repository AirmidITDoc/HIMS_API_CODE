namespace HIMS.API.Infrastructure
{
    public class AuthenticationSettings
    {
        public string APIClientId { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
