using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IOPSettlementCreditService
    {
        Task InsertAsyncSP(Bill objBill, Payment objpayment, int CurrentUserId, string CurrentUserName);
    }
}
