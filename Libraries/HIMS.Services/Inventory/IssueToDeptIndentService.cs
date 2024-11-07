using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class IssueToDeptIndentService : IIssueToDeptIndentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IssueToDeptIndentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TIssueToDepartmentHeader objIssueToDeptIndent, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update store table records
                MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objIssueToDeptIndent.FromStoreId);
                if (StoreInfo != null)
                {
                    StoreInfo.IssueToDeptNo = Convert.ToString(Convert.ToInt32(StoreInfo?.IssueToDeptNo ?? "0") + 1);
                    _context.MStoreMasters.Update(StoreInfo);
                    await _context.SaveChangesAsync();
                }

                // Add header & detail table records
                StoreInfo.IssueToDeptNo = Convert.ToString(Convert.ToInt32(StoreInfo?.IssueToDeptNo ?? "0") + 1);
                _context.TIssueToDepartmentHeaders.Add(objIssueToDeptIndent);
                await _context.SaveChangesAsync();

                scope.Complete();


            }

        }
    }
}
