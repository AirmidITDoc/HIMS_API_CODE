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
        public virtual async Task<IPagedList<UnitCategoryTestSummaryDto>> UnitBranchWiseCateGorySummaryListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<UnitCategoryTestSummaryDto>(model, "ps_UnitBranchWise_CategorySummary");
        }
        public virtual async Task<IPagedList<UnitDoctorTestSummaryDto>> UnitBranchWiseDoctorSummaryListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<UnitDoctorTestSummaryDto>(model, "ps_UnitBranchWise_DoctorSummary ");
        }
        public virtual async Task<IPagedList<UnitCompanyTestSummaryDto>> UnitBranchWiseCompanySummaryListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<UnitCompanyTestSummaryDto>(model, "ps_UnitBranchWise_CompanySummary");
        }


        public virtual async Task<IPagedList<BranchWiseTestSummaryDto>> BranchWiseTestSummaryList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BranchWiseTestSummaryDto>(model, "ps_BranchWise_TestSummary");
        }

        public virtual async Task<IPagedList<BranchWiseDoctorSummaryDto>> BranchWiseDoctorSummaryList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BranchWiseDoctorSummaryDto>(model, "ps_BranchWise_DoctorSummary");
        }

        public virtual async Task<IPagedList<BranchWiseCompanySummaryDto>> BranchWiseCompanySummaryList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BranchWiseCompanySummaryDto>(model, "ps_BranchWise_CompanySummary");
        }
        public virtual async Task<IPagedList<BranchWiseCategorySummaryDto>> BranchWiseCategorySummaryList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BranchWiseCategorySummaryDto>(model, "ps_BranchWise_CategorySummary");
        }
    }
}
