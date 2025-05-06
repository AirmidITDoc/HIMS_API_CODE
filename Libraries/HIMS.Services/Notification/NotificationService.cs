using HIMS.Core.Domain.Grid;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using HIMS.Data;

namespace HIMS.Services.Masters
{
    public class NotificationService : INotificationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public NotificationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<List<NotificationMaster>> GetNotificationByUser(int UserId, int PageCount)
        {
            if (PageCount > 0)
                return await _context.NotificationMasters.Where(x => x.UserId == UserId).OrderByDescending(x => x.CreatedDate).Take(PageCount).ToListAsync();
            else
                return await _context.NotificationMasters.Where(x => x.UserId == UserId).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }
        public virtual async Task ReadNotification(int Id)
        {
            NotificationMaster obj = await _context.NotificationMasters.FindAsync(Id);
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                obj.IsRead = true;
                obj.ReadDate = DateTime.Now;
                // Update header & detail table records
                _context.NotificationMasters.Update(obj);
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task ReadAllNotification(int UserId)
        {
            List<NotificationMaster> lst = await _context.NotificationMasters.Where(x => x.UserId == UserId && !x.IsRead).ToListAsync();
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                foreach (var obj in lst)
                {
                    obj.IsRead = true;
                    obj.ReadDate = DateTime.Now;
                    // Update header & detail table records
                    _context.NotificationMasters.Update(obj);
                    _context.Entry(obj).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
