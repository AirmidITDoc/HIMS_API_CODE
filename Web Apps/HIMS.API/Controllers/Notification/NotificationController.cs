using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Services.Notification;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Notification
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _repository;
        public NotificationController(INotificationService repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("[action]")]
        [Permission]
        public async Task<ApiResponse> List()
        {
            var List = await _repository.GetNotificationByUser(CurrentUserId, 10);
            var Count = await _repository.UnreadCount(CurrentUserId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Notification List", new { Count, List });
        }
        [HttpPost]
        [Route("read")]
        [Permission]
        public async Task<ApiResponse> ReadNotification([FromBody] long Id)
        {
            await _repository.ReadNotification(Id);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Notification List");
        }

    }
}
