using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Transactions;

namespace HIMS.Services.Nursing
{
    public class PrescriptionServise : IPrescriptionService
    {
        private readonly HIMSDbContext _context;
        public PrescriptionServise(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
       
        public virtual async Task InsertAsync(TIpmedicalRecord objmedicalRecord, int UserId, string Username)

        {
            using var scope = new TransactionScope( TransactionScopeOption.Required, new TransactionOptions {IsolationLevel = System.Transactions.IsolationLevel.Serializable }, TransactionScopeAsyncFlowOption.Enabled);

            var presNoList = await _context.TIpmedicalRecords
                .Where(x => x.PresNo != null && x.PresNo != "")
                .Select(x => x.PresNo)
                .ToListAsync();

            int lastPresNo = presNoList
                .Select(p => int.TryParse(p, out var n) ? n : 0)
                .DefaultIfEmpty(0)
                .Max();

            //  Increment & assign
            objmedicalRecord.PresNo = (lastPresNo + 1).ToString();

            objmedicalRecord.CreatedBy = UserId;
            objmedicalRecord.CreatedDate = AppTime.Now;

            _context.TIpmedicalRecords.Add(objmedicalRecord);
            await _context.SaveChangesAsync();

            scope.Complete();
        }

        public virtual async Task UpdateAsync(TIpmedicalRecord objmedicalRecord, int UserId, string Username)

        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }, TransactionScopeAsyncFlowOption.Enabled);

            TIpmedicalRecord objPresNo = _context.TIpmedicalRecords.Where(x => x.MedicalRecoredId == objmedicalRecord.MedicalRecoredId).FirstOrDefault();
            if (objPresNo != null)
                _context.Entry(objPresNo).State = EntityState.Detached;

            objmedicalRecord.PresNo = objPresNo.PresNo;

            {
                long reservationId = objmedicalRecord.MedicalRecoredId;

                // ✅ Delete related details first
                var lstAttend = await _context.TIpPrescriptions
                    .Where(x => x.IpmedId == reservationId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TIpPrescriptions.RemoveRange(lstAttend);

            

                // ✅ Save deletion first
                await _context.SaveChangesAsync();

                // ✅ Then attach and update header
                _context.Attach(objmedicalRecord);
                _context.Entry(objmedicalRecord).State = EntityState.Modified;

                _context.Entry(objmedicalRecord).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(objmedicalRecord).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(objmedicalRecord).Property(x => x.MedicalRecoredId).IsModified = false;

                objmedicalRecord.ModifiedBy = UserId;
                objmedicalRecord.ModifiedDate = AppTime.Now;

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }


        public virtual async Task InsertAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int UserId, string Username)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);


            var allPresNos = await _context.TIpprescriptionReturnHs
                .Select(x => x.PresNo)
                .ToListAsync();

            int lastNo = 0;

            foreach (var presNo in allPresNos)
            {
                var match = Regex.Match(presNo ?? "", @"\d+");
                if (match.Success)
                {
                    int number = int.Parse(match.Value);
                    if (number > lastNo) lastNo = number;
                }
            }

            objIpprescriptionReturnH.PresNo = (lastNo + 1).ToString();

            _context.TIpprescriptionReturnHs.Add(objIpprescriptionReturnH);
            await _context.SaveChangesAsync();

            scope.Complete();
        }


        public virtual async Task PrescCancelAsync(TIpPrescription objmedicalRecord, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TIpPrescription ObPres = await _context.TIpPrescriptions.FindAsync(objmedicalRecord.IppreId);
                if (ObPres == null)
                    throw new Exception("Prescription not found.");
                ObPres.IsCancelled = true;
                _context.TIpPrescriptions.Update(ObPres);
                _context.Entry(ObPres).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task PrescreturnCancelAsync(TIpprescriptionReturnH objIpprescriptionReturnH, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {


                TIpprescriptionReturnH Obj1 = await _context.TIpprescriptionReturnHs.FindAsync(objIpprescriptionReturnH.PresReId);
                if (Obj1 == null)
                    throw new Exception("Prescription Return not found.");
                // Cancel fields
                Obj1.Isclosed = true;

                _context.TIpprescriptionReturnHs.Update(Obj1);
                _context.Entry(Obj1).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();

            }
        }
    }
}
