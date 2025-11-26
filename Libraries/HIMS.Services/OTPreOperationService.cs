using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;


namespace HIMS.Services
{
    public class OTPreOperationService: IOTPreOperationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OTPreOperationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TOtPreOperationHeader ObjTOtPreOperationHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TOtPreOperationHeaders
                    .OrderByDescending(x => x.OtpreOperationNo)
                    .Select(x => x.OtpreOperationNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Assign new sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTOtPreOperationHeader.OtpreOperationNo = newSeqNo.ToString();

                // Audit fields
                ObjTOtPreOperationHeader.Createdby = UserId;
                ObjTOtPreOperationHeader.CreatedDate = DateTime.Now;

                // Insert
                _context.TOtPreOperationHeaders.Add(ObjTOtPreOperationHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task InsertAsync(TOtPreOperationHeader ObjTOtPreOperationHeader, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


        //    {
        //        // Get last sequence number
        //        var lastSeqNoStr = await _context.TOtPreOperationHeaders
        //            .OrderByDescending(x => x.OtpreOperationNo)
        //            .Select(x => x.OtpreOperationNo)
        //            .FirstOrDefaultAsync();

        //        int lastSeqNo = 0;
        //        if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
        //            lastSeqNo = parsed;

        //        // Assign new sequence number
        //        int newSeqNo = lastSeqNo + 1;
        //        ObjTOtPreOperationHeader.OtpreOperationNo = newSeqNo.ToString();

        //        // Audit fields
        //        ObjTOtPreOperationHeader.Createdby = UserId;
        //        ObjTOtPreOperationHeader.CreatedDate = DateTime.Now;

        //        // Insert
        //        _context.TOtPreOperationHeaders.Add(ObjTOtPreOperationHeader);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

    }
}
