using HIMS.Data.DataProviders;
using HIMS.Data.DTO.AbhaIntegration;
using HIMS.Data.Models;
using HIMS.Services.AbhaIntegration;
using HIMS.Services.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Transactions;

namespace HIMS.Services.AbhaIntegration
{
    public class AbhaConnectService : IAbhaConnectService
    {

        private readonly IConfiguration _configuration;
        private readonly HIMSDbContext _context;
        private readonly ICommonService _ICommonService;
        //private readonly IAbhaConnectService _abhaConnectService;
        public AbhaConnectService(IConfiguration configuration, HIMSDbContext context, ICommonService ICommonService //, IAbhaConnectService abhaConnectService
            )
        {
            _configuration = configuration;
            _context = context;
            _ICommonService = ICommonService;
            //_abhaConnectService = abhaConnectService;
        }
        public async Task<object> InitiateClient(object model)
        {
            using var client = new HttpClient();

            var abhaClientSessionUrl = _configuration["ABDMIntegration:ClientSessionUrl"];
            var abhaBaseUrl = _configuration["ABDMIntegration:BaseUrl"];

            var request = new HttpRequestMessage(HttpMethod.Post, abhaClientSessionUrl);

            request.Headers.Add("accept", "*/*");
            request.Headers.Add("x-org-id", "1");

            var json = JsonSerializer.Serialize(model);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonNode.Parse(result).AsObject();
            var responseCode = jsonObject["responseCode"]?.GetValue<int>();
            var responseMessage = jsonObject["responseMessage"]?.GetValue<string>();

            if (responseCode != 200)
            {
                return new
                {
                    ResponseCode = responseCode,
                    ResponseMessage = responseMessage,
                    ResponseData = jsonObject["responseData"]
                };
            }

            var transactionId = jsonObject["responseData"]?["transactionId"]?.GetValue<string>();
            var callbackUrl = $"{abhaBaseUrl}/{transactionId}";

            jsonObject["responseData"]["callbackUrl"] = callbackUrl;

            return new
            {
                ResponseCode = responseCode,
                ResponseMessage = responseMessage,
                ResponseData = jsonObject["responseData"]
            };
        }

        public virtual async Task InsertAsync(TAbhaCallbackformation obj)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TAbhaCallbackformations.Add(obj);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

        public async Task<object> AuthenticateUserAsync()
        {
            using var client = new HttpClient();
            var authenticateUserUrl = _configuration["ABDMIntegration:AuthenticateUserUrl"];
            var authenticateUserName = _configuration["ABDMIntegration:username"];
            var authenticatePassword = _configuration["ABDMIntegration:password"];

            var request = new HttpRequestMessage(HttpMethod.Post, authenticateUserUrl);

            var obj = new { username = authenticateUserName, password = authenticatePassword };

            var jsonContent = JsonSerializer.Serialize(obj);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonNode.Parse(result).AsObject();
            var responseCode = jsonObject["responseCode"]?.GetValue<int>();
            var responseMessage = jsonObject["responseMessage"]?.GetValue<string>();

            var jwtToken = jsonObject["jwttoken"]?.GetValue<string>();
            var roles = jsonObject["roles"];


            return new
            {
                ResponseCode = responseCode ?? 200,
                ResponseMessage = responseMessage ?? "Authentication successful.",
                ResponseData = new
                {
                    jwttoken = jwtToken,
                    roles = roles
                }
            };
        }

        public async Task<string> CareContextAsync(Object model, string jwtToken)
        {
            var url = _configuration["ABDMIntegration:LinkCareContextUrl"];

            using var client = new HttpClient();

            using var request = new HttpRequestMessage(HttpMethod.Post, url);

            //Authorization header

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            //HIP ID header
            request.Headers.Add("X-HIP-ID", "AIRMIDABHA");

            //            Serialize request model
            var jsonContent = JsonSerializer.Serialize(model);

            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //          Send request
            var response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            //        Capture Kanaad response
            var responseObject =
                JsonSerializer.Deserialize<string>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return responseObject;
        }




        public async Task<List<PatientVisitResponse>> GetPatientVisitsAsync(PatientVisitRequest model)
        {
            string sp = "ps_GetPatientVisitDetails";

            DatabaseHelper sql = new();

            SqlParameter[] para =
             {
                new SqlParameter
                {
                    ParameterName = "@OpIpId",
                    Value = model.OpIpId
                },
                new SqlParameter
                {
                    ParameterName = "@OpIpType",
                    Value = model.OpIpType
                }
            };

            DataSet ds = sql.FetchDataSetBySP(sp, para);

            List<PatientVisitResponse> result = new();

            if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count == 0)
                return result;

            DataRow patientRow = ds.Tables[0].Rows[0];

            PatientVisitResponse response = new()
            {
                Patient = new Patient
                {
                    PatientRegistrationNumber = patientRow["PatientRegistrationNumber"].ToString(),
                    FirstName = patientRow["FirstName"].ToString(),
                    LastName = patientRow["LastName"].ToString(),
                    Gender = patientRow["Gender"].ToString(),
                    Mobile = patientRow["Mobile"].ToString(),
                    Email = patientRow["Email"].ToString(),
                    HealthId = model.AbhaAddress,
                    HealthIdNumber = model.AbhaNumber,
                    DayOfBirth = patientRow["DayOfBirth"].ToString(),
                    MonthOfBirth = patientRow["MonthOfBirth"].ToString(),
                    YearOfBirth = patientRow["YearOfBirth"].ToString(),
                    HipId = model.HipId
                },
                HipId = model.HipId,
                Visits = new List<Visit>()
            };

            foreach (DataRow row in ds.Tables[1].Rows)
            {
                response.Visits.Add(new Visit
                {
                    VisitNumber = row["VisitNumber"].ToString(),
                    VisitReason = row["VisitReason"].ToString(),

                    Doctor = new Doctor
                    {
                        Id = row["DoctorId"].ToString(),
                        FirstName = row["DoctorFirstName"].ToString(),
                        LastName = row["DoctorLastName"].ToString(),
                        Prefix = row["DoctorPrefix"].ToString(),
                        Designation = row["Designation"].ToString(),
                        Degree = row["Degree"].ToString(),
                        Speciality = row["Speciality"].ToString()
                    },

                    //StartDate = Convert.ToDateTime(row["StartDate"]),
                    //EndDate = Convert.ToDateTime(row["EndDate"]),
                    StartDate = row["StartDate"] == DBNull.Value ? null : Convert.ToDateTime(row["StartDate"]),
                    EndDate = row["EndDate"] == DBNull.Value ? null : Convert.ToDateTime(row["EndDate"]),
                    Status = row["Status"].ToString(),
                    VisitType = row["VisitType"].ToString(),

                    EncounterCode = new EncounterCode
                    {
                        Text = row["EncounterText"].ToString(),

                        Code = new EncounterCodeDetails
                        {
                            HospitalId = model.HipId,
                            Category = row["Category"].ToString(),
                            Url = row["EncounterUrl"].ToString(),
                            Code = row["EncounterCode"].ToString(),
                            Display = row["EncounterDisplay"].ToString()
                        }
                    }
                });
            }

            result.Add(response);
            return result;
        }

    }
}
