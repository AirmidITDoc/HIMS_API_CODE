using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IVisitDetailsService
    {
        Task<IPagedList<dynamic>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task InsertAsync(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task CancelAsync(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);
    }
}
