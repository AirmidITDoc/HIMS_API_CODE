using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using Asp.Versioning;

namespace HIMS.API.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SubGroupMasterController : BaseController
    {
        private readonly IGenericService<MSubGroupMaster> _repository;
        public SubGroupMasterController(IGenericService<MSubGroupMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "SubGroupMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MSubGroupMaster> MSubGroupMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MSubGroupMasterList.ToGridResponse(objGrid, "MSubGroupMaster List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "SubGroupMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SubGroupId == id);
            return data.ToSingleResponse<MSubGroupMaster, SubGroupMasterModel>("MSubGroupMaster");
        }


        [HttpPost]
        [Permission(PageCode = "SubGroupMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(SubGroupMasterModel obj)
        {
            MSubGroupMaster model = obj.MapTo<MSubGroupMaster>();
            model.IsActive = true;
            if (obj.SubGroupId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SubGroup added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "SubGroupMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SubGroupMasterModel obj)
        {
            MSubGroupMaster model = obj.MapTo<MSubGroupMaster>();
            model.IsActive = true;
            if (obj.SubGroupId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SubGroup updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "SubGroupMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MSubGroupMaster model = await _repository.GetById(x => x.SubGroupId == Id);
            if ((model?.SubGroupId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SubGroup deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }



    }
}
