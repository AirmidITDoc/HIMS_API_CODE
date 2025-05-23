using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Master;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public partial interface ISalesService
    {
        Task InsertAsync(TSalesHeader user, Payment objPayment, int UserId, string Username);
        Task<IPagedList<PharSalesCurrentSumryListDto>> GetList(GridRequestModel objGrid);
        Task<IPagedList<PharCurrentDetListDto>> SalesDetailsList(GridRequestModel objGrid);
       
        Task<IPagedList<SalesDetailsListDto>> Getsalesdetaillist(GridRequestModel objGrid);

        Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel objGrid);
        Task<IPagedList<SalesDraftBillListDto>> SalesDraftBillList(GridRequestModel objGrid);
        Task<IPagedList<BalAvaStoreListDto>> BalAvaStoreList(GridRequestModel objGrid);
        Task<IPagedList<PrescriptionListforSalesDto>> PrescriptionList(GridRequestModel objGrid);

        Task<IPagedList<PrescriptionDetListDto>> PrescriptionDetList(GridRequestModel objGrid);

        Task<IPagedList<Pharbillsettlementlist>> PharIPBillSettlement(GridRequestModel objGrid);


        Task InsertAsyncSP(TSalesHeader ObjSalesHeader,List<TCurrentStock> ObjTCurrentStock, Payment ObjPayment, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int UserId, string Username);

        Task InsertAsyncSPC(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock,TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int UserId, string Username);
        Task InsertAsyncSPD( TSalesDraftHeader ObjDraftHeader, List<TSalesDraftDet> ObjTSalesDraftDet, int UserId, string Username);

    }
}
