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
        Task<IPagedList<IPBillListDto>> GetIPBillListAsync1(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPPaymentListDto>> GetIPPaymentListAsync(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPRefundListDto>> GetIPRefundBillListListAsync(GridRequestModel objGrid);
        Task InsertBillAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task InsertCreditBillAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<ServiceClassdetailListDto>> ServiceClassdetailList(GridRequestModel objGrid);
        Task<IPagedList<IPPreviousBillListDto>> GetIPPreviousBillAsync(GridRequestModel objGrid);
        Task<IPagedList<IPAddchargesListDto>> GetIPAddchargesAsync(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPDBillListDto>> GetIPBillListAsync(GridRequestModel objGrid);
        Task<IPagedList<PreviousBillListDto>> GetPreviousBillListAsync(GridRequestModel objGrid);
        Task<IPagedList<PathRadRequestListDto>> PathRadRequestListAsync(GridRequestModel objGrid);
        Task<IPagedList<IPPackageDetailsListDto>> IPPackageDetailsListAsync(GridRequestModel objGrid);
        Task InsertAsync(AddCharge objAddCharge,  int UserId, string Username);
        Task IPAddchargesdelete(AddCharge ObjaddCharge, int UserId, string Username);
        Task IPbillAsyncSp(Bill ObjBill, List <BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission,Payment Objpayment,Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader ,int UserId, string Username);
        Task IPbillCreditAsyncSp(Bill ObjBill, List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission,  Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int UserId, string Username);
        Task paymentAsyncSP(Payment objPayment, Bill ObjBill, List<AdvanceDetail> objadvanceDetailList, AdvanceHeader objAdvanceHeader, int CurrentUserId, string CurrentUserName);
        Task IPInterimBillCashCounterAsyncSp(AddCharge ObjAddCharge, Bill ObjBill, List<BillDetail> ObjBillDetails,  Payment Objpayment, int UserId, string Username);
        Task IPDraftBillAsync(TDrbill ObjTDrbill, List<TDrbillDet> ObjTDrbillDetList, int UserId, string Username);
    }
}

