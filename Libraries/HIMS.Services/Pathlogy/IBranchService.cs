using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Pathology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IBranchService
    {
        Task<IPagedList<UnitBranchWiseRevenueSummaryDto>> UnitBranchWiseRevenueSummaryListAsync(GridRequestModel objGrid);
        Task<IPagedList<UnitBranchWiseTestSummaryDto>> UnitBranchWiseTestSummaryListAsync(GridRequestModel objGrid);
        Task<IPagedList<UnitCategoryTestSummaryDto>> UnitBranchWiseCateGorySummaryListAsync(GridRequestModel objGrid);
        Task<IPagedList<UnitDoctorTestSummaryDto>> UnitBranchWiseDoctorSummaryListAsync(GridRequestModel objGrid);
        Task<IPagedList<UnitCompanyTestSummaryDto>> UnitBranchWiseCompanySummaryListAsync(GridRequestModel objGrid);


        Task<IPagedList<BranchWiseTestSummaryDto>> BranchWiseTestSummaryList(GridRequestModel objGrid);
        Task<IPagedList<BranchWiseDoctorSummaryDto>> BranchWiseDoctorSummaryList(GridRequestModel objGrid);
        Task<IPagedList<BranchWiseCompanySummaryDto>> BranchWiseCompanySummaryList(GridRequestModel objGrid);
        Task<IPagedList<BranchWiseCategorySummaryDto>> BranchWiseCategorySummaryList(GridRequestModel objGrid);





    }
}
