using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class VolunteerServices : IVolunteerServices
    {
        private readonly DataContext _dataContext;

        public VolunteerServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ApplicationUser> AddApplicationUserAsync(ApplicationUser applicationUser)
        {
            await _dataContext.ApplicationUsers.AddAsync(applicationUser);
            await _dataContext.SaveChangesAsync();
            return applicationUser;
        }

        public async Task<ApplicationUser> DeleteApplicationUserAsync(Guid Id)
        {
            var user = await _dataContext.ApplicationUsers.FindAsync(Id);
            if (user == null)
                return null;
            _dataContext.ApplicationUsers.Remove(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> GetApplicationUserAsync(Guid Id)
        {
            return await _dataContext
                .ApplicationUsers
                .Include(a => a.Children).ThenInclude(c => c.Child)
                .Include(a => a.Letters)
                .Include(a => a.PrayerVolunteer)
                .SingleOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<List<ApplicationUser>> GetApplicationUsersAsync()
        {
            return await _dataContext
                .ApplicationUsers
                .ToListAsync();
        }

        public async Task<ApplicationUser> UpdateApplicationUserAsync(ApplicationUser applicationUser)
        {
            var user = await _dataContext.ApplicationUsers.FindAsync(applicationUser.Id);

            if (user == null)
                return null;

            user.Name = applicationUser.Name;
            user.Nationality = applicationUser.Nationality;
            user.Gender = applicationUser.Gender;
            user.DateOfBirth = applicationUser.DateOfBirth;

            _dataContext.ApplicationUsers.Update(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
