using HIMS.Data.Models;

namespace HIMS.Services.OPPatient
{
    public partial interface IConsRefDoctorService
    {
        //Task UpdateAsync(VisitDetail objVisitDetail, int UserId, string Username);
        Task Update(VisitDetail objVisitDetail, int UserId, string Username);
    }
}
