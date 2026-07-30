using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models.M2
{
    public class LinkTokenRequest
    {
        [JsonPropertyName("abhaNumber")]
        public long AbhaNumber { get; set; }
        [JsonPropertyName("abhaAddress")]
        public string AbhaAddress { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;
        [JsonPropertyName("yearOfBirth")]
        public int YearOfBirth { get; set; }
        //public string HipId { get; set; }= string.Empty;
        //public string XCmId { get; set; }=string.Empty;
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
    public class LinkTokenResponseModel
    {
        public int CallbackId { get; set; }

        public string AbhaNumber { get; set; }

        public string AbhaAddress { get; set; }

        public string Name { get; set; }

        public int? YearOfBirth { get; set; }

        public DateTime? RequestOn { get; set; }

        public string RequestId { get; set; }

        public string LinkToken { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
