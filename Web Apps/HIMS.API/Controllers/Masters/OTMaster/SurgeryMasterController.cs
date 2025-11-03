using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.SurgeryMasterController
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SurgeryMasterController : BaseController
    {
        private readonly IGenericService<MOtSurgeryMaster> _repository;
        public SurgeryMasterController(IGenericService<MOtSurgeryMaster> repository)
        {
            _repository = repository;
        }


        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MOtSurgeryMaster> MSurgeryMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MSurgeryMasterList.ToGridResponse(objGrid, "SurgeryMaster List"));
        }
        [HttpGet("GetSurgeryTypeByOTSurgery/{id?}")]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetOt(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SurgeryId == id);
            return data.ToSingleResponse<MOtSurgeryMaster, SurgeryModel>("MOtSurgeryMaster");
        }


        [HttpGet("{id?}")]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SurgeryId == id);
            return data.ToSingleResponse<MOtSurgeryMaster, SurgeryMasterModel>("SurgeryMaster");
        }
        //Insert API
        [HttpPost]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(SurgeryMasterModel obj)
        {
            MOtSurgeryMaster model = obj.MapTo<MOtSurgeryMaster>();
            model.IsActive = true;
            if (obj.SurgeryId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SurgeryMasterModel obj)
        {
            MOtSurgeryMaster model = obj.MapTo<MOtSurgeryMaster>();
            model.IsActive = true;
            if (obj.SurgeryId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;

                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        //Delete API
        [HttpDelete]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MOtSurgeryMaster? model = await _repository.GetById(x => x.SurgeryId == Id);
            if ((model?.SurgeryId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}