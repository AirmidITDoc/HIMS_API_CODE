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
    public class BranchService: IBranchService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public BranchService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<UnitBranchWiseRevenueSummaryDto>> UnitBranchWiseRevenueSummaryListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<UnitBranchWiseRevenueSummaryDto>(model, "ps_UnitBranchWiseRevenueSummary");
        }
        public virtual async Task<IPagedList<UnitBranchWiseTestSummaryDto>> UnitBranchWiseTestSummaryListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<UnitBranchWiseTestSummaryDto>(model, "ps_UnitBranchWise_TestSummary");
        }
    }
}
