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

        
        public virtual async Task InsertAsyncSP(TStockAdjustment ObjTStockAdjustment, int UserId, string Username)
        {
            
            DatabaseHelper odal = new();
            string[] rEntity = { "CreatedOn","UpdatedBy","ModifiedOn", "Stk" };

            var entity = ObjTStockAdjustment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VStockAdgId = odal.ExecuteNonQuery("ps_Update_Phar_StockAjustment_1", CommandType.StoredProcedure, "StockAdgId", entity);
            ObjTStockAdjustment.StockAdgId = Convert.ToInt32(VStockAdgId);
        }
        public virtual async Task BatchUpdateSP(TBatchAdjustment ObjTBatchAdjustment, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "BatchAdjId", "AddedDateTime","Stk" };

            var Bentity = ObjTBatchAdjustment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Bentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Phar_BatchExpDate_StockAjustment_1", CommandType.StoredProcedure, Bentity);
        }
        public virtual async Task GSTUpdateSP(TGstadjustment ObjTGstadjustment, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "GstadgId", "CreatedOn", "UpdatedBy", "ModifiedOn" };
            var Bentity = ObjTGstadjustment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Bentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_CurrentStock_GSTAdjustment", CommandType.StoredProcedure, Bentity);
        }
        public virtual async Task MrpAdjustmentUpdate(TMrpAdjustment ObjTMrpAdjustment, TCurrentStock ObjTCurrentStock, int UserId, string Username )
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
            Uentity["OldUnitMrp"] = 0; 
            Uentity["OldUnitPur"] = 0; 
            Uentity["OldUnitLanded"] = 0; 
            odal.ExecuteNonQuery("m_Update_Item_MRPAdjustment_New", CommandType.StoredProcedure, Uentity);

        }














        //public virtual async Task MrpAdjustmentUpdate( TMrpAdjustment ObjTMrpAdjustment,TCurrentStock ObjTCurrentStock,int UserId,string Username, decimal PerUnitMrp, decimal PerUnitPurrate)
        //{
        //    DatabaseHelper odal = new();

        //    // Insert into T_MrpAdjustment
        //    var Mentity = ObjTMrpAdjustment.ToDictionary();
        //    Mentity.Remove("MrpAdjId");
        //    odal.ExecuteNonQuery("PS_insert_T_MrpAdjustment_1", CommandType.StoredProcedure, Mentity);

        //    // Prepare parameters for PS_Update_Item_MRPAdjustment_New
        //    var parameters = new Dictionary<string, object>
        //    {
        //        ["StoreId"] = ObjTCurrentStock.StoreId,
        //        ["Stockid"] = ObjTCurrentStock.StockId,
        //        ["ItemId"] = ObjTCurrentStock.ItemId,
        //        ["BatchNo"] = ObjTCurrentStock.BatchNo,
        //        ["PerUnitMrp"] = PerUnitMrp,
        //        ["PerUnitPurrate"] = PerUnitPurrate,
        //        ["PerUnitLanedrate"] = 0m,
        //        ["OldUnitMrp"] = 0m,
        //        ["OldUnitPur"] = 0m,
        //        ["OldUnitLanded"] = 0m
        //    };

        //    odal.ExecuteNonQuery("PS_Update_Item_MRPAdjustment_New", CommandType.StoredProcedure, parameters);
        //}



    }
}










