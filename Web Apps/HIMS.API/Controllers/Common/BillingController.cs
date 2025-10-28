using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
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
        private readonly IOPAddchargesService _IOPAddchargesService;
        private readonly IOPBillingService _oPBillingService;
        private readonly IOPSettlementCreditService _IOPCreditBillService;
        private readonly IIPBillService _IPBillService;
        private readonly IIPDraftBillSerive _iPDraftBillSerive;
        private readonly IIPInterimBillSerive _iPInterimBillSerive;

        public BillingController(IOPAddchargesService repository, IOPBillingService repository1, IOPSettlementCreditService repository2, IIPBillService iPBIllwithpay, IIPBILLCreditService iPBILLCreditService,
            IIPInterimBillSerive iPInterimBill, IIPDraftBillSerive iPDraftBill)
        {
            _IOPAddchargesService = repository;
            _oPBillingService = repository1;
            _IOPCreditBillService = repository2;
            _IPBillService = iPBIllwithpay;
            _iPDraftBillSerive = iPDraftBill;
            _iPInterimBillSerive = iPInterimBill;
        }


        [HttpPost("BrowseIPBillList")]
        public async Task<IActionResult> GetIPBillListAsync1(GridRequestModel objGrid)
        {
            IPagedList<IPBillListDto> IPDBillList = await _IPBillService.GetIPBillListAsync1(objGrid);
            return Ok(IPDBillList.ToGridResponse(objGrid, "Browse IP Bill List "));
        }
        [HttpPost("BrowseIPPaymentList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PaymentList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPPaymentListDto> IPPaymentList = await _IPBillService.GetIPPaymentListAsync(objGrid);
            return Ok(IPPaymentList.ToGridResponse(objGrid, "Browse IP Payment List"));
        }
        [HttpPost("BrowseIPRefundlist")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundBillList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPRefundListDto> IPRefundlist = await _IPBillService.GetIPRefundBillListListAsync(objGrid);
            return Ok(IPRefundlist.ToGridResponse(objGrid, " Browse IP Refund list "));
        }
        [HttpPost("ServiceClassdetaillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ServiceDetaillList(GridRequestModel objGrid)
        {
            IPagedList<ServiceClassdetailListDto> ServiceClassdetailList = await _IPBillService.ServiceClassdetailList(objGrid);
            return Ok(ServiceClassdetailList.ToGridResponse(objGrid, "ServiceClassdetail App List"));
        }

        [HttpPut("UpdateRefund")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
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

