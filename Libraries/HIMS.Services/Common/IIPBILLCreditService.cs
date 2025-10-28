using HIMS.Data.Models;

namespace HIMS.Services.Common
{
    public partial interface IIPBILLCreditService
    {
        Task InsertAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
    }
}
