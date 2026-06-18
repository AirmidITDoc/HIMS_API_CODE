using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;

namespace HIMS.ABHA.Interface
{
    public interface IHipLinkingService
    {
        Task<ApiResult<LinkTokenResponse>> GenerateLinkTokenAsync(LinkTokenRequest request);
        Task<ApiResult<string>> LinkCareContextAsync(LinkCareContextRequest request);
        Task<ApiResult<string>> LinkContextNotifyAsync(object request, string hipId, string linkToken, string xCmId);
    }
}
