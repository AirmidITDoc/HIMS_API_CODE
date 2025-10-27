using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface ISupplierPaymentStatusService
    {
        Task<IPagedList<SupplierNamelistDto>> SupplierNamelist(GridRequestModel objGrid);
        Task<IPagedList<ItemListBysupplierNameDto>> GetItemListbysuppliernameAsync(GridRequestModel objGrid);
        Task<IPagedList<SupplierPaymentStatusListDto>> GetSupplierPaymentStatusList(GridRequestModel objGrid);
        Task<IPagedList<GetSupplierPaymentListDto>> GetSupplierPaymentList(GridRequestModel objGrid);

        void InsertSP(TGrnsupPayment ObjTGrnsupPayment, List<TGrnheader> ObjTGrnheader, List<TSupPayDet> ObjTSupPayDet, int UserId, string Username);



    }
}
