using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IAppointmentService
    {
        //Task InsertAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, int UserId, string Username);
        Task InsertAsyncSP(VisitDetail model, int currentUserId, string currentUserName);
    }
}
