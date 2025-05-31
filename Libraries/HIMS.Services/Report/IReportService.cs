using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Http;

namespace HIMS.Services.Report
{
    public partial interface IReportService
    {
        string GetReportSetByProc(ReportRequestModel model);
        string GetNewReportSetByProc(ReportNewRequestModel model);
        Task<List<ServiceMasterDTO>> SearchService(string str);
        Task<List<MDepartmentMaster>> SearchDepartment(string str);
        Task<List<CashCounter>> SearchCashCounter(string str);
        Task<List<RoomMaster>> SearchWard(string str);
        //Task<List<Admission>> SearchAdmission(string str);
        Task<List<CompanyMaster>> SearchCompany(string str);
        Task<List<DischargeTypeMaster>> SearchDischargeType(string str);
        Task<List<GroupMaster>> SearchGroupMaster(string str);
        Task<List<ClassMaster>> SearchClassMaster(string str);
    }
    
}
