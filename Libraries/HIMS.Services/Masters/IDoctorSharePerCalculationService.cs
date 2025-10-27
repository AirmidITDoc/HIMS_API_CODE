using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IDoctorSharePerCalculationService
    {
        void UpdateOP(Bill objBill, int CurrentUserId, string CurrentUserName);
        void UpdateIP(Bill objBill, int CurrentUserId, string CurrentUserName);
    }
}
