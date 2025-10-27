using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public class PharmacyItemSummaryService : IPharmacyItemSummaryService
    {
        private readonly HIMSDbContext _context;
        public PharmacyItemSummaryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<NonMovingItemListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NonMovingItemListDto>(model, "ps_PharSales_NonMovingItemList");
        }
        public virtual async Task<IPagedList<NonMovingItemListBatchNoDto>> GetListAsyncB(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<NonMovingItemListBatchNoDto>(model, "ps_PharSales_NonMovingItemListWithoutBatchNo");
        }
        public virtual async Task<IPagedList<ItemExpReportMonthWiseListDto>> GetListAsyncItem(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemExpReportMonthWiseListDto>(model, "ps_Phar_ItemExpReportMonthWise");
        }
    }
}
