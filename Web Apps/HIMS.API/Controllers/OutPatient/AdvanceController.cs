using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Inventory;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdvanceController : BaseController
    {
        private readonly IAdvanceService _IAdvanceService;
        public AdvanceController(IAdvanceService repository)
        {
            _IAdvanceService = repository;
        }
        [HttpPost("AdvanceInsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(IPAdvance obj)
        {
            AdvanceHeader model = obj.AdvanceModel.MapTo<AdvanceHeader>();
            AdvanceDetail objAdvanceDetail = obj.AdvanceDetailModel.MapTo<AdvanceDetail>();
            Payment objpayment = obj.AdvancePayment.MapTo<Payment>();
            if (obj.AdvanceModel.AdvanceId == 0)
            {
                model.Date = Convert.ToDateTime(obj.AdvanceModel.Date);
                model.AddedBy = CurrentUserId;

                if (obj.AdvanceDetailModel.AdvanceId == 0)
                {
                    objAdvanceDetail.Date = Convert.ToDateTime(obj.AdvanceDetailModel.Date);
                    objAdvanceDetail.AddedBy = CurrentUserId;
                    // objVisitDetail.UpdatedBy = CurrentUserId;
                }

                if (obj.AdvanceDetailModel.AdvanceId == 0)
                {
                    objpayment.PaymentDate = Convert.ToDateTime(obj.AdvancePayment.PaymentDate);
                   
                   
                }
                await _IAdvanceService.InsertAsyncSP(model, objAdvanceDetail, objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Advance added successfully.");
        }










        //[HttpPost("InsertAdvance")]
        ////[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(AdvanceModel obj)
        //{
        //    AdvanceHeader model = obj.MapTo<AdvanceHeader>();
        //    if (obj.AdvanceId == 0)
        //    {
        //        model.Date = Convert.ToDateTime(obj.Date);
        //        model.AddedBy = CurrentUserId;

        //        await _IAdvanceService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IPAdvance added successfully.");
        //}
    }
}
