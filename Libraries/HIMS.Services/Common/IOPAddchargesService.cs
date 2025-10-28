using HIMS.Data.Models;

namespace HIMS.Services.Common
{
    public partial interface IOPAddchargesService
    {
        void InsertSP(AddCharge objAddcharges, int currentUserId, string currentUserName);
        void DeleteAsyncSP(AddCharge objAddcharges, int currentUserId, string currentUserName);
    }
}
