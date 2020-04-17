using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class LettersServices : ILettersServices
    {
        private readonly DataContext _dataContext;

        public LettersServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Letter> AddLetterAsync(Letter letter)
        {
            await _dataContext.Letters.AddAsync(letter);
            await _dataContext.SaveChangesAsync();
            return letter;
        }

        public async Task<Letter> GetLetterAsync(Guid Id)
        {
            return await _dataContext.Letters
                .Include(l => l.Child)
                .Include(l => l.ApplicationUser)
                .SingleOrDefaultAsync(l => l.Id == Id);
        }

        public async Task<List<Letter>> GetLettersAsync()
        {
            return await _dataContext.Letters
                .Include(l => l.Child)
                .Include(l => l.ApplicationUser)
                .ToListAsync();
        }

        public async Task<List<Letter>> GetUnReadLettersAsync()
        {
            return await _dataContext.Letters
                .Include(l => l.Child)
                .Include(l => l.ApplicationUser)
                .Where(l => l.Seen == false)
                .ToListAsync();
        }

        public async Task<bool> MarkLetterReadAsync(Guid Id)
        {
            var letter = await _dataContext.Letters.FindAsync(Id);
            letter.Seen = true;
            _dataContext.Letters.Update(letter);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
