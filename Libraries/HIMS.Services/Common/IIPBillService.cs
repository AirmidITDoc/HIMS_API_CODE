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
        Task<IPagedList<PharmacyDetailsListDto>> GetPharmacyDetailsList(GridRequestModel objGrid);

        Task InsertAsync(AddCharge objAddCharge, List<AddCharge> objAddCharges, int CurrentUserId, string CurrentUserName);
        Task IPAddchargesdelete(AddCharge ObjaddCharge, int CurrentUserId, string CurrentUserName);
        void IPbillSp(Bill ObjBill, List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission, Payment Objpayment, Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, List<TPayment> ObjTPayment, int UserId, string Username);
        Task IPbillCreditSp(Bill ObjBill, List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission, Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int CurrentUserId, string CurrentUserName);
        Task paymentAsyncSP(Payment objPayment, Bill ObjBill, List<AdvanceDetail> objadvanceDetailList, AdvanceHeader objAdvanceHeader, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);
        Task paymentMultipleAsyncSP(List<Payment> objPayment, List<Bill> ObjBill,  List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName);

        Task IPInterimBillCashCounterSp(AddCharge ObjAddCharge, Bill ObjBill, List<BillDetail> ObjBillDetails, Payment Objpayment, List<TPayment> ObjTPayment, int UserId, string Username);
        void IPDraftBill(TDrbill ObjTDrbill, List<TDrbillDet> ObjTDrbillDetList, int UserId, string Username);

        Task IPAddcharges(AddCharge ObjaddCharge, List<AddCharge> objAddCharges, int UserId, string Username);
        Task Update(AddCharge objAddCharge, int CurrentUserId, string CurrentUserName);
        void InsertLabRequest(AddCharge objAddCharge, int UserId, string Username, long traiffId, long ReqDetId);
        Task InsertIPDPackage(AddCharge objAddCharge, int UserId, string Username);
        //Task BillGovtUpdate(Bill ObjBill, int UserId, string Username);
        Task UpdateRefund(Refund OBJRefund, int CurrentUserId, string CurrentUserName);
        Task InsertSP(AddCharge objAddCharge, int UserId, string Username);
        void InsertSPC(AddCharge objAddCharge, int UserId, string Username, long? NewClassId);
        void InsertSPT(AddCharge model, int currentUserId, string currentUserName, long? newClassId, long? newTariffId);

        Task IPbillSp(Bill ObjBill, int currentUserId, string currentUserName);
        Task UpdateBill(List<AddCharge> objAddCharge,Bill ObjBill, int CurrentUserId, string CurrentUserName);


    }
}

