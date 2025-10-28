using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CurrentStockController : BaseController
    {
        private readonly ICurrentStockService _ICurrentStockService;
        public CurrentStockController(ICurrentStockService repository)
        {
            _ICurrentStockService = repository;
        }

        [HttpPost("StorewiseCurrentStockList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CurrentStockListDto> CurrentStockList = await _ICurrentStockService.CurrentStockList(objGrid);
            return Ok(CurrentStockList.ToGridResponse(objGrid, "StorewiseCurrentStock  List"));
        }
        [HttpPost("DayWiseCurrentStockList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> DList(GridRequestModel objGrid)
        {
            IPagedList<DayWiseCurrentStockDto> DayWiseCurrentStock = await _ICurrentStockService.DayWiseCurrentStockList(objGrid);
            return Ok(DayWiseCurrentStock.ToGridResponse(objGrid, "DayWiseCurrentStock List"));
        }
        [HttpPost("ItemWiseSalesSummaryList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IList(GridRequestModel objGrid)
        {
            IPagedList<ItemWiseSalesSummaryDto> ItemWiseSalesSummaryList = await _ICurrentStockService.ItemWiseSalesList(objGrid);
            return Ok(ItemWiseSalesSummaryList.ToGridResponse(objGrid, "ItemWiseSalesSummary List"));
        }
        [HttpPost("IssueWiseItemSummaryList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ItemSummaryList(GridRequestModel objGrid)
        {
            IPagedList<IssueWiseItemSummaryListDto> IssueWiseItemSummaryList = await _ICurrentStockService.IssueWiseItemSummaryList(objGrid);
            return Ok(IssueWiseItemSummaryList.ToGridResponse(objGrid, "IssueWiseItemSummaryList "));
        }

        //Current Stock Page --> Click on Item Qty Button


        [HttpPost("ItemMovementSummeryList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ItemMovementSummeryList(GridRequestModel objGrid)
        {
            IPagedList<ItemMovementSummeryListDto> summaryList = await _ICurrentStockService.List(objGrid);
            return Ok(summaryList.ToGridResponse(objGrid, "ItemMovementSummery  List"));
        }

        [HttpPost("BatchWiseList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BatchWiseList(GridRequestModel objGrid)
        {
            IPagedList<BatchWiseListDto> BatchWiseList = await _ICurrentStockService.BList(objGrid);
            return Ok(BatchWiseList.ToGridResponse(objGrid, "ItemMovementSummery  List"));
        }

        [HttpPost("SalesList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesList(GridRequestModel objGrid)
        {
            IPagedList<SalesListDto> BatchWiseList = await _ICurrentStockService.SList(objGrid);
            return Ok(BatchWiseList.ToGridResponse(objGrid, "SalesList"));
        }

        //Currebt stock Page --> Click On Receive Qty Button


        [HttpPost("IssueSummaryList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IssueSummaryList(GridRequestModel objGrid)
        {
            IPagedList<PharIssueCurrentSumryListDto> IssueSummaryList = await _ICurrentStockService.GetIssueSummaryList(objGrid);
            return Ok(IssueSummaryList.ToGridResponse(objGrid, "Issue Summary List"));
        }


        [HttpPost("IssueDetailsList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IssueDetailsList(GridRequestModel objGrid)
        {
            IPagedList<PharIssueCurrentDetListDto> IssueDetailsList = await _ICurrentStockService.GetIssueDetailsList(objGrid);
            return Ok(IssueDetailsList.ToGridResponse(objGrid, "Issue Details List"));
        }

        //Currebt stock Page --> Click On Return Qty Button

        [HttpPost("SalesReturnSummaryList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesReturnSummaryListDto(GridRequestModel objGrid)
        {
            IPagedList<SalesReturnSummaryListDto> SalesReturnSummaryList = await _ICurrentStockService.SalesReturnSummaryList(objGrid);
            return Ok(SalesReturnSummaryList.ToGridResponse(objGrid, "Issue Details List"));
        }
        [HttpPost("SalesReturnDetailsList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesReturnDetailsList(GridRequestModel objGrid)
        {
            IPagedList<SalesReturnDetailsListDto> IssueDetailsList = await _ICurrentStockService.SalesReturnDetailsList(objGrid);
            return Ok(IssueDetailsList.ToGridResponse(objGrid, "Issue Details List"));
        }


        // Currebt stock Page --> Click On Issue Qty Button
        [HttpPost("SalesSummaryList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesSummaryList(GridRequestModel objGrid)
        {
            IPagedList<SalesSummaryListDto> SalesSummaryList = await _ICurrentStockService.SalesSummaryList(objGrid);
            return Ok(SalesSummaryList.ToGridResponse(objGrid, "Issue Details List"));
        }

        [HttpPost("SalesDetailsList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesDetailsList(GridRequestModel objGrid)
        {
            IPagedList<SalesDetailsListDto> SalesDetailsList = await _ICurrentStockService.SalesDetailsList(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Issue Details List"));
        }

    }
}
