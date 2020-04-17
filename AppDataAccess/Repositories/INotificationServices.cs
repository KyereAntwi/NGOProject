using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface INotificationServices
    {
        Task<Notification> AddNotificationAsync(Notification notification);
        Task<List<Notification>> GetUsersNotificationsAsync(Guid Id);
        Task<Notification> MarkSeenAsync(Guid Id);
        Task<Notification> DeleteNotificationAsync(Guid Id);
    }
}
