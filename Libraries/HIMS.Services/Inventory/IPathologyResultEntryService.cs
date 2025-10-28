using HIMS.Data.Models;


namespace HIMS.Services.Inventory
{
    public partial interface IPathologyResultEntryService
    {
        Task InsertAsyncResultEntry(TPathologyReportDetail objPathologyReportDetail, int UserId, string Username);
        Task InsertAsyncTemplateResult(TPathologyReportTemplateDetail objPathologyReportTemplateDetail, int UserId, string Username);
        Task CancelAsync(TPathologyReportDetail objPathologyReportDetail, int CurrentUserId, string CurrentUserName);
    }
}
