using HIMS.Data.Models;

namespace HIMS.Services.Common
{
    public partial interface IOPBillShilpaService
    {
        Task InsertAsyncSP(Bill objBill, int UserId, string Username);
        void InsertSP1(Bill objBill, int UserId, string Username);


    }
}
