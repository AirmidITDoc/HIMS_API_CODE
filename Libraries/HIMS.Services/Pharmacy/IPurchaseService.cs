using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPurchaseService
    {
        Task InsertAsync(TPurchaseHeader objPurchase, int UserId, string Username);
        Task InsertAsyncSP(TPurchaseHeader objPurchase, int UserId, string Username);
        Task UpdateAsync(TPurchaseHeader objPurchase, int UserId, string Username);
        Task VerifyAsync(TPurchaseHeader objPurchase, int UserId, string Username);
    }
}
