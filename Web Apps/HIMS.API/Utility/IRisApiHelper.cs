using HIMS.API.Models;

namespace HIMS.API.Utility
{
    public interface IRisApiHelper
    {
        Task<AuthResponse> AuthenticateAsync();
        Task<RisApiResponse> CreateRadiologyOrderAsync(CreateRadiologyOrderRequest request);
        Task<RisApiResponse> UpdateRadiologyOrderAsync(string checkId, UpdateRadiologyOrderRequest request);
        Task<RisApiResponse> DeleteRadiologyOrderAsync(DeleteRadiologyOrderRequest request);
        Task<RisApiResponse> SendPatientHistoryAsync(PatientHistoryRequest request);
    }
}
