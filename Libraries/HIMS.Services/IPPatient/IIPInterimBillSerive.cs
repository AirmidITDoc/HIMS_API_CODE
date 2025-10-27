using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IIPInterimBillSerive
    {
        Task InsertAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
    }
}
