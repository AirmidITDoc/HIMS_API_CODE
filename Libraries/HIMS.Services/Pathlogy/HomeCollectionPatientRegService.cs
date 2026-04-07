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
    public class HomeCollectionPatientRegService : IHomeCollectionPatientRegService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public HomeCollectionPatientRegService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(THomeCollectionPatientRegistartion ObjTHomeCollectPatientRegistartion, int UserId, string Username)

        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            _context.THomeCollectionPatientRegistartions.Add(ObjTHomeCollectPatientRegistartion);
            await _context.SaveChangesAsync();

            scope.Complete();
        }
        public virtual async Task InsertAsync(TOtReservationHeader ObjTOtReservationHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TOtReservationHeaders
                    .OrderByDescending(x => x.OtreservationNo)
                    .Select(x => x.OtreservationNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTOtReservationHeader.OtreservationNo = newSeqNo.ToString();


                ObjTOtReservationHeader.Createdby = UserId;
                ObjTOtReservationHeader.CreatedDate = AppTime.Now;

                _context.TOtReservationHeaders.Add(ObjTOtReservationHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


    }
}
