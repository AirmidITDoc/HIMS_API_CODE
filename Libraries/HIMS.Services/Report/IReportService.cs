using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;

namespace HIMS.Services.Report
{
    public partial interface IReportService
    {
        string GetReportSetByProc(ReportRequestModel model);
        string GetNewReportSetByProc(ReportNewRequestModel model);
        Task<List<ServiceMasterDTO>> SearchService(string str);
        Task<List<MDepartmentMaster>> SearchDepartment(string str);
        Task<List<CashCounter>> SearchCashCounter(string str);
    }
}
