namespace HIMS.API.ABHA.Configuration
{
    public class ABDMSettings
    {
        public string Environment { get; set; } = "Sandbox";
        public BaseUrls BaseUrls { get; set; } = new();
        public Credentials Credentials { get; set; } = new();
        public Endpoints Endpoints { get; set; } = new();
        public int TokenCacheMinutes { get; set; } = 18;
        public int RequestTimeoutSeconds { get; set; } = 60;
        public int RetryCount { get; set; } = 3;
        public bool EnableRequestLogging { get; set; } = true;
    }

    public class BaseUrls
    {
        public string GatewayBaseUrl { get; set; } = string.Empty;
        public string AbhaBaseUrl { get; set; } = string.Empty;
        public string AbhaProfileBaseUrl { get; set; } = string.Empty;
        public string PhrBaseUrl { get; set; } = string.Empty;
    }

    public class Credentials
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string GrantType { get; set; } = "client_credentials";
        public string XCmId { get; set; } = "sbx";
    }

    public class Endpoints
    {
        public string SessionToken { get; set; } = string.Empty;
        public string PublicCertificate { get; set; } = string.Empty;
        public string AadhaarOtpRequest { get; set; } = string.Empty;
        public string AadhaarOtpVerify { get; set; } = string.Empty;
        public string OtpRequest { get; set; } = string.Empty;
        public string OtpVerify { get; set; } = string.Empty;
        public string AbhaAddressSuggestion { get; set; } = string.Empty;
        public string CreateAbhaAddress { get; set; } = string.Empty;
        public string AbhaProfile { get; set; } = string.Empty;
        public string AbhaCard { get; set; } = string.Empty;
        public string AbhaQrCode { get; set; } = string.Empty;
        public string SearchByMobile { get; set; } = string.Empty;
        public string SearchByAbhaNumber { get; set; } = string.Empty;
        public string LinkingTokenRequest { get; set; } = string.Empty;
        public string LinkingTokenConfirm { get; set; } = string.Empty;
        public string CareContextNotify { get; set; } = string.Empty;
        public string VerifyUser { get; set; } = string.Empty;
    }
}