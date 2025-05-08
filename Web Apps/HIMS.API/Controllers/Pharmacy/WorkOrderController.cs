using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Purchase;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    public class WorkOrderController : BaseController
    {
        private readonly IWorkOrderService _IWorkOrderService;
        public WorkOrderController(IWorkOrderService repository)
        {
            _IWorkOrderService = repository;
        }
        [HttpPost("WorkOrderList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetWorkorderlist(GridRequestModel objGrid)
        {
            IPagedList<WorkOrderListDto> List1 = await _IWorkOrderService.GetWorkorderList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "WorkOrder List"));
        }

    }
}
