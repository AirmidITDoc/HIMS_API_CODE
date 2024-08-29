using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathTestMasterController : BaseController
    {
        private readonly ITestMasterServices _ITestmasterService;
        public PathTestMasterController(ITestMasterServices repository)
        {
            _ITestmasterService = repository;
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PathTestMasterModel obj)
        {
            MPathTestMaster model = obj.MapTo<MPathTestMaster>();
            if (obj.TestId == 0)
            {
                model.TestTime = Convert.ToDateTime(obj.TestTime);
                model.AddedBy = CurrentUserId;
                model.IsActive = true;
                await _ITestmasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test Name added successfully.");
        }

    }
}
