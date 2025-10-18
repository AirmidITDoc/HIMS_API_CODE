using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class PharmacyReorderService:IPharmacyReorderService
    {
        private readonly HIMSDbContext _context;
        public PharmacyReorderService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<NonMovingItemListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NonMovingItemListDto>(model, "m_PharSales_NonMovingItemList");
        }
        public virtual async Task<IPagedList<NonMovingItemListBatchNoDto>> GetListAsyncB(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NonMovingItemListBatchNoDto>(model, "ps_PharSales_NonMovingItemListWithoutBatchNo");
        }
        public virtual async Task<IPagedList<ItemExpReportMonthWiseListDto>> GetListAsyncM(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemExpReportMonthWiseListDto>(model, "ps_Phar_ItemExpReportMonthWise");
        }
    }
}
