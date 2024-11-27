using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.Inventory.PathTestDetailModelModelValidator;

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
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PathTestMasterModel obj)
        {
            MPathTestMaster model = obj.MapTo<MPathTestMaster>();
            if (obj.TestId == 0)
            {
                model.TestTime = Convert.ToDateTime(obj.TestTime);
                model.AddedBy = CurrentUserId;
                model.IsActive = true;
                await _ITestmasterService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test Name added successfully.");
        }

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(PathTestMasterModel obj)
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

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PathTestMasterModel obj)
        {
            MPathTestMaster model = obj.MapTo<MPathTestMaster>();
            if (obj.TestId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.TestTime = Convert.ToDateTime(obj.TestTime);
                await _ITestmasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test Name  updated successfully.");
        }
        [HttpPost("PathTestCanceled")]
        //[Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(PathTestDetDelete obj)
        {
            MPathTestMaster model = new();
            if (obj.TestId != 0)
            {
                model.TestId = obj.TestId;
                model.ModifiedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _ITestmasterService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathTest Canceled successfully.");
        }
    }
}
