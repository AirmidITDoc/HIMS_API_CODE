using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IIPBIllwithpaymentService
    {
        Task InsertAsyncSP(Bill objBill,AddCharge objAddcharges, int CurrentUserId, string CurrentUserName);
    }
}
