using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class NotificationServies : INotificationServices
    {
        private readonly DataContext _dataContext;

        public NotificationServies(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Notification> AddNotificationAsync(Notification notification)
        {
            await _dataContext.Notifications.AddAsync(notification);
            await _dataContext.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> DeleteNotificationAsync(Guid Id)
        {
            var notification = await _dataContext.Notifications.FindAsync(Id);
            _dataContext.Notifications.Remove(notification);
            await _dataContext.SaveChangesAsync();
            return notification;
        }

        public async Task<List<Notification>> GetUsersNotificationsAsync(Guid Id)
        {
            return await _dataContext.Notifications
                .Where(n => n.UserId == Id).ToListAsync();
        }

        public async Task<Notification> MarkSeenAsync(Guid Id)
        {
            var notification = await _dataContext.Notifications.FindAsync(Id);
            notification.Seen = true;
            _dataContext.Notifications.Update(notification);
            await _dataContext.SaveChangesAsync();
            return notification;
        }
    }
}
