using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Masters
{
    public class MenuMasterService : IMenuMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MenuMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(MenuMaster objMenuMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MenuMasters.Add(objMenuMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}

