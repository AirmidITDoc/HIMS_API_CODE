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

namespace HIMS.API.Controllers.Masters.PathologyMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathUnitMasterController : BaseController
    {
        private readonly IGenericService<MPathUnitMaster> _repository;
        public PathUnitMasterController(IGenericService<MPathUnitMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "PathUnitMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathUnitMaster> PathUnitMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PathUnitMasterList.ToGridResponse(objGrid, "PathUnitMaster  List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "PathUnitMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.UnitId == id);
            return data.ToSingleResponse<MPathUnitMaster, PathUnitMasterModel>("PathUnitMaster");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "PathUnitMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PathUnitMasterModel obj)
        {
            MPathUnitMaster model = obj.MapTo<MPathUnitMaster>();
            model.IsActive = true;
            if (obj.UnitId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathUnitMaster added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "PathUnitMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PathUnitMasterModel obj)
        {
            MPathUnitMaster model = obj.MapTo<MPathUnitMaster>();
            model.IsActive = true;
            if (obj.UnitId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathUnitMaster  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "PathUnitMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MPathUnitMaster model = await _repository.GetById(x => x.UnitId == Id);
            if ((model?.UnitId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathUnitMaster  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
