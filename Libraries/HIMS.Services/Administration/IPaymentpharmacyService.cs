using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;

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
        void Update(TSalesHeader ObjTSalesHeader, int UserId, string Username);
        Task UpdateAsyncDate(PaymentPharmacy ObjPaymentPharmacy, int UserId, string Username);
        void InsertSp(List<TSalesHeader> ObjTSalesHeader, int UserId, string Username);

    }
}
