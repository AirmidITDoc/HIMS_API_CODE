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
using HIMS.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PrefixController : BaseController
    {
        private readonly IGenericService<DbPrefixMaster> _repository;
        private readonly IPrefixService _IPrefixService;
        public PrefixController(IGenericService<DbPrefixMaster> repository, IPrefixService prefixService)
        {
            _repository = repository;
            _IPrefixService = prefixService;
        }
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "Prefix", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            //var sessionId = Context.StoreId;
            //var unitId = Context.UnitId;
            IPagedList<DbPrefixMaster> DocList = await _IPrefixService.GetAllPagedAsync(objGrid);
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
        public async Task<ApiResponse> Post(PrefixModel obj)
        {
            DbPrefixMaster model = obj.MapTo<DbPrefixMaster>();
            model.IsActive = true;

            if (obj.PrefixId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("{id:int}")]
        [Permission(PageCode = "Prefix", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PrefixModel obj)
        {
            DbPrefixMaster model = obj.MapTo<DbPrefixMaster>();
            model.IsActive = true;

            if (obj.PrefixId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        [HttpDelete]
        [Permission(PageCode = "Prefix", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            DbPrefixMaster model = await _repository.GetById(x => x.PrefixId == Id);
            if ((model?.PrefixId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
        [HttpGet]
        [Route("get-prefixs")]
        //[Permission(PageCode = "Prefix", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MDepartmentMasterList = await _repository.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Prefix dropdown", MDepartmentMasterList.Select(x => new { x.PrefixId, x.SexId, x.PrefixName }));
        }

        [HttpGet("test-multiple-data")]
        public async Task<ApiResponse> GetTest()
        {

            var data = await _IPrefixService.GetListMultiple();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", data);
        }
    }
}
