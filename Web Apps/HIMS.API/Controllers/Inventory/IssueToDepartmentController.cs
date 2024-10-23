using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OPPatient;
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





        //[HttpPost("InsertSP")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(IssueTODepModel obj)
        //{
        //    TIssueToDepartmentHeader model = obj.issue.MapTo<TIssueToDepartmentHeader>();

        //    List<TIssueToDepartmentDetail> objDepList = obj.Depissue.MapTo<List<TIssueToDepartmentDetail>>();

        //    List<TCurrentStock> objCurrentList = obj.curruntissue.MapTo<List<TCurrentStock>>();

        //    if (obj.issue.IssueId == 0)
        //    {
        //        model.IssueDate = Convert.ToDateTime(obj.issue.IssueDate);
        //        model.Addedby = CurrentUserId;

        //        foreach (var dep in objDepList)
        //        {
        //            dep.IssueId = obj.issue.IssueId;
        //        }

        //        foreach (var stock in objCurrentList)
        //        {
        //            stock.ItemId = stock.ItemId; 
        //        }

        //        await _IIssueToDepService.InsertAsyncSP(model, objDepList, objCurrentList, CurrentUserId, CurrentUserName);
        //    }
        //    else

        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //     return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDepartment Added successfully.");
        //}




    }
}
