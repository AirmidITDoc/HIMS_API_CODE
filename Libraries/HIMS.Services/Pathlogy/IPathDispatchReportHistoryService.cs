using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IPathDispatchReportHistoryService
    {
        Task<IPagedList<PathDispatchReportHistoryListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<TestDispatchModelDto>> TestGetListAsync(GridRequestModel objGrid);
        Task InsertAsync(TPathDispatchReportHistory ObjTPathDispatchReportHistory, int UserId, string Username);
        Task UpdateAsync(TPathDispatchReportHistory ObjTPathDispatchReportHistory, int UserId, string Username, string[]? references);




    }
}
