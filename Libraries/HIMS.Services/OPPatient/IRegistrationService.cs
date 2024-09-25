using HIMS.Data.Models;

namespace HIMS.Services.OPPatient
{
    public partial interface IRegistrationService
    {
        Task InsertAsyncSP(Registration objRegistration, int UserId, string Username);
    }
}

