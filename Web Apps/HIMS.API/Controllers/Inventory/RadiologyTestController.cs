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
    public class RadiologyTestController : BaseController
    {
        private readonly IRadiologyTestService _RadiologyTestService;
        public RadiologyTestController(IRadiologyTestService repository)
        {
            _RadiologyTestService = repository;
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            if (obj.TestId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _RadiologyTestService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Test Name added successfully.");
        }
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            if (obj.TestId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                model.Addedby = CurrentUserId;
                model.Updatedby = CurrentUserId;
                await _RadiologyTestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radilogy TestName  added successfully.");
        }
    }
}
