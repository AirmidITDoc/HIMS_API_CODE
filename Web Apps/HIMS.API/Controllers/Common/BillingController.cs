using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.IPPatient;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BillingController : BaseController
    {
       
        private readonly IIPBillService _IPBillService;
        public BillingController( IIPBillService iPBIllwithpay)
        {
           _IPBillService = iPBIllwithpay;
          
        }


        [HttpPost("BrowseIPBillList")]
        [Permission]
        public async Task<IActionResult> GetIPBillListAsync1(GridRequestModel objGrid)
        {
            IPagedList<IPBillListDto> IPDBillList = await _IPBillService.GetIPBillListAsync1(objGrid);
            return Ok(IPDBillList.ToGridResponse(objGrid, "Browse IP Bill List "));
        }
        [HttpPost("BrowseIPPaymentList")]
        //[Permission(PageCode = "Billing", Permission = PagePermission.View)]
        public async Task<IActionResult> PaymentList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPPaymentListDto> IPPaymentList = await _IPBillService.GetIPPaymentListAsync(objGrid);
            return Ok(IPPaymentList.ToGridResponse(objGrid, "Browse IP Payment List"));
        }
        [HttpPost("BrowseIPRefundlist")]
        [Permission]
        //[Permission(PageCode = "Billing", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundBillList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPRefundListDto> IPRefundlist = await _IPBillService.GetIPRefundBillListListAsync(objGrid);
            return Ok(IPRefundlist.ToGridResponse(objGrid, " Browse IP Refund list "));
        }
        [HttpPost("ServiceClassdetaillList")]
        //[Permission(PageCode = "Billing", Permission = PagePermission.View)]
        public async Task<IActionResult> ServiceDetaillList(GridRequestModel objGrid)
        {
            IPagedList<ServiceClassdetailListDto> ServiceClassdetailList = await _IPBillService.ServiceClassdetailList(objGrid);
            return Ok(ServiceClassdetailList.ToGridResponse(objGrid, "ServiceClassdetail App List"));
        }

        [HttpPut("UpdateRefund")]
        [Permission]
        public async Task<ApiResponse> Update(UpdateRefundModel obj)
        {
            Refund model = obj.MapTo<Refund>();


            if (obj.RefundId != 0)
            {
                model.IsCancelledDate = Convert.ToDateTime(model.IsCancelledDate);
                model.AddedBy = CurrentUserId;
                await _IPBillService.UpdateRefund(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  update successfully .");
        }


    }
}

