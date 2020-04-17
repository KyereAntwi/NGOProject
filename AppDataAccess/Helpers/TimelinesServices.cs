using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Helpers
{
    public class TimelinesServices : ITimelineServices
    {
        private readonly DataContext _data;
        private readonly UserManager<IdentityUser> _userManager;

        public TimelinesServices(DataContext dataContext, UserManager<IdentityUser> userManager)
        {
            _data = dataContext;
            _userManager = userManager;
        }

        public Task<bool> DeleteAllActivitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TimeLine> DeleteTimeLineAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<TimeLine> DeleteUserActivitiesAsync(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeLine>> GetTimeLinesByTimeStampAsync(DateTime timeStamp)
        {
            throw new NotImplementedException();
        }

        public async Task<TimeLine> RegisterTimeLineAsync(string UserId, string activity, string status)
        {
            var user = await _data.ApplicationUsers.SingleOrDefaultAsync(u => u.UserId == UserId);

            TimeLine timeLine = new TimeLine
            {
                Activity = activity,
                TimeStamp = DateTime.Now,
                Status = status,
                UserId = user.Id
            };

            await _data.TimeLines.AddAsync(timeLine);
            await _data.SaveChangesAsync();
            return timeLine;
        }
    }
}
