using HIMS.Data.Models;

namespace HIMS.Services.Administration
{
    public partial interface IPaymentModeService
    {
        Task UpdateAsync(Payment objPayment, int UserId, string Username, string[]? references);

    }
}
