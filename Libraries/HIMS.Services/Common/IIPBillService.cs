using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Common
{
    public partial interface IIPBillService
    {
        Task<IPagedList<IPBillListDto>> GetIPBillListListAsync(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPPaymentListDto>> GetIPPaymentListAsync(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPRefundListDto>> GetIPRefundBillListListAsync(GridRequestModel objGrid);
        Task InsertBillAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task InsertCreditBillAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<ServiceClassdetailListDto>> ServiceClassdetailList(GridRequestModel objGrid);
        Task<IPagedList<IPPreviousBillListDto>> GetIPPreviousBillAsync(GridRequestModel objGrid);
        Task<IPagedList<IPAddchargesListDto>> GetIPAddchargesAsync(GridRequestModel objGrid);
        Task<IPagedList<IPBillList>> GetIPBillListAsync(GridRequestModel objGrid);
        Task InsertAsync(AddCharge objAddCharge,  int UserId, string Username);
        //   Task DeleteAsync(AddCharge ObjAddCharge, int UserId, string Username);
        Task IPbillAsyncSp(Bill ObjBill, BillDetail ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission,Payment Objpayment,Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader ,int UserId, string Username);
        Task IPbillCreditAsyncSp(Bill ObjBill, BillDetail ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission,  Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int UserId, string Username);



        Task paymentAsyncSP(Payment objPayment, Bill ObjBill, List<AdvanceDetail> objadvanceDetailList, AdvanceHeader objAdvanceHeader, int CurrentUserId, string CurrentUserName);

    }
}

