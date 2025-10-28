using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IIPBillwithCreditService
    {
        Task InsertAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
    }
}
