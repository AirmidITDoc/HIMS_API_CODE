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

namespace HIMS.API.Controllers.Masters.InventoryMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CurrencyMasterController : BaseController
    {
        private readonly IGenericService<MCurrencyMaster> _repository;
        public CurrencyMasterController(IGenericService<MCurrencyMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "CurrencyMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MCurrencyMaster> CurrencyMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CurrencyMasterList.ToGridResponse(objGrid, "Currency Master List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "CurrencyMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CurrencyId == id);
            return data.ToSingleResponse<MCurrencyMaster, CurrencyMasterModel>("CurrencyMaster");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "CurrencyMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CurrencyMasterModel obj)
        {
            MCurrencyMaster model = obj.MapTo<MCurrencyMaster>();
            model.IsActive = true;
            if (obj.CurrencyId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Currency Name added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "CurrencyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CurrencyMasterModel obj)
        {
            MCurrencyMaster model = obj.MapTo<MCurrencyMaster>();
            model.IsActive = true;
            if (obj.CurrencyId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Currenct Name  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "CurrencyMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MCurrencyMaster model = await _repository.GetById(x => x.CurrencyId == Id);
            if ((model?.CurrencyId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Currency name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
