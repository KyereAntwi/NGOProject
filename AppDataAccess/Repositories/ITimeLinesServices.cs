using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface ITimeLinesServices
    {
        Task<TimeLine> AddTimeLineAsync(TimeLine timeLine);
        Task<List<TimeLine>> GetTimeLinesAsync();
        Task<List<TimeLine>> GetUserTimeLinesAsync(Guid UserId);
    }
}
