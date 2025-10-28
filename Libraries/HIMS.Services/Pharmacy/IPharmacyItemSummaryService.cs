using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPharmacyItemSummaryService
    {
        Task<IPagedList<NonMovingItemListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<NonMovingItemListBatchNoDto>> GetListAsyncB(GridRequestModel objGrid);
        Task<IPagedList<ItemExpReportMonthWiseListDto>> GetListAsyncItem(GridRequestModel objGrid);
    }
}
