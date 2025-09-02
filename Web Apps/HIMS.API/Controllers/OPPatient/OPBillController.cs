using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using HIMS.Services.Common;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPBillController : BaseController
    {
        private readonly IOPBillingService _oPBillingService;
        private readonly IOPCreditBillService _IOPCreditBillService;
        private readonly IOPSettlementService _IOPSettlementService;
        private readonly IAdministrationService _IAdministrationService;
        private readonly IVisitDetailsService _IVisitDetailsService;
        public OPBillController(IOPBillingService repository, IOPCreditBillService repository1, IOPSettlementService repository2, IAdministrationService repository3, IVisitDetailsService repository4)
        {
            _oPBillingService = repository;
            _IOPCreditBillService = repository1;
            _IOPSettlementService = repository2;
            _IAdministrationService = repository3;
            _IVisitDetailsService = repository4;
        }
        [HttpPost("BrowseOPRefundList")]
        [Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> OPRefundList(GridRequestModel objGrid)
        {
            IPagedList<OPRefundListDto> OpRefundlist = await _IVisitDetailsService.GeOpRefundListAsync(objGrid);
            return Ok(OpRefundlist.ToGridResponse(objGrid, "OP Refund List"));
        }
        [HttpPost("BrowseOPDBillPagiList")]
        [Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseOPDBillPagList(GridRequestModel objGrid)
        {
            IPagedList<BrowseOPDBillPagiListDto> BrowseOPDBillPagList = await _IAdministrationService.BrowseOPDBillPagiList(objGrid);
            return Ok(BrowseOPDBillPagList.ToGridResponse(objGrid, "BrowseOPDBillPagi App List"));
        }
        [HttpPost("BrowseOPPaymentList")]
        [Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> OPPaymentList(GridRequestModel objGrid)
        {
            IPagedList<OPPaymentListDto> OpPaymentlist = await _IVisitDetailsService.GeOpPaymentListAsync(objGrid);
            return Ok(OpPaymentlist.ToGridResponse(objGrid, "OP Payment List"));
        }
        [HttpPost("OPBillListSettlementList")]
        [Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OPBillListSettlementListDto> OPBillListSettlementList = await _IOPSettlementService.OPBillListSettlementList(objGrid);
            return Ok(OPBillListSettlementList.ToGridResponse(objGrid, "OP Patient Bill List "));
        }

        [HttpPost("OPBillingInsert")]
     //   [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            Payment objPayment = obj.Payments.MapTo<Payment>();
        List<AddCharge> ObjPackagecharge = obj.Packcagecharges.MapTo <List<AddCharge>>();


            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _oPBillingService.InsertAsyncSP(model, objPayment, ObjPackagecharge,  CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.BillNo);
        }

        [HttpPost("OPCreditBillingInsert")]
    //    [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPCreditBillingInsert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            List<AddCharge> ObjPackagecharge = obj.Packcagecharges.MapTo<List<AddCharge>>();

            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _IOPCreditBillService.InsertAsyncSP(model, ObjPackagecharge, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.", model.BillNo);
        }


    }
}
