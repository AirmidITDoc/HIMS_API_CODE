using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IOPBillingService
    {
        Task InsertAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
        //Task InsertAsyncSP(AddCharge objAddCharge, Bill objBill, BillDetail objbillDetail, Payment objPayment, int currentUserId, string currentUserName);
    }
}
