using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class TimeLineServices : ITimeLinesServices
    {
        private readonly DataContext _dataContext;

        public TimeLineServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<TimeLine> AddTimeLineAsync(TimeLine timeLine)
        {
            await _dataContext.TimeLines.AddAsync(timeLine);
            await _dataContext.SaveChangesAsync();
            return timeLine;
        }

        public async Task<List<TimeLine>> GetTimeLinesAsync()
        {
            return await _dataContext.TimeLines
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<List<TimeLine>> GetUserTimeLinesAsync(Guid UserId)
        {
            return await _dataContext.TimeLines
                .Include(t => t.User)
                .Where(t => t.UserId == UserId)
                .ToListAsync();
        }
    }
}
