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
    public class CashCounterController : BaseController
    {
        private readonly IGenericService<CashCounter> _repository;
        public CashCounterController(IGenericService<CashCounter> repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "CashCounter", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CashCounter> DocList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DocList.ToGridResponse(objGrid, "CashCounter List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "CashCounter", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CashCounterId == id);
            return data.ToSingleResponse<CashCounter, CashCounterModel>("CashCounter");
        }

        [HttpPost]
        [Permission(PageCode = "CashCounter", Permission = PagePermission.Add)]
        public async Task<ApiResponse> post(CashCounterModel obj)
        {
            CashCounter model = obj.MapTo<CashCounter>();
            model.IsActive = true;
            if (obj.CashCounterId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CashCounter added successfully.");
        }
        [HttpPut("{id:int}")]
        [Permission(PageCode = "CashCounter", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CashCounterModel obj)
        {
            CashCounter model = obj.MapTo<CashCounter>();
            model.IsActive = true;
            if (obj.CashCounterId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CashCounter updated successfully.");
        }
        [HttpDelete]
        [Permission(PageCode = "CashCounter", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            CashCounter model = await _repository.GetById(x => x.CashCounterId == Id);
            if ((model?.CashCounterId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CashCounter deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
