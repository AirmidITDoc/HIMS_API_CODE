using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PhoneAppController : BaseController
    {
        private readonly IPhoneAppService _IPhoneAppService;
        public PhoneAppController(IPhoneAppService repository)
        {
            _IPhoneAppService = repository;
        }

        [HttpPost("PhoneAppInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(phoneAppModel obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            if (obj.PhoneAppId == 0)
            {
                model.AppDate = Convert.ToDateTime(obj.AppDate);
                model.AppTime = Convert.ToDateTime(obj.AppTime);

                model.UpdatedBy = CurrentUserId;
                model = await _IPhoneAppService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp added successfully.", model);
        }
    }
}

