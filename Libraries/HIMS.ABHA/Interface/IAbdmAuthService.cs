using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;

namespace HIMS.ABHA.Interface
{
    public interface IAbdmAuthService
    {
        // Task<SessionResponse> GetSessionAsync(SessionRequest request);
        Task<ApiResult<string>> UpdateBridgeUrlAsync(UpdateBridgeUrlRequest request);
        Task<ApiResult<string>> RegisterBridgeServicesAsync(RegisterBridgeRequest request);
        Task<ApiResult<BridgeServiceDto>> FindServiceByServiceIdAsync(string serviceId);
        Task<ApiResult<BridgeResponseDto>> FindServicesByBridgeIdAsync();
    }
}
