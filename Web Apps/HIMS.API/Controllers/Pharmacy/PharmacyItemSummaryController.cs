using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PharmacyItemSummaryController : BaseController
    {
        private readonly IPharmacyItemSummaryService _IPharmacyItemSummaryService;

        public PharmacyItemSummaryController(IPharmacyItemSummaryService repository)
        {
            _IPharmacyItemSummaryService = repository;

        }
        [HttpPost("NonMovingItemList")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<NonMovingItemListDto> NonMovingItemList = await _IPharmacyItemSummaryService.GetListAsync(objGrid);
            return Ok(NonMovingItemList.ToGridResponse(objGrid, "NonMovingItemList "));
        }
        [HttpPost("NonMovingItemWithoutBatchNoList")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<NonMovingItemListBatchNoDto> NonMovingItemListWithoutBatchNo = await _IPharmacyItemSummaryService.GetListAsyncB(objGrid);
            return Ok(NonMovingItemListWithoutBatchNo.ToGridResponse(objGrid, "NonMovingItemWithoutBatchNoList "));
        }
        [HttpPost("ItemExpReportMonthWiseList")]
        //[Permission(PageCode = "Registration", Permission = PagePermission.View)]
        public async Task<IActionResult> ListN(GridRequestModel objGrid)
        {
            IPagedList<ItemExpReportMonthWiseListDto> ItemExpReportMonthWiseList = await _IPharmacyItemSummaryService.GetListAsyncItem(objGrid);
            return Ok(ItemExpReportMonthWiseList.ToGridResponse(objGrid, "ItemExpReportMonthWiseList "));
        }

    }
}
