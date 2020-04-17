using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IUsersServices
    {
        Task<ApplicationUser> GetUserAsync(Guid userId);
        Task<ApplicationUser> GetUserByUserIdAsync(string Id);
        Task<List<ApplicationUser>> GetApplicationUsersAsync();
        Task<List<Child>> GetSupportingChildren(string UserId);
        Task<ApplicationUser> AddUserAsync(ApplicationUser user);
        Task<ApplicationUser> DeleteUserAsync(Guid Id);
        Task<ApplicationUser> UpdateUserAsync(ApplicationUser user);
        Task<Contact> AddContactAsync(Contact contact);
        Task<Contact> DeleteContactAsync(Guid Id);
    }
}
