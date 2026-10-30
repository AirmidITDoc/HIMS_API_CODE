using HIMS.Core.Domain.AIAssistant;

namespace HIMS.Services.AIAssistant
{
    public interface IAiAssistantService
    {
        Task<AiAssistantServiceResult<AiAssistantCapabilitiesResult>> GetCapabilitiesAsync(int userId, int unitId, int storeId, string requestId);
        Task<AiAssistantServiceResult<AiAssistantChatResult>> ChatAsync(AiAssistantChatInput input);
    }
}
