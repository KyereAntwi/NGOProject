using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IStoriesServies
    {
        Task<Anouncement> GetAnouncementAsync(Guid Id);
        Task<Anouncement> AddAnouncementAsync(Anouncement anouncement);
        Task<List<Anouncement>> GetAnouncementsAsync();
        Task<Anouncement> DeleteAnouncementAsync(Guid Id);
        Task<AnouncementSub> AddAnouncementSubAsync(AnouncementSub anouncementSub);
    }
}
