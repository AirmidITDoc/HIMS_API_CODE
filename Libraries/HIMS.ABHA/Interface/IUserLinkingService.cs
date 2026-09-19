using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;

namespace HIMS.ABHA.Interface
{
    public interface IUserLinkingService
    {
        Task<ApiResult<HttpResponseMessage>> OnDiscoverAsync(string TransactionId,string AbhaAddress, string RequestId);
        Task<ApiResult<HttpResponseMessage>> OnLinkInitAsync(LinkOnInitRequest request);
        Task<ApiResult<HttpResponseMessage>> OnLinkConfirmAsync(LinkOnConfirmRequest request);
    }
}
