using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IClassServices
    {
        Task<Class> AddClassAsync(Class @class);
        Task<Class> DeleteClassAsync(Guid Id);
        Task<List<Class>> ClassesAsync();
        Task<Class> ClassAsync(Guid Id);
        Task<Subject> AddSubjectToClassAsync(Guid ClassId, Guid SubjectId);
        Task<ClassSubject> RemoveSubjectFromClassAsync(Guid ClassId, Guid SubjectId);
    }
}
