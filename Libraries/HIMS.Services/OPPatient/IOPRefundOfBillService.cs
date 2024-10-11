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
        Task InsertAsyncSP(Refund objRefund, TRefundDetail objTRefundDetail, AddCharge objAddCharge, Payment objPayment, int UserId, string Username);
        Task<long> InsertAsync(Refund objRefund, int UserId, string Username);

    }
}
