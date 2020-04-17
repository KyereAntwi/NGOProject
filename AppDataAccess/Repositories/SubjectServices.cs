using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class SubjectServices : ISubjectServices
    {
        private readonly DataContext _dataContext;

        public SubjectServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Subject> AddSubject(Subject subject)
        {
            var response = await _dataContext.Subjects.SingleOrDefaultAsync(s => s.Name == subject.Name);

            if (response != null)
                return null;

            await _dataContext.Subjects.AddAsync(subject);
            await _dataContext.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject> DeleteSubjectAsync(Guid Id)
        {
            var subject = await _dataContext.Subjects.FindAsync(Id);

            if (subject == null)
                return null;

            _dataContext.Subjects.Remove(subject);
            await _dataContext.SaveChangesAsync();
            return subject;
        }

        public async Task<List<Subject>> GetStudentSubjectsAsync(Guid ChildId)
        {
            var child = await _dataContext.Children
                .Include(c => c.Class)
                .SingleOrDefaultAsync(c => c.Id == ChildId);

            var ChildClass = await _dataContext.Classes
                .Include(c => c.Subjects).ThenInclude(s => s.Subject)
                .SingleOrDefaultAsync(c => c.Id == child.Class.ClassId);

            List<Subject> subjects = new List<Subject>();

            if (ChildClass.Subjects != null) 
            {
                foreach (var sub in ChildClass.Subjects) 
                {
                    subjects.Add(sub.Subject);
                }
            }

            return subjects;
        }

        public async Task<Subject> Subject(Guid Id)
        {
            return await _dataContext.Subjects
                .Include(s => s.Classes).ThenInclude(s => s.Class)
                .SingleOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<List<Subject>> SubjectsAsync()
        {
            return await _dataContext.Subjects
                .Include(s => s.Classes).ThenInclude(s => s.Class)
                .ToListAsync();
        }
    }
}
