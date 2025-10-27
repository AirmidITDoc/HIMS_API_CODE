using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.AmbulanceMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AmbulanceController : BaseController
    {

        private readonly IGenericService<MVehicleMaster> _repository;
        public AmbulanceController(IGenericService<MVehicleMaster> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "CityMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MVehicleMaster> MVechicleList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MVechicleList.ToGridResponse(objGrid, "Vechicle List"));
        }

        [HttpGet("{id?}")]
        //[Permission(PageCode = "CityMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.VehicleId == id);
            return data.ToSingleResponse<MVehicleMaster, CityMasterModel>("CityMaster");
        }

        [HttpPost]
        //[Permission(PageCode = "CityMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(VechicleModel obj)
        {
            MVehicleMaster model = obj.MapTo<MVehicleMaster>();
            model.IsActive = true;
            if (obj.VehicleId == 0)
            {
                //model.CopyProperties = CurrentUserId;
                //model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "CityMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(VechicleModel obj)
        {
            MVehicleMaster model = obj.MapTo<MVehicleMaster>();
            if (obj.VehicleId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "CityMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MVehicleMaster model = await _repository.GetById(x => x.VehicleId == Id);
            if ((model?.VehicleId ?? 0) > 0)
            {
                model.IsActive = false;
                //model.ModifiedBy = CurrentUserId;
                //model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}

