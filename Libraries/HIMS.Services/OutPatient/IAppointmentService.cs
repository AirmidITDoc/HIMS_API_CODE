using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IAppointmentService
    {
        Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        //Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        //Task CancelAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        //Task InsertAsyncSP(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);
        Task UpdateAsyncSP(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);
        Task CancelAsyncSP(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);
    }
}
