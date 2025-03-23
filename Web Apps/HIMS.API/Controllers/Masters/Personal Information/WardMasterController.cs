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
    public class WardMasterController : BaseController

    {
        private readonly IGenericService<RoomMaster> _repository;
        public WardMasterController(IGenericService<RoomMaster> repository)
        {
            _repository = repository;
        }

         //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "WardMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<RoomMaster> RoomMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(RoomMasterList.ToGridResponse(objGrid, "Room List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "WardMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.RoomId == id);
            return data.ToSingleResponse<RoomMaster, WardMasterModel>("Ward master");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "WardMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(WardMasterModel obj)
        {
            RoomMaster model = obj.MapTo<RoomMaster>();
            model.IsActive = true;
            if (obj.RoomId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "WardName added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "WardMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(WardMasterModel obj)
        {
            RoomMaster model = obj.MapTo<RoomMaster>();
            model.IsActive = true;
            if (obj.RoomId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "WardName  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "WardMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            RoomMaster model = await _repository.GetById(x => x.RoomId == Id);
            if ((model?.RoomId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "WardName  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

        [HttpGet]
        [Route("get-WardClass")]
        [Permission(PageCode = "WardMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var WardClassWiseList = await _repository.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Ward Class dropdown List", WardClassWiseList.Select(x => new { x.RoomId, x.ClassId, x.RoomName }));
        }
    }
}
