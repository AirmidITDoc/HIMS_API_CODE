using HIMS.API.Hubs;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Notification;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace HIMS.API.Utility
{
    public class NotificationUtility : INotificationUtility
    {
        private readonly INotificationService _INotificationService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationUtility(INotificationService notificationService, IHubContext<NotificationHub> hubContext)
        {
            _INotificationService = notificationService;
            _hubContext = hubContext;
        }
        public async Task SendNotificationAsync(string title, string message, string redirectUrl, long userId)
        {
            NotificationMaster objNotification = new() { CreatedDate = AppTime.Now, IsActive = true, IsDeleted = false, IsRead = false, NotiBody = message, NotiTitle = title, UserId = userId, RedirectUrl = redirectUrl };
            await _INotificationService.Save(objNotification);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", JsonSerializer.Serialize(new { objNotification.CreatedDate, objNotification.NotiBody, objNotification.NotiTitle, objNotification.Id, objNotification.RedirectUrl }), objNotification.UserId);
        }
    }
}
