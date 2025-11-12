using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Models.Administration;
using HIMS.Api.Models.Common;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class WhatsAppEmailController : BaseController
    {
        private readonly IWhatsAppEmailService _whatsAppEmailService;

        public WhatsAppEmailController(IWhatsAppEmailService repository)
        {
            _whatsAppEmailService = repository;

        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "WhatsApp", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Posts(WhatsAppModel obj)
        {
            TWhatsAppSmsOutgoing model = obj.MapTo<TWhatsAppSmsOutgoing>();

          if (obj.SmsoutGoingId == 0)
            {

             
                model.CreatedBy = CurrentUserId;
                await _whatsAppEmailService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record  added successfully.", model.SmsoutGoingId);
        }
    }
}
