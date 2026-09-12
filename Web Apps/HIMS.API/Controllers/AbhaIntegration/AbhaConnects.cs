using Asp.Versioning;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.AbhaIntegration;
using HIMS.API.Models.MRD;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.AbhaIntegration;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HIMS.API.Controllers.AbhaIntegration
{
    //[ApiController]
   // [Route("api/[controller]")]

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AbhaConnects : ControllerBase
    {
        private readonly IAbhaConnectService _abhaConnectService;
        private readonly IGenericService<TAbhaCallbackformation> _repository;
        public AbhaConnects(IAbhaConnectService abhaConnectService, IGenericService<TAbhaCallbackformation> repository)
        {
            _abhaConnectService = abhaConnectService; 
            _repository = repository;
        }

        [HttpPost("InitiateClient")]
        public async Task<ApiResponse> InitiateClient(InitiateClientModel model)
        {
            var result = await _abhaConnectService.InitiateClient(model);

            var json = JsonSerializer.Serialize(result);
            var obj = JsonSerializer.Deserialize<JsonElement>(json);

            var responseCode = obj.GetProperty("ResponseCode").GetInt32();
            var responseMessage = obj.GetProperty("ResponseMessage").GetString();
            var responseData = obj.GetProperty("ResponseData");

            return ApiResponseHelper.GenerateResponse(
                (ApiStatusCode)responseCode,
                responseMessage,
                responseData
            );
        }

        [HttpPost("AbhaCallback")]
        public async Task<ApiResponse> Insert(AbhaCallbackModel obj)
        {
            TAbhaCallbackformation model = obj.MapTo<TAbhaCallbackformation>();
            await _abhaConnectService.InsertAsync(model);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.TransactionId);
        }
    }
}
