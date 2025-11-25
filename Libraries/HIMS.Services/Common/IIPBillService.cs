using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

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
        Task<IPagedList<PackageDetailsListDto>> Addpackagelist(GridRequestModel objGrid);
        Task<IPagedList<PackagedetListDto>> Retrivepackagedetaillist(GridRequestModel objGrid);
        Task<IPagedList<BillChargeDetailsListDto>> BillChargeDetailsList(GridRequestModel objGrid);



        Task InsertAsync(AddCharge objAddCharge, List<AddCharge> objAddCharges, int UserId, string Username);
        void IPAddchargesdelete(AddCharge ObjaddCharge, int UserId, string Username);
        void IPbillSp(Bill ObjBill, List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission, Payment Objpayment, Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int UserId, string Username);
        void IPbillCreditSp(Bill ObjBill, List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission, Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int UserId, string Username);
        Task paymentAsyncSP(Payment objPayment, Bill ObjBill, List<AdvanceDetail> objadvanceDetailList, AdvanceHeader objAdvanceHeader, int CurrentUserId, string CurrentUserName);
        void IPInterimBillCashCounterSp(AddCharge ObjAddCharge, Bill ObjBill, List<BillDetail> ObjBillDetails, Payment Objpayment, int UserId, string Username);
        void IPDraftBill(TDrbill ObjTDrbill, List<TDrbillDet> ObjTDrbillDetList, int UserId, string Username);

        void IPAddcharges(AddCharge ObjaddCharge, List<AddCharge> objAddCharges, int UserId, string Username);
        void Update(AddCharge objAddCharge, int UserId, string Username);
        void InsertLabRequest(AddCharge objAddCharge, int UserId, string Username, long traiffId, long ReqDetId);
        void InsertIPDPackage(AddCharge objAddCharge, int UserId, string Username);
        Task UpdateRefund(Refund OBJRefund, int CurrentUserId, string CurrentUserName);
        void InsertSP(AddCharge objAddCharge, int UserId, string Username);
        void InsertSPC(AddCharge objAddCharge, int UserId, string Username, long? NewClassId);
        void InsertSPT(AddCharge model, int currentUserId, string currentUserName, long? newClassId, long? newTariffId);

        void IPbillSp(Bill ObjBill, int UserId, string Username);
        void UpdateBill(AddCharge objAddCharge,Bill ObjBill, int UserId, string Username);


    }
}

