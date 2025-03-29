using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
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
        Task InsertAsyncOP(Refund objRefund, List<TRefundDetail> objTRefundDetail, List<AddCharge> objAddCharge, Payment objPayment, int UserId, string Username);
        Task<long> InsertAsync(Refund objRefund, int UserId, string Username);
        Task<IPagedList<OpBilllistforRefundDto>> GeOpbilllistforrefundAsync(GridRequestModel objGrid);
        Task<IPagedList<OPBillservicedetailListDto>> GetBillservicedetailListAsync(GridRequestModel objGrid);
        Task<IPagedList<RefundAgainstBillListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<IPBillListforRefundListDto>> IPBillGetListAsync(GridRequestModel objGrid);
        Task<IPagedList<IPBillForRefundListDto>> IPBillForRefundListAsync(GridRequestModel objGrid);


    }
}
