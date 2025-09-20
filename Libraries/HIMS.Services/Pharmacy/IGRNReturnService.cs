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
        Task InsertAsyncsp(TGrnreturnHeader objGRNReturn, List<TGrnreturnDetail> objTGrnreturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TGrndetail> ObjTGrndetails, int UserId, string Username);

        Task UpdateAsyncsp(TGrnreturnHeader objGRNReturn, List<TGrnreturnDetail> objTGrnreturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TGrndetail> ObjTGrndetails, int UserId, string Username);
        Task VerifyAsync(TGrnreturnHeader objGRN, int UserId, string Username);


        Task<IPagedList<GrnListByNameListDto>> GetGRnListbynameAsync(GridRequestModel objGrid);

        Task<IPagedList<GRNReturnListDto>> GetGRNReturnList(GridRequestModel objGrid);


        Task<IPagedList<grnlistbynameforgrnreturnlistDto>> Getgrnlistbynameforgrnreturn(GridRequestModel objGrid);
        Task<IPagedList<ItemListBysupplierNameDto>> ItemListBysupplierNameAsync(GridRequestModel objGrid);


    }
}
