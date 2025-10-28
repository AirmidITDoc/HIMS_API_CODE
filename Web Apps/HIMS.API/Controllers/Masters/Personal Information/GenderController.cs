using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class GenderController : BaseController
    {
        private readonly IGenericService<DbGenderMaster> _repository;
        public GenderController(IGenericService<DbGenderMaster> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "Gender", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DbGenderMaster> DocList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DocList.ToGridResponse(objGrid, "Gender List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "Gender", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.GenderId == id);
            return data.ToSingleResponse<DbGenderMaster, GenderModel>("Gender");
        }
        [HttpPost]
        [Permission(PageCode = "Gender", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(GenderModel obj)
        {
            DbGenderMaster model = obj.MapTo<DbGenderMaster>();
            model.IsActive = true;
            if (obj.GenderId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("{id:int}")]
        [Permission(PageCode = "Gender", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(GenderModel obj)
        {
            DbGenderMaster model = obj.MapTo<DbGenderMaster>();
            model.IsActive = true;
            if (obj.GenderId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        [HttpDelete]
        [Permission(PageCode = "Gender", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            DbGenderMaster model = await _repository.GetById(x => x.GenderId == Id);
            if ((model?.GenderId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
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
