using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface ITprocessOtpService
    {
        //Task UpdateAsync(TProcessOtp objPurchase, int UserId, string Username, string[]? references);
        Task UpdateAsync(TProcessOtp ObjTProcessOtp, int CurrentUserId, string CurrentUserName);
    }
}
