using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.GastrologyService
{
    public class QuestionMasterService : IQuestionMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public QuestionMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
       
      
        public virtual async Task InsertAsync(MSubQuestionMaster ObjMSubQuestionMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            // Get last sequence number (numeric)
            var lastSeqNo = await _context.MSubQuestionMasters .OrderByDescending(x => x.SequenceNo).Select(x => x.SequenceNo) .FirstOrDefaultAsync();

            var newSeqNo = lastSeqNo + 1;

            // Assign new sequence number
            ObjMSubQuestionMaster.SequenceNo = newSeqNo;

            ObjMSubQuestionMaster.CreatedBy = UserId;
            ObjMSubQuestionMaster.CreatedDate = AppTime.Now;

            _context.MSubQuestionMasters.Add(ObjMSubQuestionMaster);
            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task UpdateAsync(MSubQuestionMaster ObjMSubQuestionMaster, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                long subQuestionId = ObjMSubQuestionMaster.SubQuestionId;

                // ✅ Delete related details first
                var lstAttend = await _context.MSubQuestionValuesMasters
                    .Where(x => x.SubQuestionId == subQuestionId)
                    .ToListAsync();

                if (lstAttend.Any())
                {
                    _context.MSubQuestionValuesMasters.RemoveRange(lstAttend);
                    await _context.SaveChangesAsync();
                }

                // ✅ Save deletion first
                await _context.SaveChangesAsync();

                // ✅ Then attach and update header
                _context.Attach(ObjMSubQuestionMaster);
                _context.Entry(ObjMSubQuestionMaster).State = EntityState.Modified;

                _context.Entry(ObjMSubQuestionMaster).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjMSubQuestionMaster).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjMSubQuestionMaster).Property(x => x.SequenceNo).IsModified = false;

                ObjMSubQuestionMaster.ModifiedBy = UserId;
                ObjMSubQuestionMaster.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjMSubQuestionMaster).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
