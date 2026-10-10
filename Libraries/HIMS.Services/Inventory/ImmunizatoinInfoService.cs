using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class ImmunizatoinInfoService : IImmunizatoinInfoService
    {
        private readonly HIMSDbContext _context;

        public ImmunizatoinInfoService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TImmunizatoinInfo?> GetByIdAsync(long id)
        {
            return await _context.TImmunizatoinInfos.FirstOrDefaultAsync(x => x.RegId == id);
        }


        public virtual async Task ImmunizationInfoAsync(List<TImmunizatoinInfo> models, long regId, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            var oldRecords = await _context.TImmunizatoinInfos.Where(x => x.RegId == regId).ToListAsync();

            _context.TImmunizatoinInfos.RemoveRange(oldRecords);

            _context.TImmunizatoinInfos.AddRange(models);

            await _context.SaveChangesAsync(UserId, Username);

            scope.Complete();
        }
    }
}