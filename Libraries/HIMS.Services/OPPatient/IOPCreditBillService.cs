using HIMS.Data.Models;

namespace HIMS.Services.OPPatient
{
    public partial interface IOPCreditBillService
    {
        Task InsertAsyncSP(Bill objBill, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName);
    }
}
