using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IEventsServices
    {
        Task<Event> AddEventAsync(Event @event);
        Task<Event> DeleteEventAsync(Guid Id);
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEventAsync(Guid Id);
    }
}
