using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BillCancellationController : BaseController
    {
        private readonly IBillCancellationService _IOpBillCancellationService;
        public BillCancellationController(IBillCancellationService repository)
        {
            _IOpBillCancellationService = repository;
        }
        [HttpPut("OPCancelBill")]
        //[Permission(PageCode = "Cancellation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> CancelOP(OpBillCancellationModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo != 0)
            {
                await _IOpBillCancellationService.UpdateAsyncOp(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPBill updated successfully.");
        }


        [HttpPut("IPCancelBill")]
        //[Permission(PageCode = "Cancellation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> CancelIp(OpBillCancellationModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo != 0)
            {
                await _IOpBillCancellationService.UpdateAsyncIp(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IPBill updated successfully.");
        }
    }
 }
