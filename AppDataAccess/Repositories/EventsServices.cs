using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class EventsServices : IEventsServices
    {
        private readonly DataContext _dataContext;

        public EventsServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Event> AddEventAsync(Event @event)
        {
            await _dataContext.Events.AddAsync(@event);
            await _dataContext.SaveChangesAsync();
            return @event;
        }

        public async Task<Event> DeleteEventAsync(Guid Id)
        {
            var item = await _dataContext.Events.FindAsync(Id);
            if (item == null)
                return null;

            _dataContext.Events.Remove(item);
            await _dataContext.SaveChangesAsync();
            return item;
        }

        public async Task<Event> GetEventAsync(Guid Id)
        {
            return await _dataContext.Events.SingleOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await _dataContext.Events.ToListAsync();
        }
    }
}
