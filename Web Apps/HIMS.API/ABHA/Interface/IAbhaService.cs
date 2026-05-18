using HIMS.API.ABHA.Models;

namespace HIMS.API.ABHA.Interface
{
    public interface IAbhaService
    {
        // Aadhaar based ABHA creation flow
        Task<ApiResult<OtpResponse>> RequestAadhaarOtpAsync(string aadhaarNumber);
        Task<ApiResult<AbhaEnrolmentResponse>> VerifyAadhaarOtpAsync(string txnId, string otp, string? mobile = null);

        // Mobile linking flow
        Task<ApiResult<OtpResponse>> RequestMobileOtpAsync(string txnId, string mobile);
        Task<ApiResult<AbhaEnrolmentResponse>> VerifyMobileOtpAsync(string txnId, string otp);

        // ABHA address
        Task<ApiResult<AbhaAddressSuggestionResponse>> GetAbhaAddressSuggestionsAsync(string txnId);
        Task<ApiResult<AbhaEnrolmentResponse>> CreateAbhaAddressAsync(string txnId, string abhaAddress);

        // Profile / Card / QR
        Task<ApiResult<AbhaProfile>> GetAbhaProfileAsync(string xToken);
        Task<ApiResult<byte[]>> GetAbhaCardAsync(string xToken);
        Task<ApiResult<byte[]>> GetAbhaQrCodeAsync(string xToken);
    }
}
