using HIMS.Data.DTO.AbhaIntegration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HIMS.Services.AbhaIntegration
{
    public interface IAbhaConnectService
    {
        Task<object> InitiateClient(object model);
        Task InsertAsync(TAbhaCallbackformation obj);
        Task<object> AuthenticateUserAsync();
        Task<JsonElement> CareContextAsync(CareContextModel model, string jwtToken);

        Task<List<PatientVisitResponse>> GetPatientVisitsAsync(PatientVisitRequest model);
        Task SavePatientEncounterAsync(PatientVisitRequest model, string PE_Message, string PE_ErrMessage, string PE_HipId, string PE_PatientReferenceNumber, string PE_CareContext);
        Task SaveCareContextResponseAsync(CareContextModel model, string CC_WorkflowId, string CC_Message, string CC_HipId, string CC_ErrMessage);
    }
}
