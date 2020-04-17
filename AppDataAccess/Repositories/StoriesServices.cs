using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class StoriesServices : IStoriesServies
    {
        private readonly DataContext _data;

        public StoriesServices(DataContext dataContext)
        {
            _data = dataContext;
        }

        public async Task<Anouncement> AddAnouncementAsync(Anouncement anouncement)
        {
            await _data.Anouncements.AddAsync(anouncement);
            await _data.SaveChangesAsync();
            return anouncement;
        }

        public async Task<AnouncementSub> AddAnouncementSubAsync(AnouncementSub anouncementSub)
        {
            await _data.AnouncementSubs.AddAsync(anouncementSub);
            await _data.SaveChangesAsync();
            return anouncementSub;
        }

        public async Task<Anouncement> DeleteAnouncementAsync(Guid Id)
        {
            var anouncement = await _data.Anouncements.FindAsync(Id);
            if (anouncement == null)
                return null;
            _data.Anouncements.Remove(anouncement);
            await _data.SaveChangesAsync();
            return anouncement;
        }

        public async Task<Anouncement> GetAnouncementAsync(Guid Id)
        {
            return await _data.Anouncements.Include(a => a.Subs).SingleOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<List<Anouncement>> GetAnouncementsAsync()
        {
            return await _data.Anouncements.Include(a => a.Subs).ToListAsync();
        }
    }
}
