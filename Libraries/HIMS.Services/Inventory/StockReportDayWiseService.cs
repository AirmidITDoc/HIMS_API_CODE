using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public class StockReportDayWiseService : IStockReportDayWiseService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public StockReportDayWiseService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<StockReportDayWiseListDto>> StockReportDayWiseList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<StockReportDayWiseListDto>(model, "m_rptStockReportDayWise");
        }
    }
}
