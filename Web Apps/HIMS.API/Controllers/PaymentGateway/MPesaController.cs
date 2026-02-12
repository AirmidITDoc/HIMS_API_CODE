using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.PaymentGateway;
using HIMS.API.PaymentGateway;
using HIMS.Core.Domain.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace HIMS.API.Controllers.Login
{

    [ApiController]
    //[Route("api/payment")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class MPesaController : BaseController
    {
        private readonly MpesaStkService _stkService;
        private readonly IGenericService<TMpesaResponse> _mPesaService;

        public MPesaController(MpesaStkService service,  IGenericService<TMpesaResponse> genericService)
        {
            _stkService = service;
            _mPesaService = genericService;
        }
        [HttpPost("validation")]
        public IActionResult Validate([FromBody] JsonElement payload)
        {
            string path = "C:\\PaymentDataLogs\\";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string filename = path + "\\" + AppTime.Now.ToString("dd_MM_yyyy") + ".txt";
            System.IO.File.AppendAllText(filename, "\n Validation =>" + payload.ToString());
            return Ok(new { ResultCode = 0, ResultDesc = "Success" });
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "StateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<TMpesaResponse> pesaResponseList = await _mPesaService.GetAllPagedAsync(objGrid);
            return Ok(pesaResponseList.ToGridResponse(objGrid, "pesaResponse List"));
        }

        [HttpPost("confirmation")]
        public async Task<IActionResult> ConfirmAsync([FromBody] JsonElement payload)
        {
            string path = "C:\\PaymentDataLogs\\";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string filename = path + "\\" + AppTime.Now.ToString("dd_MM_yyyy") + ".txt";
            System.IO.File.AppendAllText(filename, "\n Confirmation =>" + payload.ToString());
            var obj = JsonConvert.DeserializeObject<MpesaCallbackRoot>(payload.ToString());
            var exist = (await _mPesaService.GetAll(x => x.CheckoutRequestId == obj.Body.stkCallback.CheckoutRequestID && x.MerchantRequestId == obj.Body.stkCallback.MerchantRequestID)).FirstOrDefault();
            if ((exist?.Id ?? 0) > 0)
            {
                var result = MPesaResponse.MapMpesaToTestA(obj);
                exist.ResponseOn = AppTime.Now;
                exist.ResultCode = result.ResultCode;
                exist.MpesaReceiptNumber = result.MpesaReceiptNumber;
                exist.ResultDesc = result.ResultDesc;
                await _mPesaService.Update(exist, 0, "", Array.Empty<string>());
                return Ok(new { ResultCode = 0, ResultDesc = "Success", Data = result });
            }
            else
            {
                return Ok(new { ResultCode = 0, ResultDesc = "Failed." });
            }
        }

        [HttpPost("pay")]
        public async Task<ApiResponse> Pay([FromBody] PaymentRequestDto objRequest)
        {
            //var result = await _stkService.RegisterUrls();
            string reference = Guid.NewGuid().ToString().Replace("-", "");
            var result = await _stkService.StkPushAsync(objRequest.phone, objRequest.amount, objRequest.Opdipdid ?? 0 ,AppSettings.Settings.MPesa.ConfirmationUrl, reference);
            var data = JsonConvert.DeserializeObject<MPesaResponseDto>(result);
            data.ReferenceNo = reference;
            await _mPesaService.Add(new TMpesaResponse() { Amount = objRequest.amount, Opdipdid = objRequest.Opdipdid, CheckoutRequestId = data.CheckoutRequestID, MerchantRequestId = data.MerchantRequestID, PhoneNumber = objRequest.phone, TransactionDate = AppTime.Now }, CurrentUserId, CurrentUserName);
            return new ApiResponse() { StatusCode = 200, Data = data, StatusText = "Ok", Message = "Payment Done" };
        }

        [HttpPost("register-urls")]
        public async Task<ApiResponse> RegisterUrl()
        {
            var result = await _stkService.RegisterUrls();
            return new ApiResponse() { StatusCode = 200, StatusText = "Ok", Message = "Payment Done" };
        }
        [HttpGet("check-payment")]
        public async Task<ApiResponse> CheckPayment(string MerchantRequestID, string CheckoutRequestID)
        {
            var result = (await _mPesaService.GetAll(x => x.CheckoutRequestId == CheckoutRequestID && x.MerchantRequestId == MerchantRequestID))?.FirstOrDefault() ?? new TMpesaResponse();
            return new ApiResponse() { StatusCode = 200, StatusText = "Ok", Message = "Payment Done", Data = result };
        }
    }
}
