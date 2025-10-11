using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public  class TprocessOtpService : ITprocessOtpService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public TprocessOtpService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
       
        public virtual async Task UpdateAsync(TProcessOtp ObjTProcessOtp, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            var existing = await _context.TProcessOtps.FirstOrDefaultAsync(x => x.MsgId == ObjTProcessOtp.MsgId);

            //  Update only the required fields
            existing.IsVerified = ObjTProcessOtp.IsVerified;
            existing.ModifiedBy = ObjTProcessOtp.ModifiedBy;
            existing.ModifiedDate = ObjTProcessOtp.ModifiedDate;
            await _context.SaveChangesAsync();
            scope.Complete();
        }

    }
}
