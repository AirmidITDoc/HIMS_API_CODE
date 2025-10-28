using HIMS.Data.DTO;

namespace HIMS.Services.Dashboard
{
    public partial interface IDashboardService
    {
        Task<List<DailyDashboardSummaryModel>> GetDailyDashboardSummary();
        Task<List<OPDepartmentRangeChartModel>> GetOPDepartmentRangeChart(OPDepartmentRangeChartRequestModel model);
        IPAdemissionDischargeCountModel GetIPAdemissionDischargeCount();
        OPVisitCountList GetOPVisitCount(OPVisitCountRequestModel model);
    }
}
