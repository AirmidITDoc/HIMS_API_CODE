using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ICurrentStockService
    {
        Task<IPagedList<CurrentStockListDto>> CurrentStockList(GridRequestModel objGrid);
        Task<IPagedList<DayWiseCurrentStockDto>> DayWiseCurrentStockList(GridRequestModel objGrid);
        Task<IPagedList<ItemWiseSalesSummaryDto>> ItemWiseSalesList(GridRequestModel objGrid);
        Task<IPagedList<IssueWiseItemSummaryListDto>> IssueWiseItemSummaryList(GridRequestModel objGrid);
        Task<IPagedList<ItemMovementSummeryListDto>> List(GridRequestModel objGrid);
        Task<IPagedList<BatchWiseListDto>> BList(GridRequestModel objGrid);
        Task<IPagedList<SalesListDto>> SList(GridRequestModel objGrid);
        Task<IPagedList<PharIssueCurrentSumryListDto>> GetIssueSummaryList(GridRequestModel objGrid);
        Task<IPagedList<PharIssueCurrentDetListDto>> GetIssueDetailsList(GridRequestModel objGrid);
        Task<IPagedList<SalesReturnSummaryListDto>> SalesReturnSummaryList(GridRequestModel objGrid);
        Task<IPagedList<SalesReturnDetailsListDto>> SalesReturnDetailsList(GridRequestModel objGrid);
        Task<IPagedList<SalesSummaryListDto>> SalesSummaryList(GridRequestModel objGrid);
        Task<IPagedList<SalesDetailsListDto>> SalesDetailsList(GridRequestModel objGrid);

    }
}
