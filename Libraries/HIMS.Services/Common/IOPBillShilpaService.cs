using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Common
{
    public partial interface IOPBillShilpaService
    {
        Task InsertAsyncSP(Bill objBill,int UserId, string Username);
        void InsertSP1(Bill objBill, int UserId, string Username);


    }
}
