using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPurchaseService
    {
        Task InsertAsync(TPurchaseHeader objPurchase, int UserId, string Username);
        Task InsertAsyncSP(TPurchaseHeader objPurchase, int UserId, string Username);
        Task UpdateAsync(TPurchaseHeader objPurchase, int UserId, string Username, string[]? references);
        Task VerifyAsync(TPurchaseHeader objPurchase, int UserId, string Username);


        Task<IPagedList<PurchaseListDto>> GetPurchaseListAsync(GridRequestModel objGrid);

        Task<IPagedList<PurchaseDetailListDto>> GetOldPurchaseorderAsync(GridRequestModel objGrid);

        Task<IPagedList<PurchaseDetailListDto>> GetPurchaseDetailListAsync(GridRequestModel objGrid);

        Task<IPagedList<LastthreeItemListDto>> GetLastthreeItemListAsync(GridRequestModel objGrid);

        Task<IPagedList<SupplierRatelistDto>> GetSupplierRatetAsync(GridRequestModel objGrid);



    }
}
