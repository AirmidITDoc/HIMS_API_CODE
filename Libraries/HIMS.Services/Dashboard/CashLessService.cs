using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.DashBoard;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Dashboard
{
    public  class CashLessService : ICashLessService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CashLessService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<CashlessPatientWiseSummaryDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CashlessPatientWiseSummaryDto>(model, "ps_Cashless_PatientWise_Summary");
        }
        public virtual async Task<IPagedList<CashlessCountSummaryDto>> CashLessGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CashlessCountSummaryDto>(model, "ps_Cashless_CountSummary");
        }
        public virtual async Task<IPagedList<CashlessCompanyWiseSummaryDto>> CashLessCompanyWiseGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CashlessCompanyWiseSummaryDto>(model, "ps_Cashless_CompanyWise_Summary");
        }

    }
}
