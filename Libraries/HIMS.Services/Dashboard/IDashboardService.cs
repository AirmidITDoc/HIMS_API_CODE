using HIMS.Core.Domain.Dashboard;
using HIMS.Data.DTO;

namespace HIMS.Services.Dashboard
{
    public partial interface IDashboardService
    {
        Task<List<DailyDashboardSummaryModel>> GetDailyDashboardSummary();
        Task<List<OPDepartmentRangeChartModel>> GetOPDepartmentRangeChart(OPDepartmentRangeChartRequestModel model);
        IPAdemissionDischargeCountModel GetIPAdemissionDischargeCount();
        OPVisitCountList GetOPVisitCount(OPVisitCountRequestModel model);
        Task<PathologyDashboard> GetPathologyDashboard(int UnitId,DateTime FromDate,DateTime ToDate);
        Task<FinancialDashboard> GetFinancialDashBoard(int UnitId, DateTime FromDate, DateTime ToDate);
        Task<RadiologyDashboard> GetRadiologyDashboard(int UnitId, DateTime FromDate, DateTime ToDate);
    }
}
