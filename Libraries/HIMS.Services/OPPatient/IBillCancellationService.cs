using HIMS.Data.Models;

namespace HIMS.Services.OPPatient
{
    public partial interface IBillCancellationService
    {
        Task UpdateAsyncOp(Bill objOpBillCancellation, int CurrentUserId, string CurrentUserName);
        Task UpdateAsyncIp(Bill objIPBillCancellation, int CurrentUserId, string CurrentUserName);
    }
}
