using HIMS.ABHA.Models;

namespace HIMS.ABHA.Interface
{
    public interface IAbhaService
    {
        // Aadhaar based ABHA creation flow
        Task<ApiResult<OtpResponse>> RequestAadhaarOtpAsync(string aadhaarNumber);
        Task<ApiResult<AbhaEnrolmentResponse>> VerifyAadhaarOtpAsync(string txnId, string otp, string? mobile = null);

        // Mobile linking flow
        Task<ApiResult<OtpResponse>> RequestOtpAsync(string aadhaarNumber, List<string> scope, string LoginHint, string OtpSystem);
        Task<ApiResult<VerifyOtpResponse>> VerifyOtpAsync(string txnId, string otp, List<string> scope,bool isAbhaAddress=false);
        Task<ApiResult<OtpResponse>> RequestOtpAbhaFindAsync(string aadhaarNumber, List<string> scope, string LoginHint, string OtpSystem,string txnId );
        // ABHA address
        Task<ApiResult<AbhaAddressSuggestionResponse>> GetAbhaAddressSuggestionsAsync(string txnId);
        Task<ApiResult<AbhaEnrolmentResponse>> CreateAbhaAddressAsync(string txnId, string abhaAddress);
        Task<ApiResult<List<FindAbhaResponse>>> FindAbhaMobileAsync(string mobile);

        // Profile / Card / QR
        Task<ApiResult<AbhaProfile>> GetAbhaProfileAsync(string xToken,bool IsAddress);
        Task<ApiResult<byte[]>> GetAbhaCardAsync(string xToken);
        Task<ApiResult<byte[]>> GetAbhaQrCodeAsync(string xToken,bool IsAddress);
        Task<ApiResult<VerifyUserResponse>> VerifyUser(VerifyUserRequest obj);

    }
}
