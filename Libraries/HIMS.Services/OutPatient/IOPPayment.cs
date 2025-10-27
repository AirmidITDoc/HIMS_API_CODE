using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{

    public partial interface IOPPayment
    {

        void InsertSP(Payment objPayment, int UserId, string Username);
        //Task InsertAsync(Payment objPayment, int UserId, string Username);
        // Task UpdateAsync(Payment objPayment, int UserId, string Username);
    }
}
