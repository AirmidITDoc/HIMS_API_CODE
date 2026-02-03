using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.Data.DTO;
using HIMS.Services.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Dashboard
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _IDashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _IDashboardService = dashboardService;
        }

        [HttpGet("DailyDashboardSummaryList")]
        //[Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<IActionResult> DailyDashboardSummaryList()
        {
            List<DailyDashboardSummaryModel> DashboardList = await _IDashboardService.GetDailyDashboardSummary();
            return Ok(DashboardList.ToList());
        }

        [HttpPost("OPDepartmentRangeChartList")]
        //[Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<IActionResult> OPDepartmentRangeChartList(OPDepartmentRangeChartRequestModel model)
        {
            List<OPDepartmentRangeChartModel> DashboardList = await _IDashboardService.GetOPDepartmentRangeChart(model);
            return Ok(DashboardList.ToList());
        }

        [HttpGet("IPAdemissionDischargeCountList")]
        //[Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public IActionResult IPAdemissionDischargeCountList()
        {
            IPAdemissionDischargeCountModel DashboardDetails = _IDashboardService.GetIPAdemissionDischargeCount();
            return Ok(DashboardDetails);
        }

        [HttpGet("OPVisitCountList")]
        //[Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public IActionResult OPVisitCountList(OPVisitCountRequestModel model)
        {
            OPVisitCountList DashboardDetails = _IDashboardService.GetOPVisitCount(model);
            return Ok(DashboardDetails);
        }

        [HttpGet("pathology-dashboard")]
        //[Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> Pathology(DateTime FromDate, DateTime ToDate)
        {
            int UnitId = Context.UnitId;
            var data = await _IDashboardService.GetPathologyDashboard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathologist Doctor List", data);
        }

        [HttpGet("Financial-dashboard")]
        //[Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> FinancialDashBoard(int UnitId, DateTime FromDate, DateTime ToDate)
        {
            //int UnitId = Context.UnitId;
            var data = await _IDashboardService.GetFinancialDashBoard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Financial DashBoard", data);
        }
    }
}
