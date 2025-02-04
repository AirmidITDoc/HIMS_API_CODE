using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IPaymentpharmacyService
    {
        Task InsertAsync(PaymentPharmacy objPaymentPharmacy, int UserId, string Username);

        Task UpdateAsync(PaymentPharmacy objPaymentPharmacy, int UserId, string Username);
    }
}
