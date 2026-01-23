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
    public class EstimateSerive : IEstimasteService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public EstimateSerive(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(TEstimateHeader ObjTEstimateHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TEstimateHeaders
                    .OrderByDescending(x => x.EstimateNo)
                    .Select(x => x.EstimateNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTEstimateHeader.EstimateNo = newSeqNo.ToString();
                ObjTEstimateHeader.CreatedBy = UserId;
                ObjTEstimateHeader.CreatedDate = AppTime.Now;

                _context.TEstimateHeaders.Add(ObjTEstimateHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }      
        }
        public virtual async Task UpdateAsync(TEstimateHeader ObjTEstimateHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long reservationId = ObjTEstimateHeader.EstimateId;

                // Delete related details first
                var lstAttend = await _context.TEstimateDetails
                    .Where(x => x.EstimateId == reservationId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TEstimateDetails.RemoveRange(lstAttend);

                

                // Save deletion first
                await _context.SaveChangesAsync();

                // Then attach and update header
                _context.Attach(ObjTEstimateHeader);
                _context.Entry(ObjTEstimateHeader).State = EntityState.Modified;

                _context.Entry(ObjTEstimateHeader).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTEstimateHeader).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTEstimateHeader).Property(x => x.EstimateNo).IsModified = false;

                ObjTEstimateHeader.ModifiedBy = UserId;
                ObjTEstimateHeader.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTEstimateHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

    }
}
