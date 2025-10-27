using HIMS.Data.Models;

namespace HIMS.Services.Notification
{
    public partial interface INotificationService
    {
        Task<List<NotificationMaster>> GetNotificationByUser(int UserId, int PageCount);
        Task<int> UnreadCount(int UserId);
        Task ReadNotification(long Id);
        Task ReadAllNotification(int UserId);
        Task<NotificationMaster> Save(NotificationMaster entity);
        Task<List<NotificationMaster>> Save(List<NotificationMaster> entity);
    }
}
