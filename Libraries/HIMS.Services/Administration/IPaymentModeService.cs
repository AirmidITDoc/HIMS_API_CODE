using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IPaymentModeService
    {
        Task UpdateAsync(Payment objPayment, int UserId, string Username, string[]? references);

    }
}
