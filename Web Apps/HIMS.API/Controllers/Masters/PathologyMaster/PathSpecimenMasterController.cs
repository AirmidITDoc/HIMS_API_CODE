using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.PathologyMaster
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathSpecimenMasterController : BaseController
    {
        private readonly IGenericService<MPathSpecimenMaster> _repository;
        public PathSpecimenMasterController(IGenericService<MPathSpecimenMaster> repository)
        {
            _repository = repository;
        }


        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PathSpecimenMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathSpecimenMaster> PathSpecimenMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PathSpecimenMasterList.ToGridResponse(objGrid, "PathSpecimenMaster  List"));
        }


        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PathSpecimenMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SpecimenId == id);
            return data.ToSingleResponse<MPathSpecimenMaster, PathSpecimenMasterModel>("PathSpecimenMaster");
        }

        //Add API
        [HttpPost]
        //[Permission(PageCode = "PathSpecimenMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PathSpecimenMasterModel obj)
        {
            MPathSpecimenMaster model = obj.MapTo<MPathSpecimenMaster>();
            model.IsActive = true;
            if (obj.SpecimenId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PathSpecimenMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PathSpecimenMasterModel obj)
        {
            MPathSpecimenMaster model = obj.MapTo<MPathSpecimenMaster>();
            model.IsActive = true;
            if (obj.SpecimenId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PathSpecimenMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MPathSpecimenMaster model = await _repository.GetById(x => x.SpecimenId == Id);
            if ((model?.SpecimenId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }


}
