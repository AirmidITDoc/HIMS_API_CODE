using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IStockAdjustmentService
    {

        Task<IPagedList<ItemWiseStockListDto>> StockAdjustmentList(GridRequestModel objGrid);
        Task StockUpdate(TStockAdjustment ObjTStockAdjustment, int CurrentUserId, string CurrentUserName);
        Task BatchUpdateSP(TBatchAdjustment ObjTBatchAdjustment, int CurrentUserId, string CurrentUserName);
        Task GSTUpdateSP(TGstadjustment ObjTGstadjustment, int CurrentUserId, string CurrentUserName);
        void MrpAdjustmentUpdate(TMrpAdjustment ObjTMrpAdjustment, TCurrentStock ObjTCurrentStock, int UserId, string Username);



    }
}
