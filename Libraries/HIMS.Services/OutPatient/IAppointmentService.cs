using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IAppointmentService
    {
        Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task UpdateAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int currentUserId, string currentUserName);
        Task CancelAsync(VisitDetail objVisitDetail, int CurrentUserId, string CurrentUserName);


        Task<VisitDetail> InsertAsyncSP(VisitDetail objCrossConsultation, int UserId, string Username);

        Task UpdateAsync(VisitDetail objVisitDetail, int UserId, string Username);
        Task Update(VisitDetail objVisitDetail, int UserId, string Username);
    }
}
