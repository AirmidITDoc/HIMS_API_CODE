using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
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
    public class ItemMasterServices : IItemMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ItemMasterServices(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<ItemMasterListDto>> GetItemMasterListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemMasterListDto>(model, "m_Rtrv_ItemMaster_by_Name_Pagi");
        }
        public virtual async Task InsertAsyncSP(MItemMaster objItemMaster, int UserId, string Username)
        {
            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { " UpDatedBy", "IsNarcotic", "IsUpdatedBy","CreatedBy",  "CreatedDate", " ItemTime", "MAssignItemToStore" };
                var entity = objItemMaster.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vItemId = odal.ExecuteNonQuery("Insert_ItemMaster_1_New", CommandType.StoredProcedure, "ItemId", entity);
                objItemMaster.ItemId = Convert.ToInt32(vItemId);

                // Add details table records
                foreach (var objAssign in objItemMaster.MAssignItemToStores)
                {
                    objAssign.ItemId = objItemMaster.ItemId;
                }
                _context.MAssignItemToStores.AddRange(objItemMaster.MAssignItemToStores);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Delete header table realted records
                MItemMaster? objSup = await _context.MItemMasters.FindAsync(objItemMaster.ItemId);
                if (objSup != null)
                {
                    _context.MItemMasters.Remove(objSup);
                }

                // Delete details table realted records
                var lst = await _context.MAssignItemToStores.Where(x => x.ItemId == objItemMaster.ItemId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MAssignItemToStores.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();
            }
        }
        public virtual async Task InsertAsync(MItemMaster objItemMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MItemMasters.Add(objItemMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MItemMaster objItemMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MItemMasters.Update(objItemMaster);
                _context.Entry(objItemMaster).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task CancelAsync(MItemMaster objItemMaster, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                MItemMaster objItem = await _context.MItemMasters.FindAsync(objItemMaster.ItemId);
                objItem.IsActive = false;
                objItem.CreatedDate = objItemMaster.CreatedDate;
                objItem.CreatedBy = objItemMaster.CreatedBy;
                _context.MItemMasters.Update(objItem);
                _context.Entry(objItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
