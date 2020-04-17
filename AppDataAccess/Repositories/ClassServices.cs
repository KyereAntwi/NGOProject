using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class ClassServices : IClassServices
    {
        private readonly DataContext _dataContext;

        public ClassServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Class> AddClassAsync(Class @class)
        {
            var response = await _dataContext.Classes.SingleOrDefaultAsync(c => c.Name == @class.Name);

            if (response != null)
                return null;

            await _dataContext.Classes.AddAsync(@class);
            await _dataContext.SaveChangesAsync();
            return @class;
        }

        public async Task<Subject> AddSubjectToClassAsync(Guid ClassId, Guid SubjectId)
        {
            var classRes = await _dataContext.Classes
                .Include(c => c.Subjects).ThenInclude(s => s.Subject)
                .SingleOrDefaultAsync(c => c.Id == ClassId);

            var subject = await _dataContext.Subjects.FindAsync(SubjectId);

            if (classRes == null || subject == null)
                return null;

            List<Guid> ids = new List<Guid>();
            foreach (var sub in classRes.Subjects) 
            {
                ids.Add(sub.SubjectId);
            }

            if (ids.Contains(subject.Id))
                return null;

            ClassSubject classSubject = new ClassSubject()
            {
                ClassId = ClassId,
                SubjectId = SubjectId,
            };

            await _dataContext.ClassSubjects.AddAsync(classSubject);
            await _dataContext.SaveChangesAsync();

           

            return await _dataContext.Subjects.FindAsync(classSubject.SubjectId);
        }

        public async Task<Class> ClassAsync(Guid Id)
        {
            return await _dataContext.Classes
                .Include(c => c.Subjects).ThenInclude(c => c.Subject)
                .Include(c => c.Students).ThenInclude(c => c.Child)
                .SingleOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<List<Class>> ClassesAsync()
        {
            return await _dataContext.Classes
                .Include(c => c.Subjects)
                .Include(c => c.Students)
                .ToListAsync();
        }

        public async Task<Class> DeleteClassAsync(Guid Id)
        {
            var response = await _dataContext.Classes.FindAsync(Id);

            if (response == null)
                return null;

            _dataContext.Classes.Remove(response);
            await _dataContext.SaveChangesAsync();
            return response;
        }

        public async Task<ClassSubject> RemoveSubjectFromClassAsync(Guid ClassId, Guid SubjectId)
        {
            var response = await _dataContext.ClassSubjects.SingleOrDefaultAsync(c => c.ClassId == ClassId && c.SubjectId == SubjectId);

            if (response == null)
                return null;

            _dataContext.ClassSubjects.Remove(response);
            await _dataContext.SaveChangesAsync();
            return response;
        }
    }
}
