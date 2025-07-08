using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Billing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CampMasterController : BaseController
    {
        private readonly IGenericService<MCampMaster> _repository;
        public CampMasterController(IGenericService<MCampMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "CampMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MCampMaster> CampMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CampMasterList.ToGridResponse(objGrid, "Camp List"));
        }
        [HttpGet("{id?}")]
        //[Permission(PageCode = "CampMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CampId == id);
            return data.ToSingleResponse<MCampMaster,CampMasterModel>("CampMaster");
        }
        [HttpPost]
        //[Permission(PageCode = "CampMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CampMasterModel obj)
        {
            MCampMaster model = obj.MapTo<MCampMaster>();
            model.IsActive = true;
            if (obj.CampId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "CampMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CampMasterModel obj)
        {
            MCampMaster model = obj.MapTo<MCampMaster>();
            model.IsActive = true;
            if (obj.CampId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "CampMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MCampMaster model = await _repository.GetById(x => x.CampId == Id);
            if ((model?.CampId ?? 0) > 0)
            {
                model.IsActive = false;
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
