using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Services.Administration;
using HIMS.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TallyController : BaseController
    {
        private readonly ITallyService _ITallyService;
        public TallyController(ITallyService repository)
        {
            _ITallyService = repository;

        }


        [HttpPost("TallyOPBillCashCounterList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> OPBillCashCounterList(GridRequestModel objGrid)
        {
            IPagedList<TallyListDto> OPBillCashCounterList = await _ITallyService.OPBillCashCounterListAsync(objGrid);
            return Ok(OPBillCashCounterList.ToGridResponse(objGrid, "OP Bill Cash Counter List "));
        }

        [HttpPost("TallyOPRefundBillCounterList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> OPRefundBillList(GridRequestModel objGrid)
        {
            IPagedList<OPRefundBillListCashCounterDto> OPRefundBillList = await _ITallyService.OPRefundBillListAsync(objGrid);
            return Ok(OPRefundBillList.ToGridResponse(objGrid, "Tally OPRefundBill Counter List "));
        }


        [HttpPost("IPAdvRefundPatientWisePaymentlist")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> IPAdvRefundPatientWisePaymentlist(GridRequestModel objGrid)
        {
            IPagedList<IPAdvRefundPatientWisePaymentDto> OPRefundBillList = await _ITallyService.IPAdvRefundPatientWisePaymentlistAsync(objGrid);
            return Ok(OPRefundBillList.ToGridResponse(objGrid, "IP AdvRefund PatientWise Payment list "));
        }

        [HttpPost("IPBillListPatientWisePaymentList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillListPatientWisePaymentList(GridRequestModel objGrid)
        {
            IPagedList<IPBillListPatientWisePaymentDto> OPRefundBillList = await _ITallyService.IPBillListPatientWisePaymentListAsync(objGrid);
            return Ok(OPRefundBillList.ToGridResponse(objGrid, "IPBill List Patient Wise Payment List"));
        }


        [HttpPost("IPAdvPatientWisePaymentList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> IPAdvPatientWisePaymentList(GridRequestModel objGrid)
        {
            IPagedList<IPAdvPatientWisePaymentDto> IPAdvPatientWisePaymentList = await _ITallyService.IPAdvPatientWisePaymentListAsync(objGrid);
            return Ok(IPAdvPatientWisePaymentList.ToGridResponse(objGrid, "IP Adv PatientWise Payment List"));
        }



        [HttpPost("IPBillListPatientWiseList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillListPatientWiseList(GridRequestModel objGrid)
        {
            IPagedList<IPBillListPatientWiseDto> IPBillListPatientWiseList = await _ITallyService.IPBillListPatientWiseListAsync(objGrid);
            return Ok(IPBillListPatientWiseList.ToGridResponse(objGrid, "IP Bill List Patient Wise List"));
        }



        [HttpPost("IPBillCashCounterList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillCashCounterList(GridRequestModel objGrid)
        {
            IPagedList<IPBillListCashCounterDto> IPBillCashCounterList = await _ITallyService.IPBillCashCounterListAsync(objGrid);
            return Ok(IPBillCashCounterList.ToGridResponse(objGrid, "IP Bill Cash Counter List"));
        }

        [HttpPost("IPBillRefundBillPatientWisePaymentList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillRefundBillPatientWisePaymentList(GridRequestModel objGrid)
        {
            IPagedList<IPBillRefundBillListPatientWisePaymentDto> IPBillRefundBillPatientWisePaymentList = await _ITallyService.IPBillRefundBillPatientWisePaymentListAsync(objGrid);
            return Ok(IPBillRefundBillPatientWisePaymentList.ToGridResponse(objGrid, "IP Bill Refund Bill Patient Wise Payment List"));
        }


        [HttpPost("PurchaseWiseSupplierList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> PurchaseWiseSupplierList(GridRequestModel objGrid)
        {
            IPagedList<PurchaseWiseSupplierDto> PurchaseWiseSupplierList = await _ITallyService.PurchaseWiseSupplierListAsync(objGrid);
            return Ok(PurchaseWiseSupplierList.ToGridResponse(objGrid, "Purchase Wise Supplier List"));
        }


        [HttpPost("TallyPhar2SalesList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyPhar2SalesList(GridRequestModel objGrid)
        {
            IPagedList<TallyPhar2SalesDto> TallyPhar2SalesList = await _ITallyService.TallyPhar2SalesListAsync(objGrid);
            return Ok(TallyPhar2SalesList.ToGridResponse(objGrid, "Tally Phar 2 Sales List"));
        }


        [HttpPost("TallyPhar2PaymentList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyPhar2PaymentList(GridRequestModel objGrid)
        {
            IPagedList<TallyPhar2PaymentDto> TallyPhar2PaymentList = await _ITallyService.TallyPhar2PaymentAsync(objGrid);
            return Ok(TallyPhar2PaymentList.ToGridResponse(objGrid, "Tally Phar 2 Payment List"));
        }

        [HttpPost("TallyPhar2SalesReturnList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyPhar2SalesReturnList(GridRequestModel objGrid)
        {
            IPagedList<TallyPhar2SalesReturnDto> TallyPhar2SalesReturnList = await _ITallyService.TallyPhar2SalesReturnListAsync(objGrid);
            return Ok(TallyPhar2SalesReturnList.ToGridResponse(objGrid, "Tally Phar 2 Sales Return List"));
        }

        [HttpPost("TallyPhar2ReceiptList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyPhar2ReceiptList(GridRequestModel objGrid)
        {
            IPagedList<TallyPhar2ReceiptDto> TallyPhar2ReceiptList = await _ITallyService.TallyPhar2ReceiptListAsync(objGrid);
            return Ok(TallyPhar2ReceiptList.ToGridResponse(objGrid, "Tally Phar 2 Receipt List"));
        }



    }

}
