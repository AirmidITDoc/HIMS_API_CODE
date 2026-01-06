using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Services.Pharmacy;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class InPatientController : BaseController
    {
        private readonly IInPatientService _IInPatientService;
        public InPatientController(IInPatientService repository)
        {
            _IInPatientService = repository;
        }
        [HttpPost("SalesInPatientBillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> salesbrowselist(GridRequestModel objGrid)
        {
            IPagedList<SalesBillListDto> salesbrowselist = await _IInPatientService.salesbrowselist(objGrid);
            return Ok(salesbrowselist.ToGridResponse(objGrid, "Sales InPatient BillList"));
        }
        [HttpPost("SalesInPatientDetailsList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> salesdetaillist(GridRequestModel objGrid)
        {
            IPagedList<InPatientSalesDetailsListDto> SalesBrowseDetailList = await _IInPatientService.Getsalesdetaillist(objGrid);
            return Ok(SalesBrowseDetailList.ToGridResponse(objGrid, "Sales InPatient Details List"));
        }
        [HttpPost("SalesInPatientReturnBillList")]
        //[Permission(PageCode = "SalesReturn", Permission = PagePermission.View)]
        public async Task<IActionResult> salesreturnlist(GridRequestModel objGrid)
        {
            IPagedList<SalesReturnBillListDto> salesreturnlist = await _IInPatientService.salesreturnlist(objGrid);
            return Ok(salesreturnlist.ToGridResponse(objGrid, "Sales InPatient ReturnBil lList"));
        }
    }
}
