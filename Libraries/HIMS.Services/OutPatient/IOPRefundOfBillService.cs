using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public partial interface IOPRefundOfBillService
    {
        Task InsertAsyncSP(Refund objRefund, TRefundDetail objTRefundDetail, int UserId, string Username);
        Task InsertAsync(Refund objRefund, int UserId, string Username);
    }
}
