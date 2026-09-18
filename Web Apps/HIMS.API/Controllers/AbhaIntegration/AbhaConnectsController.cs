using Asp.Versioning;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.AbhaIntegration;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.MRD;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.AbhaIntegration;
using HIMS.Data.Models;
using HIMS.Services.AbhaIntegration;
using Microsoft.AspNetCore.Mvc;
using System.Text;
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
        public async Task<ApiResponse> LinkCareContext(Data.DTO.AbhaIntegration.CareContextModel model)
        {
            try
            {
                var authResult = await _abhaConnectService.AuthenticateUserAsync();
                var json = JsonSerializer.Serialize(authResult);
                var obj = JsonSerializer.Deserialize<JsonElement>(json);

                var responseData = obj.GetProperty("ResponseData");

                string jwtToken = responseData.GetProperty("jwttoken").ToString();

                var result = await _abhaConnectService.CareContextAsync(model,jwtToken);

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK,"Link care context request sent successfully.",result);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetPatientEncounterDetails")]
        public async Task<ApiResponse> GetPatientVisits(PatientVisitRequest model)
        {
            try
            {
                var authResult = await _abhaConnectService.AuthenticateUserAsync();
                var authJson = JsonSerializer.Serialize(authResult);
                var authObj = JsonSerializer.Deserialize<JsonElement>(authJson);

                var responseData = authObj.GetProperty("ResponseData");
                string jwtToken = responseData.GetProperty("jwttoken").ToString();

                var result = await _abhaConnectService.GetPatientVisitsAsync(model);

                string jsonPayload = JsonSerializer.Serialize(
                    result,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });


                using HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization",jwtToken);
                client.DefaultRequestHeaders.Add("X-HIP-ID",model.HipId);

                using StringContent content = new StringContent(jsonPayload,Encoding.UTF8,"application/json");

                HttpResponseMessage externalResponse = await client.PostAsync("https://kanaad.co.in/wrapper/hospital/patientEncounterDetails",content);

                string responseContent =await externalResponse.Content.ReadAsStringAsync();

                if (externalResponse.IsSuccessStatusCode) 
                { 
                    var externalData = JsonSerializer.Deserialize<JsonElement>(responseContent); 
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient encounter details fetched successfully.", externalData); 
                } 
                return ApiResponseHelper.GenerateResponse( (ApiStatusCode)(int)externalResponse.StatusCode, responseContent);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id?}")]
        public async Task<ApiResponse> Get(string id)
        {
            var data = await _repository.GetById(x => x.TransactionId == id);
            return data.ToSingleResponse<TAbhaCallbackformation, AbhaCallbackModel>("Callbackformation");
        }
    }
}
