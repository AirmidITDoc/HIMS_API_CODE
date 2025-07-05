using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Http;

namespace HIMS.Services.Report
{
    public partial interface IReportService
    {
        string GetReportSetByProc(ReportRequestModel model,string PdfFontPath="");
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
        Task<List<MStoreMaster>> SearchMStoreMaster(string str);
        Task<List<MSupplierMaster>> SearchMSupplierMaster(string str);
        //Task<List<Payment>> SearchPayment(string str);
        Task<List<MItemDrugTypeMaster>> SearchMItemDrugTypeMaster(string str);
        Task<List<MCreditReasonMaster>> SearchMCreditReasonMaster(string str);
        Task<List<MItemMaster>> SearchMItemMaster(string str);
        Task InsertAsync(MReportConfig ObjMReportConfig, int UserId, string Username);
        Task UpdateAsync(MReportConfig ObjMReportConfig, int UserId, string Username);



    }

}
