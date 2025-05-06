using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SalesReturnController : BaseController
    {
        private readonly ISalesReturnService _ISalesReturnService;
        public SalesReturnController(ISalesReturnService repository)
        {
            _ISalesReturnService = repository;
        }

     //   [HttpPost("SalesList")]
     ////   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
     //   public async Task<IActionResult> PharSalesList(GridRequestModel objGrid)
     //   {
     //       IPagedList<PharSalesListDto> PharSalesList = await _ISalesService.GetListAsync(objGrid);
     //       return Ok(PharSalesList.ToGridResponse(objGrid, "Phar Sales  List"));
     //   }
        [HttpPost("SalesReturnSummaryList")]
        ///   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesSummaryList(GridRequestModel objGrid)
        {
            IPagedList<SalesRetrunCurrentSumryListDto> PharSalesList = await _ISalesReturnService.SalesReturnSummaryList(objGrid);
            return Ok(PharSalesList.ToGridResponse(objGrid, "Sales Return Summary   List"));
        }

        [HttpPost("SalesReturnDetailsList")]
        ///   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> SalesReturnDetailsList(GridRequestModel objGrid)
        {
            IPagedList<SalesRetrunLCurrentDetListDto> SalesDetailsList = await _ISalesReturnService.SalesReturnDetailsList(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Return Details  List"));
        }
        [HttpPost("SalesReturnBrowseList")]
        ///   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> salesreturnlist(GridRequestModel objGrid)
        {
            IPagedList<SalesReturnBillListDto> salesreturnlist = await _ISalesReturnService.salesreturnlist(objGrid);
            return Ok(salesreturnlist.ToGridResponse(objGrid, "Sales Return Details  List"));
        }

        [HttpPost("salesReturnBrowseDetaillist")]
        ///   [Permission(PageCode = "Menu", Permission = PagePermission.View)]
        public async Task<IActionResult> salesreturndetaillist(GridRequestModel objGrid)
        {
            IPagedList<SalesReturnDetailsListDto> SalesDetailsList = await _ISalesReturnService.salesreturndetaillist(objGrid);
            return Ok(SalesDetailsList.ToGridResponse(objGrid, "Sales Return Details  List"));
        }

       

    }
}
