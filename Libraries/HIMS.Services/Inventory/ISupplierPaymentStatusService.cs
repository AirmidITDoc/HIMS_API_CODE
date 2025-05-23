using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ISupplierPaymentStatusService
    {
        Task<IPagedList<SupplierNamelistDto>> SupplierNamelist(GridRequestModel objGrid);
        Task<IPagedList<ItemListBysupplierNameDto>> GetItemListbysuppliernameAsync(GridRequestModel objGrid);
        Task<IPagedList<SupplierPaymentStatusListDto>> GetSupplierPaymentStatusList(GridRequestModel objGrid);
        Task InsertAsyncSP(TGrnsupPayment ObjTGrnsupPayment, List<TGrnheader> ObjTGrnheader, List<TSupPayDet> ObjTSupPayDet, int UserId, string Username);



    }
}
