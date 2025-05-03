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
    public class CurrentStockService : ICurrentStockService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CurrentStockService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<CurrentStockListDto>> CurrentStockList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CurrentStockListDto>(model, "m_Retrieve_Storewise_CurrentStock");

        }
        public virtual async Task<IPagedList<DayWiseCurrentStockDto>> DayWiseCurrentStockList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DayWiseCurrentStockDto>(model, "m_rptStockReportDayWise");

        }
        public virtual async Task<IPagedList<ItemWiseSalesSummaryDto>> ItemWiseSalesList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemWiseSalesSummaryDto>(model, "m_rpt_ItemWiseSalesReport");

        }
    }
}
