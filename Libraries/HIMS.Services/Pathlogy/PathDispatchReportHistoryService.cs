using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial class PathDispatchReportHistoryService: IPathDispatchReportHistoryService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PathDispatchReportHistoryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PathDispatchReportHistoryListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathDispatchReportHistoryListDto>(model, "ps_PathDispatchReportHistory");
        }
    }
}
