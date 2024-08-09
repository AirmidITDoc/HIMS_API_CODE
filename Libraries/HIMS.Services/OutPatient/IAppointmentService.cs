using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IAppointmentService
    {
        Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task CancelAsync(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);
    }
}
