using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Transaction;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Transaction
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class smsConfigController : BaseController
    {
        private readonly IsmsConfigService _IsmsConfigService;
        private readonly IGenericService<SmsoutGoing> _repository;
        public smsConfigController(IsmsConfigService repository, IGenericService<SmsoutGoing> repository1)
        {
            _IsmsConfigService = repository;
            _repository = repository1;
        }

        [HttpPost("SMSconfigList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SMSList(GridRequestModel objGrid)
        {
            IPagedList<SMSConfigListDto> List = await _IsmsConfigService.GetSMSconfig(objGrid);
            return Ok(List.ToGridResponse(objGrid, "SMS config List"));
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
