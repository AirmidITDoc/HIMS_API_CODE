using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
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

    }
}
