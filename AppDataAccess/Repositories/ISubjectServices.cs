using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface ISubjectServices
    {
        Task<List<Subject>> SubjectsAsync();
        Task<List<Subject>> GetStudentSubjectsAsync(Guid ChildId);
        Task<Subject> Subject(Guid Id);
        Task<Subject> DeleteSubjectAsync(Guid Id);
        Task<Subject> AddSubject(Subject subject);
    }
}
