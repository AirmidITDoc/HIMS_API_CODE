using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public partial interface IDoctorSharePerCalculationService
    {
        Task UpdateAsyncOP(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task UpdateAsyncIP(Bill objBill, int CurrentUserId, string CurrentUserName);
    }
}
