using HIMS.Data.DTO.AbhaIntegration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.AbhaIntegration
{
    public interface IAbhaConnectService
    {
        Task<object> InitiateClient(object model);
        Task InsertAsync(TAbhaCallbackformation obj);
        Task<object> AuthenticateUserAsync();
        Task<string> CareContextAsync(object model, string jwtToken);

        Task<List<PatientVisitResponse>> GetPatientVisitsAsync(int visitId);
    }
}
