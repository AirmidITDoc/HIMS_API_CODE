using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
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
    public class PathSpecimenCollectionMasterController : BaseController
    {
        private readonly IGenericService<MPathSpecimenCollectionMaster> _repository;
        public PathSpecimenCollectionMasterController(IGenericService<MPathSpecimenCollectionMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PathSpecimenCollectionMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathSpecimenCollectionMaster> PathSpecimenCollectionMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PathSpecimenCollectionMasterList.ToGridResponse(objGrid, "PathSpecimenCollectionMaster  List"));
        }


        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PathSpecimenCollectionMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SpecimenCollectionId == id);
            return data.ToSingleResponse<MPathSpecimenCollectionMaster, PathSpecimenCollectionMasterModel>("PathSpecimenCollectionMaster");
        }
        [HttpGet]


        //Add API
        [HttpPost]
        //[Permission(PageCode = "PathSpecimenCollectionMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PathSpecimenCollectionMasterModel obj)
        {
            MPathSpecimenCollectionMaster model = obj.MapTo<MPathSpecimenCollectionMaster>();
            model.IsActive = true;
            if (obj.SpecimenCollectionId == 0)
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
        //[Permission(PageCode = "PathSpecimenCollectionMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PathSpecimenCollectionMasterModel obj)
        {
            MPathSpecimenCollectionMaster model = obj.MapTo<MPathSpecimenCollectionMaster>();
            model.IsActive = true;
            if (obj.SpecimenCollectionId == 0)
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
        //[Permission(PageCode = "PathSpecimenCollectionMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MPathSpecimenCollectionMaster model = await _repository.GetById(x => x.SpecimenCollectionId == Id);
            if ((model?.SpecimenCollectionId ?? 0) > 0)
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
