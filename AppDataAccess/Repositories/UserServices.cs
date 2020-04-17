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

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            await _dataContext.Contacts.AddAsync(contact);
            await _dataContext.SaveChangesAsync();
            return contact;
        }

        public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
        {
            await _dataContext.ApplicationUsers.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<Contact> DeleteContactAsync(Guid Id)
        {
            var contact = await _dataContext.Contacts.FindAsync(Id);
            _dataContext.Contacts.Remove(contact);
            await _dataContext.SaveChangesAsync();
            return contact;
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
                .Include(a => a.TimeLines)
                .ToListAsync();
        }

        public async Task<List<Child>> GetSupportingChildren(string UserId)
        {
            var user = await _dataContext
                .ApplicationUsers
                .Include(u => u.Children).ThenInclude(c => c.Child)
                .SingleOrDefaultAsync(u => u.UserId == UserId);

            List<Child> children = new List<Child>();

            if (user.Children != null) 
            {
                foreach (var child in user.Children) 
                {
                    if (child.Accepted) 
                    {
                        children.Add(child.Child);
                    }
                }
            }

            return children;
        }

        public async Task<ApplicationUser> GetUserAsync(Guid userId)
        {
            return await _dataContext.ApplicationUsers
                .Include(a => a.PrayerVolunteer)
                .Include(a => a.Children).ThenInclude(c => c.Child)
                .Include(a => a.Letters).ThenInclude(l => l.Child)
                .Include(a => a.TimeLines)
                .Include(a => a.Visitations)
                .Include(a => a.Contacts)
                .SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<ApplicationUser> GetUserByUserIdAsync(string Id)
        {
            return await _dataContext
                .ApplicationUsers
                .Include(a => a.PrayerVolunteer)
                .Include(a => a.Children).ThenInclude(l => l.Child)
                .Include(a => a.Letters).ThenInclude(l => l.Child)
                .Include(a => a.TimeLines)
                .Include(a => a.Visitations)
                .Include(a => a.Contacts)
               .SingleOrDefaultAsync(u => u.UserId == Id);
        }

        public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser user)
        {
            var currentUser = await _dataContext.ApplicationUsers.FindAsync(user.Id);

            currentUser.Name = user.Name;
            currentUser.DateOfBirth = user.DateOfBirth;
            currentUser.Gender = user.Gender;
            currentUser.Nationality = user.Nationality;
            currentUser.Photograph = user.Photograph;

            _dataContext.ApplicationUsers.Update(currentUser);
            await _dataContext.SaveChangesAsync();
            return currentUser;
        }
    }
}
