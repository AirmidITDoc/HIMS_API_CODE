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
        //public virtual async Task InsertAsync(MQuestionMaster ObjMQuestionMaster, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

        //    {
                //var lastSeqNoStr = await _context.MQuestionMasters
                //    .OrderByDescending(x => x.OtreservationNo)
                //    .Select(x => x.OtreservationNo)
                //    .FirstOrDefaultAsync();

                //int lastSeqNo = 0;
                //if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                //    lastSeqNo = parsed;

                //// Increment the sequence number
                //int newSeqNo = lastSeqNo + 1;
                //ObjMQuestionMaster.OtreservationNo = newSeqNo.ToString();


        //        ObjMQuestionMaster.CreatedBy = UserId;
        //        ObjMQuestionMaster.CreatedDate = AppTime.Now;

        //        _context.MQuestionMasters.Add(ObjMQuestionMaster);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
      

    }
}
