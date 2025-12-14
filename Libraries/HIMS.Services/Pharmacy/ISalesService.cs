using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;

namespace HIMS.Services.Users
{
    public partial interface ISalesService
    {
        //Task InsertAsync(TSalesHeader user, PaymentPharmacy objPayment, int UserId, string Username);
        Task<IPagedList<PharSalesCurrentSumryListDto>> GetList(GridRequestModel objGrid);
        Task<IPagedList<PharCurrentDetListDto>> SalesDetailsList(GridRequestModel objGrid);
        Task<IPagedList<SalesDetailsListDto>> Getsalesdetaillist(GridRequestModel objGrid);
        Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel objGrid);
        Task<IPagedList<SalesDraftBillListDto>> SalesDraftBillList(GridRequestModel objGrid);
        Task<IPagedList<BalAvaStoreListDto>> BalAvaStoreList(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionListforSalesDto>> PrescriptionList(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionDetListDto>> PrescriptionDetList(GridRequestModel objGrid);
        Task<IPagedList<Pharbillsettlementlist>> PharIPBillSettlement(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPPharAdvanceReceiptListDto>> BrowseIPPharAdvanceReceiptList(GridRequestModel objGrid);
        Task<IPagedList<PharAdvanceListDto>> PharAdvanceList(GridRequestModel objGrid);
        Task<IPagedList<PhAdvRefundReceiptListDto>> PhAdvRefundReceiptList(GridRequestModel objGrid);
        Task<IPagedList<PhARefundOfAdvanceListDto>> PhARefundOfAdvanceList(GridRequestModel objGrid);
        Task<IPagedList<ItemNameBalanceQtyListDto>> BalqtysalesDraftlist(GridRequestModel objGrid);
        Task<IPagedList<GetRefundByAdvanceIdListDto>> GetRefundByAdvanceId(GridRequestModel objGrid);
        Task<IPagedList<SalesDraftBillItemListDto>> SalesDraftBillItemDet(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionItemDetListDto>> PrescriptionItemDetList(GridRequestModel objGrid);
        Task<IPagedList<salespatientwiseListDto>> salespatientwiseList(GridRequestModel objGrid);
        Task<IPagedList<ItemGenericByNameListDto>> ItemGenericByNameList(GridRequestModel objGrid);

        Task InsertAsyncSP(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, PaymentPharmacy ObjPayment, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int UserId, string Username);
        Task InsertSalesSaveWithPayment(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, PaymentPharmacy ObjPayment, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, List<TPaymentPharmacy> ObjTPaymentPharmacy ,int UserId, string Username);

        void InsertS(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int UserId, string Username);
        void UpdateS(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, int UserId, string Username);
        void InsertR(TPhRefund ObjTPhRefund, TPhadvanceHeader ObjTPhadvanceHeader, List<TPhadvRefundDetail> ObjTPhadvRefundDetail, List<TPhadvanceDetail> ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, int UserId, string Username);
        Task InsertAsyncSPC(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int CurrentUserId, string CurrentUserName);
        void InsertSPD(TSalesDraftHeader ObjDraftHeader, List<TSalesDraftDet> ObjTSalesDraftDet, int UserId, string Username);
        void Delete(TSalesDraftHeader ObjDraftHeader, int UserId, string Username);
        void Insert(List<PaymentPharmacy> ObjPayment, List<TSalesHeader> ObjTSalesHeader, List<AdvanceDetail> ObjAdvanceDetail, AdvanceHeader ObjAdvanceHeader, List<TPaymentPharmacy> ObjTPaymentPharmacy ,int UserId, string Username);
        void InsertSP1(TSalesHeader ObjTSalesHeader, int UserId, string Username);
        void InsertSP(TSalesHeader ObjTSalesHeader, int UserId, string Username);

        Task<List<SalesPatientAutoCompleteDto>> SearchRegistration(string str);
        Task<List<SalesPatientAutoCompleteDto>> SearchExtDoctor(string str);
        Task<float> GetStock(long StockId);
    }
}
