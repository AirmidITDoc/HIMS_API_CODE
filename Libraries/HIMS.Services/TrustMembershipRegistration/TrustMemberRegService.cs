using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;


namespace HIMS.Services.TrustMembershipRegistration
{
    public class TrustMemberRegService : ITrustMembershipRegService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public TrustMemberRegService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
       
        public virtual async Task<TMembershipRegistration> GetById(int Id)
        {
            return await this._context.TMembershipRegistrations .Include(x => x.TMembershipChildren).Include(x => x.TMembershipEmrgencies).Include(x => x.TMembershipRelatives).FirstOrDefaultAsync(x => x.MembershipId == Id);
        }


        public virtual async Task InsertAsync(TMembershipRegistration ObjTMembershipRegistration, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TMembershipRegistrations
                    .OrderByDescending(x => x.MembershipNo)
                    .Select(x => x.MembershipNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTMembershipRegistration.MembershipNo = newSeqNo.ToString();


                ObjTMembershipRegistration.CreatedBy = UserId;
                ObjTMembershipRegistration.CreatedDate = AppTime.Now;

                _context.TMembershipRegistrations.Add(ObjTMembershipRegistration);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TMembershipRegistration ObjTMembershipRegistration, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long MembershipId = ObjTMembershipRegistration.MembershipId;

                // Delete related details first
                var lstAttend = await _context.TMembershipChildren
                    .Where(x => x.MembershipId == MembershipId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TMembershipChildren.RemoveRange(lstAttend);

                var lstSurgery = await _context.TMembershipEmrgencies
                    .Where(x => x.MembershipId == MembershipId)
                    .ToListAsync();
                if (lstSurgery.Any())
                    _context.TMembershipEmrgencies.RemoveRange(lstSurgery);

                var lstDiagnosis = await _context.TMembershipRelatives
                    .Where(x => x.MembershipId == MembershipId)
                    .ToListAsync();
                if (lstDiagnosis.Any())
                    _context.TMembershipRelatives.RemoveRange(lstDiagnosis);

                //Save deletion first
                await _context.SaveChangesAsync();

                // Then attach and update header
                _context.Attach(ObjTMembershipRegistration);
                _context.Entry(ObjTMembershipRegistration).State = EntityState.Modified;

                _context.Entry(ObjTMembershipRegistration).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTMembershipRegistration).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTMembershipRegistration).Property(x => x.MembershipNo).IsModified = false;

                ObjTMembershipRegistration.ModifiedBy = UserId;
                ObjTMembershipRegistration.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTMembershipRegistration).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
