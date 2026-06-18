using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2
{
    public class LinkTokenRequest
    {
        public long AbhaNumber { get; set; }
        public string AbhaAddress { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int YearOfBirth { get; set; }
        public string HipId { get; set; }= string.Empty;
        public string XCmId { get; set; }=string.Empty;
    }
    public class LinkTokenResponse
    {
        [JsonPropertyName("abhaAddress")]
        public string AbhaAddress { get; set; } = string.Empty;
        [JsonPropertyName("linkToken")]
        public string LinkToken { get; set; } = string.Empty;
        [JsonPropertyName("expiry")]
        public DateTime Expiry { get; set; }
        [JsonPropertyName("response")]
        public LinkTokenResponseReq Response { get; set; } = new LinkTokenResponseReq();
    }
    public class LinkTokenResponseReq
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;

    }
}
