﻿using Hea.Models;

namespace Hea.Service
{
    public interface INotificationService
    {
        Task<Notification> SendNotificationAsync(int userId, string message, string status);
        Task<List<Notification>> GetUserNotificationsAsync(int userId);
        Task AddNotificationAsync(int userId, string message);
        Task<List<Notification>> GetNotificationsByUserIdAsync(int userId);
    }
}
