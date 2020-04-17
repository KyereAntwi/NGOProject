using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class ChildService : IChildServices
    {
        private readonly DataContext _dataContext;

        public ChildService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> ActivateSupportAsync(Guid Id)
        {
            var supportModel = await _dataContext.ChildSponsers
                .Include(s => s.Child)
                .SingleOrDefaultAsync(s => s.ChildId == Id);
            
            if (supportModel == null)
                return false;

            supportModel.Accepted = true;
            _dataContext.ChildSponsers.Update(supportModel);

            // update user's notification
            Notification notification = new Notification() 
            {
                UserId = supportModel.ApplicationUserId,
                DateTimeAdded = DateTime.Now,
                Seen = false,
                Text = $"Request to support {supportModel.Child.Fullname} was accepted"
            };
            await _dataContext.Notifications.AddAsync(notification);

            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<Child> AddAChild(Child child)
        {
            await _dataContext.Children.AddAsync(child);
            await _dataContext.SaveChangesAsync();
            return child;
        }

        public async Task<bool> AddFeature(ChildFeature childFeature)
        {
            await _dataContext.ChildFeatures.AddAsync(childFeature);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Child> DeleteChild(Guid Id)
        {
            Child child = await _dataContext.Children.FindAsync(Id);

            if (child == null)
                return null;

            _dataContext.Children.Remove(child);
            await _dataContext.SaveChangesAsync();
            return child;
        }

        public async Task<Child> GetAChildAsync(Guid Id)
        {
            return await _dataContext
                .Children
                .Include(c => c.Class).ThenInclude(c => c.Class)
                .Include(c => c.Registrations).ThenInclude(r => r.Subject)
                .Include(c => c.Registrations).ThenInclude(r => r.Semester).ThenInclude(s => s.AcademicYear)
                .Include(c => c.Letters).ThenInclude(l => l.ApplicationUser)
                .Include(c => c.Features).ThenInclude(c => c.Feature)
                .Include(c => c.Sponser).ThenInclude(s => s.ApplicationUser)
                .SingleOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<List<Child>> GetAllChildrenAsync()
        {
            return await _dataContext.Children
                .OrderBy(c => c.Surname)
                .OrderByDescending(c => c.Gender)
                .Include(c => c.Class)
                .Include(c => c.Sponser)
                .Include(c => c.Registrations)
                .ToListAsync();
        }

        public async Task<List<ChildSponser>> GetAllUnActivatedSupportAsync()
        {
            return await _dataContext.ChildSponsers
                .Include(c => c.Child)
                .Include(c => c.ApplicationUser)
                .Where(c => c.Accepted == false)
                .ToListAsync();
        }

        public async Task<List<Child>> GetRandomChildrenAsync()
        {
            return await _dataContext.Children
                .Where(c => c.Sponser == null)
                .OrderBy(c => Guid.NewGuid())
                .Take(8)
                .ToListAsync();
        }

        public async Task<List<Child>> GetUnSupportedChildrenAsync()
        {
            return await _dataContext.Children
               .Where(c => c.Sponser == null)
               .ToListAsync();
        }

        public async Task<bool> RegisterForSemester(Guid ChildId, Guid SemId)
        {
            var child = await _dataContext.Children
                .Include(c => c.Class)
                .SingleOrDefaultAsync(c => c.Id == ChildId);

            if (child == null)
                return false;

            var @class = await _dataContext.Classes
                .Include(c => c.Subjects)
                .SingleOrDefaultAsync(c => c.Id == child.Class.ClassId);

            if (@class == null)
                return false;

            var semester = await _dataContext.Semesters.FindAsync(SemId);

            if (semester == null)
                return false;

            var semesterReg = await _dataContext.SemesterRegistrations
                .FirstOrDefaultAsync(s => s.ChildId == ChildId && s.SemesterId == SemId);

            if (semesterReg != null)
                return false;

            // when it gets here then all validations are checked
            var subjectList = @class.Subjects;

            if (subjectList.Count() < 1)
                return false;

            List<SemesterRegistration> registrations = new List<SemesterRegistration>();
            foreach (var subject in subjectList) 
            {
                registrations.Add(new SemesterRegistration 
                {
                    ChildId = ChildId,
                    DateAdded = DateTime.Now,
                    SemesterId = semester.Id,
                    SubjectId = subject.SubjectId
                });
            }

            await _dataContext.SemesterRegistrations.AddRangeAsync(registrations);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFeature(Guid childId, Guid featureId)
        {
            var feature = await _dataContext.ChildFeatures.SingleOrDefaultAsync(f => f.ChildId == childId && f.FeatureId == featureId);

            if (feature == null)
                return false;

            _dataContext.ChildFeatures.Remove(feature);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SponcerAChildAsync(ChildSponser childSponser)
        {
            await _dataContext.ChildSponsers.AddAsync(childSponser);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> StopSponcoringAChildAsync(Guid childId)
        {
            var model = await _dataContext.ChildSponsers.SingleOrDefaultAsync(c => c.ChildId == childId);

            if (model == null)
                return false;

            _dataContext.ChildSponsers.Remove(model);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Child> UpdateChild(Child child)
        {
            var newChild = await _dataContext.Children.FindAsync(child.Id);

            if (newChild == null)
                return null;

            newChild.DateOfBirth = child.DateOfBirth;
            newChild.Gender = child.Gender;
            newChild.GivingName = child.GivingName;
            newChild.Nationality = child.Nationality;
            newChild.Othernames = child.Othernames;
            newChild.Phtotograph = child.Phtotograph;
            newChild.Surname = child.Surname;
            newChild.Teaser = child.Teaser;

            _dataContext.Children.Update(newChild);
            await _dataContext.SaveChangesAsync();

            return newChild;
        }

        public async Task<bool> UpdateClass(Guid classId, Guid childId)
        {
            var childClass = await _dataContext.ChildClasses.SingleOrDefaultAsync(c => c.ChildId == childId);

            if (childClass != null)
            {
                childClass.ClassId = classId;
                _dataContext.ChildClasses.Update(childClass);
            }
            else 
            {
                ChildClass newOne = new ChildClass 
                {
                    ChildId = childId,
                    ClassId = classId
                };

                await _dataContext.ChildClasses.AddAsync(newOne);
            }

            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSemesterRegistationAsync(SemesterRegistration semesterRegistration)
        {
            var model = await _dataContext
                .SemesterRegistrations
                .SingleOrDefaultAsync(s => 
                s.ChildId == semesterRegistration.ChildId && 
                s.SemesterId == semesterRegistration.SemesterId && 
                s.SubjectId == semesterRegistration.SubjectId);

            if (model == null)
                return false;

            model.ClassScore = semesterRegistration.ClassScore;
            model.ExamScore = semesterRegistration.ExamScore;

            _dataContext.SemesterRegistrations.Update(model);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
