using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPurchaseService
    {
        Task InsertAsync(TPurchaseHeader user, int UserId, string Username);
        Task InsertAsyncSP(TPurchaseHeader user, int UserId, string Username);
        Task UpdateAsync(TPurchaseHeader user, int UserId, string Username);
        Task VerifyAsync(TPurchaseHeader user, int UserId, string Username);
    }
}
