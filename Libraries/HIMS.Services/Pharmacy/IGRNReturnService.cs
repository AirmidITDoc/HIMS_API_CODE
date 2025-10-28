using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;
using HIMS.Data.Models;

namespace HIMS.Services.Pharmacy
{
    public partial interface IGRNReturnService
    {
        Task InsertAsync(TGrnreturnHeader objGRNReturn, List<TCurrentStock> objCStock, List<TGrndetail> objReturnQty, int UserId, string Username);
        void Insertsp(TGrnreturnHeader objGRNReturn, List<TGrnreturnDetail> objTGrnreturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TGrndetail> ObjTGrndetails, int UserId, string Username);
        void Updatesp(TGrnreturnHeader objGRNReturn, List<TGrnreturnDetail> objTGrnreturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TGrndetail> ObjTGrndetails, int UserId, string Username);
        void Verify(TGrnreturnHeader objGRN, int UserId, string Username);
        Task<IPagedList<GrnListByNameListDto>> GetGRnListbynameAsync(GridRequestModel objGrid);
        Task<IPagedList<GRNReturnListDto>> GetGRNReturnList(GridRequestModel objGrid);
        Task<IPagedList<grnlistbynameforgrnreturnlistDto>> Getgrnlistbynameforgrnreturn(GridRequestModel objGrid);
        Task<IPagedList<ItemListBysupplierNameDto>> ItemListBysupplierNameAsync(GridRequestModel objGrid);


    }
}
