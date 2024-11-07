using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.NursingStation;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NursingController : BaseController
    {
        private readonly IIPLabRequestService _IIPLabRequestService;
        public NursingController(IIPLabRequestService repository)
        {
            _IIPLabRequestService = repository;
        }

        [HttpPost("LabRequestInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> LabRequestInsert(IPLabRequestModel obj)
        {
            THlabRequest model = obj.MapTo<THlabRequest>();
            //TDlabRequest objTDlabRequest = obj.TDLabRequest.MapTo<TDlabRequest>();
            if (obj.RequestId == 0)
            {
                model.ReqTime = Convert.ToDateTime(obj.ReqTime);
              
                await _IIPLabRequestService.InsertAsyncSP(model,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "labRequest added successfully.");
        }
    }
}
