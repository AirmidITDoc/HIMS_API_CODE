using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public partial interface IGRNReturnService
    {
        Task InsertAsync(TGrnreturnHeader objGRNReturn, List<TCurrentStock> objCStock, List<TGrndetail> objReturnQty, int UserId, string Username);
        Task InsertAsyncSP(TGrnreturnHeader objGRNReturn, List<TCurrentStock> objCStock, List<TGrndetail> objReturnQty, int UserId, string Username);
        Task VerifyAsync(TGrnreturnDetail objGRNReturn, int UserId, string Username);

        Task<IPagedList<GrnListByNameListDto>> GetGRnListbynameAsync(GridRequestModel objGrid);

        Task<IPagedList<GRNReturnListDto>> GetGRNReturnList(GridRequestModel objGrid);

        Task<IPagedList<ItemListBysupplierNameDto>> GetItemListbysuppliernameAsync(GridRequestModel objGrid);

        Task<IPagedList<grnlistbynameforgrnreturnlistDto>> Getgrnlistbynameforgrnreturn(GridRequestModel objGrid);
    }
}
