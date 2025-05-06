using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface IPurchaseService
    {
        Task InsertAsync(TPurchaseHeader objPurchase, int UserId, string Username);
        Task InsertAsyncSP(TPurchaseHeader objPurchase, int UserId, string Username);
        Task UpdateAsync(TPurchaseHeader objPurchase, int UserId, string Username);
        Task VerifyAsync(TPurchaseHeader objPurchase, int UserId, string Username);


        Task<IPagedList<PurchaseListDto>> GetPurchaseListAsync(GridRequestModel objGrid);

        Task<IPagedList<PurchaseDetailListDto>> GetOldPurchaseorderAsync(GridRequestModel objGrid);

        Task<IPagedList<PurchaseDetailListDto>> GetPurchaseDetailListAsync(GridRequestModel objGrid);

        Task<IPagedList<LastthreeItemListDto>> GetLastthreeItemListAsync(GridRequestModel objGrid);

        Task<IPagedList<SupplierRatelistDto>> GetSupplierRatetAsync(GridRequestModel objGrid);


        //Grn return
        Task<IPagedList<GrnListByNameListDto>> GetGRnListbynameAsync(GridRequestModel objGrid);

        Task<IPagedList<GRNReturnListDto>> GetGRNReturnList(GridRequestModel objGrid);

        Task<IPagedList<ItemListBysupplierNameDto>> GetItemListbysuppliernameAsync(GridRequestModel objGrid);

        Task<IPagedList<grnlistbynameforgrnreturnlistDto>> Getgrnlistbynameforgrnreturn(GridRequestModel objGrid);


        

        Task<IPagedList<WorkOrderListDto>> GetWorkorderList(GridRequestModel objGrid);


        Task<IPagedList<SupplierPaymentStatusListDto>> GetSupplierPaymentStatusList(GridRequestModel objGrid);

    }
}
