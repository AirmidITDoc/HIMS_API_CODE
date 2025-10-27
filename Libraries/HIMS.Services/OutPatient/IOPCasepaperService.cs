using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IOPCasepaperService
    {
        Task InsertAsync(Registration objRegistration, int currentUserId, string currentUserName);


        // Task UpdateAsyncSP(Registration objRegistration, int currentUserId, string currentUserName);
    }
}
