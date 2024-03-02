using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.Masters
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PrefixController : BaseController
    {
        private readonly IGenericService<DbPrefixMaster> _repository;
        public PrefixController(IGenericService<DbPrefixMaster> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "Prefix", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DbPrefixMaster> DocList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DocList.ToGridResponse(objGrid, "Prefix List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "Prefix", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.PrefixId == id);
            return data.ToSingleResponse<DbPrefixMaster, PrefixModel>("Prefix");
        }

        [HttpPost]
        [Permission(PageCode = "Prefix", Permission = PagePermission.Add)]
        public async Task<ApiResponse> post(PrefixModel obj)
        {
            DbPrefixMaster model = obj.MapTo<DbPrefixMaster>();
            model.IsActive = 1;
            if (obj.PrefixId == 0)
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prefix added successfully.");
        }
        [HttpPut("{id:int}")]
        [Permission(PageCode = "Prefix", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PrefixModel obj)
        {
            DbPrefixMaster model = obj.MapTo<DbPrefixMaster>();
            model.IsActive = 1;
            if (obj.PrefixId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
                await _repository.Update(model, CurrentUserId, CurrentUserName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prefix updated successfully.");
        }
        [HttpDelete]
        [Permission(PageCode = "Prefix", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            await _repository.SoftDelete(Id, CurrentUserId, CurrentUserName, x => x.PrefixId == Id);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prefix deleted successfully.");
        }
    }
}
