using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IUsersServices
    {
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<List<ApplicationUser>> GetApplicationUsersAsync();
        Task<ApplicationUser> AddUserAsync(ApplicationUser user);
        Task<ApplicationUser> DeleteUserAsync(Guid Id);
        Task<ApplicationUser> UpdateUserAsync(ApplicationUser user);
    }
}
