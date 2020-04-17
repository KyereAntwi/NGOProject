using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class PrayerSupportServices : IPrayerSupportServices
    {
        private readonly DataContext _dataContext;

        public PrayerSupportServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<PrayerVolunteer>> GetPrayerVolunteersAsync()
        {
            return await _dataContext.PrayerVolunteers
                .Include(p => p.ApplicationUser)
                .ToListAsync();
        }

        public async Task<PrayerVolunteer> JoinPrayerAsync(PrayerVolunteer prayerVolunteer)
        {
            await _dataContext.PrayerVolunteers.AddAsync(prayerVolunteer);
            await _dataContext.SaveChangesAsync();
            return prayerVolunteer;
        }

        public async Task<bool> UnSubsricbePrayerAsync(Guid Id)
        {
            var request = await _dataContext.PrayerVolunteers.FindAsync(Id);
            _dataContext.PrayerVolunteers.Remove(request);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
