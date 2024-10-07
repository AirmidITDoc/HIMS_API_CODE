using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Common
{
    public partial interface IPaymentService
    {
        Task InsertAsyncSP(Payment objPayment, int UserId, string Username);
        Task UpdateAsync(Payment objPayment, int UserId, string Username);
    }
}
