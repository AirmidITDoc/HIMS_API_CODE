using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;

namespace HIMS.Services.Inventory
{
    public partial interface IStockReportDayWiseService
    {
        Task<IPagedList<StockReportDayWiseListDto>> StockReportDayWiseList(GridRequestModel objGrid);
    }
}
