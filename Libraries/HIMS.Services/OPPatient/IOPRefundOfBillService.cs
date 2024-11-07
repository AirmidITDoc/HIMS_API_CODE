using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OPPatient
{
    public partial interface IOPRefundOfBillService
    {
        Task InsertAsyncIP(Refund objRefund, TRefundDetail objTRefundDetail, AddCharge objAddCharge, Payment objPayment, int UserId, string Username);
    

       // Task InsertAsyncOP(OPRefundOfBillModel ObjRefund, int UserId, string Username);
        Task<long> InsertAsync(Refund objRefund, int UserId, string Username);



    }
}
