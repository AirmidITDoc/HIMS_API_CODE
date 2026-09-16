using Asp.Versioning;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.AbhaIntegration;
using HIMS.API.Models.MRD;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.AbhaIntegration;
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
    public class AbhaConnectsController : ControllerBase
    {
        private readonly IAbhaConnectService _abhaConnectService;
        private readonly IGenericService<TAbhaCallbackformation> _repository;
        public AbhaConnectsController(IAbhaConnectService abhaConnectService, IGenericService<TAbhaCallbackformation> repository)
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
        }

        [HttpPost("authenticateUser")]
        public async Task<ApiResponse> authenticateUser()
        {
            var result = await _abhaConnectService.AuthenticateUserAsync();
            var json = JsonSerializer.Serialize(result);
            var obj = JsonSerializer.Deserialize<JsonElement>(json);
            var responseData = obj.GetProperty("ResponseData");

            return ApiResponseHelper.GenerateResponse(
                ApiStatusCode.Status200OK,
                "Authentication successful.",
                responseData
            );
        }

        [HttpPost("linkCareContext")]
        public async Task<ApiResponse> LinkCareContext(CareContextModel model)
        {
            // Step 1: Authenticate with Kanaad
            var authResult = await _abhaConnectService.AuthenticateUserAsync();
            var json = JsonSerializer.Serialize(authResult);
            var obj = JsonSerializer.Deserialize<JsonElement>(json);
            var responseData = obj.GetProperty("ResponseData");

            //string jwtTokenvalue = responseData.GetProperty("jwttoken").ToString();
            // Get JWT token from your authentication flow
            string jwtToken = responseData.GetProperty("jwttoken").ToString();


            var result = await _abhaConnectService.CareContextAsync(model, jwtToken);
            return ApiResponseHelper.GenerateResponse(
                 ApiStatusCode.Status200OK,
                 "Link care context request sent successfully.",
                 new
                 {
                     //workflowId = result.workflowId,
                     //message = result.message,
                     //hipId = result.hipId,
                     //errMessage = result.errMessage
                 }
             );
        }

        [HttpPost("GetPatientEncounterDetails")]
        public async Task<ApiResponse> GetPatientVisits(PatientVisitRequest model)
        {
            try
            {
                var result = await _abhaConnectService.GetPatientVisitsAsync(model);

                return ApiResponseHelper.GenerateResponse(
                    ApiStatusCode.Status200OK,
                    "Patient visit details fetched successfully.",
                    result);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.GenerateResponse(
                    ApiStatusCode.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
