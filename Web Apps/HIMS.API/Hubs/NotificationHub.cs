using Microsoft.AspNetCore.SignalR;

namespace HIMS.API.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendInvestigationDashboard(string mode, string message)
        {
            await Clients.All.SendAsync("ReceiveInvestigationDashboard", mode, message);
        }
    }

}
