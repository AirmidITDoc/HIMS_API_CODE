using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPSettlementController : BaseController
    {
        private readonly IOPSettlementService _OPSettlementService;
        public OPSettlementController(IOPSettlementService repository)
        {
            _OPSettlementService = repository;
        }

        [HttpPost("SettlementInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(OPSettlementModel obj)
        {
            Payment model = obj.OPCreditPayment.MapTo<Payment>();
            Bill BillUpdateModel = obj.BillUpdate.MapTo<Bill>();
            if (obj.OPCreditPayment.PaymentId == 0)
            {
                model.PaymentDate = Convert.ToDateTime(obj.OPCreditPayment.PaymentDate);
                model.PaymentTime = Convert.ToDateTime(obj.OPCreditPayment.PaymentTime);

                model.AddBy = CurrentUserId;
                await _OPSettlementService.InsertAsyncSP(model, BillUpdateModel, CurrentUserId, CurrentUserName);

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Op Settlement Save successfully." + model.PaymentId);
        }

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OPSettlementModel obj)
        {
            Payment model = obj.MapTo<Payment>();
            Bill Billmodel = obj.MapTo<Bill>();

            if (obj.OPCreditPayment.PaymentId == 0)
            {
                model.PaymentTime = Convert.ToDateTime(obj.OPCreditPayment.PaymentTime);
                model.PaymentDate = Convert.ToDateTime(obj.OPCreditPayment.PaymentDate);

                model.AddBy = CurrentUserId;
                //model.IsActive = true;
                await _OPSettlementService.InsertAsync(model, Billmodel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
    }
}
