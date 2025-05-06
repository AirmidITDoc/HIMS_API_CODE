using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    public class OpeningBalanceController : BaseController
    {
        private readonly IOpeningBalanceService _IOpeningBalanceService;
        public OpeningBalanceController(IOpeningBalanceService repository)
        {
            _IOpeningBalanceService = repository;
        }
        [HttpPost("OpeningBalanceList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOpningBalance(GridRequestModel objGrid)
        {
            IPagedList<OpeningBalListDto> List1 = await _IOpeningBalanceService.GetOpeningBalanceList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Oening Balance List"));
        }
        

        [HttpPost("OpeningBalnceItemDetailList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOpeningBalItemDetail(GridRequestModel objGrid)
        {
            IPagedList<OpeningBalanaceItemDetailListDto> List1 = await _IOpeningBalanceService.GetOPningBalItemDetailList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Opening Balance Item Detail List"));
        }

    }
}
