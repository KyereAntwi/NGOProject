using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IYearsSemServices
    {
        Task<AcademicYear> AddAcademicYearAsync(AcademicYear year);
        Task<List<AcademicYear>> GetAcademicYearsAsync();
        Task<AcademicYear> GetAcademicYearAsync(Guid Id);
        Task<AcademicYear> DeleteAcademicYearAsync(Guid Id);

        Task<Semester> AddSemesterAsync(Semester semester);
        Task<List<Semester>> GetSemestersAsync();
        Task<Semester> GetSemesterAsync(Guid Id);
        Task<Semester> DeleteSemester(Guid Id);
    }
}
