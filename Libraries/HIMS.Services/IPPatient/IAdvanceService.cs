using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.IPPatient
{
    public partial interface IAdvanceService
    {
        Task<IPagedList<AdvanceListDto>> GetAdvanceListAsync(GridRequestModel objGrid);
        Task<IPagedList<RefundOfAdvanceListDto>> GetRefundOfAdvanceListAsync(GridRequestModel objGrid);
        Task InsertAdvanceAsyncSP(AdvanceHeader objAdvanceHeader,AdvanceDetail objAdvanceDetail, Payment objpayment, int UserId, string UserName);
        Task UpdateAdvanceAsyncSP(AdvanceDetail objAdvanceDetail,int UserId, string UserName);
        Task InsertAdvanceAsyncSP1(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objpayment,int UserId, string UserName);


        Task<IPagedList<AdvanceDetailListDto>> AdvanceDetailListAsync(GridRequestModel objGrid);
        Task<IPagedList<IPAdvanceListDto>> IPAdvanceList(GridRequestModel objGrid);
        Task<IPagedList<IPRefundAdvanceReceiptListDto>> IPRefundAdvanceReceiptList(GridRequestModel objGrid);
        // Task InsertAsyncSP(Refund objRefund, AdvanceHeader objAdvanceHeader, AdvRefundDetail objAdvRefundDetail, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName);

    }
}
