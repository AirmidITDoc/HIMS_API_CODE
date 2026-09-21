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
        private readonly IConfiguration _configuration;
        public AbhaConnectsController(IAbhaConnectService abhaConnectService, IGenericService<TAbhaCallbackformation> repository, IConfiguration configuration)
        {
            _abhaConnectService = abhaConnectService; 
            _repository = repository;
            _configuration = configuration;
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

                string ccWorkflowId = null;
                string ccMessage = null;
                string ccHipId = null;
                string ccErrMessage = null;
                try
                {
                    var careContextData = JsonSerializer.Deserialize<JsonElement>( JsonSerializer.Serialize(result));

                    if (careContextData.TryGetProperty("workflowId", out var workflowId)) ccWorkflowId = workflowId.ToString();
                    if (careContextData.TryGetProperty("message", out var message)) ccMessage = message.ToString();
                    if (careContextData.TryGetProperty("hipId", out var hipId)) ccHipId = hipId.ToString();
                    if (careContextData.TryGetProperty("errMessage", out var errMessage)) ccErrMessage = errMessage.ValueKind == JsonValueKind.Null ? null: errMessage.ToString();
                }
                catch
                {
                    ccWorkflowId = null;
                    ccMessage = null;
                    ccHipId = null;
                    ccErrMessage = JsonSerializer.Serialize(result);
                }

                await _abhaConnectService.SaveCareContextResponseAsync(model,ccWorkflowId,ccMessage, ccHipId, ccErrMessage);

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK,"Link care context request sent successfully.",result);
            }
            catch (Exception ex)
            {
                await _abhaConnectService.SaveCareContextResponseAsync(model, null, null, null, ex.Message);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, ex.Message);
            }
        }

[HttpPost("GetPatientEncounterDetailsTest")]
        public async Task<ApiResponse> GetPatientVisitss(PatientVisitRequest model)
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
                var EncounterUrl = _configuration["ABDMIntegration:PatientEncounterUrl"];

                HttpResponseMessage externalResponse = await client.PostAsync(EncounterUrl,content);

                string responseContent =await externalResponse.Content.ReadAsStringAsync();
                string peMessage = null;
                string peErrMessage = null;
                string peHipId = null;
                string pePatientReferenceNumber = null;
                string peCareContext = null;

                if (externalResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        var externalData = JsonSerializer.Deserialize<JsonElement>(responseContent);

                        if (externalData.ValueKind == JsonValueKind.Array &&
                            externalData.GetArrayLength() > 0)
                        {
                            var item = externalData[0];

                            if (item.TryGetProperty("message", out var message))
                                peMessage = message.ToString();

                            if (item.TryGetProperty("hipId", out var hipId))
                                peHipId = hipId.ToString();

                            if (item.TryGetProperty("patientReferenceNumber", out var patientReferenceNumber))
                                pePatientReferenceNumber = patientReferenceNumber.ToString();
                            if (item.TryGetProperty("careContexts", out var careContexts) && careContexts.ValueKind == JsonValueKind.Array && careContexts.GetArrayLength() > 0)
                                peCareContext = careContexts[0].ToString();
                        }
                    }
                    catch
                    {
                        peErrMessage = responseContent;
                    }
                }
                else
                {
                    try
                    {
                        var externalData = JsonSerializer.Deserialize<JsonElement>(responseContent);

                        if (externalData.ValueKind == JsonValueKind.Array &&
                            externalData.GetArrayLength() > 0 &&
                            externalData[0].TryGetProperty("errMessage", out var errMessage))
                        {
                            peErrMessage = errMessage.ToString();
                        }
                        else
                        {
                            peErrMessage = responseContent;
                        }
                    }
                    catch
                    {
                        peErrMessage = responseContent;
                    }
                }
                await _abhaConnectService.SavePatientEncounterAsync(model,peMessage, peErrMessage,peHipId, pePatientReferenceNumber, peCareContext);

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
