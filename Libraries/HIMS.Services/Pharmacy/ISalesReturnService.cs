using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
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
        Task InsertAsyncSP(TSalesReturnHeader ObjTSalesReturnHeader,List<TSalesReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock>  ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, PaymentPharmacy ObjPayment, int UserId, string Username);
        Task InsertAsyncSPCredit(TSalesReturnHeader ObjTSalesReturnHeader, List<TSalesReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, int UserId, string Username);

    }
}
