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

    public class DietMenuMasterService : IDietMenuMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DietMenuMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(MDietMenuMaster ObjMDietMenuMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                //var lastSeqNoStr = await _context.TDietPatientRequestHeaders
                //    .OrderByDescending(x => x.DietReqNo)
                //    .Select(x => x.DietReqNo)
                //    .FirstOrDefaultAsync();

                //int lastSeqNo = 0;
                //if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                //    lastSeqNo = parsed;

                //// Increment the sequence number
                //int newSeqNo = lastSeqNo + 1;
                //ObjTDietPatientRequestHeader.DietReqNo = newSeqNo.ToString();


                ObjMDietMenuMaster.CreatedBy = UserId;
                ObjMDietMenuMaster.CreatedDate = AppTime.Now;

                _context.MDietMenuMasters.Add(ObjMDietMenuMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
