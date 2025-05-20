using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;

namespace HIMS.API.Controllers.Masters.OtTableMaster
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OtTableMasterController : BaseController
    {
        private readonly IGenericService<MOttableMaster> _repository;
        public OtTableMasterController(IGenericService<MOttableMaster> repository)
        {
            _repository = repository;
        }



        [HttpPost]
        [Route("[action]")]
      //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MOttableMaster> MOttableMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MOttableMasterList.ToGridResponse(objGrid, "ottable List"));
        }

        [HttpGet("{id?}")]
     //   [Permission(PageCode = "AreaMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.OttableId == id);
            return data.ToSingleResponse<MOttableMaster, OtTableMasterModel>("OttableMaster");
        }
        //Insert API
        [HttpPost]
     //   [Permission(PageCode = "AreaMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(OtTableMasterModel obj)
        {
            MOttableMaster model = obj.MapTo<MOttableMaster>();
            model.IsActive = true;
            if (obj.OttableId == 0)
            {
                model.IsAddedBy = CurrentUserId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Ottable added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "AreaMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OtTableMasterModel obj)
        {
            MOttableMaster model = obj.MapTo<MOttableMaster>();
            model.IsActive = true;
            if (obj.OttableId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.IsUpdatedBy = CurrentUserId;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Ottable updated successfully.");
        }


        //Delete API
        [HttpDelete]
      //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MOttableMaster? model = await _repository.GetById(x => x.OttableId == Id);
            if ((model?.OttableId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OtTable deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}