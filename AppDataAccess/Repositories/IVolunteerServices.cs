using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IVolunteerServices
    {
        Task<List<ApplicationUser>> GetApplicationUsersAsync();
        Task<ApplicationUser> GetApplicationUserAsync(Guid Id);
        Task<ApplicationUser> AddApplicationUserAsync(ApplicationUser applicationUser);
        Task<ApplicationUser> UpdateApplicationUserAsync(ApplicationUser applicationUser);
        Task<ApplicationUser> DeleteApplicationUserAsync(Guid Id);
    }
}
