using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class VisitationServices : IVisitationServices
    {
        private readonly DataContext _dataContext;

        public VisitationServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Visitation> BookVisitationAsync(Visitation visitation)
        {
            await _dataContext.Visitations.AddAsync(visitation);
            await _dataContext.SaveChangesAsync();
            return visitation;
        }

        public async Task<Visitation> CancelVisitationAsync(Guid Id)
        {
            var visitation = await _dataContext.Visitations.FindAsync(Id);
            visitation.Cancelled = true;
            _dataContext.Visitations.Update(visitation);
            await _dataContext.SaveChangesAsync();
            return visitation;
        }

        public async Task<List<Visitation>> GetPendingVisitationsAsync()
        {
            return await _dataContext.Visitations
                .Include(v => v.ApplicationUser)
                .Where(v => v.Cancelled != true && v.Done == false)
                .ToListAsync();
        }

        public async Task<List<Visitation>> GetVisitationsAsync()
        {
            return await _dataContext.Visitations
                .Include(v => v.ApplicationUser)
                .ToListAsync();
        }

        public async Task<Visitation> MarkDoneAsync(Guid Id)
        {
            var visitation = await _dataContext.Visitations.FindAsync(Id);
            visitation.Done = true;
            _dataContext.Visitations.Update(visitation);
            await _dataContext.SaveChangesAsync();
            return visitation;
        }
    }
}
