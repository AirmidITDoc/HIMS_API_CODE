using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.Api.Models.Common;
using HIMS.Core;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Data;
using HIMS.Services.Administration;

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


        [HttpPost("TExpenseInsert")]
        //[Permission(PageCode = "managment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Posts(TExpenseModel obj)
        {
            TExpense model = obj.MapTo<TExpense>();
            if (obj.ExpId == 0)
            {

                await _Texpenseservice.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record  added successfully.");
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

                await _Texpenseservice.UpdateExpensesAsync(model, CurrentUserId, CurrentUserName, new string[2]);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record updated successfully.");
        }

        [HttpDelete("TExpenseCancel")]
        //[Permission(PageCode = "managment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> TExpenseCancel(TExpenseCancelModel obj)
        {
            TExpense Model = obj.MapTo<TExpense>();

            if (obj.ExpId != 0)
            {

                //   Model.IsAddedby = CurrentUserId;
                await _Texpenseservice.TExpenseCancel(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record delete successfully.");
        }
    }
}
