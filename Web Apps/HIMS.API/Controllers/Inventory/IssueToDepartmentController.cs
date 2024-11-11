using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OPPatient;
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

        [HttpPost("IssueToDepttList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<IssuetodeptListDto> AppVisitList = await _IIssueToDepService.GetListAsync(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "Issue To dept  List"));
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
    }
}
