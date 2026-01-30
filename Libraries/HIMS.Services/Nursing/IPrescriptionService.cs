using HIMS.Data.Models;

namespace HIMS.Services.Nursing
{
    public partial interface IPrescriptionService
    {
        Task InsertAsync(TIpmedicalRecord objmedicalRecord, int UserId, string Username);
        Task UpdateAsync(TIpmedicalRecord objmedicalRecord, int UserId, string Username);

        Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username);

        Task PrescCancelAsync(TIpPrescription objTIpPrescription, int CurrentUserId, string CurrentUserName);
        Task PrescreturnCancelAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int CurrentUserId, string CurrentUserName);

    }
}
