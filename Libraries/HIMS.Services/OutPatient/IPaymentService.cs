using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IPaymentService
    {
        Task InsertAsyncSP(Payment objPayment, int UserId, string Username);
        Task UpdateAsync(Payment objPayment, int UserId, string Username);

        //Task InsertAsync(Payment objPayment, int UserId, string Username);
    }
}
