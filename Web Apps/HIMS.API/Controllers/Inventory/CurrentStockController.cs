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
        [HttpPost("StockReportDayWiseList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CurrentStockListDto> CurrentStockList = await _ICurrentStockService.CurrentStockList(objGrid);
            return Ok(CurrentStockList.ToGridResponse(objGrid, "CurrentStock App List"));
        }
    }
}
