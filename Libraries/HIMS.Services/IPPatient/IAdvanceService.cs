using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IAdvanceService
    {
        Task InsertAdvanceAsyncSP(AdvanceHeader objAdvanceHeader,AdvanceDetail objAdvanceDetail, Payment objpayment, int UserId, string UserName);
        Task UpdateAdvanceAsyncSP(AdvanceDetail objAdvanceDetail,int UserId, string UserName);
        // Task InsertAsyncSP(Refund objRefund, AdvanceHeader objAdvanceHeader, AdvRefundDetail objAdvRefundDetail, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName);
    }
}
