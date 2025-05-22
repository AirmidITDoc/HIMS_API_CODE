using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IStockAdjustmentService
    {
       
        Task<IPagedList<ItemWiseStockListDto>> StockAdjustmentList(GridRequestModel objGrid);
        Task InsertAsyncSP(TStockAdjustment ObjTStockAdjustment, int UserId, string Username);
        Task BatchUpdateSP(TBatchAdjustment ObjTBatchAdjustment, int UserId, string Username);
        Task GSTUpdateSP(TGstadjustment ObjTGstadjustment, int UserId, string Username);
        Task MrpAdjustmentUpdate(TMrpAdjustment ObjTMrpAdjustment, TCurrentStock ObjTCurrentStock, int UserId, string Username /*decimal PerUnitMrp, decimal PerUnitPurrate */);



    }
}
