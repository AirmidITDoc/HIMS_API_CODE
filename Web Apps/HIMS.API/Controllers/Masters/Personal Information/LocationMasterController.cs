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
    public class LocationMasterController : BaseController
    {
        private readonly IGenericService<LocationMaster> _repository;
        public LocationMasterController(IGenericService<LocationMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
      //  [Permission(PageCode = "LocationMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LocationMaster> LocationMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(LocationMasterList.ToGridResponse(objGrid, "location List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "LocationMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.LocationId == id);
            return data.ToSingleResponse<LocationMaster, LocationMasterModel>("Location Master");
        }
        //Add APID
        [HttpPost]
        //[Permission(PageCode = "LocationMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(LocationMasterModel obj)
        {
            LocationMaster model = obj.MapTo<LocationMaster>();
            model.IsActive = true;
            if (obj.LocationId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Location Name added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
       // [Permission(PageCode = "LocationMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(LocationMasterModel obj)
        {
            LocationMaster model = obj.MapTo<LocationMaster>();
            model.IsActive = true;
            if (obj.LocationId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Location Name updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "LocationMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            LocationMaster model = await _repository.GetById(x => x.LocationId == Id);
            if ((model?.LocationId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Location Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
