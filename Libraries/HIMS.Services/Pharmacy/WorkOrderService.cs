using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Pharmacy
{
    public  class WorkOrderService : IWorkOrderService
    {
      
        private readonly Data.Models.HIMSDbContext _context;
        public WorkOrderService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<WorkOrderListDto>> GetWorkorderList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<WorkOrderListDto>(model, "Rtrv_WorkOrderList_by_Name");
        }
        public virtual async Task WorkOrderAsyncSp(TWorkOrderHeader ObjTWorkOrderHeader, List<TWorkOrderDetail> ObjTWorkOrderDetail, int UserId, string UserName)
        {
            DatabaseHelper odal = new();

            string[] AEntity = { "Woid", "Wono", "IsCancelDate", "UpdatedBy" };

            var yentity = ObjTWorkOrderHeader.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                yentity.Remove(rProperty);
            }
            string AWOId = odal.ExecuteNonQuery("insert_T_WorkOrderHeader_1", CommandType.StoredProcedure, "WOId", yentity);
            ObjTWorkOrderHeader.Woid = Convert.ToInt32(AWOId);
            //ObjTOpeningTransactionHeaders.OpeningHid = Convert.ToInt32(BOpeningHId);


            foreach (var item in ObjTWorkOrderDetail)
            {
                string[] rEntity = { "WodetId", "PendQty" };
                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_T_WorkOrderDetail_1", CommandType.StoredProcedure, entity);
                //   ObjTOpeningTransaction.OpeningId = Convert.ToInt32(TOpeningId);
            }
        }
        //public virtual async Task InsertAsync(TWorkOrderHeader ObjTWorkOrderHeader, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        _context.TWorkOrderHeaders.Add(ObjTWorkOrderHeader);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

        //public virtual async Task UpdateAsync(TWorkOrderHeader ObjTWorkOrderHeader, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Delete details table realted records
        //        var lst = await _context.TWorkOrderDetails.Where(x => x.Woid == ObjTWorkOrderHeader.Woid).ToListAsync();
        //        if (lst.Count > 0)
        //        {
        //            _context.MAssignItemToStores.RemoveRange(lst);
        //        }
        //        await _context.SaveChangesAsync();
        //        // Update header & detail table records
        //        _context.TWorkOrderHeaders.Update(ObjTWorkOrderHeader);
        //        _context.Entry(ObjTWorkOrderHeader).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}




        //public virtual async Task UpdateAsyncSp(TWorkOrderHeader ObjTWorkOrderHeader, int UserId, string UserName)
        //{
        //    DatabaseHelper odal = new();

        //    string[] AEntity = { "Wono", "Date", "Time", "IsCancelled", "IsCancelledBy", "IsCancelDate", "AddedBy" };

        //    var yentity = ObjTWorkOrderHeader.ToDictionary();
        //    foreach (var rProperty in AEntity)
        //    {
        //        yentity.Remove(rProperty);
        //    }
        //    odal.ExecuteNonQuery("Update_WorkorderHeader", CommandType.StoredProcedure, yentity);
        // //   ObjTWorkOrderHeader.Woid = Convert.ToInt32(AWOId);
        //    //ObjTOpeningTransactionHeaders.OpeningHid = Convert.ToInt32(BOpeningHId);


            //foreach (var item in ObjTWorkOrderDetail)
            //{
            //    string[] rEntity = { "WodetId", "PendQty" };
            //    var entity = item.ToDictionary();
            //    foreach (var rProperty in rEntity)
            //    {
            //        entity.Remove(rProperty);
            //    }
            //    odal.ExecuteNonQuery("insert_T_WorkOrderDetail_1", CommandType.StoredProcedure, entity);
            //    //   ObjTOpeningTransaction.OpeningId = Convert.ToInt32(TOpeningId);
            //}
      //  }


        public virtual async Task UpdateAsyncSp(TWorkOrderHeader ObjTWorkOrderHeader,List<TWorkOrderDetail> ObjTWorkOrderDetail, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Wono", "Date", "Time", "IsCancelled", "IsCancelledBy", "IsCancelDate", "AddedBy" };
            var Sentity = ObjTWorkOrderHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                Sentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("Update_WorkorderHeader", CommandType.StoredProcedure, Sentity);

            var tokensObj = new
            {
                WOId = Convert.ToInt32(ObjTWorkOrderHeader.Woid)
            };
            odal.ExecuteNonQuery("Delete_WODetails_1", CommandType.StoredProcedure, tokensObj.ToDictionary());
            foreach (var item in ObjTWorkOrderDetail)
            {
                string[] DEntity = { "WodetId", "PendQty" };
                var pentity = item.ToDictionary();
                foreach (var Property in DEntity)
                {
                    pentity.Remove(Property);
                }
                odal.ExecuteNonQuery("insert_T_WorkOrderDetail_1", CommandType.StoredProcedure, pentity);


            }
        }

        public virtual async Task<IPagedList<WorkorderIteListDto>> GetOldworkeorderAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<WorkorderIteListDto>(model, "Rtrv_ItemDetailsForWorkOrderUpdate");
        }
    }
}

