using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class IssueToDepService : IIssueToDepService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IssueToDepService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, int UserId, string Username)
        {
            try
            {
                // //Add header table records
                DatabaseHelper odal = new();


                string[] rEntity = { "IssueNo", "Receivedby", "Updatedby", "IsAccepted", "AcceptedBy", "AcceptedDatetime", "TIssueToDepartmentDetails" };
                var entity = objIssueToDepartment.ToDictionary(); 
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string IssueId = odal.ExecuteNonQuery("v_Insert_IssueToDepartmentHeader_1_New", CommandType.StoredProcedure, "IssueId", entity);
                objIssueToDepartment.IssueId = Convert.ToInt32(IssueId);

                // Add details table records
                foreach (var objIssue in objIssueToDepartment.TIssueToDepartmentDetails)
                {
                    objIssue.IssueId = objIssueToDepartment.IssueId;
                }
                _context.TIssueToDepartmentDetails.AddRange(objIssueToDepartment.TIssueToDepartmentDetails);
                await _context.SaveChangesAsync(UserId, Username);
            }
            catch (Exception)
            {
                // Delete header table realted records
                TIssueToDepartmentHeader? objissue = await _context.TIssueToDepartmentHeaders.FindAsync(objIssueToDepartment.IssueId);
                if (objissue != null)
                {
                    _context.TIssueToDepartmentHeaders.Remove(objissue);
                }

                // Delete details table realted records
                var lst = await _context.TIssueToDepartmentDetails.Where(x => x.IssueId == objIssueToDepartment.IssueId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.TIssueToDepartmentDetails.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }
        public virtual async Task InsertAsync(TIssueToDepartmentHeader objIssueToDepartment, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update store table records
                MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objIssueToDepartment.FromStoreId);
                if (StoreInfo != null)
                {
                    StoreInfo.IssueToDeptNo = Convert.ToString(Convert.ToInt32(StoreInfo?.IssueToDeptNo ?? "0") + 1);
                    _context.MStoreMasters.Update(StoreInfo);
                    await _context.SaveChangesAsync();
                }

                // Add header & detail table records
                StoreInfo.IssueToDeptNo = Convert.ToString(Convert.ToInt32(StoreInfo?.IssueToDeptNo ?? "0") + 1);
                _context.TIssueToDepartmentHeaders.Add(objIssueToDepartment);
                await _context.SaveChangesAsync();

                scope.Complete();


            }

        }
        public virtual async Task UpdateAsync(TIssueToDepartmentHeader objIssueToDepartment, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records
                var lst = await _context.TIssueToDepartmentDetails.Where(x => x.IssueId == objIssueToDepartment.IssueId).ToListAsync();
                _context.TIssueToDepartmentDetails.RemoveRange(lst);

                // Update header & detail table records
                _context.TIssueToDepartmentHeaders.Update(objIssueToDepartment);
                _context.Entry(objIssueToDepartment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task updateissuetoDepartmentStock(TCurrentStock objCurrentStock, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update header table records
        //        TCurrentStock? objcurrunt = await _context.TCurrentStocks.FindAsync(objCurrentStock.ItemId);
        //        objcurrunt.IssueQty = objCurrentStock.IssueQty;
        //        _context.TCurrentStocks.Update(objcurrunt);
        //        _context.Entry(objcurrunt).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
    }
}

