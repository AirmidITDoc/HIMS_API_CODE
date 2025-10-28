using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class PathologyResultEntryService : IPathologyResultEntryService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PathologyResultEntryService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncResultEntry(TPathologyReportDetail objPathologyReportDetail, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TPathologyReportDetails.Add(objPathologyReportDetail);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public virtual async Task InsertAsyncTemplateResult(TPathologyReportTemplateDetail objPathologyReportTemplateDetail, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TPathologyReportTemplateDetails.Add(objPathologyReportTemplateDetail);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task CancelAsync(TPathologyReportDetail objPathologyReportDetail, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TPathologyReportDetail objPathology = await _context.TPathologyReportDetails.FindAsync(objPathologyReportDetail.PathReportDetId);
                //objPathology.Isdeleted = objPathologyReportDetail.Isdeleted;
                _context.TPathologyReportDetails.Update(objPathology);
                _context.Entry(objPathology).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
