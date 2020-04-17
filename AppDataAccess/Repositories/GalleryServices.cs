using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class GalleryServices : IGalleryServices
    {
        private readonly DataContext _data;

        public GalleryServices(DataContext dataContext)
        {
            _data = dataContext;
        }

        public async Task<Gallery> AddGalleryAsync(Gallery gallery)
        {
            await _data.Galleries.AddAsync(gallery);
            await _data.SaveChangesAsync();
            return gallery;
        }

        public async Task<Gallery> DeleteGalleryAsync(Guid Id)
        {
            var gallery = await _data.Galleries.FindAsync(Id);
            if (gallery == null)
                return null;

            _data.Galleries.Remove(gallery);
            await _data.SaveChangesAsync();
            return gallery;
        }

        public async Task<List<Gallery>> GalleriesAsync()
        {
            return await _data.Galleries.ToListAsync();
        }

        public async Task<Gallery> GalleryAsync(Guid Id)
        {
            return await _data.Galleries.SingleOrDefaultAsync(g => g.Id == Id);
        }
    }
}
