using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OTManagment
{
    public class OTOperativeNotesService : IOTOperativeNotes
    {
        private readonly HIMSDbContext _context;
        public OTOperativeNotesService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TOtRequestHeaders
                    .OrderByDescending(x => x.OtrequestNo)
                    .Select(x => x.OtrequestNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTOtRequestHeader.OtrequestNo = newSeqNo.ToString();


                ObjTOtRequestHeader.CreatedBy = UserId;
                ObjTOtRequestHeader.CreatedDate = DateTime.Now;

                _context.TOtRequestHeaders.Add(ObjTOtRequestHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public virtual async Task UpdateAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                // Attach entity
                _context.Attach(ObjTOtRequestHeader);
                _context.Entry(ObjTOtRequestHeader).State = EntityState.Modified;

                // Prevent overwriting of important fields
                _context.Entry(ObjTOtRequestHeader).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTOtRequestHeader).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTOtRequestHeader).Property(x => x.OtrequestNo).IsModified = false;

                // Update modified fields
                ObjTOtRequestHeader.ModifiedBy = UserId;
                ObjTOtRequestHeader.ModifiedDate = DateTime.Now;

                // Ignore any additional columns if specified
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(ObjTOtRequestHeader).Property(column).IsModified = false;
                    }
                }
                // Delete related detail records safely
                var lstSurgery = await _context.TOtRequestSurgeryDetails.Where(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId).ToListAsync();
                if (lstSurgery.Count > 0)
                    _context.TOtRequestSurgeryDetails.RemoveRange(lstSurgery);

                var lstAttend = await _context.TOtRequestAttendingDetails.Where(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId).ToListAsync();
                if (lstAttend.Count > 0)
                    _context.TOtRequestAttendingDetails.RemoveRange(lstAttend);

                var lstDiagnosis = await _context.TOtRequestDiagnoses.Where(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId).ToListAsync();
                if (lstDiagnosis.Count > 0)
                    _context.TOtRequestDiagnoses.RemoveRange(lstDiagnosis);

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
