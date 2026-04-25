using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial interface IInPatientService
    {
        Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel objGrid);
        Task<IPagedList<InPatientSalesDetailsListDto>> Getsalesdetaillist(GridRequestModel objGrid);
        Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel objGrid);
        Task<IPagedList<SalesInPatientReturnDetailsListDto>> salesreturndetaillist(GridRequestModel objGrid);
        Task InsertSalesInPatientAsyncSPC(TSalesInpatientHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int CurrentUserId, string CurrentUserName);
        Task<float> GetStock(long StockId);
        Task InsertInPatient(TSalesInPatientReturnHeader ObjTSalesReturnHeader, List<TSalesInPatientReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail,  List<TIpprescriptionReturnD> ObjTIpprescriptionReturnD, TIpprescriptionReturnH ObjTIpprescriptionReturnH, int UserId, string Username);








    }
}
