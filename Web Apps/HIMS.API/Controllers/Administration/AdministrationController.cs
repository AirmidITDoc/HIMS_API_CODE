using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Services.Administration;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdministrationController: BaseController
    {
        
            private readonly IAdministrationService _IAdministrationService;
            public AdministrationController(IAdministrationService repository)
            {
                _IAdministrationService = repository;
            }
        
        [HttpPost("BrowseOPDBillPagiList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseOPDBillPagList(GridRequestModel objGrid)
        {
            IPagedList<BrowseOPDBillPagiListDto> BrowseOPDBillPagList = await _IAdministrationService.BrowseOPDBillPagiList(objGrid);
            return Ok(BrowseOPDBillPagList.ToGridResponse(objGrid, "BrowseOPDBillPagi App List"));
        }
        [HttpPost("IPAdvanceList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IPAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<IPAdvanceListDto> IPAdvanceList = await _IAdministrationService.IPAdvanceList(objGrid);
            return Ok(IPAdvanceList.ToGridResponse(objGrid, "IPAdvance App List"));
        }
        [HttpPost("IPRefundAdvanceReceiptList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IPRefundAdvanceReceiptList(GridRequestModel objGrid)
        {
            IPagedList<IPRefundAdvanceReceiptListDto> IPRefundAdvanceReceiptList = await _IAdministrationService.IPRefundAdvanceReceiptList(objGrid);
            return Ok(IPRefundAdvanceReceiptList.ToGridResponse(objGrid, "IPRefundAdvanceReceipt App List"));
        }
        [HttpPost("RoleMasterList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> RoleMasterList(GridRequestModel objGrid)
        {
            IPagedList<RoleMasterListDto> RoleMasterList = await _IAdministrationService.RoleMasterList(objGrid);
            return Ok(RoleMasterList.ToGridResponse(objGrid, "RoleMaster App List"));
        }

        [HttpPost("PaymentModeList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PaymentModeDto> PaymentModeList = await _IAdministrationService.GetListAsync(objGrid);
            return Ok(PaymentModeList.ToGridResponse(objGrid, "PaymentMode App List"));
        }
        [HttpPost("BrowseIPAdvPayPharReceiptList1")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseIPAdvPayPharReceiptList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPAdvPayPharReceiptListDto> BrowseIPAdvPayPharReceiptList = await _IAdministrationService.BrowseIPAdvPayPharReceiptList(objGrid);
            return Ok(BrowseIPAdvPayPharReceiptList.ToGridResponse(objGrid, "BrowseIPAdvPayPharReceipt1 App List"));
        }
    }
}
