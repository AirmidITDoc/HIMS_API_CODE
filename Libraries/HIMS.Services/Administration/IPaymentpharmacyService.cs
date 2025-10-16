using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Administration
{
    public partial interface IPaymentpharmacyService
    {
        Task<IPagedList<BrowseIPDPaymentReceiptListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<BrowseOPDPaymentReceiptListDto>> GetListAsync1(GridRequestModel objGrid);
        Task<IPagedList<BrowseIPAdvPaymentReceiptListDto>> GetListAsync2(GridRequestModel objGrid);
        Task<IPagedList<BrowsePharmacyPayReceiptListDto>> GetListAsync3(GridRequestModel objGrid);
        Task InsertAsync(PaymentPharmacy objPaymentPharmacy, int UserId, string Username);
        Task UpdateAsync(PaymentPharmacy objPaymentPharmacy, int UserId, string Username, string[]? references);
        Task UpdateAsync(TSalesHeader ObjTSalesHeader, int UserId, string Username);
        Task UpdateAsyncDate(PaymentPharmacy ObjPaymentPharmacy, int UserId, string Username);
        Task InsertAsyncSp(List<TSalesHeader> ObjTSalesHeader, int UserId, string Username);

    }
}
