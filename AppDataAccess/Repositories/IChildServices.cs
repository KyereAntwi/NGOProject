using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IChildServices
    {
        Task<List<Child>> GetAllChildrenAsync();
        Task<Child> GetAChildAsync(Guid Id);
        Task<Child> AddAChild(Child child);
        Task<Child> UpdateChild(Child child);
        Task<Child> DeleteChild(Guid Id);
    }
}
