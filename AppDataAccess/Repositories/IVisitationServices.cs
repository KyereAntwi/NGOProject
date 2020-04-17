using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IVisitationServices
    {
        Task<Visitation> BookVisitationAsync(Visitation visitation);
        Task<Visitation> CancelVisitationAsync(Guid Id);
        Task<Visitation> MarkDoneAsync(Guid Id);
        Task<List<Visitation>> GetVisitationsAsync();
        Task<List<Visitation>> GetPendingVisitationsAsync();
    }
}
