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

namespace HIMS.API.Controllers
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
            Payment model = obj.MapTo<Payment>();
            if (obj.PaymentId == 0)
            {
                model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);

                model.AddBy = CurrentUserId;
                await _OPSettlementService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPSettlement added successfully.", model);
        }

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OPSettlementModel obj)
        {
            Payment model = obj.MapTo<Payment>();
            Bill Billmodel = obj.MapTo<Bill>();

            if (obj.PaymentId == 0)
            {
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
                model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);

                model.AddBy = CurrentUserId;
                //model.IsActive = true;
                await _OPSettlementService.InsertAsync(model, Billmodel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPSettlement   added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Edit(BilModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo != 0)
            {

                //model.AddBy = CurrentUserId;
                await _OPSettlementService.UpdateAsync(model, CurrentUserId, CurrentUserName);

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPSettlement update successfully.", model);
        }

        [HttpPut("Edit")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> UPDATE(BilModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo != 0)
            {

                //model.AddBy = CurrentUserId;
                await _OPSettlementService.UpdateAsyncSP(model, CurrentUserId, CurrentUserName);

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPSettlement update successfully.", model);
        }
    }
}
