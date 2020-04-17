using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface ILettersServices
    {
        Task<Letter> AddLetterAsync(Letter letter);
        Task<Letter> GetLetterAsync(Guid Id);
        Task<List<Letter>> GetLettersAsync();
        Task<List<Letter>> GetUnReadLettersAsync();
        Task<bool> MarkLetterReadAsync(Guid Id);
    }
}
