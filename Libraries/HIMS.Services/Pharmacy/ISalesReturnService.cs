using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.NewPharmacy;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface ISalesReturnService
    {

        Task<IPagedList<SalesReturnDetailsListDto>> salesreturndetaillist(GridRequestModel objGrid);
        Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel objGrid);
        Task<IPagedList<SalesRetrunCurrentSumryListDto>> SalesReturnSummaryList(GridRequestModel objGrid);
        Task<IPagedList<SalesRetrunLCurrentDetListDto>> SalesReturnDetailsList(GridRequestModel objGrid);
        Task<IPagedList<BrowseSalesBillListDto>> BrowseSalesBillList(GridRequestModel objGrid);
        Task<IPagedList<SalesBillReturnCashListDto>> SalesBillReturnCashList(GridRequestModel objGrid);
        Task<IPagedList<SalesBillReturnCreditListDto>> SalesBillReturnCreditList(GridRequestModel objGrid);
        Task InsertSP(TSalesReturnHeader ObjTSalesReturnHeader, List<TSalesReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, PaymentPharmacy ObjPayment, List<TPaymentPharmacy> ObjTPaymentPharmacy, int UserId, string Username);
        Task InsertSPCredit(TSalesReturnHeader ObjTSalesReturnHeader, List<TSalesReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, int UserId, string Username);
        void InsertInPatient(TSalesInPatientReturnHeader ObjTSalesReturnHeader, List<TSalesInPatientReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, List<TIpprescriptionReturnD> ObjTIpprescriptionReturnD, TIpprescriptionReturnH ObjTIpprescriptionReturnH,  int UserId, string Username);
        Task<IPagedList<PrescriptionReturnHListDto>> IPPrescriptionReturnHList(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionReturnDetailsListDto>> IPPrescriptionReturnDList(GridRequestModel objGrid);




    }
}
