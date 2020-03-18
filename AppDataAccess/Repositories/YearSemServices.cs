using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class YearSemServices : IYearsSemServices
    {
        private readonly DataContext _dataContext;

        public YearSemServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<AcademicYear> AddAcademicYearAsync(AcademicYear year)
        {
            var yearExits = await _dataContext.AcademicYears.SingleOrDefaultAsync(a => a.Year == year.Year);

            if (yearExits != null)
                return null;

            await _dataContext.AcademicYears.AddAsync(year);
            await _dataContext.SaveChangesAsync();
            return year;
        }

        public async Task<Semester> AddSemesterAsync(Semester semester)
        {
            var semesterExist = await _dataContext.Semesters.SingleOrDefaultAsync(s => s.AcademicYearId == semester.AcademicYearId && s.Title == semester.Title);
            if (semesterExist != null)
                return null;

            await _dataContext.Semesters.AddAsync(semester);
            await _dataContext.SaveChangesAsync();
            return semester;
        }

        public async Task<AcademicYear> DeleteAcademicYearAsync(Guid Id)
        {
            var year = await _dataContext.AcademicYears.FindAsync(Id);

            if (year == null)
                return null;

            _dataContext.AcademicYears.Remove(year);
            await _dataContext.SaveChangesAsync();

            return year;
        }

        public async Task<Semester> DeleteSemester(Guid Id)
        {
            var semeter = await _dataContext.Semesters.FindAsync(Id);

            if (semeter == null)
                return null;

            _dataContext.Semesters.Remove(semeter);
            await _dataContext.SaveChangesAsync();

            return semeter;
        }

        public async Task<AcademicYear> GetAcademicYearAsync(Guid Id)
        {
            return await _dataContext.AcademicYears
                .Include(a => a.Semesters)
                .SingleOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<List<AcademicYear>> GetAcademicYearsAsync()
        {
            return await _dataContext.AcademicYears
                .OrderByDescending(a => a.Year)
                .Include(a => a.Semesters).ToListAsync();
        }

        public async Task<Semester> GetSemesterAsync(Guid Id)
        {
            return await _dataContext.Semesters
                .Include(a => a.AcademicYear)
                .SingleOrDefaultAsync(a => a.Id == Id);
        }

        public Task<List<Semester>> GetSemestersAsync()
        {
            return _dataContext.Semesters.Include(a => a.AcademicYear).ToListAsync();
        }
    }
}
