using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class ItemMasterService : IItemMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public ItemMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(MItemMaster objItem, int UserId, string Username)
        {
            try
            {
                //Add header table records
                DatabaseHelper odal = new();
                string[] rEntity = { "UpDatedBy", "CreatedBy", "CreatedDate", "Addedby", "MAssignItemToStore" };
                var entity = objItem.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string ItemId = odal.ExecuteNonQuery("Insert_ItemMaster_1_New", CommandType.StoredProcedure, "ItemId", entity);
                objItem.ItemId = Convert.ToInt32(ItemId);

                //Add sub table records
                if (objItem.ItemId == 1)
                {
                    foreach (var item in objItem.MAssignItemToStores)
                    {
                        item.ItemId = objItem.ItemId;
                    }
                    _context.MAssignItemToStores.AddRange(objItem.MAssignItemToStores);
                    await _context.SaveChangesAsync();
                }
                //else
                //{
                //    foreach (var item in objTest.MPathTestDetailMasters)
                //    {
                //        item.TestId = objTest.TestId;
                //    }
                //    _context.MPathTestDetailMasters.AddRange(objTest.MPathTestDetailMasters);
                //    await _context.SaveChangesAsync();
                //}
            }
            catch (Exception)
            {
                // Delete header table realted records
                MItemMaster? objitem = await _context.MItemMasters.FindAsync(objItem.ItemId);
                if (objitem != null)
                {
                    _context.MItemMasters.Remove(objitem);
                }

                // Delete details table realted records
                var lst = await _context.MAssignItemToStores.Where(x => x.ItemId == objItem.ItemId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.MAssignItemToStores.RemoveRange(lst);
                }
                await _context.SaveChangesAsync();

                //var lst1 = await _context.MPathTestDetailMasters.Where(x => x.TestId == objTest.TestId).ToListAsync();
                //if (lst1.Count > 0)
                //{
                //    _context.MPathTestDetailMasters.RemoveRange(lst1);
                //}
                //await _context.SaveChangesAsync();
            }
        }


        public virtual async Task InsertAsync(MItemMaster objItem, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // remove conditional records
                if (objItem.ItemId == 1)
                    objItem.MAssignItemToStores = null;
                //else
                //    objTest.MPathTemplateDetails = null;
                //Add header table records
                _context.MItemMasters.Add(objItem);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}

