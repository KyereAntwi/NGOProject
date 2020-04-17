using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class Galleries : ViewComponent
    {
        private readonly IGalleryServices _data;

        public Galleries(IGalleryServices galleryServices)
        {
            _data = galleryServices;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var list = await _data.GalleriesAsync();
            GalleryNavViewModel viewModel = new GalleryNavViewModel();
            viewModel.Total = list.Count;
            return View(viewModel);
        }
    }
}
