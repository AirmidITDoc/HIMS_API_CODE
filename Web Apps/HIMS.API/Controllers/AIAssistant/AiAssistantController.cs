using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.AIAssistant;
using HIMS.Core.Domain.AIAssistant;
using HIMS.Services.AIAssistant;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.AIAssistant
{
    [Route("api/v{version:apiVersion}/ai-assistant")]
    [ApiController]
    [ApiVersion("1")]
    public class AiAssistantController : BaseController
    {
        private readonly IAiAssistantService _aiAssistantService;

        public AiAssistantController(IAiAssistantService aiAssistantService)
        {
            _aiAssistantService = aiAssistantService;
        }

        [HttpGet("capabilities")]
        [Permission]
        public async Task<IActionResult> Capabilities()
        {
            var requestId = HttpContext.TraceIdentifier;
            var result = await _aiAssistantService.GetCapabilitiesAsync(CurrentUserId, Context.UnitId, Context.StoreId, requestId);
            return ToActionResult(result, requestId,
                mapData: data => new AiAssistantCapabilitiesResponse
                {
                    Enabled = data.Enabled,
                    SupportedLanguages = data.SupportedLanguages,
                    InputModes = data.InputModes,
                    Topics = data.Topics,
                    MaxMessageLength = data.MaxMessageLength
                });
        }

        [HttpPost("chat")]
        [Permission]
        public async Task<IActionResult> Chat([FromBody] AiAssistantChatRequest request)
        {
            var requestId = HttpContext.TraceIdentifier;
            if (request == null)
            {
                return StatusCode(400, ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid request.", new AiAssistantErrorData
                {
                    Code = "AI_ASSISTANT_INVALID_REQUEST",
                    RequestId = requestId
                }));
            }

            var input = new AiAssistantChatInput
            {
                UserId = CurrentUserId,
                UnitId = Context.UnitId,
                StoreId = Context.StoreId,
                RequestId = requestId,
                ConversationId = request.ConversationId,
                Message = request.Message,
                Language = request.Language,
                Context = request.Context == null ? null : new AiAssistantInputContext
                {
                    Source = request.Context.Source,
                    ActiveDashboard = request.Context.ActiveDashboard,
                    InputMode = request.Context.InputMode
                }
            };

            var result = await _aiAssistantService.ChatAsync(input);
            return ToActionResult(result, requestId,
                mapData: data => new AiAssistantChatResponse
                {
                    ConversationId = data.ConversationId,
                    RequestId = data.RequestId,
                    Reply = new AiAssistantReply
                    {
                        Id = data.Reply.Id,
                        Role = data.Reply.Role,
                        Text = data.Reply.Text,
                        Language = data.Reply.Language,
                        CreatedAt = data.Reply.CreatedAt
                    },
                    Sources = data.Sources.Select(s => new AiAssistantSource
                    {
                        Label = s.Label,
                        PeriodStart = s.PeriodStart,
                        PeriodEnd = s.PeriodEnd,
                        AsOf = s.AsOf
                    }).ToList()
                });
        }

        private IActionResult ToActionResult<T>(AiAssistantServiceResult<T> result, string requestId, Func<T, object> mapData) where T : class
        {
            if (result.Success && result.Data != null)
            {
                var response = ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, result.Message, mapData(result.Data));
                return StatusCode(200, response);
            }

            var statusCode = result.HttpStatusCode;
            var apiStatusCode = Enum.IsDefined(typeof(ApiStatusCode), statusCode)
                ? (ApiStatusCode)statusCode
                : ApiStatusCode.Status500InternalServerError;

            var errorData = new AiAssistantErrorData
            {
                Code = string.IsNullOrWhiteSpace(result.ErrorCode) ? "AI_ASSISTANT_PROVIDER_UNAVAILABLE" : result.ErrorCode,
                RequestId = requestId,
                RetryAfterSeconds = result.RetryAfterSeconds
            };

            return StatusCode(statusCode, ApiResponseHelper.GenerateResponse(apiStatusCode, result.Message, errorData));
        }
    }
}
