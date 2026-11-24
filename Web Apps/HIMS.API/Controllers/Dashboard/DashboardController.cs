using Asp.Versioning;
using HIMS.Core;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Data.DTO;
using HIMS.Services.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<IActionResult> DailyDashboardSummaryList()
        {
            List<DailyDashboardSummaryModel> DashboardList = await _IDashboardService.GetDailyDashboardSummary();
            return Ok(DashboardList.ToList());
        }

        [HttpPost("OPDepartmentRangeChartList")]

        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<IActionResult> OPDepartmentRangeChartList(OPDepartmentRangeChartRequestModel model)
        {
            List<OPDepartmentRangeChartModel> DashboardList = await _IDashboardService.GetOPDepartmentRangeChart(model);
            return Ok(DashboardList.ToList());
        }

        [HttpGet("IPAdemissionDischargeCountList")]

        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public IActionResult IPAdemissionDischargeCountList()
        {
            IPAdemissionDischargeCountModel DashboardDetails = _IDashboardService.GetIPAdemissionDischargeCount();
            return Ok(DashboardDetails);
        }

        [HttpGet("OPVisitCountList")]

        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public IActionResult OPVisitCountList(OPVisitCountRequestModel model)
        {
            OPVisitCountList DashboardDetails = _IDashboardService.GetOPVisitCount(model);
            return Ok(DashboardDetails);
        }

        [HttpGet("pathology-dashboard")]
        [AnyPermission(Permission = PagePermission.View)]
       // [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> Pathology(int UnitId, DateTime FromDate, DateTime ToDate)
        {
          //  int UnitId = Context.UnitId;
            var data = await _IDashboardService.GetPathologyDashboard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathologist Doctor List", data);
        }

        [HttpGet("Financial-dashboard")]

        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> FinancialDashBoard(int UnitId, DateTime FromDate, DateTime ToDate)
        {
            //int UnitId = Context.UnitId;
            var data = await _IDashboardService.GetFinancialDashBoard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Financial DashBoard", data);
        }
        [HttpGet("DailyDashBoardIPSubModule-dashboard")]

        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> DailyDashBoardIPSubModuleDashBoard(int storId)
        {
            //int UnitId = Context.UnitId;
            var data = await _IDashboardService.GetDailyDashBoardIPSubModuleDashBoard(storId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DailyDashBoardIPSubModule DashBoard", data);
        }

        [HttpGet("radiology-dashboard")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> Radiology(int UnitId, DateTime FromDate, DateTime ToDate)
        {
           // int UnitId = Context.UnitId;
            var data = await _IDashboardService.GetRadiologyDashboard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Radiology Dashboard", data);
        }

        [HttpGet("pharmacy-dashboard")]

        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> Pharmacy(int UnitId, DateTime FromDate, DateTime ToDate)
        {
            // int UnitId = Context.UnitId;
                    var data = await _IDashboardService.GetPharmacyDashboard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pharmacy Dashboard", data);
        }

        [HttpGet("Mobile-pharmacy-dashboard")]
        [Permission(PageCode = "Administration", Permission = PagePermission.View)]
        public async Task<ApiResponse> MobilePharmacyDashboard(DateTime FromDate, DateTime ToDate, int Storeid, int OPIPType)
        {
            // int UnitId = Context.UnitId;
            var data = await _IDashboardService.GetMobilePharmacyDashboard(FromDate, ToDate, Storeid, OPIPType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Mobile Pharmacy Dashboard", data);
        }
        [HttpGet("Daily-Dashboard")]
        [SharedAuthenticated]
        public async Task<ApiResponse> DailyDashboard(int UnitId, DateTime FromDate, DateTime ToDate)
        {
            // int UnitId = Context.UnitId;
            var data = await _IDashboardService.GetDailyDashboard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pharmacy Dashboard", data);
        }

        [HttpGet("Cashless-Dashboard")]
        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> CashlessDashboard(int UnitId, DateTime FromDate, DateTime ToDate)
        {
            var data = await _IDashboardService.GetCashlessDashboard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status200OK,"Cashless Dashboard",data);
        }

        [HttpGet("Lab-Financial-Dashboard")]
        [Permission(PageCode = "Dashboard|ExternalInvestigation", Permission = PagePermission.View)]
        public async Task<ApiResponse> LabFinancialDashboard(int UnitId, DateTime FromDate, DateTime ToDate)
        {
            var data = await _IDashboardService.GetLabFinancialDashboard(UnitId, FromDate, ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK,"Lab Financial Dashboard", data);
        }

        [HttpGet("Lab-Financial-Department-Summary")]
        [Permission(PageCode = "Dashboard|ExternalInvestigation", Permission = PagePermission.View)]
        public async Task<ApiResponse> LabFinancialDepartmentSummary(int UnitId, int GroupId, DateTime FromDate, DateTime ToDate)
        {
            var data = await _IDashboardService.GetLabFinancialDepartmentSummary(UnitId, GroupId, FromDate, ToDate);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Lab Financial Department Summary", data);
        }

        [HttpGet("Procurement-DashBoard")]
        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> ProcurementDashboard(int UnitId)
        {
           
            var data = await _IDashboardService.GetProcurementDashboard(UnitId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Procurement DashBoard", data);
        }




        [HttpGet("DailyDashBoardOPSubModule-dashboard")]
        //[Permission(PageCode = "Dashboard", Permission = Permission.View)]
        [Permission(PageCode = "Dashboard", Permission = PagePermission.View)]
        public async Task<ApiResponse> DailyDashBoardOPSubModuleDashBoard(int UnitId)
        {
            var data = await _IDashboardService.GetDailyDashBoardOPSubModuleDashBoard(UnitId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DailyDashBoard OP SubModule DashBoard", data); 
        }

        [HttpGet("diet-dashboard")]
        [Permission(PageCode = "Dashboard|DietDashboard", Permission = PagePermission.View)]

        public async Task<ApiResponse> DietDashboard(DateTime FromDate, DateTime ToDate, string SearchText, int? WardId, int? DietTypeId)

        {
            var data = await _IDashboardService.GetDietDashboard(FromDate, ToDate, SearchText, WardId, DietTypeId);

            return ApiResponseHelper.GenerateResponse(
                ApiStatusCode.Status200OK,
                "Diet Dashboard",
                data);
        }


        [HttpGet("Mobile-Financial-Dashboard")]
        [Permission(PageCode = "Administration", Permission = PagePermission.View)]
        public async Task<ApiResponse> FinancialDashboard(int UnitId, DateTime FromDate, DateTime ToDate, int OPIPType = 2)
        {
            var data = await _IDashboardService.GetFinancialDashboard(UnitId, FromDate, ToDate, OPIPType);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK,"Financial Dashboard", data
            );
        }

    }
}