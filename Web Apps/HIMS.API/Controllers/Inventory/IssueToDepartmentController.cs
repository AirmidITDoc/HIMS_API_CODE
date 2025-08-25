using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
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

        [HttpPost("IssueToDeptList")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<IssuetodeptListDto> AppVisitList = await _IIssueToDepService.GetListAsync(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "Issue To dept  List"));
        }


        [HttpPost("IssueToDeptdetailList")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.View)]
        public async Task<IActionResult> issueitemList(GridRequestModel objGrid)
        {
            IPagedList<IssueToDepartmentDetailListDto> AppVisitList = await _IIssueToDepService.GetIssueItemListAsync(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "Issue To dept detail List"));
        }

        [HttpPost("IssueToDeptIndentByIDList")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.View)]
        public async Task<IActionResult> IndentByIDList(GridRequestModel objGrid)
        {
            IPagedList<IndentByIDListDto> AppVisitList = await _IIssueToDepService.GetIndentById(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "Issue To dept Indent BY ID List"));
        }


        [HttpPost("IssueToDeptIndentItemListList")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.View)]
        public async Task<IActionResult> issueIndentitemList(GridRequestModel objGrid)
        {
            IPagedList<IndentItemListDto> AppVisitList = await _IIssueToDepService.GetIndentItemList(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "Issue To dept Indent Item List"));
        }

         [HttpPost("InsertSP")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.Add)]

        public async Task<ApiResponse> InsertSP(IssueTODepModel obj)
        {
            TIssueToDepartmentHeader model = obj.issue.MapTo<TIssueToDepartmentHeader>();
           List<TCurrentStock> model1 = obj.TCurrentStock.MapTo<List<TCurrentStock>>();

            if (obj.issue.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.issue.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.issue.IssueTime);
                model.Addedby = CurrentUserId;
                await _IIssueToDepService.InsertAsyncSP(model, model1, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.",model.IssueId);
        }
    }
}
