using Asp.Versioning;
using HIMS.Api.Controllers;
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
    }
}
