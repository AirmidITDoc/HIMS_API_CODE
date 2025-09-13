using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Inventory;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Services.Administration;
using HIMS.API.Models.Masters;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Administration;
using HIMS.API.Models.IPPatient;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class paymentpharmacyController : BaseController
    {
        private readonly IPaymentpharmacyService _paymentpharmacyService;
        public paymentpharmacyController(IPaymentpharmacyService repository)
        {
            _paymentpharmacyService = repository;
        }

         [HttpPost("IPDPaymentReceiptList")]
        //  [Permission(PageCode = "IPDPaymentReceiptList", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPDPaymentReceiptListDto> IPDPaymentReceiptList = await _paymentpharmacyService.GetListAsync(objGrid);
            return Ok(IPDPaymentReceiptList.ToGridResponse(objGrid, "IPDPaymentReceipt List"));
        }

        [HttpPost("OPDPaymentReceiptList")]
        //  [Permission(PageCode = "OPDPaymentReceiptList", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<BrowseOPDPaymentReceiptListDto> OPDPaymentReceiptList = await _paymentpharmacyService.GetListAsync1(objGrid);
            return Ok(OPDPaymentReceiptList.ToGridResponse(objGrid, "OPDPaymentReceipt List"));
        }


        [HttpPost("IPAdvPaymentReceiptList")]
        //  [Permission(PageCode = "OPDPaymentReceiptList", Permission = PagePermission.View)]
        public async Task<IActionResult> List2(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPAdvPaymentReceiptListDto> IPAdvPaymentReceiptList = await _paymentpharmacyService.GetListAsync2(objGrid);
            return Ok(IPAdvPaymentReceiptList.ToGridResponse(objGrid, "IPAdvPaymentReceiptList"));
        }

        [HttpPost("BrowsePharmacyPayReceiptList")]
        //  [Permission(PageCode = "oPDPaymentReceiptList", Permission = PagePermission.View)]
        public async Task<IActionResult> List3(GridRequestModel objGrid)
        {
            IPagedList<BrowsePharmacyPayReceiptListDto> PharmacyPayReceiptList = await _paymentpharmacyService.GetListAsync3(objGrid);
            return Ok(PharmacyPayReceiptList.ToGridResponse(objGrid, "BrowsePharmacyPayReceiptList"));
        }
      
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "PaymentPharmacy", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(paymentpharmacyModel obj)
        {
            PaymentPharmacy model = obj.MapTo<PaymentPharmacy>();
            if (obj.PaymentId == 0)
            {
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
                model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);

                model.AddBy = CurrentUserId;
                //model.IsActive = true;
                await _paymentpharmacyService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "PaymentPharmacy", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(paymentpharmacyModel obj)
        {
            PaymentPharmacy model = obj.MapTo<PaymentPharmacy>();
            if (obj.PaymentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
                await _paymentpharmacyService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        [HttpPut("UpdatePharmSalesDate")]
        [Permission(PageCode = "PaymentPharmacy", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(PharmSalesPaymentModel obj)
        {
            TSalesHeader model = obj.MapTo<TSalesHeader>();


            if (obj.SalesId != 0)
            {
                model.Date = Convert.ToDateTime(model.Date);

                model.AddedBy = CurrentUserId;
                await _paymentpharmacyService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record update successfully .");
        }
        [HttpPut("UpdatePharmSalesPaymentDate")]
        [Permission(PageCode = "PaymentPharmacy", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(paymentpharModel obj)
        {
            PaymentPharmacy model = obj.MapTo<PaymentPharmacy>();


            if (obj.PaymentId != 0)
            {
                model.PaymentDate = Convert.ToDateTime(model.PaymentDate);

                model.AddBy = CurrentUserId;
                await _paymentpharmacyService.UpdateAsyncDate(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Updated successfully .");
        }


    }
}
