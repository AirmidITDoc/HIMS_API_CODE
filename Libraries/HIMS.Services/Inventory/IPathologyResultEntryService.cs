using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HIMS.Services.Inventory
{
    public partial interface IPathologyResultEntryService
    {
        Task InsertAsyncResultEntry(TPathologyReportDetail objPathologyReportDetail, int UserId, string Username);
        Task InsertAsyncTemplateResult(TPathologyReportTemplateDetail objPathologyReportTemplateDetail, int UserId, string Username);
        Task CancelAsync(TPathologyReportDetail objPathologyReportDetail, int CurrentUserId, string CurrentUserName);
    }
}
