using HIMS.Data.Models;

namespace HIMS.Services.Common
{
    public partial interface IOPBillingService
    {
        Task InsertAsyncSP(Bill objBill, Payment objPayment, int CurrentUserId, string CurrentUserName);
        Task InsertAsyncSP1(Bill objBill,int CurrentUserId, string CurrentUserName);
        Task InsertCreditBillAsyncSP(Bill objBill, int currentUserId, string currentUserName);


        Task InsertAsync(TCertificateInformation TCertificateInformation, int UserId, string Username);

        Task UpdateAsync(TCertificateInformation TCertificateInformation, int UserId, string Username);
    }
}
