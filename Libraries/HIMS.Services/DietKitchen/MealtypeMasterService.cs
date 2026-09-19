using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Infrastructure;
using System.Transactions;
using HIMS.Data.Models;
using LinqToDB;

namespace HIMS.Services.DietKitchen
{
    public class MealtypemasterService : IMealtypemasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MealtypemasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync( MMealTypeMaster ObjMMealTypeMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(   TransactionScopeOption.Required,  new TransactionOptions {     IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted  }, TransactionScopeAsyncFlowOption.Enabled);

            var lastSeqNo = await _context.MMealTypeMasters
                .OrderByDescending(x => x.MealSequence)
                .Select(x => x.MealSequence)
                .FirstOrDefaultAsync();

            int newSeqNo = (lastSeqNo ?? 0) + 1;

            ObjMMealTypeMaster.MealSequence = newSeqNo;

            ObjMMealTypeMaster.CreatedBy = UserId;
            ObjMMealTypeMaster.CreatedDate = AppTime.Now;

            _context.MMealTypeMasters.Add(ObjMMealTypeMaster);

            await _context.SaveChangesAsync();

            scope.Complete();
        }
    }
}
