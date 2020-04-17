using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IGalleryServices
    {
        Task<List<Gallery>> GalleriesAsync();
        Task<Gallery> GalleryAsync(Guid Id);
        Task<Gallery> AddGalleryAsync(Gallery gallery);
        Task<Gallery> DeleteGalleryAsync(Guid Id);
    }
}
