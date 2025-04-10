using Hea.Models;
using Hea.Repository;

namespace Hea.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> SendNotificationAsync(int userId, string message, string status)
        {
            try
            {
                return await _notificationRepository.SendNotificationAsync(userId, message, status);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while sending the notification.", ex);
            }
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            try
            {
                return await _notificationRepository.GetUserNotificationsAsync(userId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving notifications for user with ID {userId}.", ex);
            }
        }

        public async Task AddNotificationAsync(int userId, string message)
        {
            try
            {
                await _notificationRepository.AddNotificationAsync(userId, message);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while adding the notification.", ex);
            }
        }

        public async Task<List<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            try
            {
                return await _notificationRepository.GetNotificationsByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception($"An error occurred while retrieving notifications for user with ID {userId}.", ex);
            }
        }
    }
}