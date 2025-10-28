using HIMS.Data;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace HIMS.Services.Masters
{
    public class CampMasterService : ICampMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CampMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<MCampMaster> GetById(int Id)
        {
            return await this._context.MCampMasters.FirstOrDefaultAsync(x => x.CampId == Id);
        }


        public virtual async Task InsertAsync(MCampMaster objMCampMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MCampMasters.Add(objMCampMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MCampMaster objMCampMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MCampMasters.Update(objMCampMaster);
                _context.Entry(objMCampMaster).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task CancelAsync(MCampMaster objMCampMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                MCampMaster obj = await _context.MCampMasters.FindAsync(objMCampMaster.CampId);
                obj.IsActive = false;
                obj.CreatedDate = objMCampMaster.CreatedDate;
                obj.CreatedBy = objMCampMaster.CreatedBy;
                _context.MCampMasters.Update(obj);
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


    }
}
