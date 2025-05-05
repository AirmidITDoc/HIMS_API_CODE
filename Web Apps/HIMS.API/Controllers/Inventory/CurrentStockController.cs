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
    }
}
