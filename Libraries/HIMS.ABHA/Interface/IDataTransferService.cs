using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;

namespace HIMS.ABHA.Interface
{
    public interface IDataTransferService
    {
        Task<ApiResult<HttpResponseMessage>> ConsentHipOnNotifyAsync(ConsentHipOnNotifyRequest request);
        Task<ApiResult<HttpResponseMessage>> HipHiOnRequestAsync(HipHiOnRequestRequest request);
        Task<ApiResult<HttpResponseMessage>> HiFlowNotifyAsync(HiFlowNotifyRequest request);
    }
}
