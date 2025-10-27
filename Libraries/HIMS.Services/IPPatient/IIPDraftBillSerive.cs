using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IIPDraftBillSerive
    {
        Task InsertAsyncSP(TDrbill objBill, int CurrentUserId, string CurrentUserName);
    }
}
