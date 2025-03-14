using Hea.Models;

namespace Hea.Repository
{
    public interface INotificationRepository
    {
        Task<Notification> SendNotificationAsync(int userId, string type, string message, string status);
        Task<List<Notification>> GetUserNotificationsAsync(int userId);
        Task AddNotificationAsync(int userId, string message);
        Task<List<Notification>> GetNotificationsByUserIdAsync(int userId);
    }
}