using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
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
    public class RadiologyTestController : BaseController
    {
        private readonly IRadiologyTestService _RadiologyTestService;
        public RadiologyTestController(IRadiologyTestService repository)
        {
            _RadiologyTestService = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            List<MRadiologyTestMaster> RadiologyTestMasterList = await _RadiologyTestService.GetAllRadiologyTest();
            return Ok(RadiologyTestMasterList.ToList());
        }
        //List API Get By Id
        //[HttpGet("{id?}")]
        ////[Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        //public async Task<ApiResponse> Get(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _RadiologyTestService.GetByIdRadiologyTest(id);
        //    return data.ToSingleResponse<MRadiologyTestMaster, RadiologyTestModel>("PatientType");
        //}
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
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiology TestName  added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            if (obj.TestId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _RadiologyTestService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiology TestName updated successfully.");
        }
        [HttpPost("RadilogyCancel")]
        //[Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(PathTestDetDelete obj)
        {
            MRadiologyTestMaster model = new();
            if (obj.TestId != 0)
            {
                model.TestId = obj.TestId;
                model.ModifiedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _RadiologyTestService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiology Canceled successfully.");
        }
    }
}
