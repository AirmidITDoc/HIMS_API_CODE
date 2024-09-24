using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
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
            private readonly OPSettlementService _IOPSettlementService;
            public OPSettlementController(OPSettlementService repository)
            {
            _IOPSettlementService = repository;
            }

            [HttpPost("SettlementInsert")]
            //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
            public async Task<ApiResponse> Insert(OPSettlementModel obj)
            {
            Payment model = obj.MapTo<Payment>();
                if (obj.PaymentId == 0)
                {
                    model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);
                    model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);

                    model.AddBy = CurrentUserId;
                await _IOPSettlementService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
                //model = await _IOPSettlementService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);

            }
            else
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPSettlement added successfully.", model);
            }
        }
}
