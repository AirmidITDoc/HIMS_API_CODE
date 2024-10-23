using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Transaction;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using HIMS.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Transaction
{
    public class smsConfigController : BaseController
    {
        private readonly IsmsConfigService _IsmsConfigService;
        public smsConfigController(IsmsConfigService repository)
        {
            _IsmsConfigService = repository;
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(smsConfigModel obj)
        {
            SsSmsConfig model = obj.MapTo<SsSmsConfig>();
            if (obj.Routeid == 0)
            {
                //model.AppDate = Convert.ToDateTime(obj.AppDate);

                await _IsmsConfigService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SmsConfig added successfully.", model);
        }
        [HttpPost("UPDATESP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Edit(smsConfigModel obj)
        {
            SsSmsConfig model = obj.MapTo<SsSmsConfig>();
            if (obj.Routeid == 0)
            {
                //model.AppDate = Convert.ToDateTime(obj.AppDate);

                await _IsmsConfigService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SmsConfig Updated successfully.", model);
        }
    }
}
