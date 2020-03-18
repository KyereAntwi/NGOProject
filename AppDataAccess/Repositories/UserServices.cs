using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class UserServices : IUsersServices
    {
        private readonly DataContext _dataContext;

        public UserServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
        {
            await _dataContext.ApplicationUsers.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> DeleteUserAsync(Guid Id)
        {
            var user = await _dataContext.ApplicationUsers.FindAsync(Id);

            if (user == null)
                return null;

            _dataContext.ApplicationUsers.Remove(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<ApplicationUser>> GetApplicationUsersAsync()
        {
            return await _dataContext.ApplicationUsers
                .Include(a => a.PrayerVolunteer)
                .Include(a => a.Children)
                .Include(a => a.Letters)
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await _dataContext.ApplicationUsers
                .Include(a => a.PrayerVolunteer)
                .Include(a => a.Children)
                .Include(a => a.Letters)
                .SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public Task<ApplicationUser> UpdateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
