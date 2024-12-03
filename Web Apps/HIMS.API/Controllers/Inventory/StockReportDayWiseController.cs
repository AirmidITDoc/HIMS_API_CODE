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
    public class StockReportDayWiseController : BaseController
    {
        private readonly IStockReportDayWiseService _IStockReportDayWiseService;
        public StockReportDayWiseController(IStockReportDayWiseService repository)
        {
            _IStockReportDayWiseService = repository;
        }
        [HttpPost("StockReportDayWiseList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<StockReportDayWiseListDto> StockReportDayWiseList = await _IStockReportDayWiseService.StockReportDayWiseList(objGrid);
            return Ok(StockReportDayWiseList.ToGridResponse(objGrid, "StockReportDayWise App List"));
        }
    }
}
