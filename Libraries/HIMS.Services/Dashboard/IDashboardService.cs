using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
