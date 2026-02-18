using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core;
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
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> OPBillCashCounterList(GridRequestModel objGrid)
        {
            IPagedList<TallyListDto> OPBillCashCounterList = await _ITallyService.OPBillCashCounterListAsync(objGrid);
            return Ok(OPBillCashCounterList.ToGridResponse(objGrid, "OP Bill Cash Counter List "));
        }

        [HttpPost("TallyOPRefundBillCounterList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> OPRefundBillList(GridRequestModel objGrid)
        {
            IPagedList<OPRefundBillListCashCounterDto> OPRefundBillList = await _ITallyService.OPRefundBillListAsync(objGrid);
            return Ok(OPRefundBillList.ToGridResponse(objGrid, "Tally OPRefundBill Counter List "));
        }


        [HttpPost("IPAdvRefundPatientWisePaymentlist")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> IPAdvRefundPatientWisePaymentlist(GridRequestModel objGrid)
        {
            IPagedList<IPAdvRefundPatientWisePaymentDto> OPRefundBillList = await _ITallyService.IPAdvRefundPatientWisePaymentlistAsync(objGrid);
            return Ok(OPRefundBillList.ToGridResponse(objGrid, "IP AdvRefund PatientWise Payment list "));
        }

        [HttpPost("IPBillListPatientWisePaymentList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillListPatientWisePaymentList(GridRequestModel objGrid)
        {
            IPagedList<IPBillListPatientWisePaymentDto> OPRefundBillList = await _ITallyService.IPBillListPatientWisePaymentListAsync(objGrid);
            return Ok(OPRefundBillList.ToGridResponse(objGrid, "IPBill List Patient Wise Payment List"));
        }


        [HttpPost("IPAdvPatientWisePaymentList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> IPAdvPatientWisePaymentList(GridRequestModel objGrid)
        {
            IPagedList<IPAdvPatientWisePaymentDto> IPAdvPatientWisePaymentList = await _ITallyService.IPAdvPatientWisePaymentListAsync(objGrid);
            return Ok(IPAdvPatientWisePaymentList.ToGridResponse(objGrid, "IP Adv PatientWise Payment List"));
        }



        [HttpPost("IPBillListPatientWiseList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillListPatientWiseList(GridRequestModel objGrid)
        {
            IPagedList<IPBillListPatientWiseDto> IPBillListPatientWiseList = await _ITallyService.IPBillListPatientWiseListAsync(objGrid);
            return Ok(IPBillListPatientWiseList.ToGridResponse(objGrid, "IP Bill List Patient Wise List"));
        }



        [HttpPost("IPBillCashCounterList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillCashCounterList(GridRequestModel objGrid)
        {
            IPagedList<IPBillListCashCounterDto> IPBillCashCounterList = await _ITallyService.IPBillCashCounterListAsync(objGrid);
            return Ok(IPBillCashCounterList.ToGridResponse(objGrid, "IP Bill Cash Counter List"));
        }

        [HttpPost("IPBillRefundBillPatientWisePaymentList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillRefundBillPatientWisePaymentList(GridRequestModel objGrid)
        {
            IPagedList<IPBillRefundBillListPatientWisePaymentDto> IPBillRefundBillPatientWisePaymentList = await _ITallyService.IPBillRefundBillPatientWisePaymentListAsync(objGrid);
            return Ok(IPBillRefundBillPatientWisePaymentList.ToGridResponse(objGrid, "IP Bill Refund Bill Patient Wise Payment List"));
        }


        [HttpPost("PurchaseWiseSupplierList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> PurchaseWiseSupplierList(GridRequestModel objGrid)
        {
            IPagedList<PurchaseWiseSupplierDto> PurchaseWiseSupplierList = await _ITallyService.PurchaseWiseSupplierListAsync(objGrid);
            return Ok(PurchaseWiseSupplierList.ToGridResponse(objGrid, "Purchase Wise Supplier List"));
        }


        [HttpPost("TallyPhar2SalesList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyPhar2SalesList(GridRequestModel objGrid)
        {
            IPagedList<TallyPhar2SalesDto> TallyPhar2SalesList = await _ITallyService.TallyPhar2SalesListAsync(objGrid);
            return Ok(TallyPhar2SalesList.ToGridResponse(objGrid, "Tally Phar 2 Sales List"));
        }


        [HttpPost("TallyPhar2PaymentList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyPhar2PaymentList(GridRequestModel objGrid)
        {
            IPagedList<TallyPhar2PaymentDto> TallyPhar2PaymentList = await _ITallyService.TallyPhar2PaymentAsync(objGrid);
            return Ok(TallyPhar2PaymentList.ToGridResponse(objGrid, "Tally Phar 2 Payment List"));
        }

        [HttpPost("TallyPhar2SalesReturnList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyPhar2SalesReturnList(GridRequestModel objGrid)
        {
            IPagedList<TallyPhar2SalesReturnDto> TallyPhar2SalesReturnList = await _ITallyService.TallyPhar2SalesReturnListAsync(objGrid);
            return Ok(TallyPhar2SalesReturnList.ToGridResponse(objGrid, "Tally Phar 2 Sales Return List"));
        }

        [HttpPost("TallyPhar2ReceiptList")]
        [Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyPhar2ReceiptList(GridRequestModel objGrid)
        {
            IPagedList<TallyPhar2ReceiptDto> TallyPhar2ReceiptList = await _ITallyService.TallyPhar2ReceiptListAsync(objGrid);
            return Ok(TallyPhar2ReceiptList.ToGridResponse(objGrid, "Tally Phar 2 Receipt List"));
        }

        [HttpPost("TallyIPBillListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyIPBillListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyIPBillListMediforteDto> TallyIPBillListMediforte = await _ITallyService.TallyIPBillListMediforteAsync(objGrid);
            return Ok(TallyIPBillListMediforte.ToGridResponse(objGrid, "Tally IPBill List Mediforte"));
        }

        [HttpPost("TallyIPBillDetailListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyIPBillDetailListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyIPBillDetailListMediforteDto> TallyIPBillDetailListMediforte = await _ITallyService.TallyIPBillDetailListMediforteAsync(objGrid);
            return Ok(TallyIPBillDetailListMediforte.ToGridResponse(objGrid, "Tally IPBill Detail List Mediforte"));
        }

        [HttpPost("TallyOPBillListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyOPBillListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyOPBillListMediforteDto> TallyOPBillListMediforte = await _ITallyService.TallyOPBillListMediforteAsync(objGrid);
            return Ok(TallyOPBillListMediforte.ToGridResponse(objGrid, "Tally OPBill List Mediforte"));
        }

        [HttpPost("TallyOPBillDetailListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyOPBillDetailListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyOPBillDetailListMediforteDto> TallyOPBillDetailListMediforte = await _ITallyService.TallyOPBillDetailListMediforteAsync(objGrid);
            return Ok(TallyOPBillDetailListMediforte.ToGridResponse(objGrid, "Tally OPBill Detail List Mediforte"));
        }

        // --------------------------





        [HttpPost("TallyIPBillRefundPaymentListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyIPBillRefundListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyIPBillRefundListMediforteDto> TallyOPBillDetailListMediforte = await _ITallyService.TallyIPBillRefundListMediforteAsync(objGrid);
            return Ok(TallyOPBillDetailListMediforte.ToGridResponse(objGrid, "Tally OPBill Detail List Mediforte"));
        }


        [HttpPost("TallyIPAdvancePaymentListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyIPAdvancePaymentListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyIPAdvancePaymentListMediforteDto> TallyOPBillDetailListMediforte = await _ITallyService.TallyIPAdvancePaymentListMediforteAsync(objGrid);
            return Ok(TallyOPBillDetailListMediforte.ToGridResponse(objGrid, "Tally OPBill Detail List Mediforte"));
        }


        [HttpPost("TallyIPAdvanceRefundPaymentListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyIPAdvanceRefundPaymentListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyIPAdvanceRefundPaymentListMediforteDto> TallyOPBillDetailListMediforte = await _ITallyService.TallyIPAdvanceRefundPaymentListMediforteAsync(objGrid);
            return Ok(TallyOPBillDetailListMediforte.ToGridResponse(objGrid, "Tally OPBill Detail List Mediforte"));
        }


        [HttpPost("TallyIPBillPaymentListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyIPBillPaymentListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyIPBillPaymentListMediforteDto> TallyOPBillDetailListMediforte = await _ITallyService.TallyIPBillPaymentListMediforteAsync(objGrid);
            return Ok(TallyOPBillDetailListMediforte.ToGridResponse(objGrid, "Tally OPBill Detail List Mediforte"));
        }



        [HttpPost("TallyPharmacyOPIPSalesDetailListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyOPIPSalesDetailListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyOPIPSalesDetailListMediforteDto> TallyOPBillDetailListMediforte = await _ITallyService.TallyOPIPSalesDetailListMediforteAsync(objGrid);
            return Ok(TallyOPBillDetailListMediforte.ToGridResponse(objGrid, "Tally OPBill Detail List Mediforte"));
        }



        [HttpPost("TallyPharmacyOPIPSalesReturnDetailListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyPharmacyOPIPSalesReturnDetailListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyPharmacyOPIPSalesReturnDetailListMediforteDto> TallyOPBillDetailListMediforte = await _ITallyService.TallyPharmacyOPIPSalesReturnDetailListMediforteAsync(objGrid);
            return Ok(TallyOPBillDetailListMediforte.ToGridResponse(objGrid, "Tally OPBill Detail List Mediforte"));
        }



        [HttpPost("TallyPharmacyOPIPSalesPaymentListMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]

        public async Task<IActionResult> TallyPharmacyOPIPSalesPaymentDetailListMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyPharmacyOPIPSalesPaymentListMediforteDto> TallyOPBillDetailListMediforte = await _ITallyService.TallyPharmacyOPIPSalesPaymentListMediforteAsync(objGrid);
            return Ok(TallyOPBillDetailListMediforte.ToGridResponse(objGrid, "Tally OPBill Detail List Mediforte"));
        }



        //-----------

        [HttpPost("TallyOPPaymentMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyOPPaymentMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyOPPaymentMediforteDto> TallyOPPaymentMediforte = await _ITallyService.TallyOPPaymentMediforteAsync(objGrid);
            return Ok(TallyOPPaymentMediforte.ToGridResponse(objGrid, "Tally OP Payment Mediforte"));
        }


        [HttpPost("TallyOPBillRefundPaymentMediforte")]
        //[Permission(PageCode = "TallyInterface", Permission = PagePermission.View)]
        public async Task<IActionResult> TallyOPBillRefundPaymentMediforte(GridRequestModel objGrid)
        {
            IPagedList<TallyOPBillRefundPaymentMediforteDto> TallyOPBillRefundPaymentMediforte = await _ITallyService.TallyOPBillRefundPaymentMediforteAsync(objGrid);
            return Ok(TallyOPBillRefundPaymentMediforte.ToGridResponse(objGrid, "Tally OP Bill Refund Payment Mediforte"));
        }




    }

}
