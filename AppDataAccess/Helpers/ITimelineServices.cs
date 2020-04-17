using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Helpers
{
    public interface ITimelineServices
    {
        Task<TimeLine> RegisterTimeLineAsync(string UserId, string activity, string status);
        Task<List<TimeLine>> GetTimeLinesByTimeStampAsync(DateTime timeStamp);
        Task<TimeLine> DeleteTimeLineAsync(Guid Id);
        Task<TimeLine> DeleteUserActivitiesAsync(Guid UserId);
        Task<bool> DeleteAllActivitiesAsync();
    }
}
