using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IPrayerSupportServices
    {
        Task<PrayerVolunteer> JoinPrayerAsync(PrayerVolunteer prayerVolunteer);
        Task<List<PrayerVolunteer>> GetPrayerVolunteersAsync();
        Task<bool> UnSubsricbePrayerAsync(Guid Id);
    }
}
