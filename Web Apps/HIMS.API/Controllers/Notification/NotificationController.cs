using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using HIMS.Services.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

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
        
    }
}
