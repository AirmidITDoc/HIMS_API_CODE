using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class WorkOrderController : BaseController
    {
        private readonly IWorkOrderService _IWorkOrderService;
        public WorkOrderController(IWorkOrderService repository)
        {
            _IWorkOrderService = repository;
        }
        [HttpPost("WorkOrderHeaderList")]
        [Permission(PageCode = "WorkOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetWorkorderlist(GridRequestModel objGrid)
        {
            IPagedList<WorkOrderListDto> List1 = await _IWorkOrderService.GetWorkorderList(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "WorkOrder List"));
        }

        [HttpPost("WorkOrderDetailsList")]
        [Permission(PageCode = "WorkOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOldPurchaseItemListAsync(GridRequestModel objGrid)
        {
            IPagedList<WorkorderIteListDto> List1 = await _IWorkOrderService.GetOldworkeorderAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "WorkOrderDetailsList"));
        }


        [HttpPost("WorkOrderSave")]
        [Permission(PageCode = "WorkOrder", Permission = PagePermission.Add)]
        public async Task<ApiResponse> WorkOrderAsyncSp(WorksOrderModel obj)
        {
            TWorkOrderHeader Model = obj.WorkOrders.MapTo<TWorkOrderHeader>();
           List<TWorkOrderDetail> Models = obj.WorkOrderDetails.MapTo<List<TWorkOrderDetail>>();

            if (obj.WorkOrders.WOId == 0)
            {

                await _IWorkOrderService.WorkOrderAsyncSp(Model, Models, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Work Order Save successfully.",Model.Woid);
        }

       

        [HttpPut("WorkOrderUpdate")]
        //[Permission(PageCode = "WorkOrder", Permission = PagePermission.Add)]
        public async Task<ApiResponse> WorkOrderUpdate(UpdateWorkOrderModel obj)
        {
            TWorkOrderHeader Model = obj.UpdateWorkOrders.MapTo<TWorkOrderHeader>();
           List<TWorkOrderDetail> Models = obj.WorkOrderDetail.MapTo<List<TWorkOrderDetail>>();

            if (obj.UpdateWorkOrders.WOId != 0)
            {

                await _IWorkOrderService.UpdateAsyncSp(Model, Models, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Work Order Update successfully.", Model.Woid);
        }

    }
}
