using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.Api.Models.Login;
using HIMS.API.Extensions;
using HIMS.API.Models.PaymentGateway;
using HIMS.API.PaymentGateway;
using HIMS.API.Utility;
using HIMS.Core.Infrastructure;
using HIMS.Core.Utilities;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HIMS.API.Controllers.Login
{

    [ApiController]
    //[Route("api/payment")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class MPesaController : ControllerBase
    {
        private readonly MpesaStkService _stkService;
        private readonly IConfiguration _config;
        public MPesaController(MpesaStkService service, IConfiguration config)
        {
            _stkService = service;
            _config = config;
        }
        [HttpPost("validation")]
        public IActionResult Validate([FromBody] JsonElement payload)
        {
            string path = "C:\\PaymentDataLogs\\";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string filename = path + "\\" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
            System.IO.File.AppendAllText(filename, "\n Validation =>" + payload.ToString());
            return Ok(new { ResultCode = 0, ResultDesc = "Success" });
        }

        [HttpPost("confirmation")]
        public IActionResult Confirm([FromBody] JsonElement payload)
        {
            string path = "C:\\PaymentDataLogs\\";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string filename = path + "\\" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt";
            System.IO.File.AppendAllText(filename, "\n Confirmation =>" + payload.ToString());
            var obj = JsonConvert.DeserializeObject<MpesaCallbackRoot>(payload.ToString());
            var result = MPesaResponse.MapMpesaToTestA(obj);
            return Ok(new { ResultCode = 0, ResultDesc = "Success", Data = result });
        }

        [HttpPost("pay")]
        public async Task<ApiResponse> Pay(string phone, decimal amount, string reference)
        {
            //var result = await _stkService.RegisterUrls();
            var result = await _stkService.StkPushAsync(phone, amount, _config["MPesa:ConfirmationUrl"], reference);
            return new ApiResponse() { StatusCode = 200, Data = JsonConvert.DeserializeObject<MPesaResponseDto>(result), StatusText = "Ok", Message = "Payment Done" };
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
            var result = new TMpesaResponse() { Amount = 1, MerchantRequestId = MerchantRequestID, CheckoutRequestId = CheckoutRequestID, state = "SUCCESS" };
            return new ApiResponse() { StatusCode = 200, StatusText = "Ok", Message = "Payment Done", Data = result };
        }
    }
}
