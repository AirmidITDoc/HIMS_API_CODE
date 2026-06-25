using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;
using HIMS.ABHA.Models.M3;

namespace HIMS.ABHA.Interface
{
    public interface IM3Service
    {
        Task<ApiResult<string>> UpdateBridgeUrlAsync(UpdateBridgeUrlRequest request, string cmId);
        Task<ApiResult<BridgeServiceDto>> FindBridgeServiceByServiceIdAsync(string serviceId, string cmId);
        Task<ApiResult<BridgeResponseDto>> FindServicesByBridgeIdAsync(string cmId);
        Task<ApiResult<string>> GetCertsAsync(string cmId);
        Task<ApiResult<string>> GetOpenIdConfigurationAsync(string cmId);
        Task<ApiResult<string>> ConsentInitRequestAsync(ConsentInitRequest request, string cmId);
        Task<ApiResult<string>> ConsentRequestStatusAsync(ConsentRequestStatusRequest request, string cmId, string hiuId);
        Task<ApiResult<string>> ConsentHiuOnNotifyAsync(ConsentHiuOnNotifyRequest request, string cmId);
        Task<ApiResult<string>> ConsentFetchAsync(ConsentFetchRequest request, string cmId, string hiuId);
        Task<ApiResult<string>> HiuHealthInformationRequestAsync(HiuHealthInformationRequest request, string cmId, string hiuId);
        Task<ApiResult<string>> DataFlowNotificationAsync(DataFlowNotificationRequest request, string cmId);
    }

}
