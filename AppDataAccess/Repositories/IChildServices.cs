using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IChildServices
    {
        Task<List<Child>> GetAllChildrenAsync();
        Task<List<Child>> GetUnSupportedChildrenAsync();
        Task<List<Child>> GetRandomChildrenAsync();
        Task<Child> GetAChildAsync(Guid Id);
        Task<Child> AddAChild(Child child);
        Task<Child> UpdateChild(Child child);
        Task<Child> DeleteChild(Guid Id);

        Task<bool> AddFeature(ChildFeature childFeature);
        Task<bool> RemoveFeature(Guid childId, Guid featureId);

        Task<bool> UpdateClass(Guid classId, Guid childId);
        Task<bool> RegisterForSemester(Guid ChildId, Guid SemId);
        Task<bool> UpdateSemesterRegistationAsync(SemesterRegistration semesterRegistration);

        Task<bool> SponcerAChildAsync(ChildSponser childSponser);
        Task<List<ChildSponser>> GetAllUnActivatedSupportAsync();
        Task<bool> ActivateSupportAsync(Guid Id);
        Task<bool> StopSponcoringAChildAsync(Guid childId);
    }
}
