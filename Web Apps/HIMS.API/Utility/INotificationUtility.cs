namespace HIMS.API.Utility
{
    public interface INotificationUtility
    {
        Task SendNotificationAsync(string title, string message, string redirectUrl, long userId);
    }
}
