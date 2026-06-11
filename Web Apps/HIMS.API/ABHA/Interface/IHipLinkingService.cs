using HIMS.API.ABHA.Models;
using HIMS.API.ABHA.Models.M2;

namespace HIMS.API.ABHA.Interface
{
    public interface IHipLinkingService
    {
        Task<ApiResult<LinkTokenResponse>> GenerateLinkTokenAsync(LinkTokenRequest request, string hipId, string xCmId);
        Task<ApiResult<string>> LinkCareContextAsync(LinkCareContextRequest request, string hipId, string linkToken, string xCmId);
        Task<ApiResult<string>> LinkContextNotifyAsync(object request, string hipId, string linkToken, string xCmId);
    }
}
