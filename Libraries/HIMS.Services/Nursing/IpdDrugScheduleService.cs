using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;


namespace HIMS.Services.Nursing
{
    public class IpdDrugScheduleService : IIpdDrugScheduleService

    {
        private readonly Data.Models.HIMSDbContext _context;
        public IpdDrugScheduleService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        //public virtual async Task InsertAsync(IpdDrugSchedule ObjIpdDrugSchedule, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

        //    {
        //        var lastSeqNoStr = await _context.IpdDrugSchedules
        //            .OrderByDescending(x => x.DoseNo)
        //            .Select(x => x.DoseNo)
        //            .FirstOrDefaultAsync();

        //        int lastSeqNo = 0;
        //        if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
        //            lastSeqNo = parsed;

        //        // Increment the sequence number
        //        int newSeqNo = lastSeqNo + 1;
        //        ObjIpdDrugSchedule.DoseNo = newSeqNo.ToString();
        //        ObjIpdDrugSchedule.DoseTime = AppTime.Now;

        //        _context.IpdDrugSchedules.Add(ObjIpdDrugSchedule);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
        public virtual async Task InsertAsync(IpdDrugSchedule ObjIpdDrugSchedule, int UserId, string Username)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);
            {
                var lastSeqNo = await _context.IpdDrugSchedules
                    .OrderByDescending(x => x.DoseNo)
                    .Select(x => x.DoseNo)
                    .FirstOrDefaultAsync();  

                int newSeqNo = lastSeqNo + 1;

                ObjIpdDrugSchedule.DoseNo = newSeqNo;
                ObjIpdDrugSchedule.DoseTime = AppTime.Now;

                _context.IpdDrugSchedules.Add(ObjIpdDrugSchedule);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }

}
