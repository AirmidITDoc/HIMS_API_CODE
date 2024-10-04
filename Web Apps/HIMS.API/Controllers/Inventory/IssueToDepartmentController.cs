using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IssueToDepartmentController : BaseController
    {
        private readonly IIssueToDepService _IIssueToDepService;
        public IssueToDepartmentController(IIssueToDepService repository)
        {
            _IIssueToDepService = repository;
        }
       
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(IssueToDepartmentModel obj)
        {
            TIssueToDepartmentHeader model = obj.MapTo<TIssueToDepartmentHeader>();
            if (obj.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.IssueTime);
                model.Addedby = CurrentUserId;
                await _IIssueToDepService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment added successfully.");
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IssueToDepartmentModel obj)
        {
            TIssueToDepartmentHeader model = obj.MapTo<TIssueToDepartmentHeader>();
            if (obj.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.IssueTime);
                model.Addedby = CurrentUserId;
                await _IIssueToDepService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment added successfully.");
        }
         [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(IssueToDepartmentModel obj)
        {
            TIssueToDepartmentHeader model = obj.MapTo<TIssueToDepartmentHeader>();
            if (obj.IssueId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.IssueDate = Convert.ToDateTime(obj.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.IssueTime);
                await _IIssueToDepService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment updated successfully.");
        }
        //[HttpPost("UpdateIssue")]
        ////[Permission(PageCode = "Indent", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> UpdateIssue(updateissuetoDepartmentStockModel obj)
        //{
        //    TCurrentStock model = obj.MapTo<TCurrentStock>();
        //    if (obj.ItemId == 0)
        //    {
        //        model.BatchExpDate = DateTime.Now.Date;
        //        await _IIssueToDepService.updateissuetoDepartmentStock(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "updateissuetoDepartmentStock successfully.");
        //}
    }
}
