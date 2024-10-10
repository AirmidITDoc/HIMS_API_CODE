using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.Nursing;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Nursing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabRequestController : BaseController
    {
        private readonly ILabRequestService _ILabRequestService;
        public LabRequestController(ILabRequestService repository)
        {
            _ILabRequestService = repository;
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(LabRequestModel obj)
        {
            THlabRequest model = obj.MapTo<THlabRequest>();
            if (obj.RequestId == 0)
            {
                model.ReqDate = Convert.ToDateTime(obj.ReqDate);
                model.ReqTime = Convert.ToDateTime(obj.ReqTime);

                await _ILabRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "labRequest added successfully.", model);
        }
    }
}

    

