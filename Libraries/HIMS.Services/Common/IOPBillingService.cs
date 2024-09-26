using HIMS.Data.Models;

namespace HIMS.Services.Common
{
    public partial interface IOPBillingService
    {
        Task InsertAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task InsertCreditBillAsyncSP(Bill objBill, int currentUserId, string currentUserName);
    }
}
