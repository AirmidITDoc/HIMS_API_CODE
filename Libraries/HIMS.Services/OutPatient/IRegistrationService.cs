using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IRegistrationService
    {
        Task InsertAsyncSP(Registration objRegistration, int UserId, string Username);
    }
}
