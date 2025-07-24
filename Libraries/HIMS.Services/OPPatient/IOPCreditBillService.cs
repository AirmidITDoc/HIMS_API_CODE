using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public partial interface IOPCreditBillService
    {
        Task InsertAsyncSP(Bill objBill, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName);
    }
}
