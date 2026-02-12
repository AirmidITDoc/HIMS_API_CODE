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
        //[Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.View)]
        public async Task<IActionResult> UnitBranchWiseRevenueSummaryList(GridRequestModel objGrid)
        {
            IPagedList<UnitBranchWiseRevenueSummaryDto> UnitBranchWiseRevenueSummaryList = await _IBranchService.UnitBranchWiseRevenueSummaryListAsync(objGrid);
            return Ok(UnitBranchWiseRevenueSummaryList.ToGridResponse(objGrid, "Unit Branch Wise Revenue Summary List"));
        }


        [HttpPost("UnitBranchWiseTestSummary")]
        //[Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.View)]
        public async Task<IActionResult> UnitBranchWiseTestSummaryList(GridRequestModel objGrid)
        {
            IPagedList<UnitBranchWiseTestSummaryDto> UnitBranchWiseTestSummaryList = await _IBranchService.UnitBranchWiseTestSummaryListAsync(objGrid);
            return Ok(UnitBranchWiseTestSummaryList.ToGridResponse(objGrid, "Unit Branch Wise Test Summary List"));
        }
        [HttpPost("UnitBranchWiseCategorySummary")]
        // [Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.View)]
        public async Task<IActionResult> UnitBranchWiseCategorySummaryList(GridRequestModel objGrid)
        {
            IPagedList<UnitCategoryTestSummaryDto> UnitBranchWiseCategorySummaryList = await _IBranchService.UnitBranchWiseCateGorySummaryListAsync(objGrid);
            return Ok(UnitBranchWiseCategorySummaryList.ToGridResponse(objGrid, "UnitBranchWiseCategorySummary List"));
        }
        [HttpPost("UnitBranchWiseDoctorSummary")]
        // [Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.View)]
        public async Task<IActionResult> UnitBranchWiseDoctorSummaryList(GridRequestModel objGrid)
        {
            IPagedList<UnitDoctorTestSummaryDto> UnitBranchWiseCategorySummaryList = await _IBranchService.UnitBranchWiseDoctorSummaryListAsync(objGrid);
            return Ok(UnitBranchWiseCategorySummaryList.ToGridResponse(objGrid, "UnitBranchWiseDoctorSummary List"));
        }
        [HttpPost("UnitBranchWiseCompanySummary")]
        // [Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.View)]
        public async Task<IActionResult> UnitBranchWiseCompanySummaryList(GridRequestModel objGrid)
        {
            IPagedList<UnitCompanyTestSummaryDto> UnitBranchWiseCompanySummaryList = await _IBranchService.UnitBranchWiseCompanySummaryListAsync(objGrid);
            return Ok(UnitBranchWiseCompanySummaryList.ToGridResponse(objGrid, "UnitBranchWiseDoctorSummary List"));
        }


        [HttpPost("BranchWiseTestSummaryList")]
        public async Task<IActionResult> BranchWiseTestSummaryList(GridRequestModel objGrid)
        {
            IPagedList<BranchWiseTestSummaryDto> BranchWiseTestSummaryList = await _IBranchService.BranchWiseTestSummaryList(objGrid);
            return Ok(BranchWiseTestSummaryList.ToGridResponse(objGrid, "BranchWiseTestSummary List"));
        }

        [HttpPost("BranchWiseDoctorSummaryList")]
        public async Task<IActionResult> BranchWiseDoctorSummaryList(GridRequestModel objGrid)
        {
            IPagedList<BranchWiseDoctorSummaryDto> BranchWiseDoctorSummaryList = await _IBranchService.BranchWiseDoctorSummaryList(objGrid);
            return Ok(BranchWiseDoctorSummaryList.ToGridResponse(objGrid, "BranchWiseDoctorSummary List"));
        }


        [HttpPost("BranchWiseCompanySummaryList")]
        public async Task<IActionResult> BranchWiseCompanySummaryList(GridRequestModel objGrid)
        {
            IPagedList<BranchWiseCompanySummaryDto> BranchWiseCompanySummaryList = await _IBranchService.BranchWiseCompanySummaryList(objGrid);
            return Ok(BranchWiseCompanySummaryList.ToGridResponse(objGrid, "BranchWiseCompanySummary List"));
        }

        [HttpPost("BranchWiseCategorySummaryList")]
        public async Task<IActionResult> BranchWiseCategorySummaryList(GridRequestModel objGrid)
        {
            IPagedList<BranchWiseCategorySummaryDto> BranchWiseCategorySummaryList = await _IBranchService.BranchWiseCategorySummaryList(objGrid);
            return Ok(BranchWiseCategorySummaryList.ToGridResponse(objGrid, "BranchWiseCategorySummary List"));
        }

    }
}
