using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Marketing;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Marketing
{
    public  class MarketingService : IMarketingService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MarketingService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<MarketingListDto>> MarketingAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MarketingListDto>(model, "ps_Marketing_App_VisitSummary");
        }
    }
}
