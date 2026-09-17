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

            // =====================================================
            // VISITS
            // =====================================================

            foreach (DataRow row in ds.Tables[1].Rows)
            {
                Visit visit = new Visit
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

                    StartDate = row["StartDate"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(row["StartDate"]),

                    EndDate = row["EndDate"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(row["EndDate"]),

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
                    },

                    Diagnosis = new List<Diagnosis>(),
                    ChiefComplaints = new List<ChiefComplaint>(),
                    Prescriptions = new List<Prescription>()
                };

                response.Visits.Add(visit);
            }


            // =====================================================
            // SECOND PROCEDURE
            // =====================================================

            DatabaseHelper sql1 = new();

            DataSet prescriptionDs = sql1.FetchDataSetBySP(
                "ps_GetPrescriptionPayload",
                new SqlParameter[]
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
                }
            );


            // =====================================================
            // DIAGNOSIS
            // =====================================================

            if (prescriptionDs != null &&
                prescriptionDs.Tables.Count > 0 &&
                prescriptionDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in prescriptionDs.Tables[0].Rows)
                {
                    response.Visits[0].Diagnosis.Add(new Diagnosis
                    {
                        Summary = row["DiagnosisSummary"].ToString(),

                        ConditionCode = new ConditionCode
                        {
                            Text = row["DiagnosisConditionText"].ToString(),

                            Code = new CodeDetails
                            {
                                HospitalId = model.HipId,
                                Category = row["ConditionCategory"].ToString(),
                                Url = row["ConditionUrl"].ToString(),
                                Code = row["ConditionCode"].ToString(),
                                Display = row["DiagnosisConditionText"].ToString()
                            }
                        },

                        RecordedDate = row["RecordedDate"].ToString()
                    });
                }
            }


            // =====================================================
            // CHIEF COMPLAINTS
            // =====================================================

            if (prescriptionDs != null &&
                prescriptionDs.Tables.Count > 1 &&
                prescriptionDs.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow row in prescriptionDs.Tables[1].Rows)
                {
                    response.Visits[0].ChiefComplaints.Add(new ChiefComplaint
                    {
                        Summary = row["Summary"].ToString(),

                        ConditionCode = new ConditionCode
                        {
                            Text = row["ConditionText"].ToString(),

                            Code = new CodeDetails
                            {
                                HospitalId = model.HipId,
                                Category = row["ConditionCategory"].ToString(),
                                Url = row["ConditionUrl"].ToString(),
                                Code = row["ConditionCode"].ToString(),
                                Display = row["ConditionText"].ToString()
                            }
                        },

                        RecordedDate = row["RecordedDate"].ToString()
                    });
                }
            }


            // =====================================================
            // PRESCRIPTIONS
            // =====================================================

            if (prescriptionDs != null &&
                prescriptionDs.Tables.Count > 2 &&
                prescriptionDs.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow row in prescriptionDs.Tables[2].Rows)
                {
                    response.Visits[0].Prescriptions.Add(new Prescription
                    {
                        Status = row["Status"].ToString(),
                        Intent = row["Intent"].ToString(),
                        AuthoredOn = row["AuthoredOn"].ToString(),

                        Drug = new Drug
                        {
                            DrugCode = new DrugCode
                            {
                                Text = row["DrugText"].ToString(),

                                Code = new CodeDetails
                                {
                                    HospitalId = model.HipId,
                                    Category = row["DrugCategory"].ToString(),
                                    Url = row["DrugUrl"].ToString(),
                                    Code = row["DrugCode"].ToString(),
                                    Display = row["DrugDisplay"].ToString()
                                }
                            },
                            Manufacturer = row["Manufacturer"].ToString(),

                            Brand = row["Brand"].ToString() == "1" ||
                                row["Brand"].ToString().ToLower() == "true"
                        },


                        Reason = new Reason
                        {
                            Text = row["ReasonText"].ToString(),

                            Code = new CodeDetails
                            {
                                HospitalId = model.HipId,
                                Category = row["ReasonCategory"].ToString(),
                                Url = row["ReasonUrl"].ToString(),
                                Code = row["ReasonCode"].ToString(),
                                Display = row["ReasonDisplay"].ToString()
                            }
                        },

                        Dosage = new Dosage
                        {
                            Text = row["DosageText"].ToString(),

                            AdditionalInstruction = new AdditionalInstruction
                            {
                                Text = row["AdditionalInstructionText"].ToString(),

                                Code = new CodeDetails
                                {
                                    HospitalId = model.HipId,
                                    Category = row["InstructionCategory"].ToString(),
                                    Url = row["InstructionUrl"].ToString(),
                                    Code = row["InstructionCode"].ToString(),
                                    Display = row["InstructionDisplay"].ToString()
                                }
                            },

                            Frequency = row["Frequency"].ToString(),
                            Period = row["Period"].ToString(),
                            PeriodUnit = row["PeriodUnit"].ToString(),

                            Route = new Route
                            {
                                Text = row["RouteText"].ToString(),

                                Code = new CodeDetails
                                {
                                    HospitalId = model.HipId,
                                    Category = row["RouteCategory"].ToString(),
                                    Url = row["RouteUrl"].ToString(),
                                    Code = row["RouteCode"].ToString(),
                                    Display = row["RouteDisplay"].ToString()
                                }
                            },

                            Method = new Method
                            {
                                Text = row["MethodText"].ToString(),

                                Code = new CodeDetails
                                {
                                    HospitalId = model.HipId,
                                    Category = row["MethodCategory"].ToString(),
                                    Url = row["MethodUrl"].ToString(),
                                    Code = row["MethodCode"].ToString(),
                                    Display = row["MethodDisplay"].ToString()
                                }
                            }
                        }
                    });
                }
            }


            // =====================================================
            // FINAL RESULT
            // =====================================================

            result.Add(response);

            return result;
        }

    }
}
