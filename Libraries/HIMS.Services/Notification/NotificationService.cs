using HIMS.Data;
using HIMS.Data.Models;
using System.Transactions;

namespace HIMS.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly HIMSDbContext _context;
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
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                obj.IsRead = true;
                obj.ReadDate = DateTime.Now;
                // Update header & detail table records
                _context.NotificationMasters.Update(obj);
                _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task<int> UnreadCount(int UserId)
        {
            return await _context.NotificationMasters.Where(x => x.UserId == UserId && x.IsActive && !x.IsDeleted && !x.IsRead).CountAsync();
        }
        public virtual async Task ReadAllNotification(int UserId)
        {
            List<NotificationMaster> lst = await _context.NotificationMasters.Where(x => x.UserId == UserId && x.IsActive && !x.IsDeleted && !x.IsRead).ToListAsync();
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                foreach (var obj in lst)
                {
                    obj.IsRead = true;
                    obj.ReadDate = DateTime.Now;
                    // Update header & detail table records
                    _context.NotificationMasters.Update(obj);
                    _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public async Task<NotificationMaster> Save(NotificationMaster entity)
        {
            await _context.NotificationMasters.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<NotificationMaster>> Save(List<NotificationMaster> entity)
        {
            await _context.NotificationMasters.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
