using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;

namespace HIMS.Services.Pathlogy
{
    public partial interface IPathlogyService
    {
        Task<IPagedList<PathParaFillListDto>> PathParaFillList(GridRequestModel objGrid);
        Task<IPagedList<PathSubtestFillListDto>> PathSubtestFillList(GridRequestModel objGrid);
        //Task<IPagedList<PathologyTestListDto>> PathologyTestList(GridRequestModel objGrid);
        Task<IPagedList<PathResultEntryListDto>> PathResultEntry(GridRequestModel objGrid);
        Task<IPagedList<PathPatientTestListDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsyncResultEntry(List<TPathologyReportDetail> ObjPathologyReportDetail, TPathologyReportHeader ObjTPathologyReportHeader, int UserId, string Username);

        Task InsertAsyncResultEntry1(TPathologyReportTemplateDetail ObjTPathologyReportTemplateDetail, TPathologyReportHeader ObjTPathologyReportHeader, int UserId, string Username);
        Task DeleteAsync(TPathologyReportDetail ObjTPathologyReportDetail, int UserId, string Username);
        void InsertPathPrintResultentry(List<TempPathReportId> model, int currentUserId, string currentUserName);
        Task UpdateAsync(TPathologyReportHeader ObjTPathologyReportHeader, int UserId, string Username);
        Task VerifyAsync(TPathologyReportHeader ObjTPathologyReportHeader, int UserId, string Username);
        List<pathologistdoctorDto> SearchPatient();




    }
}
