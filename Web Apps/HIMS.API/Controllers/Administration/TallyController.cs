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


    }

}
