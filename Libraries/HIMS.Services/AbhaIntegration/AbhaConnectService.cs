using HIMS.Data;
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

        public async Task<JsonElement> CareContextAsync(CareContextModel model, string jwtToken)
        {
            var url = _configuration["ABDMIntegration:LinkCareContextUrl"];

            using var client = new HttpClient();

            using var request = new HttpRequestMessage(HttpMethod.Post,url);

            request.Headers.TryAddWithoutValidation("Authorization",jwtToken);
            request.Headers.Add("X-HIP-ID", "AIRMIDABHA");
            var jsonContent = JsonSerializer.Serialize(model);

            request.Content = new StringContent(jsonContent,Encoding.UTF8,"application/json");

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }
            return JsonSerializer.Deserialize<JsonElement>(result);
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

                    //StartDate = row["StartDate"] == DBNull.Value
                    //    ? null
                    //    : Convert.ToDateTime(row["StartDate"]),

                    //EndDate = row["EndDate"] == DBNull.Value
                    //    ? null
                    //    : Convert.ToDateTime(row["EndDate"]),
                    StartDate = row["StartDate"] == DBNull.Value
    ? ""
    : Convert.ToDateTime(row["StartDate"]).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),

                    EndDate = row["EndDate"] == DBNull.Value
    ? ""
    : Convert.ToDateTime(row["EndDate"]).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),


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
                    Prescriptions = new List<Prescription>(),
                    DiagnosticReports = new List<DiagnosticReport>(),
                    DischargeSummaries = new List<DischargeSummaryItem>(),
                    ObservationResult = new List<ObservationResult>(),
                    AllergiesData = new List<AllergyData>(),
                    PhysicalExams = new List<PhysicalExam>(),
                    InvoiceRecord = new List<InvoiceRecord>(),
                    Procedures = new List<Procedure>(),
                    FamilyMedicalHistory = new List<FamilyMedicalHistory>(),
                    CarePlan = new List<CarePlan>(),
                    Reports = new List<Reports>(),
                    FollowUp = null
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

                       // RecordedDate = row["RecordedDate"].ToString()
                        RecordedDate = row["RecordedDate"] == DBNull.Value? null: Convert.ToDateTime(row["RecordedDate"]).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
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

                        //RecordedDate = row["RecordedDate"].ToString()
                         RecordedDate = row["RecordedDate"] == DBNull.Value ? null : Convert.ToDateTime(row["RecordedDate"]).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
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
                        // AuthoredOn = row["AuthoredOn"].ToString(),
                        AuthoredOn = row["AuthoredOn"] == DBNull.Value ? null : Convert.ToDateTime(row["AuthoredOn"]).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),

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
             //=====================================================
             //DIAGNOSTIC REPORTS + DISCHARGE SUMMARY

            DatabaseHelper sql2 = new();

            DataSet diagnosticDs = sql2.FetchDataSetBySP(
                "ps_GetDiagnosticReports", // rename to match your actual SP name
                new SqlParameter[]
                {
        new SqlParameter { ParameterName = "@OpIpId", Value = model.OpIpId },
        new SqlParameter { ParameterName = "@OpIpType", Value = model.OpIpType }
                }
            );

            response.Visits[0].DiagnosticReports = new List<DiagnosticReport>();
            response.Visits[0].DischargeSummaries = new List<DischargeSummaryItem>();

            if (diagnosticDs != null && diagnosticDs.Tables.Count > 0 && diagnosticDs.Tables[0].Rows.Count > 0)
            {
                var groupedByTest = diagnosticDs.Tables[0].AsEnumerable()
                    .GroupBy(row => row["DiagnosticCode"].ToString());

                foreach (var testGroup in groupedByTest)
                {
                    DataRow firstRow = testGroup.First();

                    DiagnosticReport report = new()
                    {
                        Status = firstRow["DiagnosticStatus"].ToString(),

                        DiagnosticCode = new DiagnosticCodeableConcept
                        {
                            Text = firstRow["DiagnosticText"].ToString(),
                            Code = new CodeDetails
                            {
                                HospitalId = model.HipId,
                                Category = firstRow["DiagnosticCategory"].ToString(),
                                Url = firstRow["DiagnosticUrl"].ToString(),
                                Code = firstRow["DiagnosticCode"].ToString(),
                                Display = firstRow["DiagnosticDisplay"].ToString()
                            }
                        },

                        Conclusion = firstRow["Conclusion"].ToString(),
                        EffectiveDate = firstRow["EffectiveDate"].ToString(),
                        IssuedAt = firstRow["IssuedAt"].ToString(),

                        Results = new List<ObservationResult>()
                    };

                    foreach (DataRow row in testGroup)
                    {
                        //object resultValue = decimal.TryParse(row["ResultValue"].ToString(), out decimal rv)
                        //    ? rv
                        //    : row["ResultValue"].ToString();
                        string resultText = row["ResultValue"].ToString();
                        bool isNumeric = decimal.TryParse(resultText, out decimal numericValue);

                        object refHighValue = decimal.TryParse(row["ReferenceHighValue"].ToString(), out decimal rh)
                            ? rh
                            : row["ReferenceHighValue"].ToString();

                        object refLowValue = decimal.TryParse(row["ReferenceLowValue"].ToString(), out decimal rl)
                            ? rl
                            : row["ReferenceLowValue"].ToString();

                        //report.Results.Add(new ObservationResult
                        //{
                        //    Status = row["ResultStatus"].ToString(),

                        //    ResultCode = new ResultCodeableConcept
                        //    {
                        //        Text = row["ResultText"].ToString(),
                        //        Code = new CodeDetails
                        //        {
                        //            HospitalId = model.HipId,
                        //            Category = row["ResultCategory"].ToString(),
                        //            Url = row["ResultUrl"].ToString(),
                        //            Code = row["ResultCode"].ToString(),
                        //            Display = row["ResultDisplay"].ToString()
                        //        }
                        //    },

                        //    Value = new ValueQuantity
                        //    {
                        //        Value = resultValue,
                        //        Code = new QuantityCode
                        //        {
                        //            Id = row["ResultValueId"].ToString(),
                        //            Unit = row["ResultUnit"].ToString(),
                        //            Url = row["ResultValueUrl"].ToString(),
                        //            Code = row["ResultValueCode"].ToString()
                        //        }
                        //    },

                        //    Category = new CategoryCodeableConcept
                        //    {
                        //        Text = row["ResultCategoryText"].ToString(),
                        //        Code = new CodeDetails
                        //        {
                        //            HospitalId = model.HipId,
                        //            Category = row["ResultCategory"].ToString(),
                        //            Url = row["ResultCategoryUrl"].ToString(),
                        //            Code = row["ResultCategoryCode"].ToString(),
                        //            Display = row["ResultCategoryDisplay"].ToString()
                        //        }
                        //    },

                        //    ReferenceRange = new ReferenceRange
                        //    {
                        //        High = new ValueQuantity
                        //        {
                        //            Value = refHighValue,
                        //            Code = new QuantityCode
                        //            {
                        //                Id = row["ReferenceHighId"].ToString(),
                        //                Unit = row["ReferenceHighUnit"].ToString(),
                        //                Url = row["ReferenceHighUrl"].ToString(),
                        //                Code = row["ReferenceHighCode"].ToString()
                        //            }
                        //        },
                        //        Low = new ValueQuantity
                        //        {
                        //            Value = refLowValue,
                        //            Code = new QuantityCode
                        //            {
                        //                Id = row["ReferenceLowId"].ToString(),
                        //                Unit = row["ReferenceLowUnit"].ToString(),
                        //                Url = row["ReferenceLowUrl"].ToString(),
                        //                Code = row["ReferenceLowCode"].ToString()
                        //            }
                        //        }
                        //    },

                        //    EffectiveOn = row["EffectiveOn"].ToString(),

                        //    Interpretation = new InterpretationCodeableConcept
                        //    {
                        //        Text = row["InterpretationText"].ToString(),
                        //        Code = new CodeDetails
                        //        {
                        //            HospitalId = model.HipId,
                        //            Category = row["InterpretationCategory"].ToString(),
                        //            Url = row["InterpretationUrl"].ToString(),
                        //            Code = row["InterpretationCode"].ToString(),
                        //            Display = row["InterpretationDisplay"].ToString()
                        //        }
                        //    }
                        //});
                        report.Results.Add(new ObservationResult
                        {
                            Status = row["ResultStatus"].ToString(),

                            ResultCode = new ResultCodeableConcept
                            {
                                Text = row["ResultText"].ToString(),
                                Code = new CodeDetails
                                {
                                    HospitalId = model.HipId,
                                    Category = row["ResultCategory"].ToString(),
                                    Url = row["ResultUrl"].ToString(),
                                    Code = row["ResultCode"].ToString(),
                                    Display = row["ResultDisplay"].ToString()
                                }
                            },

                            Value = isNumeric
                            ? new ValueQuantity
                            {
                                Value = numericValue,
                                Code = new QuantityCode
                                {
                                    Id = row["ResultValueId"].ToString(),
                                    Unit = row["ResultUnit"].ToString(),
                                    Url = row["ResultValueUrl"].ToString(),
                                    Code = row["ResultValueCode"].ToString()
                                }
                            }
                            : null,

                            ValueString = isNumeric ? null : resultText,

                            Category = new CategoryCodeableConcept
                            {
                                Text = row["ResultCategoryText"].ToString(),
                                Code = new CodeDetails
                                {
                                    HospitalId = model.HipId,
                                    Category = row["ResultCategory"].ToString(),
                                    Url = row["ResultCategoryUrl"].ToString(),
                                    Code = row["ResultCategoryCode"].ToString(),
                                    Display = row["ResultCategoryDisplay"].ToString()
                                }
                            },

                            ReferenceRange = isNumeric
                                ? new ReferenceRange
                                {
                                    High = new ValueQuantity
                                    {
                                        Value = refHighValue,
                                        Code = new QuantityCode
                                        {
                                            Id = row["ReferenceHighId"].ToString(),
                                            Unit = row["ReferenceHighUnit"].ToString(),
                                            Url = row["ReferenceHighUrl"].ToString(),
                                            Code = row["ReferenceHighCode"].ToString()
                                        }
                                    },
                                    Low = new ValueQuantity
                                    {
                                        Value = refLowValue,
                                        Code = new QuantityCode
                                        {
                                            Id = row["ReferenceLowId"].ToString(),
                                            Unit = row["ReferenceLowUnit"].ToString(),
                                            Url = row["ReferenceLowUrl"].ToString(),
                                            Code = row["ReferenceLowCode"].ToString()
                                        }
                                    }
                                }
                                : null,

                            EffectiveOn = row["EffectiveOn"].ToString(),

                            Interpretation = isNumeric
                            ? new InterpretationCodeableConcept
                            {
                                Text = row["InterpretationText"].ToString(),
                                Code = new CodeDetails
                                {
                                    HospitalId = model.HipId,
                                    Category = row["InterpretationCategory"].ToString(),
                                    Url = row["InterpretationUrl"].ToString(),
                                    Code = row["InterpretationCode"].ToString(),
                                    Display = row["InterpretationDisplay"].ToString()
                                }
                            }
                            : null
                                            });
                    }

                    response.Visits[0].DiagnosticReports.Add(report);
                }
            }

            if (diagnosticDs != null && diagnosticDs.Tables.Count > 1 && diagnosticDs.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow row in diagnosticDs.Tables[1].Rows)
                {
                    string dischargeSummary = System.Text.RegularExpressions.Regex.Replace(
                        row["DischargeSummary"].ToString(),
                        "<.*?>",
                        string.Empty
                    );

                    dischargeSummary = System.Net.WebUtility.HtmlDecode(dischargeSummary).Trim();

                    response.Visits[0].DischargeSummaries.Add(new DischargeSummaryItem
                    {
                        DischargeSummary = dischargeSummary,
                        DischargeStatus = row["DischargeStatus"].ToString()
                    });
                }
            }
            response.Visits[0].ObservationResult = GetObservations(diagnosticDs != null && diagnosticDs.Tables.Count > 2 ? diagnosticDs.Tables[2] : null, model.HipId);



            DatabaseHelper sql3 = new();

            DataSet allergyDs = sql3.FetchDataSetBySP("ps_GetAllergyPhysicalExam",new SqlParameter[]
                {
                new SqlParameter { ParameterName = "@OpIpId", Value = model.OpIpId },
                new SqlParameter { ParameterName = "@OpIpType", Value = model.OpIpType }
                }
            );


            response.Visits[0].AllergiesData = GetAllergiesData(allergyDs != null && allergyDs.Tables.Count > 0 ? allergyDs.Tables[0] : null,model.HipId);

            response.Visits[0].PhysicalExams = GetPhysicalExams(allergyDs != null && allergyDs.Tables.Count > 1 ? allergyDs.Tables[1] : null);

            response.Visits[0].InvoiceRecord = GetInvoiceRecords( allergyDs != null && allergyDs.Tables.Count > 2 ? allergyDs.Tables[2] : null,model.HipId);


            DatabaseHelper medicalHistorySql = new();

            DataSet medicalHistoryDs = medicalHistorySql.FetchDataSetBySP("ps_GetMedicalHistory", new SqlParameter[]
                {
                new SqlParameter { ParameterName = "@OpIpId", Value = model.OpIpId },
                new SqlParameter { ParameterName = "@OpIpType", Value = model.OpIpType }
                }
            );

            response.Visits[0].MedicalHistory = GetMedicalHistory(medicalHistoryDs != null && medicalHistoryDs.Tables.Count > 0 ? medicalHistoryDs.Tables[0] : null,model.HipId);

            response.Visits[0].Referrals = GetReferrals(medicalHistoryDs != null && medicalHistoryDs.Tables.Count > 1 ? medicalHistoryDs.Tables[1] : null,model.HipId);

            response.Visits[0].InvestigationAdvice = GetInvestigationAdvice(medicalHistoryDs != null && medicalHistoryDs.Tables.Count > 2 ? medicalHistoryDs.Tables[2] : null,model.HipId);

            DatabaseHelper proceduresSql = new();

            DataSet proceduresDs = proceduresSql.FetchDataSetBySP("ps_GetProcedures", new SqlParameter[]
                {
                new SqlParameter { ParameterName = "@OpIpId", Value = model.OpIpId },
                new SqlParameter { ParameterName = "@OpIpType", Value = model.OpIpType }
                }
            );

            response.Visits[0].Procedures = GetProcedures(proceduresDs != null && proceduresDs.Tables.Count > 0 ? proceduresDs.Tables[0] : null,model.HipId);

            response.Visits[0].FamilyMedicalHistory = GetFamilyMedicalHistory(proceduresDs != null && proceduresDs.Tables.Count > 1 ? proceduresDs.Tables[1] : null,model.HipId);

            response.Visits[0].CarePlan = GetCarePlan(proceduresDs != null && proceduresDs.Tables.Count > 2 ? proceduresDs.Tables[2] : null,model.HipId);

            response.Visits[0].Reports = GetReports(proceduresDs != null && proceduresDs.Tables.Count > 3 ? proceduresDs.Tables[3] : null);

            response.Visits[0].FollowUp = GetFollowUp(proceduresDs != null && proceduresDs.Tables.Count > 4 ? proceduresDs.Tables[4] : null,model.HipId);
            // =====================================================
            // FINAL RESULT
            // =====================================================

            result.Add(response);

            return result;
        }
        private FollowUp GetFollowUp(DataTable table, string hipId)
        {
            if (table == null || table.Rows.Count == 0)
                return null;

            DataRow row = table.Rows[0];

            return new FollowUp
            {
                status = row["Status"].ToString(),

                serviceCategory = new CodeableConcept
                {
                    text = row["ServiceCategoryText"].ToString(),
                    code = new CodeDetails
                    {
                        HospitalId = hipId,
                        Category = row["ServiceCategoryCategory"].ToString(),
                        Url = row["ServiceCategoryUrl"].ToString(),
                        Code = row["ServiceCategoryCode"].ToString(),
                        Display = row["ServiceCategoryDisplay"].ToString()
                    }
                },

                serviceType = new List<CodeableConcept>
        {
            new CodeableConcept
            {
                text = row["ServiceTypeText"].ToString(),
                code = new CodeDetails
                {
                    HospitalId = hipId,
                    Category = row["ServiceTypeCategory"].ToString(),
                    Url = row["ServiceTypeUrl"].ToString(),
                    Code = row["ServiceTypeCode"].ToString(),
                    Display = row["ServiceTypeDisplay"].ToString()
                }
            }
        },

                specialty = new List<CodeableConcept>
        {
            new CodeableConcept
            {
                text = row["SpecialtyText"].ToString(),
                code = new CodeDetails
                {
                    HospitalId = hipId,
                    Category = row["SpecialtyCategory"].ToString(),
                    Url = row["SpecialtyUrl"].ToString(),
                    Code = row["SpecialtyCode"].ToString(),
                    Display = row["SpecialtyDisplay"].ToString()
                }
            }
        },

                reasonCode = new List<CodeableConcept>
        {
            new CodeableConcept
            {
                text = row["ReasonCodeText"].ToString(),
                code = new CodeDetails
                {
                    HospitalId = hipId,
                    Category = row["ReasonCodeCategory"].ToString(),
                    Url = row["ReasonCodeUrl"].ToString(),
                    Code = row["ReasonCodeCode"].ToString(),
                    Display = row["ReasonCodeDisplay"].ToString()
                }
            }
        },

                start = row["Start"].ToString(),
                end = row["End"].ToString(),
                description = row["Description"].ToString()
            };
        }
        private List<Reports> GetReports(DataTable table)
        {
            List<Reports> reports = new();

            if (table == null || table.Rows.Count == 0)
                return reports;

            foreach (DataRow row in table.Rows)
            {
                reports.Add(new Reports
                {
                    visitNumber = row["VisitNumber"].ToString(),
                    patientRegistrationNumber = row["PatientRegistrationNumber"].ToString(),
                    fileName = row["FileName"].ToString(),
                    documentType = row["DocumentType"].ToString()
                });
            }

            return reports;
        }
        private List<CarePlan> GetCarePlan(DataTable table, string hipId)
        {
            List<CarePlan> carePlans = new();

            if (table == null || table.Rows.Count == 0)
                return carePlans;

            foreach (DataRow row in table.Rows)
            {
                carePlans.Add(new CarePlan
                {
                    status = row["Status"].ToString(),
                    intent = row["Intent"].ToString(),

                    category = new List<CodeableConcept>
            {
                new CodeableConcept
                {
                    text = row["CategoryText"].ToString(),

                    code = new CodeDetails
                    {
                        HospitalId = hipId,
                        Category = row["CategoryType"].ToString(),
                        Url = row["CategoryUrl"].ToString(),
                        Code = row["CategoryCode"].ToString(),
                        Display = row["CategoryDisplay"].ToString()
                    }
                }
            },

                    codeDescription = row["CodeDescription"].ToString(),
                    title = row["Title"].ToString()
                });
            }

            return carePlans;
        }
        private List<FamilyMedicalHistory> GetFamilyMedicalHistory(DataTable table, string hipId)
        {
            List<FamilyMedicalHistory> history = new();

            if (table == null || table.Rows.Count == 0)
                return history;

            foreach (DataRow row in table.Rows)
            {
                history.Add(new FamilyMedicalHistory
                {
                    status = row["Status"].ToString(),
                    subjectId = row["SubjectId"].ToString(),

                    relationship = new CodeableConcept
                    {
                        text = row["RelationshipText"].ToString(),

                        code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["RelationshipCategory"].ToString(),
                            Url = row["RelationshipUrl"].ToString(),
                            Code = row["RelationshipCode"].ToString(),
                            Display = row["RelationshipDisplay"].ToString()
                        }
                    },

                    name = row["Name"].ToString(),
                    contributedToDeath = Convert.ToBoolean(row["ContributedToDeath"]),
                    age = Convert.ToInt32(row["Age"]),
                    reasonCode = new List<CodeableConcept>
            {
                new CodeableConcept
                {
                    text = row["ReasonCodeText"].ToString(),

                    code = new CodeDetails
                    {
                        HospitalId = hipId,
                        Category = row["ReasonCodeCategory"].ToString(),
                        Url = row["ReasonCodeUrl"].ToString(),
                        Code = row["ReasonCode"].ToString(),
                        Display = row["ReasonCodeDisplay"].ToString()
                    }
                }
            },

                    date = row["Date"].ToString(),
                    gender = row["Gender"].ToString(),

                    condition = new List<MedicalHistory>
            {
                new MedicalHistory
                {
                    summary = row["ConditionSummary"].ToString(),

                    conditionCode = new ConditionCode
                    {
                        Text = row["ConditionText"].ToString(),

                        Code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["ConditionCategory"].ToString(),
                            Url = row["ConditionUrl"].ToString(),
                            Code = row["ConditionCode"].ToString(),
                            Display = row["ConditionDisplay"].ToString()
                        }
                    },

                    recordedDate = row["ConditionRecordedDate"].ToString()
                }
            }
                });
            }

            return history;
        }
        private List<Procedure> GetProcedures(DataTable table, string hipId)
        {
            List<Procedure> procedures = new();

            if (table == null || table.Rows.Count == 0)
                return procedures;

            foreach (DataRow row in table.Rows)
            {
                procedures.Add(new Procedure
                {
                    status = row["Status"].ToString(),

                    procedureCode = new CodeableConcept
                    {
                        text = row["ProcedureText"].ToString(),

                        code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["ProcedureCategory"].ToString(),
                            Url = row["ProcedureUrl"].ToString(),
                            Code = row["ProcedureCode"].ToString(),
                            Display = row["ProcedureDisplay"].ToString()
                        }
                    },

                    performedAt = row["PerformedAt"].ToString(),

                    complications = new List<CodeableConcept>
            {
                new CodeableConcept
                {
                    text = row["ComplicationText"].ToString(),

                    code = new CodeDetails
                    {
                        HospitalId = hipId,
                        Category = "Complication",
                        Url = "https://complications.example.com",
                        Code = row["ComplicationCode"].ToString(),
                        Display = row["ComplicationDisplay"].ToString()
                    }
                }
            }
                });
            }

            return procedures;
        }
        private List<InvestigationAdvice> GetInvestigationAdvice(DataTable table, string hipId)
        {
            List<InvestigationAdvice> investigationAdvice = new();

            if (table == null || table.Rows.Count == 0)
                return investigationAdvice;

            foreach (DataRow row in table.Rows)
            {
                investigationAdvice.Add(new InvestigationAdvice
                {
                    investigation = new CodeableConcept
                    {
                        text = row["InvestigationText"].ToString(),

                        code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["InvestigationCategory"].ToString(),
                            Url = row["InvestigationUrl"].ToString(),
                            Code = row["InvestigationCode"].ToString(),
                            Display = row["InvestigationDisplay"].ToString()
                        }
                    },

                    status = row["Status"].ToString(),
                    intent = row["Intent"].ToString()
                });
            }

            return investigationAdvice;
        }
        private List<Referral> GetReferrals(DataTable table, string hipId)
        {
            List<Referral> referrals = new();

            if (table == null || table.Rows.Count == 0)
                return referrals;

            foreach (DataRow row in table.Rows)
            {
                referrals.Add(new Referral
                {
                    referral = new CodeableConcept
                    {
                        text = row["ReferralText"].ToString(),

                        code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["ReferralCategory"].ToString(),
                            Url = row["ReferralUrl"].ToString(),
                            Code = row["ReferralCode"].ToString(),
                            Display = row["ReferralDisplay"].ToString()
                        }
                    },

                    status = row["Status"].ToString(),
                    intent = row["Intent"].ToString()
                });
            }

            return referrals;
        }
        private List<MedicalHistory> GetMedicalHistory(DataTable table, string hipId)
        {
            List<MedicalHistory> medicalHistory = new();

            if (table == null || table.Rows.Count == 0)
                return medicalHistory;

            foreach (DataRow row in table.Rows)
            {
                medicalHistory.Add(new MedicalHistory
                {
                    summary = row["MedicalHistorySummary"].ToString(),

                    conditionCode = new ConditionCode
                    {
                        Text = row["MedicalHistorySummary"].ToString(),

                        Code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["ConditionCategory"].ToString(),
                            Url = row["ConditionUrl"].ToString(),
                            Code = row["ConditionCode"].ToString(),
                            Display = row["ConditionDisplay"].ToString()
                        }
                    },

                    recordedDate = row["RecordedDate"].ToString()
                });
            }

            return medicalHistory;
        }
        private List<AllergyData> GetAllergiesData(DataTable table, string hipId)
        {
            List<AllergyData> allergies = new();

            if (table == null || table.Rows.Count == 0)
                return allergies;

            foreach (DataRow row in table.Rows)
            {
                allergies.Add(new AllergyData
                {
                    clinicalStatus = new CodeableConcept
                    {
                        text = row["ClinicalStatusText"].ToString(),
                        code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["ClinicalStatusCategory"].ToString(),
                            Url = row["ClinicalStatusUrl"].ToString(),
                            Code = row["ClinicalStatusCode"].ToString(),
                            Display = row["ClinicalStatusDisplay"].ToString()
                        }
                    },

                    verificationStatus = new CodeableConcept
                    {
                        text = row["VerificationStatusText"].ToString(),
                        code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["VerificationStatusCategory"].ToString(),
                            Url = row["VerificationStatusUrl"].ToString(),
                            Code = row["VerificationStatusCode"].ToString(),
                            Display = row["VerificationStatusDisplay"].ToString()
                        }
                    },

                    allergy = new CodeableConcept
                    {
                        text = row["AllergyText"].ToString(),
                        code = new CodeDetails
                        {
                            HospitalId = hipId,
                            Category = row["AllergyCategory"].ToString(),
                            Url = row["AllergyUrl"].ToString(),
                            Code = row["AllergyCode"].ToString(),
                            Display = row["AllergyDisplay"].ToString()
                        }
                    },

                    recordedDate = row["RecordedDate"].ToString(),
                    note = row["Note"].ToString()
                });
            }

            return allergies;
        }
        private List<PhysicalExam> GetPhysicalExams(DataTable table)
        {
            List<PhysicalExam> physicalExams = new();

            if (table == null || table.Rows.Count == 0)
                return physicalExams;

            foreach (DataRow row in table.Rows)
            {
                physicalExams.Add(new PhysicalExam
                {
                    physicalExamSummary = row["PhysicalExamSummary"].ToString()
                });
            }

            return physicalExams;
        }
        private List<InvoiceRecord> GetInvoiceRecords(DataTable table, string hipId)
        {
            List<InvoiceRecord> invoices = new();

            if (table == null || table.Rows.Count == 0)
                return invoices;

            var invoiceGroups = table.AsEnumerable()
                .GroupBy(row => row["BillNo"].ToString());

            foreach (var billGroup in invoiceGroups)
            {
                DataRow firstRow = billGroup.First();

                InvoiceRecord invoice = new InvoiceRecord
                {
                    recepient = firstRow["Recepient"].ToString(),
                    issuer = firstRow["Issuer"].ToString(),
                    issuedDate = firstRow["IssuedDate"].ToString(),

                    billIdentifier = new BillIdentifier
                    {
                        type = firstRow["BillIdentifierType"].ToString(),
                        value = firstRow["BillIdentifierValue"].ToString()
                    },

                    lineItems = new List<InvoiceLineItem>(),

                    invoiceRecordType = firstRow["InvoiceRecordType"].ToString()
                };

                foreach (DataRow row in billGroup)
                {
                    invoice.lineItems.Add(new InvoiceLineItem
                    {
                        chargeItem = new ChargeItem
                        {
                            chargeItemCodeableConcept = new CodeableConcept
                            {
                                text = row["ChargeItemText"].ToString(),

                                code = new CodeDetails
                                {
                                    HospitalId = hipId,
                                    Category = row["ChargeItemCategory"].ToString(),
                                    Url = row["ChargeItemUrl"].ToString(),
                                    Code = row["ChargeItemCode"].ToString(),
                                    Display = row["ChargeItemDisplay"].ToString()
                                }
                            }
                        },

                        priceComponents = new List<PriceComponent>
                {
                    new PriceComponent
                    {
                        type = row["PriceComponentType"].ToString(),

                        amount = new PriceAmount
                        {
                            amount = row["Amount"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(row["Amount"]),

                            currency = row["Currency"].ToString()
                        }
                    }
                }
                    });
                }

                invoices.Add(invoice);
            }

            return invoices;
        }
        private List<ObservationResult> GetObservations(DataTable table, string hipId)
        {
            List<ObservationResult> observations = new();

            if (table == null || table.Rows.Count == 0)
                return observations;

            foreach (DataRow row in table.Rows)
            {
                object resultValue = decimal.TryParse(row["ResultValue"].ToString(), out decimal rv)
                    ? rv
                    : row["ResultValue"].ToString();

                object refHighValue = decimal.TryParse(row["ReferenceHighValue"].ToString(), out decimal rh)
                    ? rh
                    : row["ReferenceHighValue"].ToString();

                object refLowValue = decimal.TryParse(row["ReferenceLowValue"].ToString(), out decimal rl)
                    ? rl
                    : row["ReferenceLowValue"].ToString();

                observations.Add(new ObservationResult
                {
                    Status = row["ObservationStatus"].ToString(),

                    ResultCode = new ResultCodeableConcept
                    {
                        Text = row["ResultText"].ToString(),
                        Code = new CodeDetails
                        {
                            HospitalId = row["ResultHospitalId"].ToString(),
                            Category = row["ResultCategory"].ToString(),
                            Url = row["ResultUrl"].ToString(),
                            Code = row["ResultCode"].ToString(),
                            Display = row["ResultDisplay"].ToString()
                        }
                    },

                    Value = new ValueQuantity
                    {
                        Value = resultValue,
                        Code = new QuantityCode
                        {
                            Id = row["ResultValueId"].ToString(),
                            Unit = row["ResultUnit"].ToString(),
                            Url = row["ResultValueUrl"].ToString(),
                            Code = row["ResultValueCode"].ToString()
                        }
                    },

                    Category = new CategoryCodeableConcept
                    {
                        Text = row["ResultCategoryText"].ToString(),
                        Code = new CodeDetails
                        {
                            HospitalId = row["ResultCategoryHospitalId"].ToString(),
                            Category = row["ResultCategoryName"].ToString(),
                            Url = row["ResultCategoryUrl"].ToString(),
                            Code = row["ResultCategoryCode"].ToString(),
                            Display = row["ResultCategoryDisplay"].ToString()
                        }
                    },

                    ReferenceRange = new ReferenceRange
                    {
                        High = new ValueQuantity
                        {
                            Value = refHighValue,
                            Code = new QuantityCode
                            {
                                Id = row["ReferenceHighId"].ToString(),
                                Unit = row["ReferenceHighUnit"].ToString(),
                                Url = row["ReferenceHighUrl"].ToString(),
                                Code = row["ReferenceHighCode"].ToString()
                            }
                        },

                        Low = new ValueQuantity
                        {
                            Value = refLowValue,
                            Code = new QuantityCode
                            {
                                Id = row["ReferenceLowId"].ToString(),
                                Unit = row["ReferenceLowUnit"].ToString(),
                                Url = row["ReferenceLowUrl"].ToString(),
                                Code = row["ReferenceLowCode"].ToString()
                            }
                        }
                    },

                    EffectiveOn = row["EffectiveOn"].ToString(),

                    Interpretation = new InterpretationCodeableConcept
                    {
                        Text = row["InterpretationText"].ToString(),
                        Code = new CodeDetails
                        {
                            HospitalId = row["InterpretationHospitalId"].ToString(),
                            Category = row["InterpretationCategory"].ToString(),
                            Url = row["InterpretationUrl"].ToString(),
                            Code = row["InterpretationCode"].ToString(),
                            Display = row["InterpretationDisplay"].ToString()
                        }
                    }
                });
            }

            return observations;
        }
        //public virtual async Task SavePatientEncounterAsync(PatientVisitRequest model, string PE_Message, string PE_ErrMessage, string PE_HipId, string PE_PatientReferenceNumber)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

        //    var encounter = new TabhaPatientEncounterCareContextDetail
        //    {
        //        AbhaNumber = model.AbhaNumber,
        //        AbhaAddress = model.AbhaAddress,
        //        RegId = (long)long.Parse(PE_PatientReferenceNumber),
        //        OpIpId = (long)long.Parse(model.OpIpId),
        //        OpIpType = (int?)(long)long.Parse(model.OpIpType),

        //        PeMessage = PE_Message,
        //        PeErrMessage = PE_ErrMessage,
        //        PeHipId = PE_HipId,
        //        PePatientReferenceNumber = PE_PatientReferenceNumber

        //    };

        //    await _context.TabhaPatientEncounterCareContextDetails.AddAsync(encounter);
        //    await _context.SaveChangesAsync();
        //    scope.Complete();
        //}

        public virtual async Task SavePatientEncounterAsync(PatientVisitRequest model,string PE_Message,string PE_ErrMessage, string PE_HipId,string PE_PatientReferenceNumber, string PE_CareContext)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,new TransactionOptions{IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted},TransactionScopeAsyncFlowOption.Enabled);

            long opIpId = long.Parse(model.OpIpId);
            int opIpType = int.Parse(model.OpIpType);

            var encounter = await _context.TabhaPatientEncounterCareContextDetails.FirstOrDefaultAsync(x => x.OpIpId == opIpId &&x.OpIpType == opIpType);
            if (encounter == null)
            {
                encounter = new TabhaPatientEncounterCareContextDetail
                {
                    AbhaNumber = model.AbhaNumber,
                    AbhaAddress = model.AbhaAddress,
                    OpIpId = opIpId,
                    OpIpType = opIpType,
                    PeMessage = PE_Message,
                    PeErrMessage = PE_ErrMessage,
                    PeHipId = PE_HipId,
                    PePatientReferenceNumber = PE_PatientReferenceNumber,
                    PeCareContext = PE_CareContext
                };

                if (!string.IsNullOrEmpty(PE_PatientReferenceNumber) && long.TryParse(PE_PatientReferenceNumber, out long regId))
                {
                    encounter.RegId = regId;
                }

                await _context.TabhaPatientEncounterCareContextDetails.AddAsync(encounter);
            }
            else
            {
                encounter.AbhaNumber = model.AbhaNumber;
                encounter.AbhaAddress = model.AbhaAddress;
                encounter.PeMessage = PE_Message;
                encounter.PeErrMessage = PE_ErrMessage;
                encounter.PeHipId = PE_HipId;
                encounter.PePatientReferenceNumber = PE_PatientReferenceNumber;
                encounter.PeCareContext = PE_CareContext;

                if (!string.IsNullOrEmpty(PE_PatientReferenceNumber) &&
                    long.TryParse(PE_PatientReferenceNumber, out long regId))
                {
                    encounter.RegId = regId;
                }

                _context.TabhaPatientEncounterCareContextDetails.Update(encounter);
            }

            await _context.SaveChangesAsync();

            scope.Complete();
        }

     public virtual async Task SaveCareContextResponseAsync(CareContextModel model,string CC_WorkflowId,string CC_Message,string CC_HipId,string CC_ErrMessage)
        {
            using var scope = new TransactionScope( TransactionScopeOption.Required,new TransactionOptions{IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted},TransactionScopeAsyncFlowOption.Enabled);

            string referenceNumber = model.careContexts?.FirstOrDefault()?.referenceNumber;

            var encounter = await _context.TabhaPatientEncounterCareContextDetails.FirstOrDefaultAsync(x => x.PeCareContext == referenceNumber);

            if (encounter != null)
            {
                encounter.CcWorkflowId = CC_WorkflowId;
                encounter.CcMessage = CC_Message;
                encounter.CcHipId = CC_HipId;
                encounter.CcErrMessage = CC_ErrMessage;

                _context.TabhaPatientEncounterCareContextDetails.Update(encounter);

                await _context.SaveChangesAsync();
            }

            scope.Complete();
        }


    }
}
