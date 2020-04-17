using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class FeatureServices : IFeaturesServices
    {
        private readonly DataContext _dataContext;

        public FeatureServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Feature> AddFeatureAsync(Feature feature)
        {
            var existingFeature = await _dataContext.Features.SingleOrDefaultAsync(f => f.Title == feature.Title);

            if (existingFeature != null)
                return null;

            await _dataContext.Features.AddAsync(feature);
            await _dataContext.SaveChangesAsync();
            return feature;
        }

        public async Task<Feature> DeleteFeatureAsync(Guid Id)
        {
            var feature = await _dataContext.Features.FindAsync(Id);
            if (feature == null)
                return null;
            _dataContext.Features.Remove(feature);
            await _dataContext.SaveChangesAsync();
            return feature;
        }

        public async Task<Feature> GetFeatureAsync(Guid Id)
        {
            return await _dataContext.Features
                .Include(f => f.Children).ThenInclude(f => f.Child)
                .SingleOrDefaultAsync(f => f.Id == Id);
        }

        public async Task<List<Feature>> GetFeaturesAsync()
        {
            return await _dataContext.Features
                .Include(f => f.Children).ThenInclude(f => f.Child)
                .ToListAsync();
        }
    }
}
