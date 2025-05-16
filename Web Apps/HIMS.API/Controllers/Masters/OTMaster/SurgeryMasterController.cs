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

namespace HIMS.API.Controllers.Masters.SurgeryMasterController
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SurgeryMasterController : BaseController
    {
        private readonly IGenericService<MSurgeryMaster> _repository;
        public SurgeryMasterController(IGenericService<MSurgeryMaster> repository)
        {
            _repository = repository;
        }



        [HttpPost]
        [Route("[action]")]
      //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MSurgeryMaster> MSurgeryMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MSurgeryMasterList.ToGridResponse(objGrid, "SurgeryMaster List"));
        }

        [HttpGet("{id?}")]
     //   [Permission(PageCode = "AreaMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SurgeryId == id);
            return data.ToSingleResponse<MSurgeryMaster, SurgeryMasterModel>("SurgeryMaster");
        }
        //Insert API
        [HttpPost]
     //   [Permission(PageCode = "AreaMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(SurgeryMasterModel obj)
        {
            MSurgeryMaster model = obj.MapTo<MSurgeryMaster>();
            model.IsActive = true;
            if (obj.SurgeryId == 0)
            {
                model.AddedBy = CurrentUserId;
                model.AddedDateTime = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SurgeryMaster  added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
      //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SurgeryMasterModel obj)
        {
            MSurgeryMaster model = obj.MapTo<MSurgeryMaster>();
            model.IsActive = true;
            if (obj.SurgeryId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedBy = CurrentUserId;
                model.UpdatedDateTime = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SurgeryMaster updated successfully.");
        }


        //Delete API
        [HttpDelete]
        //  [Permission(PageCode = "AreaMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MSurgeryMaster? model = await _repository.GetById(x => x.SurgeryId == Id);
            if ((model?.SurgeryId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Surgery Master deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}