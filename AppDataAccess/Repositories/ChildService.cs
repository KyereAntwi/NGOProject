using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class ChildService : IChildServices
    {
        private readonly DataContext _dataContext;

        public ChildService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Child> AddAChild(Child child)
        {
            await _dataContext.Children.AddAsync(child);
            await _dataContext.SaveChangesAsync();
            return child;
        }

        public async Task<Child> DeleteChild(Guid Id)
        {
            Child child = await _dataContext.Children.FindAsync(Id);

            if (child == null)
                return null;

            _dataContext.Children.Remove(child);
            await _dataContext.SaveChangesAsync();
            return child;
        }

        public async Task<Child> GetAChildAsync(Guid Id)
        {
            return await _dataContext
                .Children
                .Include(c => c.Class)
                .Include(c => c.Sponser)
                .Include(c => c.Registrations)
                .SingleOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<List<Child>> GetAllChildrenAsync()
        {
            return await _dataContext.Children
                .Include(c => c.Class)
                .Include(c => c.Sponser)
                .Include(c => c.Registrations)
                .ToListAsync();
        }

        public Task<Child> UpdateChild(Child child)
        {
            throw new NotImplementedException();
        }
    }
}
