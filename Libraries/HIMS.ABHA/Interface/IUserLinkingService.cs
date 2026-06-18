using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;

namespace HIMS.ABHA.Interface
{
    public interface IUserLinkingService
    {
        Task<ApiResult<HttpResponseMessage>> OnDiscoverAsync(OnDiscoverRequest request);
        Task<ApiResult<HttpResponseMessage>> OnLinkInitAsync(LinkOnInitRequest request);
        Task<ApiResult<HttpResponseMessage>> OnLinkConfirmAsync(LinkOnConfirmRequest request);
    }
}
