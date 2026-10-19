using Asp.Versioning;
using HIMS.ABHA.Models.M2;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.AbhaIntegration;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.MRD;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.AbhaIntegration;
using HIMS.Data.Models;
using HIMS.Services.AbhaIntegration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using static LinqToDB.Sql;

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
        private readonly IGenericService<TabhaPatientEncounterCareContextDetail> _repository1;
        public AbhaConnectsController(IAbhaConnectService abhaConnectService, IGenericService<TAbhaCallbackformation> repository, IConfiguration configuration, IGenericService<TabhaPatientEncounterCareContextDetail> repository1)
        {
            _abhaConnectService = abhaConnectService; 
            _repository = repository;
            _configuration = configuration;
            _repository1 = repository1;
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
                    var careContextData = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(result));

                    if (careContextData.TryGetProperty("workflowId", out var workflowId)) ccWorkflowId = workflowId.ToString();
                    if (careContextData.TryGetProperty("message", out var message)) ccMessage = message.ToString();
                    if (careContextData.TryGetProperty("hipId", out var hipId)) ccHipId = hipId.ToString();
                    if (careContextData.TryGetProperty("errMessage", out var errMessage)) ccErrMessage = errMessage.ValueKind == JsonValueKind.Null ? null : errMessage.ToString();
                }
                catch
                {
                    ccWorkflowId = null;
                    ccMessage = null;
                    ccHipId = null;
                    ccErrMessage = JsonSerializer.Serialize(result);
                }

                await _abhaConnectService.SaveCareContextResponseAsync(model, ccWorkflowId, ccMessage, ccHipId, ccErrMessage);

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
                await _abhaConnectService.SavePatientEncounterAsync(model, peMessage, peErrMessage, peHipId, pePatientReferenceNumber, peCareContext);

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
        [HttpGet("Encounter/{id?}")]
        public async Task<ApiResponse> GetEncounter(int id)
        {
            var data = await _repository1.GetById(x => x.Peccid == id);
            return data.ToSingleResponse<TabhaPatientEncounterCareContextDetail, abhaPatientEncounterCareContextDetailModel>("Patient Encounter Care Context Details");
        }

        [HttpPost("ValidateHiTypes")]
        public async Task<ApiResponse> ValidateHiTypes(PatientVisitRequest model)
        {
            try
            {
                var result = await _abhaConnectService.GetPatientVisitsAsync(model);
                var payload = result?.FirstOrDefault();

                if (payload == null || payload.Visits == null || payload.Visits.Count == 0)
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "No visit data found.");

                var validation = _abhaConnectService.Validate(payload);

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "HI type validation completed.", validation);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("ValidateHiTypesSummary")]
        public async Task<ApiResponse> ValidateHiTypesSummary(PatientVisitRequest model)
        {
            try
            {
                var result = await _abhaConnectService.GetPatientVisitsAsync(model);
                var payload = result?.FirstOrDefault();

                if (payload?.Visits == null || payload.Visits.Count == 0)
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "No visit data found.");

                var summary = _abhaConnectService.ValidateHiTypesSummary(payload);

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK,"HI type validation summary completed.",summary);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("AbhaErrorLog")]
        public async Task<ApiResponse> InsertErrorLog(AbhaErroLog obj)
        {
            TAbhaErrorLog model = obj.MapTo<TAbhaErrorLog>();
            await _abhaConnectService.InsertErrorLogAsync(model);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Error log added successfully.");
        }


        [HttpPost("UploadReportFiles")]
        [Consumes("multipart/form-data")]
        public async Task<ApiResponse> UploadReportFiles(string patientRegistrationNumber,string visitNumber,long OpIpId,long opipType,[FromForm] List<IFormFile> files)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(patientRegistrationNumber) || string.IsNullOrWhiteSpace(visitNumber))
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest,"patientRegistrationNumber and visitNumber are required.");

                if (files == null || files.Count == 0 || files.All(f => f.Length == 0))
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest,"Please select at least one file.");

                // Fresh JWT, same way your encounter API does it
                var authResult = await _abhaConnectService.AuthenticateUserAsync();
                var authObj = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(authResult));
                string jwtToken = authObj.GetProperty("ResponseData").GetProperty("jwttoken").ToString();

                var uploadUrl = _configuration["ABDMIntegration:KanaadUploadUrl"];
                var hipId = _configuration["ABDMIntegration:HipId"];
                using var client = new HttpClient();
                using var request = new HttpRequestMessage(HttpMethod.Post, uploadUrl);
                request.Headers.Add("Authorization", jwtToken);
                request.Headers.Add("X-HIP-ID", hipId);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(patientRegistrationNumber), "patientRegistrationNumber");
                content.Add(new StringContent(visitNumber), "visitNumber");

                foreach (var file in files.Where(f => f.Length > 0))
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);          // read this file fully, one at a time
                    var bytes = ms.ToArray();

                    var fileContent = new ByteArrayContent(bytes);
                    if (!string.IsNullOrEmpty(file.ContentType))
                        fileContent.Headers.ContentType =
                            new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                    content.Add(fileContent, "files", Path.GetFileName(file.FileName));
                }

                request.Content = content;

                var response = await client.SendAsync(request);
                var responseText = await response.Content.ReadAsStringAsync();

                JsonElement data;
                try
                {
                    data = JsonSerializer.Deserialize<JsonElement>(responseText);
                }
                catch
                {
                    return ApiResponseHelper.GenerateResponse( (ApiStatusCode)(int)response.StatusCode,"Kanaad upload failed.",responseText);
                }

                // Return error without inserting database rows
                if (!response.IsSuccessStatusCode || data.ValueKind != JsonValueKind.Object ||!data.TryGetProperty("success", out var successElement) ||successElement.ValueKind != JsonValueKind.True)
                {
                    return ApiResponseHelper.GenerateResponse((ApiStatusCode)(int)response.StatusCode, "Kanaad upload failed.", data);
                }

                // Get uploaded file names returned by Kanaad
                if (!data.TryGetProperty("fileNames", out var fileNamesElement) || fileNamesElement.ValueKind != JsonValueKind.Array)
                {
                    return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status500InternalServerError, "Kanaad reported success but returned no valid fileNames.",data);
                }

                var fileNames = fileNamesElement.EnumerateArray().Where(x => x.ValueKind == JsonValueKind.String).Select(x => x.GetString()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                if (fileNames.Count == 0)
                {
                    return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status500InternalServerError,"Kanaad reported success but returned no file names.",data);
                }

                // Insert one database row for each returned file name
                var uploadRecords = fileNames.Select(fileName => new TAbhaFileUpload
                {
                    RegNo = patientRegistrationNumber,
                    Opipno = visitNumber,
                    Opipid = OpIpId,
                    Opiptype = opipType,
                    FileName = fileName,
                    CreatedDate = DateTime.Now
                }).ToList();

                await _abhaConnectService.InsertAbhaFileUploadsAsync(uploadRecords);

                return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status200OK,"Files uploaded to Kanaad successfully and saved in the database.",data);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("FileUploadList")]
        //[Permission]
        public async Task<IActionResult> FileUploadList(GridRequestModel objGrid)
        {
            IPagedList<AbhaFileUploadListDto> FileUpload = await _abhaConnectService.GetAbhaFileUploadListAsync(objGrid);
            return Ok(FileUpload.ToGridResponse(objGrid, "Abha File Upload List"));
        }
        [HttpPost("AbhaAddressList")]
        public async Task<IActionResult> AbhaAddressList([FromQuery] string abhaNumber)
        {
            var objGrid = new GridRequestModel
            {
                First = 0,
                Rows = 1000,
                SortField = "AbhaNumber",
                SortOrder = 1,
                ExportType = ExportType.JSON,
                Columns = new List<GridColumn>(),
                Filters = new List<SearchGrid>
        {
            new SearchGrid
            {
                FieldName = "AbhaNumber",
                FieldValue = abhaNumber,
                OpType = OperatorComparer.Contains
            }
        }
            };

            IPagedList<AbhaAddressDropdownDto> abhaAddresses =
                await _abhaConnectService.GetAbhaAddressListAsync(objGrid);

            return Ok(abhaAddresses.ToGridResponse(objGrid, "Abha Address List"));
        }
    }
}
