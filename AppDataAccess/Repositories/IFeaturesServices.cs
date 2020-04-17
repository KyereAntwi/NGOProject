using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IFeaturesServices
    {
        Task<Feature> GetFeatureAsync(Guid Id);
        Task<List<Feature>> GetFeaturesAsync();
        Task<Feature> AddFeatureAsync(Feature feature);
        Task<Feature> DeleteFeatureAsync(Guid Id);
    }
}
