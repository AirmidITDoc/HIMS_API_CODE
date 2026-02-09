using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;


namespace HIMS.Services.Pathlogy
{
    public  class HomeCollectionService:IHomeCollectionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public HomeCollectionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(THomeCollectionRegistrationInfo ObjTHomeCollectionRegistrationInfo, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.THomeCollectionRegistrationInfos
                    .OrderByDescending(x => x.HomeSeqNo)
                    .Select(x => x.HomeSeqNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTHomeCollectionRegistrationInfo.HomeSeqNo = newSeqNo.ToString();


                ObjTHomeCollectionRegistrationInfo.CreatedBy = UserId;
                ObjTHomeCollectionRegistrationInfo.CreatedDate = AppTime.Now;

                _context.THomeCollectionRegistrationInfos.Add(ObjTHomeCollectionRegistrationInfo);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
