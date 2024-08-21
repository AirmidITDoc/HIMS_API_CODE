using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RadiologyTestController : BaseController
    {
        private readonly IGenericService<MRadiologyTestMaster> _repository;
        public RadiologyTestController(IGenericService<MRadiologyTestMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MRadiologyTestMaster> RadiologyTestList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(RadiologyTestList.ToGridResponse(objGrid, "RadiologyTest List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.TestId == id);
            return data.ToSingleResponse<MRadiologyTestMaster, RadiologyTestModel>("RadiologyTestMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            model.IsActive = true;
            if (obj.TestId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiology test Name added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(RadiologyTestModel obj)
        {
            MRadiologyTestMaster model = obj.MapTo<MRadiologyTestMaster>();
            model.IsActive = true;
            if (obj.TestId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiology Name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MRadiologyTestMaster model = await _repository.GetById(x => x.TestId == Id);
            if ((model?.TestId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient Type deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
