using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BranchController : BaseController
    {
        private readonly IBranchService _IBranchService;
        public BranchController(IBranchService repository)
        {
            _IBranchService = repository;

        }

        [HttpPost("UnitBranchWiseRevenueSummary")]
       // [Permission(PageCode = "Branch", Permission = PagePermission.View)]
        public async Task<IActionResult> UnitBranchWiseRevenueSummaryList(GridRequestModel objGrid)
        {
            IPagedList<UnitBranchWiseRevenueSummaryDto> UnitBranchWiseRevenueSummaryList = await _IBranchService.UnitBranchWiseRevenueSummaryListAsync(objGrid);
            return Ok(UnitBranchWiseRevenueSummaryList.ToGridResponse(objGrid, "Unit Branch Wise Revenue Summary List"));
        }


        [HttpPost("UnitBranchWiseTestSummary")]
       // [Permission(PageCode = "Branch", Permission = PagePermission.View)]
        public async Task<IActionResult> UnitBranchWiseTestSummaryList(GridRequestModel objGrid)
        {
            IPagedList<UnitBranchWiseTestSummaryDto> UnitBranchWiseTestSummaryList = await _IBranchService.UnitBranchWiseTestSummaryListAsync(objGrid);
            return Ok(UnitBranchWiseTestSummaryList.ToGridResponse(objGrid, "Unit Branch Wise Test Summary List"));
        }
    }
}
