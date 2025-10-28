using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
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

        [HttpPost("MaterialRecvedByDeptList")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.View)]
        public async Task<IActionResult> MaterialreceivedList(GridRequestModel objGrid)
        {
            IPagedList<MateralreceivedbyDeptLstDto> AppVisitList = await _IIssueToDepService.GetMaterialrecivedbydeptList(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "Recceived To dept  List"));
        }


        [HttpPost("MaterialreceiveddetailList")]
        //[Permission(PageCode = "IssueToDepartment", Permission = PagePermission.View)]
        public async Task<IActionResult> materialreciveditemList(GridRequestModel objGrid)
        {
            IPagedList<MaterialrecvedbydepttemdetailslistDto> AppVisitList = await _IIssueToDepService.GetRecceivedItemListAsync(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "Materialreceiveddetail List"));
        }

        [HttpPost("AcceptIssueItemDetList")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.View)]
        public async Task<IActionResult> AcceptIssueItemDetList(GridRequestModel objGrid)
        {
            IPagedList<AcceptIssueItemDetListDto> AppVisitList = await _IIssueToDepService.AcceptIssueItemDetList(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "AcceptIssueItemDetList"));
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
                model.CreatedBy = CurrentUserId;
                model.Addedby = CurrentUserId;

                await _IIssueToDepService.InsertAsyncSP(model, model1, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.IssueId);
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "IssueToDepartment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IssueToDIndentModel obj)
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IssueToDept Indent added successfully.");
        }


        [HttpPost("UpdateIndentStatusAganist")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(UpdateIndentStatusModel obj)
        {
            TIssueToDepartmentHeader model = obj.UpdateIndent.MapTo<TIssueToDepartmentHeader>();
            List<TCurrentStock> model1 = obj.TCurStockModel.MapTo<List<TCurrentStock>>();
            TIndentHeader model2 = obj.IndentHeader.MapTo<TIndentHeader>();
            List<TIndentDetail> model3 = obj.TIndentDetails.MapTo<List<TIndentDetail>>();

            if (obj.UpdateIndent.IssueId == 0)
            {
                model.IssueDate = Convert.ToDateTime(obj.UpdateIndent.IssueDate);
                model.IssueTime = Convert.ToDateTime(obj.UpdateIndent.IssueTime);
                model.Addedby = CurrentUserId;
                await _IIssueToDepService.UpdateSP(model, model1, model2, model3, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "UpdateIndentStatusAganist  successfully.", model.IssueId);
        }


        [HttpPost("UpdateMaterialAcceptance")]
        [Permission(PageCode = "IssueToDepartment", Permission = PagePermission.Add)]
        public ApiResponse Update(UpdateMaterialAcceptanceModel obj)
        {
            TIssueToDepartmentHeader model = obj.materialAcceptIssueHeader.MapTo<TIssueToDepartmentHeader>();
            List<TIssueToDepartmentDetail> model1 = obj.materialAcceptIssueDetails.MapTo<List<TIssueToDepartmentDetail>>();
            TCurrentStock model3 = obj.materialAcceptStockUpdate.MapTo<TCurrentStock>();

            if (obj.materialAcceptIssueHeader.IssueId != 0)
            {

                model.Addedby = CurrentUserId;
                _IIssueToDepService.Update(model, model1, model3, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "UpdateMaterialAcceptance  successfully.");
        }


    }
}
