using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.OTManagment;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LabBrowseListController : BaseController
    {
        private readonly ILabBrowseListService _ILabBrowseList;

        public LabBrowseListController(ILabBrowseListService ILabBrowseList)
        {
            _ILabBrowseList = ILabBrowseList;
        }

      
        [HttpPost("LabBillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<LabBrowsListDto> LabBillList = await _ILabBrowseList.GetLabListListAsync(objGrid);
            return Ok(LabBillList.ToGridResponse(objGrid, "LabBill List"));
        }
        [HttpPost("LabPaymentList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PaymentList(GridRequestModel objGrid)
        {
            IPagedList<LabBrowsePaymentListDto> LabBillList = await _ILabBrowseList.GetLabPaymentListListAsync(objGrid);
            return Ok(LabBillList.ToGridResponse(objGrid, "Lab payment List"));
        }

        [HttpPost("LabRefundList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundList(GridRequestModel objGrid)
        {
            IPagedList<LabBrowseRefundListDto> LabBillList = await _ILabBrowseList.GetLabRefundListListAsync(objGrid);
            return Ok(LabBillList.ToGridResponse(objGrid, "Lab Refund List"));
        }

    }
}
