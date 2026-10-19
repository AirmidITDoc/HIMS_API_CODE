using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.OutPatient
{
    public partial interface IOPSettlementService
    {
        Task InsertSettlementMultiple(List<Payment> objpayment, List<Bill> objBill, List<TPayment> ObjTPayment ,int CurrentUserId, string CurrentUserName);
        Task InsertAsyncSP(Payment objpayment, Bill objBill, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);
        Task InsertAsync(Payment objpayment, Bill objBill, int CurrentUserId, string CurrentUserName);

        //Task UpdateAsync(Bill objBill, int CurrentUserId, string CurrentUserName);
        //Task UpdateAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<OPBillListSettlementListDto>> OPBillListSettlementList(GridRequestModel objGrid);


        Task InsertAsyncSP1(Payment objpayment, Bill objBill, List<TPayment> ObjTPayment, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int CurrentUserId, string CurrentUserName);


    }
}
