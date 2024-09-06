using HIMS.Data.Models;
using System;
using System.Collections.Generic;
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
        public virtual async Task InsertAsync(MItemMaster objItem, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Insert into MSupplierMaster table
                objItem.ItemId = (objItem != null) ? objItem.ItemId : long.Parse("0");
                _context.MItemMasters.Add(objItem);
                await _context.SaveChangesAsync();

                var ItemStore = new MAssignItemToStore
                {
                    ItemId = objItem.ItemId,
                    AssignId = UserId,
                    StoreId = UserId,

                };

                _context.MAssignItemToStores.Add(ItemStore);
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

    }


}
