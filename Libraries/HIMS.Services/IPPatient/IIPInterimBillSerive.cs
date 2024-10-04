using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.IPPatient
{
    public partial interface IIPInterimBillSerive
    {
        Task InsertAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
    }
}
