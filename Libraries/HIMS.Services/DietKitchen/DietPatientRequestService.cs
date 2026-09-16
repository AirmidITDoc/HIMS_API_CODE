using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietKitchen
{
    public  class DietPatientRequestService : IDietPatientRequestService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DietPatientRequestService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TDietPatientRequestHeaders
                    .OrderByDescending(x => x.DietReqNo)
                    .Select(x => x.DietReqNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTDietPatientRequestHeader.DietReqNo = newSeqNo.ToString();


                ObjTDietPatientRequestHeader.CreatedBy = UserId;
                ObjTDietPatientRequestHeader.CreatedDate = AppTime.Now;

                _context.TDietPatientRequestHeaders.Add(ObjTDietPatientRequestHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TDietPatientRequestHeader ObjTDietPatientRequestHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);

            long dietReqId = ObjTDietPatientRequestHeader.DietReqId;

            var newDetails = ObjTDietPatientRequestHeader.TDietPatReqDetails?.ToList()
                              ?? new List<TDietPatReqDetail>();
            ObjTDietPatientRequestHeader.TDietPatReqDetails = null;

            // Delete existing related details first
            var lstDetails = await _context.TDietPatReqDetails
                .Where(x => x.DietReqId == dietReqId)
                .ToListAsync();

            if (lstDetails.Any())
                _context.TDietPatReqDetails.RemoveRange(lstDetails);

            // Save deletion first
            await _context.SaveChangesAsync();

            // Then attach and update header
            _context.Attach(ObjTDietPatientRequestHeader);
            _context.Entry(ObjTDietPatientRequestHeader).State = EntityState.Modified;

            _context.Entry(ObjTDietPatientRequestHeader).Property(x => x.DietReqNo).IsModified = false;

            ObjTDietPatientRequestHeader.ModifiedBy = UserId;
            ObjTDietPatientRequestHeader.ModifiedDate = AppTime.Now;

            if (ignoreColumns?.Length > 0)
            {
                foreach (var column in ignoreColumns)
                    _context.Entry(ObjTDietPatientRequestHeader).Property(column).IsModified = false;
            }

            // Re-insert the (new) detail rows against this header
            foreach (var detail in newDetails)
            {
                detail.DietReqId = dietReqId;
            }
            await _context.TDietPatReqDetails.AddRangeAsync(newDetails);

            await _context.SaveChangesAsync();
            scope.Complete();
        }


    }
}
