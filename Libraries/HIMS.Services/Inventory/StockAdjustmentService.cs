using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Inventory;

namespace HIMS.Services.Inventory
{
    public class StockAdjustmentService : IStockAdjustmentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public StockAdjustmentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        // Changes done by Rachana Date : 12/10/2025
        public virtual async Task<IPagedList<ItemWiseStockListDto>> StockAdjustmentList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemWiseStockListDto>(model, "ps_Rtrv_BatchNoForMrpAdj");
        }


        public virtual async Task StockUpdate(TStockAdjustment ObjTStockAdjustment, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "StoreId", "StkId", "ItemId", "BatchNo", "AdDdType", "AdDdQty", "PreBalQty", "AfterBalQty", "AddedBy", "StockAdgId" };

            var entity = ObjTStockAdjustment.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string VStockAdgId = odal.ExecuteNonQuery("ps_Update_Phar_StockAjustment_1", CommandType.StoredProcedure, "StockAdgId", entity);
            await _context.LogProcedureExecution(entity, nameof(TStockAdjustment), ObjTStockAdjustment.StockAdgId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

            ObjTStockAdjustment.StockAdgId = Convert.ToInt32(VStockAdgId);
        }
        public virtual async Task BatchUpdateSP(TBatchAdjustment ObjTBatchAdjustment, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "StoreId", "ItemId", "OldBatchNo", "OldExpDate", "NewBatchNo", "NewExpDate", "AddedBy", "StkId" };

            var Bentity = ObjTBatchAdjustment.ToDictionary();
            foreach (var rProperty in Bentity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    Bentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Phar_BatchExpDate_StockAjustment_1", CommandType.StoredProcedure, Bentity);
            await _context.LogProcedureExecution(Bentity, nameof(TBatchAdjustment), ObjTBatchAdjustment.BatchAdjId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }
        public virtual async Task GSTUpdateSP(TGstadjustment ObjTGstadjustment, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] TEntity = { "StoreId", "StkId", "ItemId", "BatchNo", "OldCgstper", "OldSgstper", "OldIgstper", "Cgstper", "Sgstper", "Igstper", "AddedBy" };
            var Bentity = ObjTGstadjustment.ToDictionary();
            foreach (var rProperty in Bentity.Keys.ToList())
            {
                if (!TEntity.Contains(rProperty))
                    Bentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_CurrentStock_GSTAdjustment", CommandType.StoredProcedure, Bentity);
            await _context.LogProcedureExecution(Bentity, nameof(TGstadjustment), ObjTGstadjustment.GstadgId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }
        public virtual async Task MrpAdjustmentUpdate(TMrpAdjustment ObjTMrpAdjustment, TCurrentStock ObjTCurrentStock, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "MrpAdjId" };
            var Mentity = ObjTMrpAdjustment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Mentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_insert_T_MrpAdjustment_1", CommandType.StoredProcedure, Mentity);

            string[] Entity = { "OpeningBalance", "ReceivedQty", "IssueQty", "BalanceQty", "VatPercentage", "BatchExpDate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "IstkId", "GrnRetQty", "IssDeptQty", "PurUnitRate" };
            var Uentity = ObjTCurrentStock.ToDictionary();
            foreach (var rProperty in Entity)
            {
                Uentity.Remove(rProperty);
            }
            
            Uentity["OldUnitMrp"] = 0; // Ensure objpayment
            Uentity["OldUnitPur"] = 0; // Ensure objpayment
            Uentity["OldUnitLanded"] = 0; // Ensure objpayment
            odal.ExecuteNonQuery("m_Update_Item_MRPAdjustment_New", CommandType.StoredProcedure, Uentity);
        }
    }
}


