using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TExpenseController : BaseController
    {

        private readonly ITexpenseservice _Texpenseservice;

        public TExpenseController(ITexpenseservice repository)
        {
            _Texpenseservice = repository;

        }
        [HttpPost("DailyExpenceList")]
        //[Permission(PageCode = "managment", Permission = PagePermission.View)]
        public async Task<IActionResult> DailyExpenceList(GridRequestModel objGrid)
        {
            IPagedList<DailyExpenceListtDto> DailyExpenceList = await _Texpenseservice.DailyExpencesList(objGrid);
            return Ok(DailyExpenceList.ToGridResponse(objGrid, "DailyExpenceList"));
        }

        [HttpPost("TExpenseInsert")]
        //[Permission(PageCode = "managment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Posts(TExpenseModel obj)
        {
            TExpense model = obj.MapTo<TExpense>();

            if (obj.ExpId == 0)

            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;

                await _Texpenseservice.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record  added successfully.", model.ExpId);
        }

        [HttpPut("TExpenseUpdate{id:int}")]
        //[Permission(PageCode = "managment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edits(TExpenseModel obj)
        {
            TExpense model = obj.MapTo<TExpense>();
            if (obj.ExpId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;

                await _Texpenseservice.UpdateExpensesAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record updated successfully.", model.ExpId);
        }

        [HttpPost("TExpenseCancel")]
        //[Permission(PageCode = "managment", Permission = PagePermission.Add)]
        public ApiResponse TExpenseCancel(TExpenseCancelModel obj)
        {
            TExpense Model = obj.MapTo<TExpense>();

            if (obj.ExpId != 0)
            {
                _Texpenseservice.TExpenseCancel(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record delete successfully.");
        }
    }
}
